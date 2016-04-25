using System;

namespace Conzo.Commands
{
   internal interface ICommandConfigurationManager
   {
      void Validate();
      
      CommandConfiguration AddCommandIfNecessary(Command command);
      void AddGlobalCommand(ConsoleKey consoleKey, Command command);

      InternalCommand GetNextCommand(InternalCommand currentCommand, ConsoleKey consoleKey);
   }
}