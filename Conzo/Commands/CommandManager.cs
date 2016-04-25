using System;
using System.Threading;
using Conzo.Configuration;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class CommandManager : ICommandManager
   {
      private InternalCommand _currentCommand;
      private string _currentCommandContents;
      private readonly Settings _settings;
      private readonly IConsoleWriter _consoleWriter;
      private readonly ICommandConfigurationManager _commandConfigurationManager;
      private readonly IKeyboardListener _keyboardListener;

      public CommandManager(Settings settings, ICommandConfigurationManager commandConfigurationManager)
         : this(settings, new ConsoleWriter(settings.Layout), commandConfigurationManager, new KeyboardListener())
      {
      }

      internal CommandManager(Settings settings, IConsoleWriter consoleWriter, ICommandConfigurationManager commandConfigurationManager, IKeyboardListener keyboardListener)
      {
         _settings = settings;
         _currentCommand = settings.StartCommand;
         _consoleWriter = Enforce.ArgumentNotNull(consoleWriter, "consoleWriter can not be null");
         _commandConfigurationManager = Enforce.ArgumentNotNull(commandConfigurationManager, "commandConfigurationManager can not be null");
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
      }

      public void Start()
      {
         _keyboardListener.KeyPressed += OnKeyPressed;
         _consoleWriter.Initialize();
         _commandConfigurationManager.Validate();
         ExecuteCurrentCommand();
         ShowCurrentCommandContents();
         _keyboardListener.Start();
      }

      public void Stop()
      {
         // Stopping listening to keys pressed will stop the program.
         _keyboardListener.Stop();
      }

      private void OnKeyPressed(KeyPressedEventArgs keyPressedEventArgs)
      {
         ConsoleKey consoleKey = keyPressedEventArgs.Key;

         // Determine the next command from the current command with this key. If no command found the current command will be used again.
         InternalCommand newCurrentCommand = _commandConfigurationManager.GetNextCommand(_currentCommand, consoleKey) ?? _currentCommand;

         // Only execute the command if it has a condition or if it is not the same as the current command.
         if (newCurrentCommand.Condition != null || !newCurrentCommand.Equals(_currentCommand))
         {
            _currentCommand = newCurrentCommand;
            ExecuteCurrentCommand(consoleKey);
         }

         ShowCurrentCommandContents();

         if (consoleKey == _settings.QuitKey)
         {
            // The quit key is pressed. After displaying the command, wait a while and then stop the application.
            Thread.Sleep(_settings.QuitDelay);
            Stop();
         }
      }

      private void ExecuteCurrentCommand(ConsoleKey consoleKey = default(ConsoleKey))
      {
         try
         {
            string newCommandContents;

            var commandExecuter = new CommandExecuter(_currentCommand, consoleKey);
            bool commandExecuted = commandExecuter.TryExecute(out newCommandContents);

            if (commandExecuted)
            {
               _currentCommandContents = newCommandContents;
            }
         }
         catch (Exception exception)
         {
            // If we end up here an unexpected exception occurred and the application crashed.
            //TODO Een Command met error tonen of zo? En een key command eraan toevoegen. "Press any key to continue..."
            throw;
         }
      }

      private void ShowCurrentCommandContents()
      {
         string renderedTemplate = _settings.TemplateProvider.GetRenderedTemplate(_currentCommandContents);
         _consoleWriter.WriteToConsole(renderedTemplate);
      }
   }
}
