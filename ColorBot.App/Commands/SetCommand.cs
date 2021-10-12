using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class SetCommand : CommandBase
    {
        [Command("set")]
        public async Task HandleCommandAsync(string value)
        {
            System.Drawing.Color color;
            string colorHex;
            
            try
            {
                color = System.Drawing.ColorTranslator.FromHtml(value);
                colorHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            catch // Inform the user that the color could not be parsed by name or hex value
            {
                await ReplyAsync($"{Mention} Color could not be parsed from input. Please try again.");
                return;
            }
            
            foreach (var id in ((IGuildUser)Context.User).RoleIds)
            {
                var userRole = Context.Guild.Roles.FirstOrDefault(r => r.Id == id);
                if (userRole == null) continue;
                
                if (userRole.Name.Contains("#") && userRole.Name != colorHex)
                {
                    if (userRole.Members.Count() == 1) // Check if user is the only one in role
                    {
                        await userRole.DeleteAsync(); // Delete if so
                    }
                    else // Else, simply remove them from the role
                    {
                        await ((IGuildUser)Context.User).RemoveRoleAsync(userRole);
                    }
                }
            }
            
            var role = Context.Guild.Roles.FirstOrDefault(r => r.Name == colorHex);
            if (role != null) // Color role exists
            {
                if (role.Members.Any(m => m.Id == Context.User.Id))
                {
                    await ReplyAsync($"{Mention} You are already the color {colorHex}.");
                    return;
                }
                await ((IGuildUser)Context.User).AddRoleAsync(role);
            }
            else // Color role does not exist
            {
                var createdRole = await Context.Guild.CreateRoleAsync(colorHex, GuildPermissions.None,
                    new Color(color.R, color.G, color.B), false, false);
                
                await ((IGuildUser)Context.User).AddRoleAsync(createdRole);
            }
            
            await ReplyAsync($"{Mention} Your color has been updated to {colorHex}!");
        }
    }
}