create table public.member (
    "memberId" uuid not null default gen_random_uuid (),
    "organizationId" uuid null,
    "profileId" uuid not null,
    constraint "memberPkey" primary key ("memberId"),
    constraint "memberOrganizationIdFkey" foreign key ("organizationId") references organization ("organizationId") on delete cascade,
    constraint "memberProfileIdFkey" foreign key ("profileId") references profile ("profileId") on delete cascade
);

alter table public.member enable row level security;

create policy "Owner can list organization profiles"
on public.member for select
to authenticated
using (
    "organizationId" in (
    select "organizationId"
    from public.organization
    where "ownerId" = auth.uid())
);

create policy "Owner can add profile to his organization"
on public.member for insert
to authenticated
with check (
    "organizationId" in (
    select "organizationId"
    from public.organization
    where "ownerId" = auth.uid())
);