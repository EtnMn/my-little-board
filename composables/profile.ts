import type { Profile } from "@/types";

export function useMe() {
  return useState<Profile | undefined>("me", () => undefined);
}
