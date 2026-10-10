import { Link } from "react-router-dom";




import type { Owner } from "../types/Owner";
import { DataTableAction } from "@/@/components/data-table";
import { Button } from "@/@/components/ui/button";
import { Archive, Eye, Pencil } from "@/@/components/icons";
import { useState } from "react";
import { useArchiveOwnerMutation, useDeleteOwnerMutation, useRestoreOwnerMutation } from "../api/ownerApi";
import { notify } from "@/@/lib/notify";
import { getErrorMessage } from "@/api/errorHandler";
import { ConfirmDialog } from "@/@/components/shared";
import { RefreshCw, Trash2 } from "lucide-react";

interface OwnerActionsProps {
    owner: Owner;
}

type OwnerAction = "archive" | "restore" | "delete" | null;

// handles the actions for each owner in the data table, such as view, edit, archive and remove
export function OwnerActions({
    owner,
}: OwnerActionsProps) {
    const [activeAction, setActiveAction] = useState<OwnerAction>(null);

    const [archiveOwner, {isLoading: isArchiving}] = useArchiveOwnerMutation();
    const [restoreOwner, {isLoading: isRestoring}] = useRestoreOwnerMutation();
    const [deleteOwner, {isLoading: isDeleting}] = useDeleteOwnerMutation();

    const isLoading = isArchiving || isRestoring || isDeleting;

    const closeDialog = () => {
        if (!isLoading){
            setActiveAction(null);
        }
    }

    const handleArchive = async () => {
        try {
            await archiveOwner(owner.id).unwrap();
            notify.success("Owner archived successfully");

            setActiveAction(null);
            
        } catch (error) {
            notify.error(getErrorMessage(error));
        }
    }

    const handleRestore = async () => {
        try {
            await restoreOwner(owner.id).unwrap();
            notify.success("Owner restored successfully");

            setActiveAction(null);
            
        } catch (error) {
            notify.error(getErrorMessage(error));
        }
    }

    const handleDelete = async () => {
        try {
            await deleteOwner(owner.id).unwrap();
            notify.success("Owner deleted successfully");

            setActiveAction(null);
        } catch (error) {
            notify.error(getErrorMessage(error));
        }
    }

    const actionConfig = {
        archive: {
            title: "Archive Owner",
            description: `Are you sure you want to archive "${owner.fullName}"? The owner can be restored later.`,
            confirmText: "Archive",
            destructive: false,
            onConfirm: handleArchive,
        },
        restore: {
            title: "Restore Owner",
            description: `Restore "${owner.fullName}" and make this owner active again.`,
            confirmText: "Restore",
            destructive: false,
            onConfirm: handleRestore,
        },
        delete: {
            title: "Permanently Delete Owner",
            description: `Are you sure you want to delete "${owner.fullName}"? This action cannot be undone.The operation
            will fail if the owner has any associated listings. Please archive the owner first before deleting.`,
            confirmText: "Delete",
            destructive: true,
            onConfirm: handleDelete,
        },
    } as const;

    const dialogConfig = activeAction ? actionConfig[activeAction] : null;

    return (
        <DataTableAction>

            <Button
                asChild
                variant="ghost"
                size="icon"
                aria-label = {`View ${owner.fullName}`}
            >
                <Link
                    to={`/owners/${owner.id}`}
                >
                    <Eye className="h-4 w-4" />
                </Link>
            </Button>

            {!owner.isArchived && (
            <>
            <Button
                asChild
                variant="ghost"
                size="icon"
                aria-label = {`Edit ${owner.fullName}`}
            >
                <Link
                    to={`/owners/${owner.id}/edit`}
                >
                    <Pencil className="h-4 w-4" />
                </Link>
            </Button>

            <Button
                variant="ghost"
                size="icon"
                aria-label = {`Archive ${owner.fullName}`}
                onClick={() => setActiveAction("archive")}
            >
                <Archive  />
            </Button>
            </>
            )}

            {owner.isArchived && (
                <>
                    <Button
                        variant="ghost"
                        size="icon"
                        aria-label = {`Restore ${owner.fullName}`}
                        onClick={() => setActiveAction("restore")}
                    >
                        <RefreshCw className="h-4 w-4" />
                    </Button>

                    <Button
                        variant="ghost"
                        size="icon"
                        aria-label = {`Delete ${owner.fullName}`}
                        onClick={() => setActiveAction("delete")}
                    >
                        <Trash2 className="h-4 w-4" />
                    </Button>
                </>
            )}

            {dialogConfig && (
                <ConfirmDialog 
                    open={activeAction !== null}
                    onOpenChange={(open) => !open && closeDialog()}
                    title={dialogConfig.title}
                    description={dialogConfig.description}
                    confirmText={dialogConfig.confirmText}
                    destructive={dialogConfig.destructive}
                    loading={isLoading}
                    onConfirm={dialogConfig.onConfirm}
                />
            )}

                {/* <ConfirmDialog
                    open={activeAction === "archive"}
                    onOpenChange={(open) => !open && setActiveAction(null)}
                    title="Archive Owner"
                    description={`Are you sure you want to archive "${owner.fullName}"? The owner can be restored later.`}
                    confirmText="Archive"
                    destructive
                    loading={isLoading}
                    onConfirm={handleArchive}
                /> */}
        </DataTableAction>
    );
}