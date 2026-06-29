import { EmptyState } from "../../shared";

interface DataTableEmptyProps {
    title: string;
    description?: string;
}

export function DataTableEmpty({
    title,
    description,
}: DataTableEmptyProps) {
    return (
        <EmptyState
            title={title}
            description={description}
        />
    );
}