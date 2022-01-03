using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ColorBot.App.Commands
{
    public class SetCommand : CommandBase
    {
        [Command("set")]
        public async Task HandleCommandAsync([Remainder] string value)
        {
            var isRandom = string.Equals(value, "random", StringComparison.OrdinalIgnoreCase);
            var tryForceSet = value.Contains("force");

            System.Drawing.Color color;
            string colorHex;
            
            if (tryForceSet)
            {
                try
                {
                    var remainingCommand = value.Split("force")[1];
                    var commandParts = remainingCommand.Split(" ")
                        .Where(str => !string.IsNullOrEmpty(str))
                        .ToArray();
                    var commandUsername = commandParts[0].Trim();
                    Console.WriteLine($"Command Username: {commandUsername}");
                    var commandColor = commandParts[1].Trim();
                    Console.WriteLine($"Command Color: {commandColor}");
                    
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
                    else if (!TryParseColor(commandColor, out color, out colorHex))
                    {
                        await ReplyAsync($"{Mention} Color could not be parsed from input. Please try again.");
                        return;
                    }

                    var targetUser = Context.Guild.Users.FirstOrDefault(u => u.Username == commandUsername);
                    if (targetUser == null)
                    {
                        throw new ArgumentException("Attempted to force set color of a user that could not be found.");
                    }
                    var targetGuildUser = (IGuildUser)targetUser;
                    
                    var role = Context.Guild.Roles.FirstOrDefault(r => r.Name == colorHex);
                    if (role != null)
                    {
                        if (role.Members.Select(m => m.Id).Contains(Context.Message.Author.Id))
                        {
                            await ReplyAsync($"{Mention} {targetGuildUser.Username} is already the color {colorHex}.");
                            return;
                        }

                        await RemoveUserFromColorRoles(targetGuildUser);
                        await targetGuildUser.AddRoleAsync(role);
                    }
                    else
                    {
                        var createdRole = await Context.Guild.CreateRoleAsync(colorHex, GuildPermissions.None,
                            new Color(color.R, color.G, color.B), false, false);
                
                        await RemoveUserFromColorRoles(targetGuildUser);
                        await targetGuildUser.AddRoleAsync(createdRole);
                    }
                    
                    await RemoveEmptyRoles();
                    await ReplyAsync($"{Mention} {targetGuildUser.Username}'s color has been updated to {colorHex}!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + e.StackTrace);
                }
            }
            else
            {
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

        private async Task RemoveUserFromColorRoles(IGuildUser guildUser = null)
        {
            guildUser ??= GuildUser;
            
            var roles = Context.Guild.Roles.Where(r =>
                r.Name.StartsWith("#")
                && r.Members.Select(m => m.Id).Contains(guildUser.Id)
            ).ToArray();

            if (!roles.Any()) return;

            foreach (var role in roles)
            {
                await guildUser.RemoveRoleAsync(role);
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