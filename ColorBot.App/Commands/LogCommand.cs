using System.Threading.Tasks;
using ColorBot.App.Models;
using ColorBot.App.Repositories;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class LogCommand : CommandBase
    {
        [Command("log")]
        public async Task HandleCommandAsync(string value)
        {
            await Log(value, "log");
        }
    }
}