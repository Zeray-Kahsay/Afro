import type { ReactNode } from "react";

interface PageHeaderProps {
    title: string;
    description?: string;
    action?: ReactNode;
    navigation?: ReactNode;
}

export function PageHeader({
    title,
    description,
    action,
    navigation,
}: PageHeaderProps) {
    return (
        <div
            className="
            flex
            items-start
            justify-between
            gap-4
            "
        >

            {navigation}

            <div>

                <h1
                    className="
                    text-3xl
                    font-bold
                    tracking-tight
                    "
                >
                    {title}
                </h1>

                {description && (

                    <p
                        className="
                        mt-1
                        text-muted-foreground
                        "
                    >
                        {description}
                    </p>

                )}

            </div>

            {action}

        </div>
    );
}