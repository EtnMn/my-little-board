import { createError } from "h3";
import { serverSupabaseClient, serverSupabaseUser } from "#supabase/server";
import type { Database } from "~/server/types";
import type { Organization } from "~/types";

export default defineEventHandler(async (event): Promise<Organization[]> => {
	const user = await serverSupabaseUser(event);
	const client = await serverSupabaseClient<Database>(event);

	const { data: organizations, error } = await client
		.from("organization")
		.select("*")
		.eq("ownerId", user?.id ?? "");

	if (error)
		throw createError({ statusMessage: error.message });

	// await new Promise(resolve => setTimeout(resolve, 1000));
	return organizations;
});
