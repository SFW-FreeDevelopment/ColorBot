using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColorBot.App.Models
{
    public class LogMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public string SubCommand { get; set; }
        public string Author { get; set; }
        [NotMapped] public List<string> Mentions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}