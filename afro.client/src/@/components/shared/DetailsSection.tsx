import type { ReactNode } from "react";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "../ui/card";

interface DetailsSectionProps {
    title: string;
    description?: string;
    children: ReactNode;
}

export function DetailsSection({title, description, children}: DetailsSectionProps){
    return (
        <Card className="rounded-xl shadow-sm">

            <CardHeader>
                <CardTitle className="text-center text-lg font-semibold">
                    {title}
                </CardTitle>
                
                {description && (
                    <CardDescription className="text-center text-sm text-muted-foreground">
                        {description}
                    </CardDescription>
                )}
            </CardHeader>

            <CardContent className="space-y-1">
                {children}
            </CardContent>

        </Card>
    );
}
