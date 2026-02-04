using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.UseServices;
using Infrastructure;
using Application.Dto;
using Application.Mappers;

namespace TestApplication
{
    [TestClass]
    public sealed class TestUseCases
    {
        private CommandProcessor commandProcessor;
        private InMemoryDataStore dataStore;

        [TestInitialize]
        public void Setup()
        {
            dataStore = new InMemoryDataStore();
            commandProcessor = new CommandProcessor(dataStore);
        }

        [TestMethod]
        public void UndoUserAction_Adds2Command_RemoveLast()
        {
            var command1 = new CommandDto("Action 1", DateTime.UtcNow, true);
            var command2 = new CommandDto("Action 2", DateTime.UtcNow, true);

            commandProcessor.AddData(command1);
            commandProcessor.AddData(command2);

            var undoneAction = commandProcessor.UndoLastAction();

            Assert.AreEqual(command2.ToEntity().GetDetails(), undoneAction);
        }

        [TestMethod]
        public void UndoLastAction_OnEmptyStack_ReturnsNullOrEmpty()
        {
            var result = commandProcessor.UndoLastAction();
            Assert.IsTrue(string.IsNullOrEmpty(result), "Undo on empty should return null or empty string");
        }

        [TestMethod]
        public void ProcessAllLogs_ReturnsLogEntriesInOrder()
        {
            var log1 = new CommandDto("Log 1", DateTime.UtcNow, false);
            var log2 = new CommandDto("Log 2", DateTime.UtcNow.AddSeconds(1), false);

            commandProcessor.AddData(log1);
            commandProcessor.AddData(log2);

            var logs = commandProcessor.ProcessAllLogs().ToList();

            var expected = new List<string>
            {
                log1.ToEntity().GetDetails(),
                log2.ToEntity().GetDetails()
            };

            CollectionAssert.AreEqual(expected, logs);
        }

        [TestMethod]
        public void AddData_CallsSaveCommands()
        {
            var cmd = new CommandDto("SaveTest", DateTime.UtcNow, true);
            commandProcessor.AddData(cmd);

            Assert.IsNotNull(dataStore.LastSavedCommands, "SaveCommands should be invoked and recorded by the data store");
            Assert.IsTrue(dataStore.LastSavedCommands.Any(), "Saved list must not be empty after adding a command");

            var savedDetails = dataStore.LastSavedCommands.Select(c => c.GetDetails()).ToList();
            Assert.IsTrue(savedDetails.Contains(cmd.ToEntity().GetDetails()));
        }

        [TestMethod]
        public void Mixed_AddLogThenUser_UndoRemovesOnlyUser_LogsRemain()
        {
            var log = new CommandDto("System Log", DateTime.UtcNow, false);
            var user = new CommandDto("User Action", DateTime.UtcNow.AddSeconds(1), true);

            commandProcessor.AddData(log);
            commandProcessor.AddData(user);

            var undone = commandProcessor.UndoLastAction();
            Assert.AreEqual(user.ToEntity().GetDetails(), undone);

            var remainingLogs = commandProcessor.ProcessAllLogs().ToList();
            Assert.AreEqual(1, remainingLogs.Count);
            Assert.AreEqual(log.ToEntity().GetDetails(), remainingLogs[0]);
        }
    }
}