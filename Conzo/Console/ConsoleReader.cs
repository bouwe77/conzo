using System;

namespace Conzo.Console
{
   internal class ConsoleReader : IConsoleReader
   {
      public ConsoleKey ReadFromConsole()
      {
         return System.Console.ReadKey(true).Key;
      }
   }
}
