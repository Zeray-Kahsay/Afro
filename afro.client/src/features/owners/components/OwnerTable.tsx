import { Table, TableBody, TableHead, TableHeader, TableRow } from "@/@/components/ui/table";
import type { Owner } from "../types/Owner";
import { OwnerTableRow } from "./OwnerTableRow";
import { Card, CardContent } from "@/@/components/ui/card";



interface Props {
    owners: Owner[];
}

export function OwnerTable({
    owners,
}: Props) {
    return (
        <Card>
            <CardContent className="p-0">
        <Table>

            <TableHeader>

                <TableRow>

                    <TableHead>Name</TableHead>

                    <TableHead>Email</TableHead>

                    <TableHead>Phone</TableHead>

                    <TableHead>Actions</TableHead>

                    <TableHead />

                </TableRow>

            </TableHeader>

            <TableBody>

                {owners.map(owner => (
                    <OwnerTableRow
                        key={owner.id}
                        owner={owner}
                    />
                ))}

            </TableBody>

        </Table>

            </CardContent>
        </Card>
    );
}