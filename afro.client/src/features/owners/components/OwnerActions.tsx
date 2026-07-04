import { Link } from "react-router-dom";




import type { Owner } from "../types/Owner";
import { DataTableAction } from "@/@/components/data-table";
import { Button } from "@/@/components/ui/button";
import { Archive, Eye, Pencil } from "@/@/components/icons";

interface OwnerActionsProps {
    owner: Owner;
}

export function OwnerActions({
    owner,
}: OwnerActionsProps) {
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
                onClick={() => {
                    console.log("Archive", owner.id);
                }}
            >
                <Archive  />
            </Button>

        </DataTableAction>
    );
}