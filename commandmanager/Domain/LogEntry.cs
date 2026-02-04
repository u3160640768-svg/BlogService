using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LogEntry : Command
    {
        public LogEntry(string description,DateTime exTime) : base(description, exTime)
        {
            
        }

        // Implementazione specifica per i log
        public override string GetDetails()
        {
            return $"[LOG - {ExecutionTime:HH:mm:ss}] {Description}";
        }
    }
}
