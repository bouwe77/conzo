using System;
using System.Collections.Generic;
using System.Linq;

namespace Conzo.Keys
{
   internal class SupportedKeys
   {
      public static ConsoleKey Validate(ConsoleKey consoleKey)
      {
         var supportedConsoleKeys = GetAll();
         if (!supportedConsoleKeys.Contains(consoleKey))
         {
            throw new ArgumentException(consoleKey + " is not supported");
         }

         return consoleKey;
      }

      private static IEnumerable<ConsoleKey> GetAll()
      {
         var supportedConsoleKeys = new List<ConsoleKey>
         {
            ConsoleKey.A,
            ConsoleKey.B,
            ConsoleKey.C,
            ConsoleKey.D,
            ConsoleKey.E,
            ConsoleKey.F,
            ConsoleKey.G,
            ConsoleKey.H,
            ConsoleKey.I,
            ConsoleKey.J,
            ConsoleKey.K,
            ConsoleKey.L,
            ConsoleKey.M,
            ConsoleKey.N,
            ConsoleKey.O,
            ConsoleKey.P,
            ConsoleKey.Q,
            ConsoleKey.R,
            ConsoleKey.S,
            ConsoleKey.T,
            ConsoleKey.U,
            ConsoleKey.V,
            ConsoleKey.W,
            ConsoleKey.X,
            ConsoleKey.Y,
            ConsoleKey.Z,
            ConsoleKey.D0,
            ConsoleKey.D1,
            ConsoleKey.D2,
            ConsoleKey.D3,
            ConsoleKey.D4,
            ConsoleKey.D5,
            ConsoleKey.D6,
            ConsoleKey.D7,
            ConsoleKey.D8,
            ConsoleKey.D9,
            ConsoleKey.Escape,
            ConsoleKey.Tab,
            ConsoleKey.Enter,
            ConsoleKey.Spacebar,
            ConsoleKey.Backspace,
            ConsoleKey.Home,
            ConsoleKey.Insert,
            ConsoleKey.PageUp,
            ConsoleKey.Delete,
            ConsoleKey.End,
            ConsoleKey.PageDown,
            ConsoleKey.RightArrow,
            ConsoleKey.LeftArrow,
            ConsoleKey.UpArrow,
            ConsoleKey.DownArrow,
            ConsoleKey.Add,
            ConsoleKey.Subtract,
            ConsoleKey.Multiply,
            ConsoleKey.Divide,
            ConsoleKey.F1,
            ConsoleKey.F2,
            ConsoleKey.F3,
            ConsoleKey.F4,
            ConsoleKey.F5,
            ConsoleKey.F6,
            ConsoleKey.F7,
            ConsoleKey.F8,
            ConsoleKey.F9,
            ConsoleKey.F10,
            ConsoleKey.F11,
            ConsoleKey.F12,
            ConsoleKey.NumPad0,
            ConsoleKey.NumPad1,
            ConsoleKey.NumPad2,
            ConsoleKey.NumPad3,
            ConsoleKey.NumPad4,
            ConsoleKey.NumPad5,
            ConsoleKey.NumPad6,
            ConsoleKey.NumPad7,
            ConsoleKey.NumPad8,
            ConsoleKey.NumPad9
         };

         return supportedConsoleKeys;
      }
   }
}
