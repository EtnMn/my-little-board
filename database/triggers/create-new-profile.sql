-- inserts a row into public.profile
create or replace function public."createNewProfile"()
returns trigger
language plpgsql
security definer set search_path = public
as $$
begin
  insert into public.profile ("profileId", avatar, email, name)
  values (new.id, new.raw_user_meta_data->>'avatar_url', new.email, new.raw_user_meta_data->>'user_name');
  return new;
end;
$$;

-- trigger the function every time a user is created
create or replace trigger "onAuthUserCreated"
after insert on auth.users     
for each row execute procedure public."createNewProfile"();
