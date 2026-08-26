import { baseApi } from "@/api/baseApi";
import type { Owner } from "../types/Owner";
import type { CreateOwnerRequest } from "../types/CreateOwnerRequest";
import type { UpdateOwnerRequest } from "../types/UpdateOwnerRequest";
import type { OwnerStatistics } from "../types/OwnerStatistics";
import type { OwnerStatusFilter } from "../types/OwnerStatusFilter";
import type { CursorPagedResponse } from "@/shared/pagination/index";


export interface SearchOwnerParams {
    search?: string;
    status?: (typeof OwnerStatusFilter)[keyof typeof OwnerStatusFilter];
    pageNumber?: number;
    pageSize?: number;
}

export const ownerApi = baseApi.injectEndpoints({
    endpoints: builder => ({

        searchOwners: builder.query<Owner[], string | void>({
            query: search => ({
                url: "/owners",
                params: {
                    search,
                },
            }),
            providesTags: ["Owners"],
        }),

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
                url: `/owners/${ownerId}`,
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
    useSearchOwnersQuery,
    useArchiveOwnerMutation
} = ownerApi