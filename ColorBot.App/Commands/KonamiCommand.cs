using System.Threading.Tasks;
using ColorBot.App.Models;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class KonamiCommand : CommandBase
    {
        [Command("konami")]
        public async Task HandleCommandAsync()
        {
            await Log(new LogMessage
            {
                Message = 
            });
            await ReplyAsync("⬆⬆⬇⬇⬅➡⬅➡🅱🅰");
        }
    }
}