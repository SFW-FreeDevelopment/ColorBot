using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class PingCommand : CommandBase
    {
        public PingCommand(LogMessageRepository logMessageRepository) : base(logMessageRepository)
        {
        }
        
        [Command("ping")]
        public async Task HandleCommandAsync()
        {
            await ReplyAsync("I am pinging the server.");
        }
    }
}