using Conzo;
using Conzo.Commands;

namespace Example
{
   public class HelloWorld
   {
      public static void Start()
      {
         var startCommand = CommandFactory.Create(() => "Hello World");
         var myApp = new ConzoApplication(startCommand);
         myApp.Start();
      }
   }
}
