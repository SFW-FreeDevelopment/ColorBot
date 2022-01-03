using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Discord.Commands;

namespace ColorBot.App.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public string SubCommand { get; set; }
        public string Author { get; set; }
        public string ServerId { get; set; }
        public string ServerName { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        [NotMapped] public List<string> Mentions { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public LogMessage() { }

        public LogMessage(SocketCommandContext context, string message, string command, string subCommand = null)
        {
            Author = context.User.Username;
            ServerId = context.Guild.Id.ToString();
            ServerName = context.Guild.Name;
            ChannelId = context.Channel.Id.ToString();
            ChannelName = context.Channel.Name;
            Message = message;
            Command = command;
            SubCommand = subCommand;
        }
    }
}