using System;

namespace Conzo.Commands
{
   internal interface ICommandConfigurationManager
   {
      void Validate();
      
      CommandConfiguration AddCommandIfNecessary(CommandBase command);
      void AddGlobalCommand(ConsoleKey consoleKey, CommandBase command);

      InternalCommand GetNextCommand(InternalCommand currentCommand, ConsoleKey consoleKey);
   }
}