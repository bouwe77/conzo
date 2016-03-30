using System;
using Conzo;
using Conzo.Screens;

//TODO Indien er async meuk gebruikt moet worden: NuGet package Nito.AsynxEx toevoegen en dan de AsyncContext gebruiken: https://github.com/StephenCleary/AsyncEx/wiki/AsyncContext

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
         var screen = new Screen(() => "Hello World");
         var screen2 = new Screen(() => "moio");

         var myApp = new ConsoleApplication(screen, "My FUCKING app");

         myApp.Configure(screen)
            .AddCommand(ConsoleKey.D1, screen2);

         myApp.Configure(screen2);

         myApp.Start();
      }


      private static void Example1()
      {
         var controller = new Controller();

         var welcome = new Screen(controller.GetWelcome);
         var screen1 = new Screen(controller.GetScreen1);
         var screen2 = new Screen(controller.GetScreen2);
         var outro = new Screen(controller.GetOutro);

         var myTemplateProvider = new MyTemplateProvider();
         var myApp = new ConsoleApplication(welcome, myTemplateProvider);

         myApp.Configure(welcome)
            .AddCommand(ConsoleKey.D1, screen1)
            .AddCommand(ConsoleKey.Q, outro);

         myApp.Configure(screen1)
            .AddCommand(ConsoleKey.D2, screen2)
            .AddCommand(ConsoleKey.Q, outro);

         myApp.Configure(screen2)
            .AddCommand(ConsoleKey.D1, screen1)
            .AddCommand(ConsoleKey.Q, outro);

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

      public string GetScreen1()
      {
         return "This is screen 1..." + Environment.NewLine + "Press 2 to go to screen 2...";
      }

      public string GetScreen2()
      {
         return "This is screen 2..." + Environment.NewLine + "Press 1 to go to screen 1...";
      }

      public string GetOutro()
      {
         return "Goodbye...";
      }
   }
}
