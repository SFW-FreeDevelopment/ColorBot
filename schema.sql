create table if not exists "LogMessages" (
    "Id" int generated always as identity,
    "Message" text not null,
    "Command" text not null,
    "SubCommand" text null,
    "Author" text not null,
    "ServerId" text not null,
    "ServerName" text not null,
    "ChannelId" text not null,
    "ChannelName" text not null,
    "CreatedAt" timestamp without timezone not null
);