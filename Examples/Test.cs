using System;
using System.Collections.Generic;
using Conzo;
using Conzo.Commands;
using HandlebarsDotNet;

namespace Example
{
   public class Test
   {
      public static void Start()
      {
         var startCommand = new Command(DoSomething);

         var myApp = new ConzoApplication(startCommand);

         myApp.Start();
      }

      private static string DoSomething()
      {
         string source = @"{{title}}
-------------
{{#names}}
{{name}}
{{/names}}";

         var template = Handlebars.Compile(source);

         var data = new
         {
            title = "Ja moio",
            names = new[]
            {
               new { name = "Kees" },
               new { name = "Miep" }
            }
         };

         var result = template(data);

         return result;
      }
   }


   public class Customer
   {
      public string Name { get; set; }
   }
}
