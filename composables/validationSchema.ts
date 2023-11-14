import { z } from "zod";

export function useOrganizationSchema() {
  return z.object({
    name: z.string().min(3).max(25),
  });
}
