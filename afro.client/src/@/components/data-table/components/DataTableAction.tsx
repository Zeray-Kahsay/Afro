import type { ReactNode } from "react";

interface DataTableActionProps {
    children: ReactNode;
}

export function DataTableAction({
    children,
}: DataTableActionProps) {
    return (
        <div className="flex items-center justify-end gap-2">
            {children}
        </div>
    );
}

//Later, this can evolve into a dropdown menu without changing feature code.