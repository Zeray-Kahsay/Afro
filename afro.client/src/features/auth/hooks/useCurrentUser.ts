import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { useGetCurrentUserQuery } from "../api/authApi";
import { setUser } from "../slices/authSlice";

export function useCurrentUser(){
    const dispatch = useAppDispatch();
    const auth = useAppSelector(state => state.auth);

    const shouldFetch = !!auth.accessToken; //  fetch only if there is access token

    const {data, isLoading, isError} = useGetCurrentUserQuery(
        undefined,
        {
            skip:!shouldFetch
        }
    );


    useEffect(() =>{
        if (data){
            dispatch(setUser(data))
        }
    },[data, dispatch])

    return {
        user: data,
        isLoading,
        isError,
    }
}