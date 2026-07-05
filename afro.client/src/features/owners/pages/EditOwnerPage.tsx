import { useNavigate, useParams } from "react-router-dom";
import { useGetOwnerQuery, useUpdateOwnerMutation } from "../api/ownerApi";
import { EmptyState, LoadingSpinner } from "@/@/components/shared";
import type { OwnerFormValues } from "../validation/ownerSchema";
import { notify } from "@/@/lib/notify";
import { getErrorMessage } from "@/api/errorHandler";
import { OwnerForm } from "../components/OwnerForm";

export function EditOwnerPage (){
    const {ownerId} = useParams();
    const navigate = useNavigate();
    const {data: owner, isLoading } = useGetOwnerQuery(ownerId!);
    const [upddateOwner, {isLoading: isSaving }] = useUpdateOwnerMutation();
    
    
    if (isLoading){
        return <LoadingSpinner /> 
    }
    
    if (!owner){
        return <EmptyState title="Owner not found"/>
    }
    
    const handleSubmit = async (values: OwnerFormValues) => {
        try {
            await upddateOwner({ownerId: ownerId!, ...values}).unwrap();
            notify.success("Owner updated successfully");
            navigate(`/owners/${ownerId}`);
        } catch (error) {
            notify.error(getErrorMessage(error));
        }
    }

    return (
        <OwnerForm 
            title="Edit Owner"
            submitText="Save Changes"
            loading={isSaving}
            initialValues={{
                fullName: owner.fullName,
                phoneNumber: owner.phoneNumber,
                email: owner.email ?? "",
                address: owner.address ?? "",
                notes: owner.notes ?? "",
            }}
            onSubmit={handleSubmit}
        />
    );
}