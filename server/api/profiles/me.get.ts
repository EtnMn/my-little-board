import { createError } from "h3";
import { serverSupabaseClient, serverSupabaseUser } from "#supabase/server";
import type { Database } from "~/server/types";
import type { Profile } from "~/types/Profile";
import { ROLES_TYPES, type Role } from "~/types";

function isRole(value: string | null): value is Role {
	return ROLES_TYPES.findIndex(v => v === value) >= 0;
}

export default defineEventHandler(async (event): Promise<Profile> => {
	const client = await serverSupabaseClient<Database>(event);
	const user = await serverSupabaseUser(event);

	const { data: profile, error } = await client
		.from("getProfiles")
		.select("profileId, avatar, email, name, role")
		.eq("profileId", user?.id ?? "")
		.single();

	if (error)
		throw createError({ statusMessage: error.message });

	return {
		profileId: profile.profileId ?? "",
		avatar: profile.avatar ?? "",
		email: profile.email ?? "",
		name: profile.name ?? "",
		role: isRole(profile.role) ? profile.role : "user",
	};
});
