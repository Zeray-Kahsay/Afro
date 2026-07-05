import { useNavigate } from "react-router-dom";
import { useCreateOwnerMutation } from "../api/ownerApi";
import { OwnerForm } from "../components/OwnerForm";
import type { OwnerFormValues } from "../validation/ownerSchema";
import { notify } from "@/@/lib/notify";
import { getErrorMessage } from "@/api/errorHandler";

export function CreateOwnerPage(){
    const [createOwner, { isLoading }] = useCreateOwnerMutation();
    const navigate = useNavigate();

    const handleSubmit = async (values: OwnerFormValues) => {
        try {
            await createOwner(values).unwrap();
            notify.success("Owner created successfully");
            navigate("/owners")
            
        } catch (error) {
            notify.error(getErrorMessage(error));
        }

    }


    return (
        <div className="max-w-3xl mx-auto">
            <OwnerForm 
                title="Create Owner"
                submitText="Create Owner"
                loading={isLoading}
                onSubmit={handleSubmit}
            />
        </div>
    )
}