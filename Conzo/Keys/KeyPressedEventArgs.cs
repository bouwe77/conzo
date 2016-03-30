using System;

namespace Conzo.Keys
{
   internal class KeyPressedEventArgs : EventArgs
   {
      public ConsoleKey Key { get; private set; }

      public KeyPressedEventArgs(ConsoleKey key)
      {
         Key = key;
      }
   }
}
