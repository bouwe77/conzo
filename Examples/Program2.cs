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
   class Program2
   {
      static string DisplayTime()
      {
         return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
      }

      static void Main()
      {
         var displayTimeCommand = CommandFactory.Create(DisplayTime);

         // Change the application settings. 
         var settings = new Settings(displayTimeCommand)
         {
            ApplicationTitle = "Hello World",
            QuitKey = ConsoleKey.F10,
            Layout = new LayoutSettings
            {
               BackgroundColor = ConsoleColor.White,
               TextColor = ConsoleColor.DarkBlue
            }
         };

         var myApp = ConsoleApplication.Create(settings);

         myApp.Configure(displayTimeCommand)
            .AddNextCommand(ConsoleKey.R, displayTimeCommand);

         myApp.Run();
      }
   }
}
