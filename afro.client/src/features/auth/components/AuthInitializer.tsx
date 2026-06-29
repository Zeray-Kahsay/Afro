import type { ReactNode } from "react";
import { useCurrentUser } from "../hooks/useCurrentUser";
import { LoadingSpinner } from "@/@/components/shared/LoadingSpinner";

interface Props {
    children: ReactNode;
}

export function AuthInitializer({children}: Props){
    const {isLoading} = useCurrentUser();

    if (isLoading){
        return (
            <div className="flex min-h-screen items-center justify-center">
                <LoadingSpinner size={48} />
            </div>
        );
    }

    return children;
}
