import PhoneInput from "react-phone-number-input";
import "react-phone-number-input/style.css";


import type { Control, FieldPath, FieldValues } from "react-hook-form";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/@/components/ui/form";

interface PhoneNumberFieldProps<
    TFieldValues extends FieldValues
> {
    control: Control<TFieldValues>;

    name: FieldPath<TFieldValues>;

    label: React.ReactNode;

    defaultCountry?: "NO" | "SE" | "DK" | "FI";
}

export function PhoneNumberField<
    TFieldValues extends FieldValues
>({
    control,
    name,
    label,
    defaultCountry = "NO",
}: PhoneNumberFieldProps<TFieldValues>) {

    return (
        <FormField
            control={control}
            name={name}
            render={({ field }: { field: any }) => (

                <FormItem>

                    <FormLabel>
                        {label}
                    </FormLabel>

                    <FormControl>

                        <PhoneInput
                            international
                            defaultCountry={defaultCountry}
                            value={field.value ?? ""}
                            onChange={(value) =>
                                field.onChange(value ?? "")
                            }
                        />

                    </FormControl>

                    <FormMessage />

                </FormItem>

            )}
        />
    );
}