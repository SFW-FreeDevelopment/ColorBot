using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorBot.App.Database;
using ColorBot.App.Models;

namespace ColorBot.App.Repositories
{
    public static class LogMessageRepository
    {
        // TODO: Assign this
        private static readonly ColorBotContext _context;

        public static async Task<LogMessage> CreateAsync(LogMessage logMessage)
        {
            logMessage.CreatedAt = DateTime.UtcNow;
            _context.LogMessages.Add(logMessage);
            await _context.SaveChangesAsync();
            return logMessage;
        }

        public static async Task<LogMessage> GetByIdAsync(int id)
        {
            return await _context.LogMessages.FindAsync(id);
        }
        
        public static async Task<List<LogMessage>> GetAllAsync()
        {
            return await _context.LogMessages.ToListAsync();
        }

        public static async Task<LogMessage> GetByAuthorAsync(string username)
        {
            return await _context.LogMessages.FirstOrDefaultAsync(logMessage => logMessage.Author == username);
        }
        
        public static async Task<LogMessage> GetByCommandAsync(string command)
        {
            return await _context.LogMessages.FirstOrDefaultAsync(logMessage => logMessage.Command == command);
        }
    }
}