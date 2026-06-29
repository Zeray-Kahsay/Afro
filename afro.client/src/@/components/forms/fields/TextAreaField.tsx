import type { Control, FieldPath, FieldValues } from "react-hook-form";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "../../ui/form";
import { Textarea } from "../../ui/textarea";


interface TextAreaFieldProps<
    TFieldValues extends FieldValues
> {
    control: Control<TFieldValues>;

    name: FieldPath<TFieldValues>;

    label: React.ReactNode;

    placeholder?: string;

    rows?: number;

    disabled?: boolean;
}

export function TextAreaField<
    TFieldValues extends FieldValues
>({
    control,
    name,
    label,
    placeholder,
    rows = 4,
    disabled = false,
}: TextAreaFieldProps<TFieldValues>) {
    return (
        <FormField
            control={control}
            name={name}
            render={({ field }) => (
                <FormItem>

                    <FormLabel>
                        {label}
                    </FormLabel>

                    <FormControl>
                        <Textarea
                            {...field}
                            placeholder={placeholder}
                            rows={rows}
                            disabled={disabled}
                        />
                    </FormControl>

                    <FormMessage />

                </FormItem>
            )}
        />
    );
}