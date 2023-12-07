export const ROLES_TYPES = ["administrator", "manager", "user"] as const;
type roleTypes = typeof ROLES_TYPES;

export type Role = roleTypes[number];
