interface EmailCellProps {
    email?: string | null;
}

export function EmailCell({
    email,
}: EmailCellProps) {
    if (!email) {
        return (
            <span className="text-muted-foreground">
                —
            </span>
        );
    }

    return (
        <a
            href={`mailto:${email}`}
            className="
                text-muted-foreground
                transition-colors
                hover:text-primary
            "
        >
            {email}
        </a>
    );
}