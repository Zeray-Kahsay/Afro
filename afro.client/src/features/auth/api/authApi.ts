
import type { LoginRequest }
  from "../types/LoginRequest";

import type { LoginResponse }
  from "../types/LoginResponse";

import type { CurrentUser }
  from "../types/CurrentUser";
import { baseApi } from "../../../api/baseApi";

export const authApi =
  baseApi.injectEndpoints({
    endpoints: builder => ({

      login:
        builder.mutation<
          LoginResponse,
          LoginRequest
        >({
          query: body => ({
            url: "/auth/loginUser",
            method: "POST",
            body,
          }),
        }),

      logout:
        builder.mutation<void, void>({
          query: () => ({
            url: "/auth/logout",
            method: "POST",
          }),
        }),

      getCurrentUser:
        builder.query<
          CurrentUser,
          void
        >({
          query: () => ({
            url: "/auth/me",
          }),
        }),
    }),
  });

export const {
  useLoginMutation,
  useLogoutMutation,
  useGetCurrentUserQuery,
} = authApi;