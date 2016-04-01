using System;
using System.Threading;

namespace TempStuff
{
   internal class Program
   {
      private static int _counter;

      private static void Main(string[] args)
      {
         try
         {
            DoSomething();
         }
         catch (Exception exception)
         {
            Console.WriteLine(exception.Message);
         }
      }

      private static void DoSomething()
      {
         var handler = new LongRunningTaskHandler(DisplaySpinner, 100);
         handler.ExecuteLongRunningTask(Floep);
      }

      private static void Floep()
      {
         Thread.Sleep(400);
         Console.WriteLine("Done!");
      }

      private static void DisplaySpinner()
      {
         var spinnerCharacters = new[] { '|', '/', '-', '\\' };

         char spinnerCharacter = spinnerCharacters[_counter];

         string textToDisplay = "Please wait... " + spinnerCharacter;

         Console.CursorVisible = false;
         Console.SetCursorPosition(2,2);
         Console.WriteLine(textToDisplay);
         Console.WriteLine();
         Console.WriteLine();

         _counter++;
         if (_counter == spinnerCharacters.Length)
         {
            _counter = 0;
         }
      }
   }
}
