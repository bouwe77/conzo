using System;
using System.Threading;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Screens;
using Conzo.Templates;
using Conzo.Utilities;

namespace Conzo
{
   public class ConsoleApplication : IConsoleApplication
   {
      private readonly IConsoleWrapper _consoleWrapper;
      private readonly IKeyboardListener _keyboardListener;
      private readonly IScreenManager _screenManager;
      private readonly ITemplateProvider _templateProvider;
      private string _applicationTitle;
      private ConsoleKey _quitKey;
      private int _quitDelay;

      /// <summary>
      /// Gets or sets the application title which is displayed by the <see cref="ITemplateProvider"/>.
      /// </summary>
      public string ApplicationTitle
      {
         get
         {
            return _applicationTitle;
         }
         set
         {
            _applicationTitle = Enforce.StringNotNullOrEmpty(value, "ApplicationTitle can not be empty");

            if (_templateProvider != null)
            {
               _templateProvider.ApplicationTitle = value;
            }
         }
      }

      /// <summary>
      /// Gets or sets the key that makes the application quit.
      /// </summary>
      public ConsoleKey QuitKey
      {
         get
         {
            return _quitKey;
         }
         set
         {
            SupportedKeys.Validate(value);
            _quitKey = value;

            if (_templateProvider != null)
            {
               _templateProvider.QuitKey = _quitKey;
            }
         }
      }

      /// <summary>
      /// Gets or sets the quit delay in milliseconds.
      /// Use this if you want to display a screen when hitting the <see cref="QuitKey"/>, because then you need a delay so the user will at least see the screen.
      /// </summary>
      public int QuitDelay
      {
         get
         {
            return _quitDelay;
         }
         set
         {
            _quitDelay = Enforce.Condition(value, value >= 0, "QuitDelay must 0 or greater");
         }
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// Use this constructor if you want to use your own <see cref="ITemplateProvider"/>.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      /// <param name="templateProvider">The template provider.</param>
      public ConsoleApplication(Screen startScreen, ITemplateProvider templateProvider)
         : this(
            startScreen,
            new ConsoleWrapper(),
            new KeyboardListener(),
            new ScreenManager(),
            templateProvider)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// Use this constructor if you want to use Conzo's default <see cref="ITemplateProvider"/>.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      public ConsoleApplication(Screen startScreen)
         : this(
            startScreen,
            new ConsoleWrapper(),
            new KeyboardListener(),
            new ScreenManager(),
            new DefaultTemplateProvider())
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      /// <param name="consoleManager">The console manager.</param>
      /// <param name="keyboardListener">The keyboard listener.</param>
      /// <param name="screenManager">The screen manager.</param>
      /// <param name="templateProvider">The template provider.</param>
      internal ConsoleApplication(
         Screen startScreen,
         IConsoleWrapper consoleManager,
         IKeyboardListener keyboardListener,
         IScreenManager screenManager,
         ITemplateProvider templateProvider)
      {
         QuitKey = Defaults.QuitKey;
         ApplicationTitle = Defaults.ApplicationTitle;
         QuitDelay = Defaults.QuitDelay;

         _consoleWrapper = Enforce.ArgumentNotNull(consoleManager, "ConsoleManager can not be null");
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
         _screenManager = Enforce.ArgumentNotNull(screenManager, "ScreenManager can not be null");

         _templateProvider = templateProvider ?? new DefaultTemplateProvider();
         _templateProvider.ApplicationTitle = _applicationTitle;
         _templateProvider.QuitKey = _quitKey;

         AddOrUpdateScreen(startScreen);
      }

      //TODO Allow adding commands that apply to all screens. Solution: introduce a general list of configurations that apply to all screens, whether they are created before or after configuring it.

      public ScreenConfiguration AddOrUpdateScreen(Screen screen)
      {
         Enforce.ArgumentNotNull(screen, "screen can not be null");

         var configuration = _screenManager.AddOrUpdateScreen(screen);
         return configuration;
      }

      internal Screen StartScreen
      {
         get { return _screenManager.StartScreen; }
         set { _screenManager.StartScreen = value; }
      }

      public void Start()
      {
         _screenManager.Validate();

         _consoleWrapper.Initialize();

         _keyboardListener.KeyPressed += OnKeyPressed;

         ShowScreen();

         _keyboardListener.Start();
      }

      private void ShowScreen()
      {
         var screen = _screenManager.CurrentScreen;
         string currentScreenContents = screen.GetScreenContents.Invoke();
         string renderedTemplate = _templateProvider.GetRenderedTemplate(currentScreenContents);
         _consoleWrapper.WriteToConsole(renderedTemplate);
      }

      private void OnKeyPressed(KeyPressedEventArgs keyPressedEventArgs)
      {
         ConsoleKey key = keyPressedEventArgs.Key;

         try
         {
            var nextScreen = _screenManager.GetNextScreen(key);
            if (nextScreen != null)
            {
               _screenManager.CurrentScreen = nextScreen;
            }
         }
         catch (Exception exception)
         {
            // If we end up here an unexpected exception occurred and the application crashed.
            //TODO Een Screen met error tonen of zo?
            throw;
         }

         ShowScreen();

         if (key == QuitKey)
         {
            // The quit key is pressed, after displaying the screen, wait a while and then stop the keyboard listener which will result in the program stops.
            Thread.Sleep(QuitDelay);
            _keyboardListener.Stop();
         }
      }
   }
}
