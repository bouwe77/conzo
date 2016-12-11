using System;
using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   //class Example1FromReadme
   //{
   //   static void Main()
   //   {
   //      // Create a command that returns the text "Hello World".
   //      var helloWorldCommand = CommandFactory.Create(() => "Hello World");

   //      // Create a new ConzoApplication with the Hello World command as start command.
   //      var myApp = new ConzoApplication(helloWorldCommand);

   //      // Start the application which means the start command will be invoked 
   //      // and the "Hello World" string that is returned will be displayed.
   //      myApp.Start();
   //   }
   //}

   //class Example2FromReadme
   //{
   //   static string DisplayTime()
   //   {
   //      return $"Time: {DateTime.Now.ToString("hh:mm:ss")}";
   //   }

   //   static void Main()
   //   {
   //      var displayTimeCommand = CommandFactory.Create(DisplayTime);
   //      var myApp = new ConzoApplication(displayTimeCommand);
   //      myApp.Start();
   //   }
   //}

   //class Example3FromReadme
   //{
   //   static string DisplayTime()
   //   {
   //      return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
   //   }

   //   static void Main()
   //   {
   //      var displayTimeCommand = CommandFactory.Create(DisplayTime);
   //      var myApp = new ConzoApplication(displayTimeCommand);

   //      // Configure the displayTimeCommand by triggering the same command 
   //      // when the R key is pressed.
   //      myApp.Configure(displayTimeCommand)
   //         .AddNextCommand(ConsoleKey.R, displayTimeCommand);

   //      myApp.Start();
   //   }
   //}

   class Example4FromReadme
   {
      static string DisplayTime()
      {
         return $"Time: {DateTime.Now.ToString("hh:mm:ss")} (Press R to refresh)";
      }

      static void Main()
      {
         var displayTimeCommand = new Command(DisplayTime);
         var myApp = new ConzoApplication(displayTimeCommand);

         myApp.Configure(displayTimeCommand)
            .AddNextCommand(ConsoleKey.R, displayTimeCommand);

         // Change some settings:
         myApp.QuitKey = ConsoleKey.F10;
         myApp.Layout = new LayoutSettings
         {
            BackgroundColor = ConsoleColor.DarkBlue,
            TextColor = ConsoleColor.White
         };

         myApp.Start();
      }
   }
}
