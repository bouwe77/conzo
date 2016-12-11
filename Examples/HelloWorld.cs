using Conzo;
using Conzo.Commands;

namespace Example
{
   public class HelloWorld
   {
      public static void Start()
      {
         var startCommand = new Command(() => "Hello World");
         var myApp = new ConzoApplication(startCommand);
         myApp.Start();
      }
   }
}
