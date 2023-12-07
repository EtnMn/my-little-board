import { fromZodError } from "zod-validation-error";
import { useOrganizationSchema } from "@/composables/validationSchema";
import { serverSupabaseClient, serverSupabaseUser } from "#supabase/server";
import type { Database } from "~/server/types";

export default defineEventHandler(async (event) => {
  const body = await readBody(event);

  const organizationSchema = useOrganizationSchema();
  const parsed = organizationSchema.safeParse(body);
  if (!parsed.success)
    throw createError({ statusCode: 400, statusMessage: fromZodError(parsed.error).message });

  const user = await serverSupabaseUser(event);
  const client = await serverSupabaseClient<Database>(event);
  const { error } = await client.from("organization").insert({ name: parsed.data.name, ownerId: user!.id });
  if (error)
    throw createError({ statusText: error.code, statusMessage: error.message });

  return body;
});
