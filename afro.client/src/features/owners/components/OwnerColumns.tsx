import type { DataTableColumn } from "@/@/components/data-table/types/DataTableColumn";
import type { Owner } from "../types/Owner";
import { OwnerNameCell } from "./OwnerNameCell";
import { OwnerActions } from "./OwnerActions";
import { PhoneCell } from "@/@/components/data-table/cells/PhoneCell";
import { EmailCell } from "@/@/components/data-table/cells/EmailCell";

export const ownerColumns: DataTableColumn<Owner>[] = [

    {
        id: "name",

        header: "Owner",

        cell: owner => (
            <OwnerNameCell owner={owner}/>
        ),
    },

    {
        id: "phone",

        header: "Phone",

        cell: owner => (
            <PhoneCell 
               phoneNumber={owner.phoneNumber}
            />
        ),
    },

    {
        id: "email",
        header: "Email",
        cell: owner => (
            <EmailCell 
                email={owner.email}
            />
        )
    },

    {
        id: "actions",

        header: "",

        className: "text-right",

        cell: owner => (
            <OwnerActions owner={owner}/>
        ),
    },
];