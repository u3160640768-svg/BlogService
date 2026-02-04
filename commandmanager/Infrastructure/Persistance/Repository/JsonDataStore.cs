using Application.Dto;
using Application.Interfaces;
using Domain;
using Infrastructure.Persistance.Dto;
using Infrastructure.Persistance.Mappers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repository
{
    public class JsonDataStore : IDataStore
    {
        private readonly string _filePath = "commands.json";
        private readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };
        private static List<CommandDtoPersistance> _cacheDto = new List<CommandDtoPersistance>();

        private async Task EnsureLoadedAsync()
        {
            if (_cacheDto.Count != 0)
            {
                return;
            }
            if (!File.Exists(_filePath))
            {
                return;
            }

            var json = File.ReadAllTextAsync(_filePath);
            var result = await json;
            _cacheDto = JsonSerializer.Deserialize<List<CommandDtoPersistance>>(result) ?? new List<CommandDtoPersistance>();
        }

        public async void SaveCommand(Command command)
        {
            await EnsureLoadedAsync();
            if (command?.Description == null)
            {
                return;
            }
            _cacheDto.Add(command.ToDto());
            var json = JsonSerializer.Serialize(_cacheDto, _jsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public async Task<List<Command>> LoadCommandsAsync()
        {
            await EnsureLoadedAsync();
            List<Command> commands = new List<Command>();
            /*
            foreach (var command in _cacheDto) {
                    commands.Add(command.ToEntity());
            }
            return commands;
            */
            return _cacheDto.Select(dto => dto.ToEntity()).ToList();
        }

        public async Task SaveCommandsAsync(IEnumerable<Command> commands)
        {
            _cacheDto = commands.Select(c => c.ToDto()).ToList();
            var json = JsonSerializer.Serialize(_cacheDto, _jsonOptions);
            await File.WriteAllTextAsync(_filePath, json);
        }
    }
}