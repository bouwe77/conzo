using System;
using Conzo.Templates;

namespace Conzo
{
   public partial class ConsoleApplication
   {
      private static bool _created;

      public static IConsoleApplication Create(ConsoleApplicationConfiguration configuration)
      {
         if (_created)
         {
            throw new Exception("ConsoleApplication can only be created once.");
         }

         SetDefaults(configuration);

         var consoleApplication = new ConsoleApplication(configuration);
         _created = true;

         return consoleApplication;
      }

      private static void SetDefaults(ConsoleApplicationConfiguration configuration)
      {
         if (string.IsNullOrEmpty(configuration.ApplicationTitle))
         {
            configuration.ApplicationTitle = Defaults.ApplicationTitle;
         }

         if (!configuration.QuitKeySet)
         {
            configuration.QuitKey = Defaults.QuitKey;
         }

         if (!configuration.QuitDelaySet)
         {
            configuration.QuitDelay = Defaults.QuitDelay;
         }

         if (configuration.TemplateProvider == null)
         {
            configuration.TemplateProvider = new DefaultTemplateProvider(configuration.QuitKey, configuration.ApplicationTitle);
         }
      }
   }
}
