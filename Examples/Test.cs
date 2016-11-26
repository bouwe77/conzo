using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   public class Test
   {
      public static void Run()
      {
         var startCommand = CommandFactory.Create(DoSomething);

         var settings = new Settings(startCommand);

         var myApp = ConsoleApplication.Create(settings);

         myApp.Configure(startCommand)
            .AddNextCommandIf(ConsoleKey.A, startCommand, () => false);

         // Run the application which means the startAction will be invoked and the string value will be displayed.
         myApp.Run();
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
