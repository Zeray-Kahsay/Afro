interface TextCellProps {
    value: string;
}

export function TextCell({value}: TextCellProps) {
    return (
        <span className="font-medium">
            {value}
        </span>
    );
}