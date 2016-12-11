using System;
using System.Text;
using Conzo;
using Conzo.Commands;

namespace Example
{
   public class Test
   {
      public static void Run()
      {
         var startCommand = new Command(DoSomething);

         var myApp = new ConzoApplication(startCommand);

         myApp.Configure(startCommand)
            .AddNextCommandIf(ConsoleKey.A, startCommand, () => false);

         // Run the application which means the startAction will be invoked and the string value will be displayed.
         myApp.Start();
      }

      private static string DoSomething()
      {
         var stringBuilder = new StringBuilder();

         for (int i = 0; i < 10; i++)
         {
            for (int j = 0; j < 6; j++)
            {
               stringBuilder.Append("a");
            }

            stringBuilder.AppendLine();
         }

         stringBuilder.Append(DateTime.Now);

         return stringBuilder.ToString();
      }
   }
}
