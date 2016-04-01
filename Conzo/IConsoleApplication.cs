using System;
using Conzo.Screens;

namespace Conzo
{
   public interface IConsoleApplication
   {
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      void Start();
      ConsoleKey QuitKey { get; set; }
      string ApplicationTitle { get; set; }
      int QuitDelay { get; set; }
   }
}
