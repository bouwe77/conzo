using System;
using Conzo.Configuration;
using Conzo.Templates;

namespace Conzo
{
   public partial class ConsoleApplication
   {
      private static bool _created;

      internal static IConsoleApplication Create(ConsoleApplicationConfiguration configuration, Func<IConsoleApplication> consoleApplicationFactoryMethod, Func<ITemplateProvider> templateProviderFactoryMethod)
      {
         if (_created)
         {
            throw new Exception("ConsoleApplication can only be created once.");
         }

         SetDefaults(configuration, templateProviderFactoryMethod);

         var consoleApplication = consoleApplicationFactoryMethod.Invoke();
         _created = true;

         return consoleApplication;
      }

      public static IConsoleApplication Create(ConsoleApplicationConfiguration configuration)
      {
         return Create(configuration, () => new ConsoleApplication(configuration), () => new DefaultTemplateProvider(configuration.QuitKey, configuration.ApplicationTitle));
      }

      private static void SetDefaults(ConsoleApplicationConfiguration configuration, Func<ITemplateProvider> templateProviderFactoryMethod)
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
            configuration.TemplateProvider = templateProviderFactoryMethod.Invoke();
         }

         if (configuration.Layout == null)
         {
            configuration.Layout = new LayoutConfiguration
            {
               BackgroundColor = Defaults.BackgroundColor,
               TextColor = Defaults.TextColor
            };
         }
         else
         {
            if (!configuration.Layout.BackgroundColorSet)
            {
               configuration.Layout.BackgroundColor = Defaults.BackgroundColor;
            }

            if (!configuration.Layout.TextColorSet)
            {
               configuration.Layout.TextColor = Defaults.TextColor;
            }
         }
      }
   }
}
