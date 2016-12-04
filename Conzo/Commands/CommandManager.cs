using System;
using System.Threading;
using Conzo.Configuration;
using Conzo.Console;
using Conzo.Helpers;
using Conzo.Keys;

namespace Conzo.Commands
{
   internal class CommandManager : ICommandManager
   {
      private CommandBase _currentCommand;
      private string _currentCommandContents;
      private readonly IConsoleWriter _consoleWriter;
      private readonly IKeyboardListener _keyboardListener;

      public CommandManager(CommandBase startCommand)
         : this(startCommand, new ConsoleWriter(), new KeyboardListener())
      {
      }

      internal CommandManager(CommandBase startCommand, IConsoleWriter consoleWriter, IKeyboardListener keyboardListener)
      {
         _currentCommand = startCommand;
         _consoleWriter = Enforce.ArgumentNotNull(consoleWriter, "consoleWriter can not be null");
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
      }

      public void Start()
      {
         _keyboardListener.KeyPressed += OnKeyPressed;
         _consoleWriter.Initialize();
         CommandRepository.Validate();
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

         // Determine the next command from the current command with this key.
         var newCurrentCommand = CommandRepository.GetNextCommand(_currentCommand, consoleKey);

         // Only execute the command if a new one was found.
         if (newCurrentCommand != null)
         {
            _currentCommand = newCurrentCommand;
            ExecuteCurrentCommand(consoleKey);
         }

         ShowCurrentCommandContents();

         if (consoleKey == Settings.QuitKey)
         {
            // The quit key is pressed. After displaying the command, wait a while and then stop the application.
            Thread.Sleep(Settings.QuitDelay);
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
         //         string renderedTemplate = Settings.TemplateProvider.GetRenderedTemplate(_currentCommandContents);
         string renderedTemplate = _currentCommandContents;
         _consoleWriter.WriteToConsole(renderedTemplate);
      }
   }
}
