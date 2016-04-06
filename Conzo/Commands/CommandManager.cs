using System;
using System.Collections.Generic;
using System.Linq;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class CommandManager : ICommandManager
   {
      private readonly Dictionary<Command, CommandConfiguration> _configuredCommands;
      private Command _startCommand;
      private readonly Dictionary<ConsoleKey, Command> _globalCommands;

      internal CommandManager()
      {
         _configuredCommands = new Dictionary<Command, CommandConfiguration>();
         _globalCommands = new Dictionary<ConsoleKey, Command>();
      }

      public CommandConfiguration Configure(Command command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         CommandConfiguration configuration;
         if (!_configuredCommands.ContainsKey(command))
         {
            configuration = new CommandConfiguration();

            foreach (var globalCommand in _globalCommands)
            {
               configuration.AddNextCommand(globalCommand.Key, globalCommand.Value);
            }

            configuration.GlobalCommandsAdded = true;

            _configuredCommands.Add(command, configuration);
         }
         else
         {
            configuration = _configuredCommands[command];
         }

         if (StartCommand == null)
         {
            StartCommand = command;
         }

         return configuration;
      }

      public Command GetNewCurrentCommand(Command currentCommand, ConsoleKey key)
      {
         Command newCurrentCommand = null;

         // Determine whether there are there any command configurations for the current command.
         // And if so, determine whether for this key a command is configured.
         if (_configuredCommands.ContainsKey(currentCommand))
         {
            var configuration = _configuredCommands[currentCommand];
            newCurrentCommand = configuration.GetCommand(key);
         }

         if (newCurrentCommand == null)
         {
            newCurrentCommand = currentCommand;
         }

         return newCurrentCommand;
      }

      public void Validate()
      {
         // At least one command is required. This typically is the start command.
         if (!_configuredCommands.Any())
         {
            throw new Exception("No commands configured");
         }

         //TODO refactor this:
         var commandsThatHaveCommandPointingToIt = new List<Command>();
         foreach (var commandConfiguration in _configuredCommands.Values)
         {
            commandsThatHaveCommandPointingToIt.AddRange(commandConfiguration.GetAllCommands());
         }

         bool isOrphaned = false;

         // Commands that are configured must not be "orphans", i.e. they must be either the start command or there must be a command pointing to it.
         foreach (var configuredCommand in _configuredCommands)
         {
            isOrphaned = !configuredCommand.Key.Equals(_startCommand);
            if (isOrphaned)
            {
               foreach (var command in commandsThatHaveCommandPointingToIt)
               {
                  if (command.Equals(configuredCommand.Key))
                  {
                     isOrphaned = false;
                     break;
                  }
               }
            }

            if (isOrphaned)
            {
               break;
            }
         }

         if (isOrphaned)
         {
            throw new Exception("You can not configure a orphaned command, i.e. a command that has no command pointing to it");
         }
      }

      public Command StartCommand
      {
         get
         {
            return _startCommand;
         }
         set
         {
            Enforce.ArgumentNotNull(value, "startCommand can not be null");
            Enforce.DictionaryKeyExists(_configuredCommands, value, "startCommand does not exist");
            _startCommand = value;
         }
      }

      public void AddGlobalCommand(ConsoleKey key, Command command)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(_globalCommands, key, "Dictionary _globalCommands already contains key" + key);
         Enforce.ArgumentNotNull(command, "command can not be null");

         _globalCommands.Add(key, command);

         foreach (var configuredCommand in _configuredCommands)
         {
            configuredCommand.Value.AddNextCommand(key, command);
         }
      }
   }
}
