using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public class InMemoryDataStore : IDataStore
    {
        public List<Command> LastSavedCommands { get; private set; } = new List<Command>();

        private List<Command> _store = new List<Command>();

        public Task SaveCommandsAsync(IEnumerable<Command> commands)
        {
            LastSavedCommands = commands != null ? new List<Command>(commands) : new List<Command>();
            _store = new List<Command>(LastSavedCommands);
            return Task.CompletedTask;
        }

        public Task<List<Command>> LoadCommandsAsync()
        {
            return Task.FromResult(new List<Command>(_store));
        }
    }
}
