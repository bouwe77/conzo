using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Conzo.Exceptions;
using Conzo.Helpers;
using Conzo.Keys;

namespace Conzo.Commands
{
   internal class CommandRepository
   {
      private static readonly Dictionary<CommandBase, CommandConfiguration> ConfiguredCommands;
      private static CommandBase StartCommand;
      private static readonly Dictionary<ConsoleKey, CommandBase> GlobalCommands;

      static CommandRepository()
      {
         ConfiguredCommands = new Dictionary<CommandBase, CommandConfiguration>();
         GlobalCommands = new Dictionary<ConsoleKey, CommandBase>();
      }

      public static void SetStartCommand(CommandBase startCommand)
      {
         AddCommandIfNecessary(startCommand);
         StartCommand = startCommand;
      }

      public static void AddGlobalCommand(ConsoleKey key, CommandBase command)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(GlobalCommands, key, "Dictionary _globalCommands already contains key" + key);
         Enforce.ArgumentNotNull(command, "command can not be null");

         GlobalCommands.Add(key, command);

         foreach (var configuredCommand in ConfiguredCommands)
         {
            configuredCommand.Value.AddNextCommand(key, command);
         }
      }

      public static CommandBase GetNextCommand(CommandBase currentCommand, ConsoleKey consoleKey)
      {
         CommandBase nextCommand = null;

         // Determine whether there are there any command configurations for the current command.
         // And if so, determine whether for this key a command is configured.
         if (ConfiguredCommands.ContainsKey(currentCommand))
         {
            var configuration = ConfiguredCommands[currentCommand];
            nextCommand = configuration.GetCommand(consoleKey);
         }

         return nextCommand;
      }

      //TODO kan deze private worden?
      public static CommandConfiguration AddCommandIfNecessary(CommandBase command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         CommandConfiguration configuration;
         if (!ConfiguredCommands.ContainsKey(command))
         {
            configuration = new CommandConfiguration();

            foreach (var globalCommand in GlobalCommands)
            {
               configuration.AddNextCommand(globalCommand.Key, globalCommand.Value);
            }

            configuration.GlobalCommandsAdded = true;

            ConfiguredCommands.Add(command, configuration);
         }
         else
         {
            configuration = ConfiguredCommands[command];
         }

         return configuration;
      }

      public static void Validate()
      {
         // At least one command is required. This typically is the start command.
         if (!ConfiguredCommands.Any())
         {
            throw new ConzoException("No commands configured");
         }

         //TODO refactor this orphan stuff:
         var commandsThatHaveCommandPointingToIt = new List<CommandBase>();
         foreach (var commandConfiguration in ConfiguredCommands.Values)
         {
            commandsThatHaveCommandPointingToIt.AddRange(commandConfiguration.GetAllCommands());
         }

         bool isOrphaned = false;

         // Commands that are configured must not be "orphans", i.e. they must be either the start command or there must be a command pointing to it.
         foreach (var configuredCommand in ConfiguredCommands)
         {
            isOrphaned = !configuredCommand.Key.Equals(StartCommand);
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
            throw new ConzoException("You can not configure a orphaned command, i.e. a command that has no command pointing to it");
         }

         //TODO The orphan stuff that must be refactored ends here...
      }

      /// <summary>
      /// For testing only: Clears the configured commands.
      /// </summary>
      internal static void Clear()
      {
         ConfiguredCommands.Clear();
         GlobalCommands.Clear();
      }
   }
}
