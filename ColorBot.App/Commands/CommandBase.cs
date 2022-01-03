using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LogMessage = ColorBot.App.Models.LogMessage;

namespace ColorBot.App.Commands
{
    public class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected SocketUser User => Context.Message.Author;
        protected IGuildUser GuildUser => (IGuildUser)User;
        protected string Mention => User.Mention;

        public async Task Log(string message, string command, string subCommand = null)
        {
            await LogMessageRepository.CreateAsync(new LogMessage(Context, message, command, subCommand));
        }
    }
}