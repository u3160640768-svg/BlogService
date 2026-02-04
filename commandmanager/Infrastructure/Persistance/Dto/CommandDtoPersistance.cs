using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Dto
{
    public record CommandDtoPersistance(
        string Description, 
        DateTime Timestamp, 
        bool IsUserAction
        );
}
