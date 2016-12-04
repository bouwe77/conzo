using System;
using Conzo.Helpers;

namespace Conzo.Commands
{
   internal class CommandExecuter : ICommandExecuter
   {
      private readonly CommandBase _command;
      private readonly ConsoleKey _consoleKey;

      public CommandExecuter(CommandBase command, ConsoleKey consoleKey)
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
            var command1 = _command as Command;
            if (command1 != null)
            {
               commandContents = command1.Action.Invoke();
            }
            else
            {
               CommandWithPressedKey commandWithPressedKey = (CommandWithPressedKey) _command;
               commandContents = commandWithPressedKey.Action.Invoke(_consoleKey);
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
