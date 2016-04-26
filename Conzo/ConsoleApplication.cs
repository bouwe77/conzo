using System;
using Conzo.Configuration;
using Conzo.Commands;
using Conzo.Utilities;

namespace Conzo
{
   public partial class ConsoleApplication : IConsoleApplication
   {
      private readonly ICommandManager _commandManager;
      private readonly ICommandConfigurationManager _commandConfigurationManager;
      private bool _running;

      ///// <summary>
      ///// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      ///// </summary>
      ///// <param name="settings">The settings.</param>
      ///// <param name="commandConfigurationManager">The command configuration manager.</param>
      //internal ConsoleApplication(Settings settings, ICommandConfigurationManager commandConfigurationManager)
      //   : this(commandConfigurationManager, new CommandManager(settings, commandConfigurationManager))
      //{
      //}

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      /// </summary>
      /// <param name="commandConfigurationManager">The command configuration manager.</param>
      /// <param name="commandManager">The command manager.</param>
      internal ConsoleApplication(ICommandConfigurationManager commandConfigurationManager, ICommandManager commandManager)
      {
         _commandConfigurationManager = Enforce.ArgumentNotNull(commandConfigurationManager, "commandConfigurationManager can not be null");
         _commandManager = Enforce.ArgumentNotNull(commandManager, "CommandManager can not be null");
      }

      public void AddGlobalCommand(ConsoleKey key, Command command)
      {
         _commandConfigurationManager.AddGlobalCommand(key, command);
      }

      public CommandConfiguration Configure(Command command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         var commandConfiguration = _commandConfigurationManager.AddCommandIfNecessary(command);
         return commandConfiguration;
      }

      //TODO Rename Run to Start?
      public void Run()
      {
         if (_running)
         {
            throw new Exception("ConsoleApplication can only be running once.");
         }

         _running = true;

         _commandManager.Start();
      }

      public void Stop()
      {
         if (!_running)
         {
            throw new Exception("Can not stop an application that is not running");
         }

         _commandManager.Stop();
      }
   }
}
