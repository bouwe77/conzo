using System;

namespace Conzo.Commands
{
   internal interface ICommandManager
   {
      CommandConfiguration Configure(Command command);
      Command GetNewCurrentCommand(Command currentCommand, ConsoleKey key);
      void Validate();
      Command StartCommand { get; set; }
      void AddGlobalCommand(ConsoleKey consoleKey, Command command);
   }
}
