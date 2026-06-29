import type { ReactNode } from "react";

interface EmptyStateProps {
    title: string;

    description?: string;

    action?: ReactNode;
}

export function EmptyState({
    title,
    description,
    action,
}: EmptyStateProps) {
    return (
        <div
            className="
            rounded-xl
            border
            bg-card
            p-12
            text-center
            "
        >
            <h2
                className="
                text-xl
                font-semibold
                "
            >
                {title}
            </h2>

            {description && (

                <p
                    className="
                    mt-2
                    text-muted-foreground
                    "
                >
                    {description}
                </p>

            )}

            {action && (

                <div className="mt-6">

                    {action}

                </div>

            )}
        </div>
    );
}