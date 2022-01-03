using ColorBot.App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ColorBot.App.Database
{
    public class ColorBotContext : DbContext
    {
        public ColorBotContext(DbContextOptions options) : base(options) { }
        
        public DbSet<LogMessage> LogMessages { get; set; }
    }
}