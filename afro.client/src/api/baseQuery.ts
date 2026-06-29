import type { BaseQueryFn, FetchBaseQueryError } from "@reduxjs/toolkit/query";
import type { FetchArgs } from "@reduxjs/toolkit/query";
import {
  fetchBaseQuery,
} from "@reduxjs/toolkit/query/react";
import { logout, setTokens } from "../features/auth/slices/authSlice";

export const baseQuery =
  fetchBaseQuery({
    baseUrl:
      import.meta.env.VITE_API_URL,

    prepareHeaders: (
      headers,
      { getState }
    ) => {
        const state = getState() as any;

        const token = state.auth.accessToken;

      if (token) {
        headers.set(
          "Authorization",
          `Bearer ${token}`
        );
      }

      return headers;
    },
  });

  

  export const baseQueryWithReauth: BaseQueryFn<string | FetchArgs, unknown, FetchBaseQueryError> = 
      async ( args, api, extraOptions) => {

        let result = await baseQuery(args, api, extraOptions);

        if (result.error?.status === 401){
          const state = api.getState() as any;

          const refreshToken = state.auth.refreshToken;

          if (!refreshToken){
            api.dispatch(logout());

            return result;
          }
          const refreshResult = await baseQuery({
            url: "/auth/refresh",
            method: "POST",
            body: {refreshToken},
          },
          api,
          extraOptions
        );

        if (refreshResult.data){
          const tokens = refreshResult.data as {accessToken: string; refreshToken: string;};
          api.dispatch(setTokens(tokens));

          result = await baseQuery(args, api, extraOptions);
        } else {
          api.dispatch(logout());
        }
        }

        return result;
  }