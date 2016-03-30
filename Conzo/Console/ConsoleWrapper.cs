using System;
using Conzo.Utilities;

namespace Conzo.Console
{
   internal class ConsoleWrapper : IConsoleWrapper
   {
      public void Initialize()
      {
         // CTRL+C will not quit the program but is just an ordinary key combination.
         System.Console.TreatControlCAsInput = true;
         SetWindowsSize();
         SetCursor();
         SetBackAndForeGroundColor();
      }

      public ConsoleKey ReadFromConsole()
      {
         return System.Console.ReadKey(true).Key;
      }

      public void WriteToConsole(string textToWrite)
      {
         Enforce.ArgumentNotNull(textToWrite, "textToWrite can not be null");

         System.Console.Clear();

         bool doLayoutStuff = textToWrite.Contains("$");
         if (!doLayoutStuff)
         {
            System.Console.WriteLine(textToWrite);
         }
         else
         {
            WriteLinesWithLayout(textToWrite);
         }
      }

      private void WriteLinesWithLayout(string textToWrite)
      {
         Enforce.ArgumentNotNull(textToWrite, "textToWrite can not be null");

         ConsoleColor defaultBackgroundColor = System.Console.BackgroundColor;
         ConsoleColor defaultForegroundColor = System.Console.ForegroundColor;

         var lines = textToWrite.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
         foreach (var line in lines)
         {
            string lineToWrite = line;

            // Display cursor, if necessary:
            if (line.Contains("$CURSOR$"))
            {
               var cursorSegments = line.Split(new[] { "$CURSOR$" }, StringSplitOptions.None);
               for (int i = 0; i < cursorSegments.Length; i++)
               {
                  bool secondLastItem = i == cursorSegments.Length - 2;
                  if (secondLastItem)
                  {
                     System.Console.BackgroundColor = ConsoleColor.White;
                     System.Console.ForegroundColor = ConsoleColor.Black;
                  }
                  else
                  {
                     System.Console.BackgroundColor = defaultBackgroundColor;
                     System.Console.ForegroundColor = defaultForegroundColor;
                  }

                  bool lastItem = i == cursorSegments.Length - 1;
                  if (lastItem)
                  {
                     System.Console.WriteLine(cursorSegments[i]);
                  }
                  else
                  {
                     System.Console.Write(cursorSegments[i]);
                  }
               }
               // TODO Display full width background color.
            }
            else
            {
               System.Console.WriteLine(lineToWrite);
            }
         }
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
         System.Console.BackgroundColor = ConsoleColor.Black;
         System.Console.ForegroundColor = ConsoleColor.Green;
         System.Console.Clear();
      }
   }
}
