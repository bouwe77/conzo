using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   public class HelloWorld
   {
      public static void Run()
      {
         // The command that must be invoked when the application starts.
         // This is a method that (at least) returns a string but also could do something useful like quering a database.
         var startCommand = new Command(() => "Hello World");

         // We need a Settings object containing at least the startCommand, but you could configure more if you want.
         var settings = new Settings(startCommand)
         {
            // configure more stuff...
         };

         // Create a new ConsoleApplication with the settings.
         var myApp = ConsoleApplication.Create(settings);

         // Run the application which means the startAction will be invoked and the string value will be displayed.
         myApp.Run();
      }
   }
}
