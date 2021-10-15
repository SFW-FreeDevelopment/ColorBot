using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class HelpCommand : CommandBase
    {
        [Command("help")]
        public async Task HandleCommandAsync()
        {
            await ReplyAsync($"**The following commands can be used:**{Environment.NewLine}" +
                             $"  • **ping** - Pings the Discord channel{Environment.NewLine}" +
                             $"  • **konami** - Displays the Konami code as emojis{Environment.NewLine}" +
                             $"  • **set \"name or hex value or random\"** - Updates user color via role{Environment.NewLine}" +
                             $"  • **list** - Lists all currently used color roles{Environment.NewLine}" +
                             $"  • **clean** - Removes empty color roles");
        }
    }
}