using System;
using Conzo.Helpers;

namespace Conzo.Commands
{
   internal class CommandExecuter : ICommandExecuter
   {
      private readonly Command _command;
      private readonly ConsoleKey _consoleKey;

      public CommandExecuter(Command command, ConsoleKey consoleKey)
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
            if (_command.Action != null)
            {
               commandContents = _command.Action.Invoke();
            }
            else
            {
               commandContents = _command.ActionWithPressedKey.Invoke(_consoleKey);
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
