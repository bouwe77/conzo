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
         var handler = new LongRunningTaskHandler(Bla, 100);
         handler.ExecuteLongRunningTask(Floep);
      }

      private static void Floep()
      {
         Thread.Sleep(5000);
      }

      private static void Bla()
      {
         var spinnerCharacters = new[] { '|', '/', '-', '\\' };

         char c = spinnerCharacters[_counter];

         Console.CursorVisible = false;
         Console.SetCursorPosition(2,2);
         Console.Write(c);

         _counter++;
         if (_counter == spinnerCharacters.Length)
         {
            _counter = 0;
         }
      }
   }
}
