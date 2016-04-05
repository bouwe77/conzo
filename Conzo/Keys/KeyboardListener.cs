using Conzo.Console;
using Conzo.Utilities;

namespace Conzo.Keys
{
   internal class KeyboardListener : IKeyboardListener
   {
      private bool _listenToKeyPressed;
      private readonly IConsoleReader _consoleWrapper;

      //TODO deze handler in aparte class want nu verwijst de IKeyboardListener interface naar deze class
      public delegate void KeyPressedHandler(KeyPressedEventArgs keyPressedEventArgs);
      public event KeyPressedHandler KeyPressed;

      public KeyboardListener()
         : this(new ConsoleReader())
      {
      }

      internal KeyboardListener(IConsoleReader consoleWrapper)
      {
         _consoleWrapper = Enforce.ArgumentNotNull(consoleWrapper, "ConsoleWrapper can not be null");
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
            var key = _consoleWrapper.ReadFromConsole();
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
