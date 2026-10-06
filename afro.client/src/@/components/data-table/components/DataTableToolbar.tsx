import type { ReactNode } from "react";

interface DataTableToolbarProps {
    search?: ReactNode;
    children?: ReactNode;
}

export function DataTableToolbar({
    search,
    children,
}: DataTableToolbarProps) {
    return (
        <div className="mb-6 flex items-center justify-between gap-4">
            <div className="flex flex-1 items-center gap-2">
                {search}
            </div>

            <div className="flex items-center gap-2">
                {children}

            </div>
        </div>
    );
}

//Later we'll add filters and sorting controls.