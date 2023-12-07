import { z } from "zod";

export function useOrganizationSchema() {
  return z.object({
    name: z.string().min(3).max(25),
  });
}

export function useUuidSchema() {
  return z.string().uuid({ message: "Invalid UUID" });
}
