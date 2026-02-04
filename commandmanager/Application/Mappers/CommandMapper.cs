using Application.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class CommandMapper
    {
        public static Command ToEntity(this CommandDto command)
        {
            if (command.IsUserAction)
            {
               return new UserAction(command.Command,command.ExecutionTime);
            }
            else
            {
                return new LogEntry(command.Command,command.ExecutionTime);
            }
        }

        public static CommandDto ToDto(this Command command)
        {
            return new CommandDto
            (
                command.Description,
                command.ExecutionTime,
                command is UserAction
            );
        }
    }
}
