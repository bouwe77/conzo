using System;
using Conzo.Screens;

namespace Conzo
{
   public interface IConsoleApplication
   {
      void Configure(ConsoleKey quitKey = ConsoleApplication.DefaultQuitKey, int quitDelay = 0, string applicationTitle = "");
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      void Start();
   }
}
