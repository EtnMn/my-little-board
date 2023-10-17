export const USER_ROLES_TYPES = ["administrator", "manager", "user"] as const;
type userRoleTypes = typeof USER_ROLES_TYPES;

export type UserRole = userRoleTypes[number];
