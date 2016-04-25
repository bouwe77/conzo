using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Commands
{
   [TestClass]
   public class CommandConfigurationTest
   {
      private Command _validCommand;
      private ConsoleKey _validKey;

      [TestInitialize]
      public void TestInitialize()
      {
         _validCommand = Command.Create(() => "Hello World");
         _validKey = ConsoleKey.A;
      }

      [TestMethod]
      public void Constructor_Success()
      {
         var commandConfiguration = new CommandConfiguration();
         Assert.IsNotNull(commandConfiguration);
      }

      [TestMethod]
      public void AddNextCommand_Success()
      {
         var commandConfiguration = new CommandConfiguration();

         var returnedCommandConfiguration = commandConfiguration.AddNextCommand(_validKey, _validCommand);

         Assert.IsNotNull(returnedCommandConfiguration);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void AddNextCommand_ThrowsException_WhenKeyIsNotSupported()
      {
         var commandConfiguration = new CommandConfiguration();

         commandConfiguration.AddNextCommand(ConsoleKey.PrintScreen, _validCommand);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void AddNextCommand_ThrowsException_WhenKeyIsAlreadyAdded()
      {
         var commandConfiguration = new CommandConfiguration();

         commandConfiguration.AddNextCommand(_validKey, _validCommand);
         commandConfiguration.AddNextCommand(_validKey, _validCommand);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void AddNextCommand_ThrowsException_WhenCommandIsNull()
      {
         var commandConfiguration = new CommandConfiguration();

         commandConfiguration.AddNextCommand(_validKey, null);
      }

      [TestMethod]
      public void GetCommand_ReturnsCommand_WhenItExists()
      {
         var commandConfiguration = new CommandConfiguration();
         commandConfiguration.AddNextCommand(_validKey, _validCommand);

         var command = commandConfiguration.GetCommand(_validKey);

         Assert.IsNotNull(command);
      }

      [TestMethod]
      public void GetCommand_ReturnsNull_WhenItDoesNotExist()
      {
         var commandConfiguration = new CommandConfiguration();

         var command = commandConfiguration.GetCommand(_validKey);

         Assert.IsNull(command);
      }

      [TestMethod]
      public void GetAllCommands_ReturnsEmptyList_WhenNoCommands()
      {
         var commandConfiguration = new CommandConfiguration();

         var allCommands = commandConfiguration.GetAllCommands();

         Assert.IsNotNull(allCommands);
         Assert.IsFalse(allCommands.Any());
      }

      [TestMethod]
      public void GetAllCommands_ReturnsExpectedCommands()
      {
         var commandConfiguration = new CommandConfiguration();
         commandConfiguration.AddNextCommand(_validKey, _validCommand);

         var allCommands = commandConfiguration.GetAllCommands().ToList();

         Assert.IsNotNull(allCommands);
         Assert.AreEqual(1, allCommands.Count);
         Assert.IsTrue(allCommands[0].Equals(_validCommand));
      }
   }
}
