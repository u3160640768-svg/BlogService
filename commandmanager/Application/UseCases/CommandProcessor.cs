using Application.Interfaces;
using Application.Dto;
using Domain;
using Application.Mappers;

namespace Application.UseServices
{
    public class CommandProcessor
    {
        private readonly Stack<UserAction> _commandStack;
        private readonly Queue<LogEntry> _logQueue;
        private readonly IDataStore _dataStore;


        public CommandProcessor(IDataStore dataStore)
        {
            _logQueue = new Queue<LogEntry>();
            _commandStack = new Stack<UserAction>();
            _dataStore = dataStore;
        }


        public void AddData(CommandDto commandDto)
        {
            LoadState();
            if (commandDto.IsUserAction)
            {
                _commandStack.Push(new UserAction(commandDto.Command,commandDto.ExecutionTime));
            }
            else
            {
                _logQueue.Enqueue(new LogEntry(commandDto.Command,commandDto.ExecutionTime));
            }
            SaveState();
        }
        

        public IEnumerable<string> ProcessAllLogs()
        {
            LoadState();
            if (_logQueue.Count == 0)
            {
                return new List<string>();
            }
            List<string> processedLogs = new List<string>();
            int logCount = _logQueue.Count;
            for (int i = 0; i < logCount; i++)
            {
                var log = _logQueue.Dequeue();
                processedLogs.Add(log.GetDetails());
            }
            SaveState();
            return processedLogs;
        }

        public string UndoLastAction()
        {
            LoadState();
            if (_commandStack.Count == 0)
            {
                return string.Empty;
            }
            var removed = _commandStack.Pop();
            SaveState();
            return removed.GetDetails();
        }

        private void SaveState()
        {
            List<Command> allCommands = new List<Command>();            

            var stackArray = _commandStack.ToArray();
            for (int i = stackArray.Length - 1; i >= 0; i--)
            {
                allCommands.Add(stackArray[i]);
            }
            
            foreach (var log in _logQueue)
            {
                allCommands.Add(log);
            }
            _dataStore.SaveCommandsAsync(allCommands);
        }

        private void LoadState()
        {
            _commandStack.Clear();
            _logQueue.Clear();

            var commands = _dataStore.LoadCommandsAsync();
            var result = commands.Result;
            if (commands is null) return;
            if(result.Count == 0) return;
            foreach (var command in result)
            {
                if (command is UserAction userAction)
                {
                    _commandStack.Push(userAction);
                }
                else if (command is LogEntry logEntry)
                {
                    _logQueue.Enqueue(logEntry);
                }
            }
        }
    }
}
