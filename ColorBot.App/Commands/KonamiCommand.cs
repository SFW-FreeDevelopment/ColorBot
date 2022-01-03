using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class KonamiCommand : CommandBase
    {
        public KonamiCommand(LogMessageRepository logMessageRepository) : base(logMessageRepository)
        {
        }
        
        [Command("konami")]
        public async Task HandleCommandAsync()
        {
            await Log("⬆⬆⬇⬇⬅➡⬅➡🅱🅰", "konami");
            await ReplyAsync("⬆⬆⬇⬇⬅➡⬅➡🅱🅰");
        }
    }
}