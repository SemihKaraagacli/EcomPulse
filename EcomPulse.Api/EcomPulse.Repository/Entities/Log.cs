using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomPulse.Repository.Entities
{
    public sealed class Log
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Logger { get; set; }
        public string Level { get; set; }
        public string Url { get; set; }
        public string Action { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
