using System;

namespace Example
{
   internal class Program
   {
      private static void Main2()
      {
         try
         {
            // Choose which demo to run, you can start one demo at a time.
            //HelloWorld.Start();
            //Test.Start();
            Quiz.Start();
         }
         catch (Exception exception)
         {
            Console.WriteLine(exception.Message);
         }
      }
   }
}
