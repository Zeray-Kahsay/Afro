import type { Control, FieldPath, FieldValues } from "react-hook-form";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "../../ui/form";
import { Input } from "../../ui/input";




interface TextFieldProps<TFieldValues extends FieldValues> {
    control: Control<TFieldValues>;

    name: FieldPath<TFieldValues>;

    label: React.ReactNode;

    placeholder?: string;

    type?: React.HTMLInputTypeAttribute;

    disabled?: boolean;
}

export function TextField<TFieldValues extends FieldValues>({
    control,
    name,
    label,
    placeholder,
    type = "text",
    disabled = false,
}: TextFieldProps<TFieldValues>) {
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
                        <Input
                            {...field}
                            type={type}
                            placeholder={placeholder}
                            disabled={disabled}
                        />
                    </FormControl>

                    <FormMessage />

                </FormItem>
            )}
        />
    );
}