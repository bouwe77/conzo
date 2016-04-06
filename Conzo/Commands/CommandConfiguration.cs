using System;
using System.Collections.Generic;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Commands
{
   public class CommandConfiguration
   {
      private readonly Dictionary<ConsoleKey, Command> _commands = new Dictionary<ConsoleKey, Command>();

      internal bool GlobalCommandsAdded { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="CommandConfiguration"/> class.
      /// This constructor prevents creating new instances from outside the project.
      /// </summary>
      internal CommandConfiguration()
      {
         // Avoid creating instances from outside the project.
      }

      public CommandConfiguration AddNextCommand(ConsoleKey key, Command nextCommand)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(_commands, key, "Dictionary _commands already contains key" + key);
         Enforce.ArgumentNotNull(nextCommand, "nextCommand can not be null");

         _commands.Add(key, nextCommand);

         return this;
      }

      internal Command GetCommand(ConsoleKey consoleKey)
      {
         Command command = null;
         if (_commands.ContainsKey(consoleKey))
         {
            command = _commands[consoleKey];
         }

         return command;
      }

      internal IEnumerable<Command> GetAllCommands()
      {
         return _commands.Values;
      }
   }
}
