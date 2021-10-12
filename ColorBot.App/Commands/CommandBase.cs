using Discord.Commands;

namespace ColorBot.App.Commands
{
    public class CommandBase : ModuleBase<SocketCommandContext>
    {
        protected string Mention => Context.Message.Author.Mention;
    }
}