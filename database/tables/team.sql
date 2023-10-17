create table teams (
  team_id serial primary key,
  name text,
  owner_id uuid references auth.users
);

alter table teams enable row level security;