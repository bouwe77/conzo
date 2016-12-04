using System;
using Conzo.Commands;
using Conzo.Configuration;
using Conzo.Exceptions;
using Conzo.Helpers;

//TODO Ensure only one ConzoApplication is instantiated (add internal singleton class that manages this?)
//TODO RazorEngine https://antaris.github.io/RazorEngine/

namespace Conzo
{
   /// <summary>
   /// Allows creating and configuring Conzo applications.
   /// </summary>
   /// <seealso cref="IConzoApplication" />
   public class ConzoApplication : IConzoApplication
   {
      private readonly ICommandManager _commandManager;
      private static bool _started;

      /// <summary>
      /// Initializes a new instance of the <see cref="ConzoApplication"/> class.
      /// </summary>
      /// <param name="startCommand">The start command.</param>
      public ConzoApplication(CommandBase startCommand)
         : this(startCommand, new CommandManager(startCommand))
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConzoApplication"/> class.
      /// </summary>
      /// <param name="startCommand">The start command.</param>
      /// <param name="commandManager">The command manager.</param>
      internal ConzoApplication(CommandBase startCommand, ICommandManager commandManager)
      {
         _started = false;
         CommandRepository.SetStartCommand(startCommand);
         _commandManager = commandManager;
      }

      /// <summary>
      /// Adds a global command.
      /// </summary>
      /// <param name="key">The key.</param>
      /// <param name="command">The command.</param>
      public void AddGlobalCommand(ConsoleKey key, CommandBase command)
      {
         CommandRepository.AddGlobalCommand(key, command);
      }

      /// <summary>
      /// Configures the specified command.
      /// </summary>
      /// <param name="command">The command.</param>
      /// <returns>The <see cref="CommandConfiguration" />.</returns>
      public CommandConfiguration Configure(CommandBase command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         var commandConfiguration = CommandRepository.AddCommandIfNecessary(command);
         return commandConfiguration;
      }

      /// <summary>
      /// Starts the Conzo application.
      /// </summary>
      /// <exception cref="ConzoException">Thrown when application is started more than once.</exception>
      public void Start()
      {
         if (_started)
         {
            throw new ConzoException("ConsoleApplication can only be started once.");
         }

         _started = true;

         _commandManager.Start();
      }

      /// <summary>
      /// Stops the Conzo application.
      /// </summary>
      /// <exception cref="ConzoException">Thrown when the application was not started yet.</exception>
      public void Stop()
      {
         if (!_started)
         {
            throw new ConzoException("Can not stop an application that has not been started.");
         }

         _commandManager.Stop();
      }

      /// <summary>
      /// Gets or sets the quit key, i.e. the <see cref="ConsoleKey"/> that must be pressed to quit the Conzo application.
      /// </summary>
      public ConsoleKey QuitKey
      {
         get { return Settings.QuitKey; }
         set { Settings.QuitKey = value; }
      }

      /// <summary>
      /// Gets or sets the quit delay, i.e. the time in milliseconds the aplication is stopped after the <see cref="QuitKey"/> is pressed.
      /// </summary>
      public int QuitDelay
      {
         get { return Settings.QuitDelay; }
         set { Settings.QuitDelay = value; }
      }

      /// <summary>
      /// Gets or sets the layout.
      /// </summary>
      /// <value>The layout.</value>
      public LayoutSettings Layout
      {
         get { return Settings.Layout; }
         set { Settings.Layout = value; }
      }
   }
}
