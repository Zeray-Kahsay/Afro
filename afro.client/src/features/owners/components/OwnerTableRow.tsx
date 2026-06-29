import { Link } from "react-router-dom";

import type { Owner } from "../types/Owner";
import { Button } from "@/@/components/ui/button";
import { Archive, Eye, Pencil } from "@/@/components/icons";
import { TableCell } from "@/@/components/ui/table";


interface Props {
    owner: Owner;
}

export function OwnerTableRow({
    owner,
}: Props) {
    return (
        <tr>
            <TableCell>
                <div>
                    <p className="font-medium">
                        {owner.fullName}
                    </p>
                    <p className="text-sm text-muted-foreground">
                        Property Owner
                    </p>
                </div>
            </TableCell>
            <TableCell>
                <a 
                    href={`mailto:${owner.email}`}
                    className="text-muted-foreground hover:text-primary"
                >
                    {owner.email}
                </a>
            </TableCell>

            <TableCell>
                <a href={`tel:${owner.phoneNumber}`}
                    className="hover:text-primary"
                >
                    {owner.phoneNumber}
                </a>
            </TableCell>
          

            <td className="px-4">

                <Button
                    asChild
                    variant="ghost"
                    size="icon"
                    className="rounded-md bg-slate-800 px-4 py-2 text-sm font-medium text-white shadow-sm transition hover:bg-slate-700"
                >
                    <Link
                        to={`/owners/${owner.id}`}
                        className="inline-flex items-center justify-center"
                    >
                        <Eye />
                    </Link>
                </Button>

            </td>
            <td className="px-4">

                <Button
                    asChild
                    variant="ghost"
                    size="icon"
                    className="rounded-md bg-slate-800 px-4 py-2 text-sm font-medium text-white shadow-sm transition hover:bg-slate-700"
                >
                    <Link
                        to={`/owners/${owner.id}`}
                        className="inline-flex items-center justify-center"
                    >
                        <Pencil />
                    </Link>
                </Button>

            </td>
            <td className="px-4">

                <Button
                    asChild
                    variant="ghost"
                    size="icon"
                    className="rounded-md bg-slate-800 px-4 py-2 text-sm font-medium text-white shadow-sm transition hover:bg-slate-700"
                >
                    <Link
                        to={`/owners/${owner.id}`}
                        className="inline-flex items-center justify-center"
                    >
                        <Archive />
                    </Link>
                </Button>

            </td>

        </tr>
    );
}