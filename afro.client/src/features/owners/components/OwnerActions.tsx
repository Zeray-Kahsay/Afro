import { Link } from "react-router-dom";




import type { Owner } from "../types/Owner";
import { DataTableAction } from "@/@/components/data-table";
import { Button } from "@/@/components/ui/button";
import { Archive, Eye, Pencil } from "@/@/components/icons";
import { useState } from "react";
import { useArchiveOwnerMutation } from "../api/ownerApi";
import { notify } from "@/@/lib/notify";
import { getErrorMessage } from "@/api/errorHandler";
import { ConfirmDialog } from "@/@/components/shared";

interface OwnerActionsProps {
    owner: Owner;
}

export function OwnerActions({
    owner,
}: OwnerActionsProps) {
    const [open, setOpen] = useState(false);
    const [archiveOwner, {isLoading}] = useArchiveOwnerMutation();

    const handleArchive = async () => {
        try {
            await archiveOwner(owner.id).unwrap();
            notify.success("Owner archived successfully");

            setOpen(false);
        } catch (error) {
            notify.error(getErrorMessage(error));
        }
    }



    return (
        <DataTableAction>

            <Button
                asChild
                variant="ghost"
                size="icon"
            >
                <Link
                    to={`/owners/${owner.id}`}
                >
                    <Eye />
                </Link>
            </Button>

            <Button
                asChild
                variant="ghost"
                size="icon"
            >
                <Link
                    to={`/owners/${owner.id}/edit`}
                >
                    <Pencil />
                </Link>
            </Button>

            <Button
                variant="ghost"
                size="icon"
                onClick={() => setOpen(true)}
            >
                <Archive  />
            </Button>

                <ConfirmDialog
                    open={open}
                    onOpenChange={setOpen}
                    title="Archive Owner"
                    description={`Are you sure you want to archive "${owner.fullName}"?`}
                    confirmText="Archive"
                    destructive
                    loading={isLoading}
                    onConfirm={handleArchive}
                />
        </DataTableAction>
    );
}