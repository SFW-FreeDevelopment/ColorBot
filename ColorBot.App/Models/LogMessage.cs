using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Discord.Commands;

namespace ColorBot.App.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public ulong MessageId { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public string SubCommand { get; set; }
        public ulong AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ulong ServerId { get; set; }
        public string ServerName { get; set; }
        public ulong ChannelId { get; set; }
        public string ChannelName { get; set; }
        [NotMapped] public List<string> Mentions { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public LogMessage() { }

        public LogMessage(SocketCommandContext context, string message, string command, string subCommand = null)
        {
            MessageId = context.Message.Id;
            AuthorId = context.User.Id;
            AuthorName = context.User.Username;
            ServerId = context.Guild.Id;
            ServerName = context.Guild.Name;
            ChannelId = context.Channel.Id;
            ChannelName = context.Channel.Name;
            Message = context.Message.Content;
            Command = command;
            SubCommand = subCommand;
        }
    }
}