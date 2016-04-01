using System;

namespace Conzo.Screens
{
   internal interface IScreenManager
   {
      ScreenConfiguration AddOrUpdateScreen(Screen screen);
      Screen GetNextScreen(ConsoleKey key);
      void Validate();
      Screen StartScreen { get; set; }
      Screen CurrentScreen { get; set; }
   }
}
