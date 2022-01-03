using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ColorBot.App.Database;
using ColorBot.App.Models;

namespace ColorBot.App.Repositories
{
    public class LogMessageRepository
    {
        private readonly ColorBotContext _context;

        public LogMessageRepository(ColorBotContext context)
        {
            _context = context;
        }
        
        public async Task<LogMessage> CreateAsync(LogMessage logMessage)
        {
            logMessage.CreatedAt = DateTime.UtcNow;
            _context.LogMessages.Add(logMessage);
            await _context.SaveChangesAsync();
            return logMessage;
        }

        public async Task<LogMessage> GetByIdAsync(int id)
        {
            return await _context.LogMessages.FindAsync(id);
        }
        
        public async Task<List<LogMessage>> GetAllAsync()
        {
            return await _context.LogMessages.ToListAsync();
        }

        public async Task<LogMessage> GetByAuthorAsync(string username)
        {
            return await _context.LogMessages.FirstOrDefaultAsync(logMessage => logMessage.AuthorName == username);
        }
        
        public async Task<LogMessage> GetByCommandAsync(string command)
        {
            return await _context.LogMessages.FirstOrDefaultAsync(logMessage => logMessage.Command == command);
        }
    }
}