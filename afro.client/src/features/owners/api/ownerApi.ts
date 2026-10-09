import { baseApi } from "@/api/baseApi";
import type { Owner } from "../types/Owner";
import type { CreateOwnerRequest } from "../types/CreateOwnerRequest";
import type { UpdateOwnerRequest } from "../types/UpdateOwnerRequest";
import type { OwnerStatistics } from "../types/OwnerStatistics";
import { OwnerStatusFilter } from "../types/OwnerStatusFilter";
import type { CursorPagedResponse } from "@/shared/pagination/index";


export interface SearchOwnerParams {
    search?: string;
    status?: (typeof OwnerStatusFilter)[keyof typeof OwnerStatusFilter];
    cursor?: string;
    pageSize?: number;
}

export const ownerApi = baseApi.injectEndpoints({
    endpoints: builder => ({

        createOwner: builder.mutation<Owner, CreateOwnerRequest>({
            query: body => ({
                url: "/owners",
                method: "POST",
                body
            }),
            invalidatesTags: ["Owners"]
        }),

        getOwner: builder.query<Owner, string>({
            query: ownerId => `/owners/${ownerId}`,
            providesTags: (_result, _error, ownerId) =>
                 [{ type: "Owners", id: ownerId }],
        }),

        updateOwner: builder.mutation<void, UpdateOwnerRequest>({
            query: ({ownerId, ...body}) => ({
                url: `/owners/${ownerId}`,
                method: "PUT",
                body
            }),
            invalidatesTags: (_result, _error, arg) => [
                {type: "Owners", id: arg.ownerId}, "Owners"
            ],
        }),

        archiveOwner: builder.mutation<void, string>({
            query: ownerId => ({
                url: `/owners/${ownerId}/archive`,
                method: "POST",
            }),
            invalidatesTags: ["Owners"]
        }),

        restoreOwner: builder.mutation<void, string>({
            query: ownerId => ({
                url: `/owners/${ownerId}/restore`,
                method: "POST",
            }),
            invalidatesTags: ["Owners"]
        }),

        deleteOwner: builder.mutation<void, string>({
            query: ownerId => ({
                url: `/owners/${ownerId}/clear`,
                method: "DELETE",
            }),
            invalidatesTags: ["Owners"]
        }),

        getOwners: builder.query<CursorPagedResponse<Owner>, SearchOwnerParams>({
            query: params => ({
                url: "/owners",
                method: "GET",
                params,
            }),

            serializeQueryArgs: ({endpointName, queryArgs }) => ({
                endpointName,
                search: queryArgs.search ?? "",
                status: queryArgs.status ?? OwnerStatusFilter.Active,
                pageSize: queryArgs.pageSize ?? 20,
            }),

            forceRefetch({currentArg, previousArg}) {
                return (
                    currentArg?.cursor !== previousArg?.cursor ||
                    currentArg?.search !== previousArg?.search ||
                    currentArg?.status !== previousArg?.status ||
                    currentArg?.pageSize !== previousArg?.pageSize
                );
            },

            merge(currentCache, newPage, {arg}){
                if (!arg?.cursor){
                    currentCache.items = newPage.items;
                    currentCache.nextCursor = newPage.nextCursor;
                    currentCache.hasMore = newPage.hasMore;

                    return;
                }

                const existingIds = new Set(currentCache.items.map(owner => owner.id));

                for (const owner of newPage.items){
                    if (!existingIds.has(owner.id)){
                        currentCache.items.push(owner);
                    }
                }

                currentCache.nextCursor = newPage.nextCursor;
                currentCache.hasMore = newPage.hasMore;
            },
            providesTags: ["Owners"]
        }),

        getOwnerStatistics: builder.query<OwnerStatistics, void>({
            query: () => "/owners/statistics",
            providesTags:["Owners"]
        }),

    }),// endpoints
});

export const {
    useCreateOwnerMutation,
    useGetOwnerQuery,
    useGetOwnersQuery,
    useUpdateOwnerMutation,
    useArchiveOwnerMutation,
    useRestoreOwnerMutation,
    useDeleteOwnerMutation,
    useGetOwnerStatisticsQuery,
} = ownerApi