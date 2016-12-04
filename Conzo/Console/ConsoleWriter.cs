using System;
using Conzo.Configuration;
using Conzo.Helpers;

namespace Conzo.Console
{
   internal class ConsoleWriter : IConsoleWriter
   {
      public void Initialize()
      {
         // CTRL+C will not quit the program but is just an ordinary key combination.
         System.Console.TreatControlCAsInput = true;
         SetWindowsSize();
         SetCursor();
         SetBackAndForeGroundColor();
      }

      public void WriteToConsole(string textToWrite)
      {
         Enforce.ArgumentNotNull(textToWrite, "textToWrite can not be null");

         System.Console.Clear();
         System.Console.WriteLine(textToWrite);
      }

      private void SetCursor()
      {
         System.Console.SetCursorPosition(0, 0);
         System.Console.CursorVisible = false;
      }

      private void SetWindowsSize()
      {
         System.Console.SetWindowSize(System.Console.LargestWindowWidth - 80, System.Console.LargestWindowHeight - 20);
      }

      private void SetBackAndForeGroundColor()
      {
         // Set the background for the whole console window by calling Clear() afterwards, but before writing output.
         System.Console.BackgroundColor = Settings.Layout.BackgroundColor;
         System.Console.ForegroundColor = Settings.Layout.TextColor;
         System.Console.Clear();
      }
   }
}
