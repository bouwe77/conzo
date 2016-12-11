using System;
using System.Collections.Generic;
using System.Linq;
using Conzo.Helpers;
using Conzo.Keys;

namespace Conzo.Commands
{
   public class CommandConfiguration
   {
      private readonly Dictionary<ConsoleKey, Command> _internalCommands = new Dictionary<ConsoleKey, Command>();

      internal bool GlobalCommandsAdded { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="CommandConfiguration"/> class.
      /// This constructor only exists to avoid creating instances from outside the project.
      /// </summary>
      internal CommandConfiguration()
      {
         // This constructor only exists to avoid creating instances from outside the project.
      }

      public CommandConfiguration AddNextCommand(ConsoleKey key, Command nextCommand)
      {
         return AddNextCommandIf(key, nextCommand, null);
      }

      public CommandConfiguration AddNextCommandIf(ConsoleKey key, Command nextCommand, Func<bool> condition)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(_internalCommands, key, "Dictionary _commands already contains key" + key);
         Enforce.ArgumentNotNull(nextCommand, "nextCommand can not be null");

         nextCommand.Condition = condition;
         _internalCommands.Add(key, nextCommand);

         return this;
      }

      internal Command GetCommand(ConsoleKey consoleKey)
      {
         Command command = null;
         if (_internalCommands.ContainsKey(consoleKey))
         {
            command = _internalCommands[consoleKey];
         }

         return command;
      }

      internal IEnumerable<Command> GetAllCommands()
      {
         return _internalCommands.Values.Distinct();
      }
   }
}
