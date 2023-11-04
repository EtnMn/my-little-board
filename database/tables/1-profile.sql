create table public.profile (
    "profileId" uuid not null references auth.users on delete cascade,
    avatar text not null default ''::text,
    email text not null default ''::text,
    name text not null default ''::text,
    constraint "profilePkey" primary key ("profileId")
);

alter table public.profile enable row level security;

create policy "Public profile are viewable by everyone."
on profile for select
using ( true );

create policy "Users can update own profile."
on profile for update
using ( auth.uid() = "profileId" );