using System;
using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   class Program
   {
      static void Main()
      {
         try
         {
            Example2();
         //Example1();
         }
         catch (Exception exception)
         {
            Console.WriteLine(exception.Message);
         }
      }

      private static void Example2()
      {
         var command = new Command(() => "Hello World, press 1 to continue...");
         
         var config = new ConsoleApplicationConfiguration(command)
         {
            Layout = new LayoutConfiguration
            {
               BackgroundColor = ConsoleColor.DarkRed
            }
         //   ApplicationTitle = "HOI",
         //   QuitKey = ConsoleKey.D,
         //   QuitDelay = 2000
         };

         var myApp = ConsoleApplication.Create(config);

         myApp.Configure(command)
            .AddNextCommand(ConsoleKey.D1, new Command(Hoppekeej));

         myApp.AddGlobalCommand(ConsoleKey.A, new Command(() => "Ja deze"));

         myApp.Start();
      }

      private static string Hoppekeej()
      {
         return "moio " + DateTime.Now;
      }

      private static void Example1()
      {
         var controller = new Controller();

         var welcome = new Command(controller.GetWelcome);
         var command1 = new Command(controller.GetCommand1);
         var command2 = new Command(controller.GetCommand2);
         var outro = new Command(controller.GetOutro);

         var config = new ConsoleApplicationConfiguration(welcome);
         var myApp = ConsoleApplication.Create(config);

         //TODO Onderstaande QuitKey met outro vervangen door GlobalCommand zodra dit werkt.

         myApp.Configure(welcome)
            .AddNextCommand(ConsoleKey.D1, command1)
            .AddNextCommand(config.QuitKey, outro);

         myApp.Configure(command1)
            .AddNextCommand(ConsoleKey.D2, command2)
            .AddNextCommand(config.QuitKey, outro);

         myApp.Configure(command2)
            .AddNextCommand(ConsoleKey.D1, command1)
            .AddNextCommand(config.QuitKey, outro);

         myApp.Configure(outro);

         myApp.Start();
      }
   }

   class Controller
   {
      public string GetWelcome()
      {
         return "Welcome!" + Environment.NewLine + "Press 1 to continue...";
      }

      public string GetCommand1()
      {
         return "This is command 1..." + Environment.NewLine + "Press 2 to go to command 2...";
      }

      public string GetCommand2()
      {
         return "This is command 2..." + Environment.NewLine + "Press 1 to go to command 1...";
      }

      public string GetOutro()
      {
         return "Goodbye...";
      }
   }
}
