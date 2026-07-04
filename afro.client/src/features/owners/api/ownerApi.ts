import { baseApi } from "@/api/baseApi";
import type { Owner } from "../types/Owner";
import type { CreateOwnerRequest } from "../types/CreateOwnerRequest";

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


    }),// endpoints
});

export const {
    useCreateOwnerMutation,
    useGetOwnerQuery,
    useSearchOwnersQuery,
} = ownerApi;