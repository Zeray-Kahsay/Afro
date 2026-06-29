import { Card, CardContent } from "../../ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "../../ui/table";
import type { DataTableColumn } from "../types/DataTableColumn";
import type { DataTableOptions } from "../types/DataTableOptions";

interface Props<T> {
    columns: DataTableColumn<T>[];
    data: T[];
    options?: DataTableOptions;
}
export function DataTable<T>({columns, data}: Props<T>){

    return (
        <Card>
            <CardContent className="P-0">
                <Table>
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
                    <TableBody>
                        {data.map((item, index) => (
                            <TableRow key={index}>
                                {columns.map(column => (
                                    <TableCell
                                        key={column.id}
                                        className={column.className}
                                    >
                                        {column.cell(item)}
                                    </TableCell>
                                ))}
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </CardContent>
        </Card>
    )
}