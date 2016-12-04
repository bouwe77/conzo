using System;
using System.Collections.Generic;
using System.Linq;
using Conzo.Helpers;
using Conzo.Keys;

namespace Conzo.Commands
{
   public class CommandConfiguration
   {
      private readonly Dictionary<ConsoleKey, CommandBase> _internalCommands = new Dictionary<ConsoleKey, CommandBase>();

      internal bool GlobalCommandsAdded { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="CommandConfiguration"/> class.
      /// This constructor only exists to avoid creating instances from outside the project.
      /// </summary>
      internal CommandConfiguration()
      {
         // This constructor only exists to avoid creating instances from outside the project.
      }

      public CommandConfiguration AddNextCommand(ConsoleKey key, CommandBase nextCommand)
      {
         return AddNextCommandIf(key, nextCommand, null);
      }

      public CommandConfiguration AddNextCommandIf(ConsoleKey key, CommandBase nextCommand, Func<bool> condition)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(_internalCommands, key, "Dictionary _commands already contains key" + key);
         Enforce.ArgumentNotNull(nextCommand, "nextCommand can not be null");

         _internalCommands.Add(key, nextCommand);

         return this;
      }

      internal CommandBase GetCommand(ConsoleKey consoleKey)
      {
         CommandBase command = null;
         if (_internalCommands.ContainsKey(consoleKey))
         {
            command = _internalCommands[consoleKey];
         }

         return command;
      }

      internal IEnumerable<CommandBase> GetAllCommands()
      {
         return _internalCommands.Values.Distinct();
      }
   }
}
