import type { FetchBaseQueryError } from "@reduxjs/toolkit/query";
import type { ApiError } from "./apiError";

export function getErrorMessage(error: unknown) : string {
    const fallback = "Something went wrong. Please try again";

    if(!error)
        return fallback;

    const apiError = error as FetchBaseQueryError;

    if (!("status" in apiError))
        return fallback;

    const data = apiError.data as ApiError;

    if (data?.detail)
        return data.detail;

    if (data?.title)
        return data.title;

    if (data?.errors){
        const first = Object.values(data.errors)[0];

        if (first?.length)
            return first[0];
    }

    return fallback;
        
}