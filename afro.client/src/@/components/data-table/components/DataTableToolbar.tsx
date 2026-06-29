import type { ReactNode } from "react";

interface DataTableToolbarProps {
    search?: ReactNode;
    actions?: ReactNode;
}

export function DataTableToolbar({
    search,
    actions,
}: DataTableToolbarProps) {
    return (
        <div className="mb-6 flex items-center justify-between gap-4">
            <div>{search}</div>

            <div>{actions}</div>
        </div>
    );
}

//Later we'll add filters and sorting controls.