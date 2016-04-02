using System;
using System.Threading;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Screens;
using Conzo.Utilities;

namespace Conzo
{
   public partial class ConsoleApplication : IConsoleApplication
   {
      private readonly IConsoleWrapper _consoleWrapper;
      private readonly IKeyboardListener _keyboardListener;
      private readonly IScreenManager _screenManager;
      private readonly ConsoleApplicationConfiguration _configuration;
      private bool _started;
      private Screen _currentScreen;
      private string _currentScreenContents;

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      /// </summary>
      /// <param name="configuration">The configuration.</param>
      internal ConsoleApplication(ConsoleApplicationConfiguration configuration)
         : this(
            configuration,
            new ConsoleWrapper(),
            new KeyboardListener(),
            new ScreenManager())
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication" /> class.
      /// </summary>
      /// <param name="configuration">The configuration.</param>
      /// <param name="consoleManager">The console manager.</param>
      /// <param name="keyboardListener">The keyboard listener.</param>
      /// <param name="screenManager">The screen manager.</param>
      internal ConsoleApplication(
         ConsoleApplicationConfiguration configuration,
         IConsoleWrapper consoleManager,
         IKeyboardListener keyboardListener,
         IScreenManager screenManager)
      {
         _configuration = Enforce.ArgumentNotNull(configuration, "Configuration can not be null");

         _consoleWrapper = Enforce.ArgumentNotNull(consoleManager, "ConsoleManager can not be null");
         _consoleWrapper.Initialize();
         
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
         _keyboardListener.KeyPressed += OnKeyPressed;

         _screenManager = Enforce.ArgumentNotNull(screenManager, "ScreenManager can not be null");
      }

      //TODO Allow adding commands that apply to all screens. Solution: introduce a general list of configurations that apply to all screens, whether they are created before or after configuring it.

      public ScreenConfiguration AddOrUpdateScreen(Screen screen)
      {
         Enforce.ArgumentNotNull(screen, "screen can not be null");

         var screenConfiguration = _screenManager.AddOrUpdateScreen(screen);
         return screenConfiguration;
      }

      public void Start()
      {
         if (_started)
         {
            throw new Exception("ConsoleApplication can only be started once.");
         }

         _started = true;

         _currentScreen = _configuration.StartScreen;
         RefreshCurrentScreenContents();

         _screenManager.Validate();

         ShowCurrentScreenContents();

         _keyboardListener.Start();
      }

      private void RefreshCurrentScreenContents()
      {
         try
         {
            _currentScreenContents = _currentScreen.GetScreenContents.Invoke();
         }
         catch (Exception exception)
         {
            // If we end up here an unexpected exception occurred and the application crashed.
            //TODO Een Screen met error tonen of zo? En een key command eraan toevoegen. "Press any key to continue..."
            throw;
         }
      }

      public void Stop()
      {
         // Stopping listening to keys pressed will stop the program.
         _keyboardListener.Stop();
      }

      private void ShowCurrentScreenContents()
      {
         string renderedTemplate = _configuration.TemplateProvider.GetRenderedTemplate(_currentScreenContents);
         _consoleWrapper.WriteToConsole(renderedTemplate);
      }

      private void OnKeyPressed(KeyPressedEventArgs keyPressedEventArgs)
      {
         ConsoleKey key = keyPressedEventArgs.Key;

         // Only refresh the current screen stuff if another screen must be displayed after pressing this key.
         var newCurrentScreen = _screenManager.GetNewCurrentScreen(_currentScreen, key);
         if (!newCurrentScreen.Equals(_currentScreen))
         {
            _currentScreen = newCurrentScreen;
            RefreshCurrentScreenContents();
         }

         ShowCurrentScreenContents();

         if (key == _configuration.QuitKey)
         {
            // The quit key is pressed, after displaying the screen, wait a while and then stop the application.
            Thread.Sleep(_configuration.QuitDelay);
            Stop();
         }
      }
   }
}
