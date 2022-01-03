using System;
using System.Linq;
using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class ListCommand : CommandBase
    {
        public ListCommand(LogMessageRepository logMessageRepository) : base(logMessageRepository)
        {
        }
        
        [Command("list")]
        public async Task HandleCommandAsync()
        {
            var colorRoles = Context.Guild.Roles.Where(r => r.Name.StartsWith("#")).ToArray();
            
            if (colorRoles.Length == 0)
            {
                await ReplyAsync($"{Mention} There are no color roles currently in use.");
            }
            else
            {
                var message = $"{Mention} The following color roles are currently in use:";
                
                message = colorRoles.Aggregate(message, (current, role) =>
                    current + $"{Environment.NewLine}  • {role.Name} - {role.Members.Count()} user(s)");
                
                await ReplyAsync(message);
            } 
        }
    }
}