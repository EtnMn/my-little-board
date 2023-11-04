import type { Role } from "~/types";

export type Profile = {
  profileId: string;
  avatar: string;
  email: string;
  name: string;
  role?: Role;
};
