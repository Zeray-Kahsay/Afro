import type { ReactNode } from "react";
import { cn } from "@/@/lib/utils";

interface InfoRowProps {
    icon?:ReactNode;
    label: string;
    value?: ReactNode;
    className?: string;
}
export function InfoRow({ label, value, className, icon }: InfoRowProps) {
    return (
        <div className={cn("flex flex-start gap-4 py-4", className)}>
            {icon && (
                <div className="mt-0.5 text-primary">
                    {icon}
                </div>
            )}
            <div className="flex-1">
                <p className="text-sm font-medium text-muted-foreground">
                    {label}
                </p>
                <div className="mt-1 text-sm text-foreground">
                    {value ?? "-"}
                </div>
            </div>
        </div>
    )
}