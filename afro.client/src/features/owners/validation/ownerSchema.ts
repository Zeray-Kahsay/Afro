import { z } from "zod";

export const ownerSchema = z.object({
  fullName: z.string().min(2),

  phoneNumber: z.string().min(5),

  email: z
    .string()
    .email()
    .optional()
    .or(z.literal("")),

  address: z.string().optional(),

  notes: z.string().optional(),
});

export type OwnerFormValues =
  z.infer<typeof ownerSchema>;