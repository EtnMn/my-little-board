import { fromZodError } from "zod-validation-error";
import { useOrganizationSchema } from "@/composables/validationSchema";

export default defineEventHandler(async (event) => {
  const body = await readBody(event);

  const organizationSchema = useOrganizationSchema();
  const parsed = organizationSchema.safeParse(body);
  if (!parsed.success)
    throw createError({ statusCode: 400, statusMessage: fromZodError(parsed.error).message });

  console.log("ff", body);

  return body;
});
