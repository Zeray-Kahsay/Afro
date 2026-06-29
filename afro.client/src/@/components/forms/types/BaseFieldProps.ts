import type { Control, FieldPath, FieldValues } from "react-hook-form";


export interface BaseFieldProps<
    TFieldValues extends FieldValues
> {
    control: Control<TFieldValues>;
    name: FieldPath<TFieldValues>;
    label: React.ReactNode;
    disabled?: boolean;
    rows?: number;
}