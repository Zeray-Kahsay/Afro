import { z } from "zod";

export const loginSchema = z.object({
  phoneNumber: z
    .string()
    .min(1),

  password: z
    .string()
    .min(6),
});

export type LoginFormValues =
  z.infer<typeof loginSchema>;