using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class CommandExecuter : ICommandExecuter
   {
      private readonly InternalCommand _command;
      private readonly ConsoleKey _consoleKey;

      public CommandExecuter(InternalCommand command, ConsoleKey consoleKey)
      {
         _command = Enforce.ArgumentNotNull(command, "command can not be null");
         _consoleKey = consoleKey;
      }

      public bool TryExecute(out string commandContents)
      {
         commandContents = null;

         bool commandExecuted = true;
         if (_command.Condition != null)
         {
            commandExecuted = _command.Condition.Invoke();
         }

         if (commandExecuted)
         {
            try
            {
               var command1 = _command.Command as Command1;
               if (command1 != null)
               {
                  commandContents = command1.Action.Invoke();
               }
               else
               {
                  Command2 command2 = (Command2) _command.Command;
                  commandContents = command2.Action.Invoke(_consoleKey);
               }
            }
            catch
            {
               //TODO logging
               throw;
            }
         }

         return commandExecuted;
      }
   }
}
