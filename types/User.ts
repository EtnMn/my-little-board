import type { UserRole } from "@/types";

export type User = {
  avatarUrl: string;
  name: string;
  role: UserRole;
  userId: string;
};
