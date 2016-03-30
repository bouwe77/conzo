using System;

namespace Conzo
{
   internal interface IConsoleWrapper
   {
      void Initialize();
      ConsoleKey ReadFromConsole();
      void WriteToConsole(string stuffToWrite);
   }
}
