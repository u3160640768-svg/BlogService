using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Persistance.Dto;
using Domain;
using System.Runtime.CompilerServices;

namespace Infrastructure.Persistance.Mappers
{
    public static class PersistanceCommandMappers
    {
        public static Command ToEntity(this CommandDtoPersistance dto)
        {
            if(dto.IsUserAction)
            {
                return new UserAction(dto.Description, dto.Timestamp);
            }
            else
            {
                return new LogEntry(dto.Description, dto.Timestamp);
            }
        }
        public static CommandDtoPersistance ToDto(this Command command)
        {
            return new CommandDtoPersistance(
                command.Description,
                command.ExecutionTime,
                command is UserAction
                );
        }
    }
}
