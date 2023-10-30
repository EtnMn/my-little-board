create table
  public.profile (
    name text not null default ''::text,
    "createdAt" timestamp with time zone not null default current_timestamp,
    "profileId" uuid not null default auth.uid (),
    constraint "profilesPkey" primary key ("profileId")
  ) tablespace pg_default;

alter table profile enable row level security;
