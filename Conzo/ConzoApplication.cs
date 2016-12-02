using System;
using Conzo.Commands;
using Conzo.Configuration;
using Conzo.Utilities;

//TODO Ensure only one ConzoApplication is instantiated (add internal singleton class that manages this?)
//TODO RazorEngine https://antaris.github.io/RazorEngine/

namespace Conzo
{
   public class ConzoApplication : IConzoApplication
   {
      private readonly ICommandManager _commandManager;
      private readonly ICommandConfigurationManager _commandConfigurationManager;
      private static bool _started;

      public ConzoApplication(CommandBase startCommand)
      {
         var internalStartCommand = new InternalCommand(startCommand);
         _commandConfigurationManager = new CommandConfigurationManager(internalStartCommand);
         _commandManager = new CommandManager(internalStartCommand, _commandConfigurationManager);
      }

      public void AddGlobalCommand(ConsoleKey key, CommandBase command)
      {
         _commandConfigurationManager.AddGlobalCommand(key, command);
      }

      /// <summary>
      /// Configures the specified command.
      /// </summary>
      /// <param name="command">The command.</param>
      /// <returns>The <see cref="CommandConfiguration" />.</returns>
      public CommandConfiguration Configure(CommandBase command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         var commandConfiguration = _commandConfigurationManager.AddCommandIfNecessary(command);
         return commandConfiguration;
      }

      public void Start()
      {
         if (_started)
         {
            throw new Exception("ConsoleApplication can only be started once.");
         }

         _started = true;

         _commandManager.Start();
      }

      public void Stop()
      {
         if (!_started)
         {
            throw new Exception("Can not stop an application that is not running");
         }

         _commandManager.Stop();
      }

      public ConsoleKey QuitKey
      {
         get { return Settings.QuitKey; }
         set { Settings.QuitKey = value; }
      }

      public int QuitDelay
      {
         get { return Settings.QuitDelay; }
         set { Settings.QuitDelay = value; }
      }

      public LayoutSettings Layout
      {
         get { return Settings.Layout; }
         set { Settings.Layout = value; }
      }
   }
}
