using System;

namespace Example
{
   internal class Program
   {
      private static void Main()
      {
         try
         {
            // Choose which demo to run, you can start one demo at a time.
            //HelloWorld.Run();
            //Test.Run();
            Quiz.Run();
         }
         catch (Exception exception)
         {
            Console.WriteLine(exception.Message);
         }
      }
   }
}
