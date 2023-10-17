import type { User as SupabaseUser } from "@supabase/supabase-js";
import { USER_ROLES_TYPES } from "@/types";
import type { User, UserRole } from "@/types";

export function useIsUserRole(value: string): value is UserRole {
  return USER_ROLES_TYPES.includes(value as UserRole);
}

export function useSetMe(value: SupabaseUser) {
  useMe().value = {
    avatarUrl: value?.user_metadata?.avatar_url ?? "",
    name: value?.user_metadata?.user_name ?? "",
    role: useIsUserRole(value?.app_metadata?.user_role) ? value.app_metadata.user_role : "user",
  };
}

export function useMe() {
  return useState<User>("me", () => ({
    avatarUrl: "",
    name: "",
    role: "user",
  }));
}
