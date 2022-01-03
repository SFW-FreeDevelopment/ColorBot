using System.Threading.Tasks;
using ColorBot.App.Models;
using ColorBot.App.Repositories;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class LogCommand : CommandBase
    {
        public LogCommand(LogMessageRepository logMessageRepository) : base(logMessageRepository)
        {
        }
        
        [Command("log")]
        public async Task HandleCommandAsync([Remainder] string value)
        {
            await Log(value, "log");
        }
    }
}