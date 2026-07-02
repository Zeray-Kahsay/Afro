import { cn } from "@/lib/utils";
import type { ReactNode } from "react";

interface DataTableActionProps {
    children: ReactNode;
    className?: string;
}

export function DataTableAction({children, className}: DataTableActionProps){
    return (
        <div className={cn(
            "flex items-center justify-end gap-2",
            className
        )}>
            {children}
        </div>
    );
}