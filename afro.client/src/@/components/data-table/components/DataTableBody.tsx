
import { DataTableRow } from "./DataTableRow";

import type { DataTableColumn } from "../types/DataTableColumn";
import { TableBody } from "../../ui/table";

interface DataTableBodyProps<T> {
    data: T[];
    columns: DataTableColumn<T>[];
}

export function DataTableBody<T>({
    data,
    columns,
}: DataTableBodyProps<T>) {
    return (
        <TableBody>
            {data.map((item, index) => (
                <DataTableRow
                    key={index}
                    item={item}
                    columns={columns}
                />
            ))}
        </TableBody>
    );
}