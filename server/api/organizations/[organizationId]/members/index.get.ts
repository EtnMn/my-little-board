import { createError } from "h3";
import { serverSupabaseClient } from "#supabase/server";
import { useUuidSchema } from "@/composables/validationSchema";
import type { Database } from "~/server/types";

export default defineEventHandler(async (event) => {
	const organizationId = getRouterParam(event, "organizationId");
	const uuidSchema = useUuidSchema();
	uuidSchema.safeParse(organizationId);

	const client = await serverSupabaseClient<Database>(event);

	const { data: members, error } = await client
		.from("member")
		.select("memberId, organization(organizationId, name, ownerId), profile(profileId, avatar, email, name)")
		.eq("organizationId", organizationId ?? "");

	if (error)
		throw createError({ statusMessage: error.message });

	return members;
});
