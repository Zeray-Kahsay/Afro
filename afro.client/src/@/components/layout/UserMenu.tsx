import { LogoutButton } from "../../../features/auth/components/LogoutButton";
import { useAuth } from "../../../features/auth/hooks/useAuth";

export function UserMenu(){
    const {user} = useAuth();


    return (
        <div className="flex items-center gap-4">
            <div>
                <div>
                    {user?.fullName}
                </div>
                <div className="text-sm text-slate-500">
                    {user?.phoneNumber}
                </div>
            </div>
            <LogoutButton />
        </div>
    )
}