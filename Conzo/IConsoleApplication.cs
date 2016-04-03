using System;
using Conzo.Screens;

namespace Conzo
{
   public interface IConsoleApplication
   {
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      void AddGlobalCommand(ConsoleKey key, Screen screen);
      void Start();
      void Stop();
   }
}
