using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class UserAction : Command
    {
        public UserAction(string description,DateTime exTime) : base(description,exTime)
        {
        }

        // Implementazione specifica per le azioni utente
        public override string GetDetails()
        {
            // Aggiungiamo il prefisso "UNDO" per l'output di annullamento
            return $"[ACTION - {ExecutionTime:HH:mm:ss}] {Description}";
        }
    }
}
