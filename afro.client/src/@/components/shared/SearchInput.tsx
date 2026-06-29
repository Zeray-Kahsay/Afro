import { Search } from "../icons";
import { Input } from "../ui/input";


interface SearchInputProps {
    value: string;
    onChange(value: string): void;
    placeholder?: string;
}

export function SearchInput({
    value,
    onChange,
    placeholder = "Search...",
}: SearchInputProps) {
    return (
        <div className="relative max-w-md">

            <Search
                className="
                    absolute
                    left-3
                    top-1/2
                    h-5
                    w-5
                    -translate-y-1/2
                    text-muted-foreground
                "
            />

            <Input
                value={value}
                onChange={(e) => onChange(e.target.value)}
                placeholder={placeholder}
                className="pl-10"
            />

        </div>
    );
}