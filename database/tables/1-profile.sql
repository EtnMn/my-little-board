create table public.profile (
    "profileId" uuid not null,
    avatar text not null default ''::text,
    email text not null default ''::text,
    name text not null default ''::text,
    constraint "profilePkey" primary key ("profileId"),
    constraint "profileProfileIdFkey" foreign key ("profileId") references auth.users (id) on delete cascade
);

alter table public.profile enable row level security;

create policy "Public profile are viewable by everyone."
on profile for select
using ( true );

create policy "Users can update own profile."
on profile for update
using ( auth.uid() = "profileId" );