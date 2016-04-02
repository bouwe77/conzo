using Conzo.Console;
using Conzo.Utilities;

namespace Conzo.Keys
{
   internal class KeyboardListener : IKeyboardListener
   {
      private bool _listenToKeyPressed;
      private readonly IConsoleWrapper _consoleManager;

      //TODO deze handler in aparte class want nu verwijst de IKeyboardListener interface naar deze class
      public delegate void KeyPressedHandler(KeyPressedEventArgs keyPressedEventArgs);
      public event KeyPressedHandler KeyPressed;

      public KeyboardListener()
         : this(new ConsoleWrapper())
      {
      }

      internal KeyboardListener(IConsoleWrapper consoleManager)
      {
         _consoleManager = Enforce.ArgumentNotNull(consoleManager, "ConsoleManager can not be null");
         _listenToKeyPressed = true;
      }

      protected void OnKeyPressed(KeyPressedEventArgs keyPressedEventArgs)
      {
         if (KeyPressed != null)
         {
            KeyPressed(keyPressedEventArgs);
         }
      }

      public void Start()
      {
         while (_listenToKeyPressed)
         {
            var key = _consoleManager.ReadFromConsole();
            var eventArgs = new KeyPressedEventArgs(key);
            OnKeyPressed(eventArgs);
         }
      }

      public void Stop()
      {
         _listenToKeyPressed = false;
      }
   }
}
