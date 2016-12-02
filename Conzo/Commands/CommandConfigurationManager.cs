using System;
using System.Collections.Generic;
using System.Linq;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class CommandConfigurationManager : ICommandConfigurationManager
   {
      private readonly Dictionary<CommandBase, CommandConfiguration> _configuredCommands;
      private InternalCommand _startCommand;
      private readonly Dictionary<ConsoleKey, CommandBase> _globalCommands;

      public CommandConfigurationManager(InternalCommand startCommand)
      {
         _configuredCommands = new Dictionary<CommandBase, CommandConfiguration>();
         _globalCommands = new Dictionary<ConsoleKey, CommandBase>();
         AddCommandIfNecessary(startCommand.Command);
         StartCommand = startCommand;
      }

      public InternalCommand StartCommand
      {
         get
         {
            return _startCommand;
         }
         set
         {
            Enforce.ArgumentNotNull(value, "startCommand can not be null");
            Enforce.ArgumentNotNull(value.Command, "Command can not be null");
            Enforce.DictionaryKeyExists(_configuredCommands, value.Command, "startCommand does not exist");
            _startCommand = value;
         }
      }

      public void AddGlobalCommand(ConsoleKey key, CommandBase command)
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

      public InternalCommand GetNextCommand(InternalCommand currentCommand, ConsoleKey consoleKey)
      {
         InternalCommand nextCommand = null;

         // Determine whether there are there any command configurations for the current command.
         // And if so, determine whether for this key a command is configured.
         if (_configuredCommands.ContainsKey(currentCommand.Command))
         {
            var configuration = _configuredCommands[currentCommand.Command];
            nextCommand = configuration.GetCommand(consoleKey);
         }

         return nextCommand;
      }

      public CommandConfiguration AddCommandIfNecessary(CommandBase command)
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

         return configuration;
      }

      public void Validate()
      {
         // At least one command is required. This typically is the start command.
         if (!_configuredCommands.Any())
         {
            throw new Exception("No commands configured");
         }

         //TODO refactor this orphan stuff:
         var commandsThatHaveCommandPointingToIt = new List<CommandBase>();
         foreach (var commandConfiguration in _configuredCommands.Values)
         {
            commandsThatHaveCommandPointingToIt.AddRange(commandConfiguration.GetAllCommands());
         }

         bool isOrphaned = false;

         // Commands that are configured must not be "orphans", i.e. they must be either the start command or there must be a command pointing to it.
         foreach (var configuredCommand in _configuredCommands)
         {
            isOrphaned = !configuredCommand.Key.Equals(_startCommand.Command);
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

         //TODO The orphan stuff that must be refactored ends here...
      }

   }
}
