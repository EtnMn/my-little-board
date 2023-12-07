import { createError } from "h3";
import { fromZodError } from "zod-validation-error";
import { serverSupabaseClient } from "#supabase/server";
import { useUuidSchema } from "@/composables/validationSchema";
import type { Database } from "~/server/types";

export default defineEventHandler(async (event) => {
	const organizationId = getRouterParam(event, "organizationId");
	const memberId = getRouterParam(event, "memberId");
	const uuidSchema = useUuidSchema();
	const organizationIdParsed = uuidSchema.safeParse(organizationId);
	if (!organizationIdParsed.success)
		throw createError({ statusCode: 400, statusMessage: fromZodError(organizationIdParsed.error).message });

	const memberIdParsed = uuidSchema.safeParse(memberId);
	if (!memberIdParsed.success)
		throw createError({ statusCode: 400, statusMessage: fromZodError(memberIdParsed.error).message });

	const client = await serverSupabaseClient<Database>(event);

	const { count, error } = await client
		.from("member")
		.delete({ count: "exact" })
		.eq("organizationId", organizationIdParsed.data)
		.eq("memberId", memberIdParsed.data);

	if (error)
		throw createError({ statusMessage: error.message });

	if (!count)
		throw createError({ statusMessage: "Not found", statusCode: 404 });

	return true;
});
