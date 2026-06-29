import { SearchInput } from "../../shared";

interface DataTableSearchProps {
    value: string;
    onChange(value: string): void;
    placeholder?: string;
}

export function DataTableSearch({
    value,
    onChange,
    placeholder,
}: DataTableSearchProps) {
    return (
        <SearchInput
            value={value}
            onChange={onChange}
            placeholder={placeholder}
        />
    );
}