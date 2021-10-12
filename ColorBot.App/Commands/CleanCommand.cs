using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class CleanCommand : CommandBase
    {
        [Command("clean")]
        public async Task HandleCommandAsync()
        {
            var roles = Context.Guild.Roles.Where(r =>
                r.Name.StartsWith("#") && !r.Members.Any()).ToArray();

            if (!roles.Any())
            {
                await ReplyAsync($"{Mention} There are no color roles that require clean up.");
                return;
            };

            foreach (var role in roles)
            {
                await role.DeleteAsync();
            }

            await ReplyAsync(roles.Length == 1
                ? $"{Mention} 1 role has been deleted."
                : $"{Mention} {roles.Length} roles have been deleted.");   
        }
    }
}