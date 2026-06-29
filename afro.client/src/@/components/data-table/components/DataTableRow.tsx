

import { TableCell, TableRow } from "../../ui/table";
import type { DataTableColumn } from "../types/DataTableColumn";

interface DataTableRowProps<T> {
    item: T;
    columns: DataTableColumn<T>[];
}

export function DataTableRow<T>({
    item,
    columns,
}: DataTableRowProps<T>) {
    return (
        <TableRow>
            {columns.map(column => (
                <TableCell
                    key={column.id}
                    className={column.className}
                >
                    {column.cell(item)}
                </TableCell>
            ))}
        </TableRow>
    );
}