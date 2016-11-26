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
         bool commandExecuted = false;
         commandContents = null;

         bool executeCommand = CheckCondition();
         if (executeCommand)
         {
            commandExecuted = true;
            commandContents = ExecuteCommand();
         }

         return commandExecuted;
      }

      private string ExecuteCommand()
      {
         string commandContents;

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

         return commandContents;
      }

      private bool CheckCondition()
      {
         bool conditionSatisfied = true;

         if (_command.Condition != null)
         {
            try
            {
               conditionSatisfied = _command.Condition.Invoke();
            }
            catch (Exception exception)
            {
               //TODO logging
               throw;
            }
         }

         return conditionSatisfied;
      }
   }
}
