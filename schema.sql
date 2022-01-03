create table if not exists "LogMessages" (
    "Id" int generated always as identity,
    "MessageId" numeric not null,
    "Message" text not null,
    "Command" text not null,
    "SubCommand" text null,
    "AuthorId" numeric not null,
    "AuthorName" text not null,
    "ServerId" numeric not null,
    "ServerName" text not null,
    "ChannelId" numeric not null,
    "ChannelName" text not null,
    "CreatedAt" timestamp without time zone not null
);