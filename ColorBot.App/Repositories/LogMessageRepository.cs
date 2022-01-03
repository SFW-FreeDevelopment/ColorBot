using System.Collections.Generic;
using System.Linq;
using ColorBot.App.Database;
using ColorBot.App.Models;

namespace ColorBot.App.Repositories
{
    public static class LogMessageRepository
    {
        private static readonly ColorBotContext _context;

        public static LogMessage Create(LogMessage logMessage)
        {
            _context.LogMessages.Add(logMessage);
            _context.SaveChanges();
            return logMessage;
        }

        public static LogMessage GetById(int id)
        {
            return _context.LogMessages.Find(id);
        }
        
        public static List<LogMessage> GetAll()
        {
            return _context.LogMessages.ToList();
        }

        public static LogMessage GetByAuthor(string username)
        {
            return _context.LogMessages.FirstOrDefault(logMessage => logMessage.Author == username);
        }
        
        public static LogMessage GetByCommand(string command)
        {
            return _context.LogMessages.FirstOrDefault(logMessage => logMessage.Command == command);
        }
    }
}