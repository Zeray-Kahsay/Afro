import type { ReactNode } from "react";
import { Card, CardContent, CardHeader, CardTitle } from "../../ui/card";



interface Props {
    title: string;
    children: ReactNode;
}

export function FormCard({
    title,
    children,
}: Props) {
    return (
        <Card className="rounded-xl shadow-sm">

            <CardHeader>
                <CardTitle>
                    {title}
                </CardTitle>
            </CardHeader>

            <CardContent>
                {children}
            </CardContent>

        </Card>
    );
}