using System;
using System.Linq;
using System.Threading.Tasks;
using ColorBot.App.Repositories;
using Discord;
using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class SetCommand : CommandBase
    {
        public SetCommand(LogMessageRepository logMessageRepository) : base(logMessageRepository)
        {
        }
        
        [Command("set")]
        public async Task HandleCommandAsync(string value)
        {
            var isRandom = string.Equals(value, "random", StringComparison.OrdinalIgnoreCase);

            System.Drawing.Color color;
            string colorHex;
            if (isRandom)
            {
                var random = new Random();
                color = System.Drawing.Color.FromArgb(
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256)
                );
                colorHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            else if (!TryParseColor(value, out color, out colorHex))
            {
                await ReplyAsync($"{Mention} Color could not be parsed from input. Please try again.");
                return;
            }

            var role = Context.Guild.Roles.FirstOrDefault(r => r.Name == colorHex);
            if (role != null)
            {
                if (role.Members.Select(m => m.Id).Contains(Context.Message.Author.Id))
                {
                    await ReplyAsync($"{Mention} You are already the color {colorHex}.");
                    return;
                }

                await RemoveUserFromColorRoles();
                await GuildUser.AddRoleAsync(role);
            }
            else
            {
                var createdRole = await Context.Guild.CreateRoleAsync(colorHex, GuildPermissions.None,
                    new Color(color.R, color.G, color.B), false, false);
                
                await RemoveUserFromColorRoles();
                await GuildUser.AddRoleAsync(createdRole);
            }
            
            await RemoveEmptyRoles();
            await ReplyAsync($"{Mention} Your color has been updated to {colorHex}!");
        }

        private static bool TryParseColor(string value, out System.Drawing.Color color, out string colorHex)
        {
            try
            {
                color = System.Drawing.ColorTranslator.FromHtml(value);
                colorHex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                return true;
            }
            catch
            {
                color = default;
                colorHex = default;
                return false;
            }
        }

        private async Task RemoveUserFromColorRoles()
        {
            var roles = Context.Guild.Roles.Where(r =>
                r.Name.StartsWith("#")
                && r.Members.Select(m => m.Id).Contains(User.Id)
            ).ToArray();

            if (!roles.Any()) return;

            foreach (var role in roles)
            {
                await GuildUser.RemoveRoleAsync(role);
            }
        }
        
        private async Task RemoveEmptyRoles()
        {
            var roles = Context.Guild.Roles.Where(r =>
                r.Name.StartsWith("#") && !r.Members.Any()).ToArray();

            if (!roles.Any()) return;

            foreach (var role in roles)
            {
                await role.DeleteAsync();
            }
        }
    }
}