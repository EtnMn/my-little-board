create table public.organization (
    "organizationId" uuid not null default gen_random_uuid (),
    name text not null,
    "ownerId" uuid null,
    constraint "organizationPkey" primary key ("organizationId"),
    constraint "organizationOwnerIdFkey" foreign key ("ownerId") references profile ("profileId") on delete set null
);

alter table organization enable row level security;

-- Todo: EM: il faut également tester la claim manager ou admin
create policy "Owners can view their own organizations."
on public.organization for select
to authenticated
using (auth.uid() = "ownerId");

create policy "Manager can insert an organization"
on public.organization for insert
to authenticated
with check (get_my_claim('user_role') = '"manager"');