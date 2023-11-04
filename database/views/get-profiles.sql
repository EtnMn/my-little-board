create or replace view public."getProfiles" as
  select
    profile."profileId",
    profile.avatar,
    profile.email,
    profile.name,
    users.raw_app_meta_data->>'user_role' as role
  from public.profile
  inner join auth.users on users.id = profile."profileId";