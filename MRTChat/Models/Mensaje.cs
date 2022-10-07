using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRTChat.Models
{
    public class Mensaje
    {
        public string Data { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = Preferences.Get("Username", "");
        public string UID { get; set; } = Preferences.Get("UID", "");
    }
}
