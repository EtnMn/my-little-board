import { createError } from "h3";
import { serverSupabaseClient, serverSupabaseUser } from "#supabase/server";
import type { Database } from "~/server/types";
import type { Profile } from "~/types/Profile";

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

  return profile;
});
