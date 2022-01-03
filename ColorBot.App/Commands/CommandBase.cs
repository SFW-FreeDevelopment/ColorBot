using System;
using System.Text.Json;
using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LogMessage = ColorBot.App.Models.LogMessage;

namespace ColorBot.App.Commands
{
    public abstract class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected readonly LogMessageRepository _logMessageRepository;
        
        protected CommandBase(LogMessageRepository logMessageRepository)
        {
            _logMessageRepository = logMessageRepository;
        }
        
        protected SocketUser User => Context.Message.Author;
        protected IGuildUser GuildUser => (IGuildUser)User;
        protected string Mention => User.Mention;

        public async Task Log(string message, string command, string subCommand = null)
        {
            var logMessage = new LogMessage(Context, message, command, subCommand);
            var logMessageJson = JsonSerializer.Serialize(logMessage, new JsonSerializerOptions
            {
                WriteIndented = false
            });
            Console.WriteLine(logMessageJson);
            await _logMessageRepository.CreateAsync(logMessage);
        }
    }
}