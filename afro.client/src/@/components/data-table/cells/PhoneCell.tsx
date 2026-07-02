interface PhoneCellProps {
    phoneNumber: string;
}

export function PhoneCell({
    phoneNumber,
}: PhoneCellProps) {
    return (
        <a
            href={`tel:${phoneNumber}`}
            className="
                transition-colors
                hover:text-primary
            "
        >
            {phoneNumber}
        </a>
    );
}