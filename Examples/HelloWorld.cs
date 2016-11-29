using Conzo;
using Conzo.Commands;
using Conzo.Configuration;

namespace Example
{
   public class HelloWorld
   {
      public static void Run()
      {
         var startCommand = CommandFactory.Create(() => "Hello World");
         var settings = new Settings(startCommand);
         var myApp = ConsoleApplication.Create(settings);
         myApp.Run();
      }
   }
}
