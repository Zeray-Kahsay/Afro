

import { TableHead, TableHeader, TableRow } from "../../ui/table";
import type { DataTableColumn } from "../types/DataTableColumn";

interface DataTableHeaderProps<T> {
    columns: DataTableColumn<T>[];
}

export function DataTableHeader<T>({
    columns,
}: DataTableHeaderProps<T>) {
    return (
        <TableHeader>
            <TableRow>
                {columns.map(column => (
                    <TableHead
                        key={column.id}
                        className={column.className}
                    >
                        {column.header}
                    </TableHead>
                ))}
            </TableRow>
        </TableHeader>
    );
}