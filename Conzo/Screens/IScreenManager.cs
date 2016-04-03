using System;

namespace Conzo.Screens
{
   internal interface IScreenManager
   {
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      Screen GetNewCurrentScreen(Screen currentScreen, ConsoleKey key);
      void Validate();
      Screen StartScreen { get; set; }
      void AddGlobalCommand(ConsoleKey consoleKey, Screen screen);
   }
}
