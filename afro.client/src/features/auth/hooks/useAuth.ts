import { useAppSelector } from "../../../app/hooks";

export function useAuth(){
    const auth = useAppSelector(state => state.auth);

    return {
        isAuthenticated: !!auth.accessToken,
        user: auth.user,
        accessToken: auth.accessToken,
        refreshToken: auth.refreshToken
    }
}