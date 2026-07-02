import type { Owner } from "../types/Owner";

interface Props {
    owner: Owner;
}

export function OwnerNameCell({
    owner,
}: Props) {
    return (
        <div>

            <p className="font-medium">
                {owner.fullName}
            </p>

            <p className="text-sm text-muted-foreground">
                Property Owner
            </p>

        </div>
    );
}