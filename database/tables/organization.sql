create table public.organization (
    "organizationId" uuid not null default gen_random_uuid (),
    name text not null,
    "ownerId" uuid null,
    constraint "organizationsPkey" primary key ("organizationId"),
    constraint "organizationOwnerIdFkey" foreign key ("ownerId") references profile ("profileId") on delete set null
) tablespace pg_default;

alter table organization enable row level security;

create policy "Managers can view their own organizations."
on organization for select
using ( auth.uid() = "ownerId" );