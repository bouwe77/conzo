using System;
using System.Threading;
using Conzo.Configuration;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Commands;
using Conzo.Utilities;

namespace Conzo
{
   public partial class ConsoleApplication : IConsoleApplication
   {
      private readonly IConsoleWriter _consoleWriter;
      private readonly IKeyboardListener _keyboardListener;
      private readonly ICommandManager _commandManager;
      private readonly Settings _settings;
      private bool _running;
      private Command _currentCommand;
      private string _currentContents;

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      /// </summary>
      /// <param name="settings">The settings.</param>
      internal ConsoleApplication(Settings settings)
         : this(
            settings,
            new ConsoleWriter(settings.Layout),
            new KeyboardListener(),
            new CommandManager())
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      /// </summary>
      /// <param name="settings">The settings.</param>
      /// <param name="consoleWriter">The console writer.</param>
      /// <param name="keyboardListener">The keyboard listener.</param>
      /// <param name="commandManager">The command manager.</param>
      internal ConsoleApplication(
         Settings settings,
         IConsoleWriter consoleWriter,
         IKeyboardListener keyboardListener,
         ICommandManager commandManager)
      {
         _settings = Enforce.ArgumentNotNull(settings, "Settings can not be null");
         _consoleWriter = Enforce.ArgumentNotNull(consoleWriter, "ConsoleWriter can not be null");
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
         _commandManager = Enforce.ArgumentNotNull(commandManager, "CommandManager can not be null");
      }

      public void AddGlobalCommand(ConsoleKey key, Command command)
      {
         _commandManager.AddGlobalCommand(key, command);
      }

      public CommandConfiguration Configure(Command command)
      {
         Enforce.ArgumentNotNull(command, "command can not be null");

         var commandConfiguration = _commandManager.Configure(command);
         return commandConfiguration;
      }

      public void Run()
      {
         if (_running)
         {
            throw new Exception("ConsoleApplication can only be running once.");
         }

         _running = true;

         _currentCommand = _settings.StartCommand;
         RefreshCurrentCommandContents();

         _keyboardListener.KeyPressed += OnKeyPressed;

         _consoleWriter.Initialize();

         _commandManager.Validate();

         ShowCurrentCommandContents();

         _keyboardListener.Start();
      }

      private void RefreshCurrentCommandContents()
      {
         try
         {
            _currentContents = _currentCommand.Action.Invoke();
         }
         catch (Exception exception)
         {
            // If we end up here an unexpected exception occurred and the application crashed.
            //TODO Een Command met error tonen of zo? En een key command eraan toevoegen. "Press any key to continue..."
            throw;
         }
      }

      public void Stop()
      {
         if (!_running)
         {
            throw new Exception("Can not stop an application that is not running");
         }

         // Stopping listening to keys pressed will stop the program.
         _keyboardListener.Stop();
      }

      private void ShowCurrentCommandContents()
      {
         string renderedTemplate = _settings.TemplateProvider.GetRenderedTemplate(_currentContents);
         _consoleWriter.WriteToConsole(renderedTemplate);
      }

      private void OnKeyPressed(KeyPressedEventArgs keyPressedEventArgs)
      {
         ConsoleKey key = keyPressedEventArgs.Key;

         // Only refresh the current command stuff if another command must be displayed after pressing this key.
         var newCurrentCommand = _commandManager.GetNewCurrentCommand(_currentCommand, key);
         if (!newCurrentCommand.Equals(_currentCommand))
         {
            _currentCommand = newCurrentCommand;
            RefreshCurrentCommandContents();
         }

         ShowCurrentCommandContents();

         if (key == _settings.QuitKey)
         {
            // The quit key is pressed, after displaying the command, wait a while and then stop the application.
            Thread.Sleep(_settings.QuitDelay);
            Stop();
         }
      }
   }
}
