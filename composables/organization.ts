import type { Organization } from "@/types";

export function useOrganization() {
  return useState<Organization[]>("organization", () => []);
}
