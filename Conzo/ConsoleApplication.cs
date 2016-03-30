using System;
using System.Threading;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Screens;
using Conzo.Templates;
using Conzo.Utilities;

namespace Conzo
{
   public class ConsoleApplication
   {
      private const ConsoleKey DefaultQuitKey = ConsoleKey.Q;
      private readonly IConsoleWrapper _consoleWrapper;
      private readonly IKeyboardListener _keyboardListener;
      private readonly IScreenManager _screenManager;
      private readonly ITemplateProvider _templateProvider;

      public ConsoleKey QuitKey { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// Use this constructor if you want to use your own <see cref="ITemplateProvider"/>.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      /// <param name="templateProvider">The template provider.</param>
      public ConsoleApplication(Screen startScreen, ITemplateProvider templateProvider)
         : this(
            startScreen,
            DefaultQuitKey,
            null,
            new ConsoleWrapper(),
            new KeyboardListener(),
            new ScreenManager(),
            templateProvider)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// Use this constructor if you want to use Conzo's default <see cref="ITemplateProvider"/>.
      /// The <paramref name="applicationTitle"/> is displayed in the default template.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      /// <param name="applicationTitle">The application title.</param>
      public ConsoleApplication(Screen startScreen, string applicationTitle)
         : this(
            startScreen,
            DefaultQuitKey,
            applicationTitle,
            new ConsoleWrapper(),
            new KeyboardListener(),
            new ScreenManager(),
            new DefaultTemplateProvider(DefaultQuitKey, applicationTitle))
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ConsoleApplication"/> class.
      /// </summary>
      /// <param name="startScreen">The start screen.</param>
      /// <param name="quitKey">The quit key.</param>
      /// <param name="applicationTitle">The application title.</param>
      /// <param name="consoleManager">The console manager.</param>
      /// <param name="keyboardListener">The keyboard listener.</param>
      /// <param name="screenManager">The screen manager.</param>
      /// <param name="templateProvider">The template provider.</param>
      internal ConsoleApplication(
         Screen startScreen,
         ConsoleKey quitKey,
         string applicationTitle,
         IConsoleWrapper consoleManager,
         IKeyboardListener keyboardListener,
         IScreenManager screenManager,
         ITemplateProvider templateProvider)
      {
         _consoleWrapper = Enforce.ArgumentNotNull(consoleManager, "ConsoleManager can not be null");
         _keyboardListener = Enforce.ArgumentNotNull(keyboardListener, "KeyboardListener can not be null");
         _screenManager = Enforce.ArgumentNotNull(screenManager, "ScreenManager can not be null");

         if (templateProvider != null)
         {
            _templateProvider = templateProvider;
         }
         else
         {
            Enforce.StringNotNullOrEmpty(applicationTitle, "ApplicationTitle can not be null");
            _templateProvider = new DefaultTemplateProvider(quitKey, applicationTitle);
         }

         Configure(startScreen);
         QuitKey = SupportedKeys.Validate(quitKey);
      }

      public ScreenConfiguration Configure(Screen screen)
      {
         Enforce.ArgumentNotNull(screen, "screen can not be null");

         var configuration = _screenManager.AddConfiguration(screen);
         return configuration;
      }

      public Screen StartScreen
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
            // The quit key is pressed, wait a while and then stop the keyboard listener so the program will also stop.
            Thread.Sleep(2000);
            _keyboardListener.Stop();
         }
      }
   }
}
