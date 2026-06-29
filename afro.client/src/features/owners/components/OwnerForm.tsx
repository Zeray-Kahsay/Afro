import { useNavigate } from "react-router-dom";
import { useCreateOwnerMutation } from "../api/ownerApi";
import { useForm } from "react-hook-form";
import {  ownerSchema, type OwnerFormValues } from "../validation/ownerSchema";
import { zodResolver } from "@hookform/resolvers/zod";
import { Card, CardContent, CardHeader, CardTitle } from "@/@/components/ui/card";
import { Form } from "@/@/components/ui/form";
import { PhoneNumberField, SubmitButton, TextField } from "@/@/components/forms";
import { TextAreaField } from "@/@/components/forms/fields/TextAreaField";

interface Props {
    onSuccess?: () => void;
}

export function OwnerForm({onSuccess}: Props){
    const navigate = useNavigate();
    const [createOwner, {isLoading}] = useCreateOwnerMutation();

    const form = useForm<OwnerFormValues>({
        resolver: zodResolver(ownerSchema),

        defaultValues: {
            fullName: "",
            phoneNumber: "",
            email: "",
            address: "",
            notes: ""
        },
    });

    const onSubmit = async (values: OwnerFormValues) => {
        console.log("Submitting owner");
        console.log(values);
        try {
            const result = await createOwner(values).unwrap();
            console.log(result);
            console.log(values);
            form.reset();

            if (onSuccess){
                onSuccess();
                return;
            }
            navigate("/owners");
        } catch (error) {
            console.error(error)
        }
    };

    return (
        <Card className="rounded-xl shadow-sm">
            <CardHeader>
                <CardTitle>Create Owner</CardTitle>
            </CardHeader>

            <CardContent>
                <Form {...form}>
                    <form
                        onSubmit={form.handleSubmit(onSubmit)}
                        className="space-y-6"
                    >

                        <TextField 
                            control={form.control}
                            name="fullName"
                            label="Full Name"
                            placeholder="Hagos Meressa"
                        />

                        <PhoneNumberField 
                            control={form.control}
                            name="phoneNumber"
                            label="Phone Number"
                        />

                        <TextField 
                            control={form.control}
                            name="email"
                            label="Email"
                            type="email"
                        />

                        <TextField 
                            control={form.control}
                            name="address"
                            label="Address"
                        />

                        <TextAreaField
                            control={form.control}
                            name="notes"
                            label="Notes"
                         />

                         <SubmitButton 
                            loading={isLoading}
                         >
                            Create Owner
                        </SubmitButton>

                    </form>
                </Form>
            </CardContent>
        </Card>
    )
    
}




{/* <form
   onSubmit={form.handleSubmit(onSubmit)}
   className="space-y-6"
>
     <FormField
         control={form.control}
         name="fullName"
         render={({ field }: { field: any }) => (
             <FormItem>
                 <FormLabel>Full Name</FormLabel>
                 <FormControl>
                     <Input 
                         placeholder="John Doe"
                         {...field}
                     />
                 </FormControl>
                 <FormMessage />
             </FormItem>
         )}
     />

     <FormField 
         control={form.control}
         name="phoneNumber"
         render={({ field }: { field: any }) => (
             <FormItem>
                 <FormLabel>Phone Number</FormLabel>
                 <FormControl>
                     <PhoneInput
                         international
                         defaultCountry="NO"
                         value={field.value ?? ""}
                         onChange={value => 
                             field.onChange(
                                 value ?? ""
                             )
                         }             
                     />
                 </FormControl>
                 <FormMessage />
             </FormItem>
       )}
   />

     <FormField 
         control={form.control}
         name="email"
         render={({ field }: { field: any }) => (
             <FormItem>
                 <FormLabel>Email</FormLabel>
                 <FormControl>
                     <Input 
                         placeholder="john@gmail.com"
                         {...field}
                     />
                 </FormControl>
                 <FormMessage />
             </FormItem>
       )}
   />
     <FormField 
         control={form.control}
         name="address"
         render={({ field }: { field: any }) => (
             <FormItem>
                 <FormLabel>Country</FormLabel>
                 <FormControl>
                     <Input 
                         placeholder="Oslo, Norway"
                         {...field}
                     />
                 </FormControl>
                 <FormMessage />
             </FormItem>
       )}
   />
     <FormField 
         control={form.control}
         name="notes"
         render={({ field }: { field: any }) => (
             <FormItem>
                 <FormLabel>Notes</FormLabel>
                 <FormControl>
                     <Input 
                         placeholder="say something"
                         {...field}
                     />
                 </FormControl>
                 <FormMessage />
             </FormItem>
       )}
   />

   <Button
     type="submit"
     className="w-full"
     disabled={isLoading}
   >
      {isLoading ? "Creating" : "Create Owner"} 
   </Button> 
</form>*/}