using System;
using System.CodeDom;
using Conzo.Commands;
using Conzo.Configuration;
using Conzo.Templates;
using Conzo.Utilities;

namespace Conzo
{
   public partial class ConsoleApplication
   {
      private static bool _created;

      internal static IConsoleApplication Create(Settings settings, Func<IConsoleApplication> consoleApplicationFactoryMethod, Func<ITemplateProvider> templateProviderFactoryMethod)
      {
         Enforce.ArgumentNotNull(settings, "Settings can not be null");

         if (_created)
         {
            throw new Exception("ConsoleApplication can only be created once.");
         }

         SetDefaults(settings, templateProviderFactoryMethod);

         var consoleApplication = consoleApplicationFactoryMethod.Invoke();
         _created = true;

         return consoleApplication;
      }

      public static IConsoleApplication Create(Settings settings)
      {
         Func<ITemplateProvider> templateProviderFactoryMethod = () => new DefaultTemplateProvider(settings.QuitKey, settings.ApplicationTitle);
         SetDefaults(settings, templateProviderFactoryMethod);

         var commandConfigurationManager = new CommandConfigurationManager(settings);
         var commandManager = new CommandManager(settings, commandConfigurationManager);
         return Create(settings, () => new ConsoleApplication(commandConfigurationManager, commandManager), templateProviderFactoryMethod);
      }

      /// <summary>
      /// For unit tests only: Resets the factory to enable creating multiple <see cref="ConsoleApplication"/> instances in different tests.
      /// </summary>
      internal static void Reset()
      {
         _created = false;
      }

      private static void SetDefaults(Settings settings, Func<ITemplateProvider> templateProviderFactoryMethod)
      {
         if (string.IsNullOrEmpty(settings.ApplicationTitle))
         {
            settings.ApplicationTitle = Defaults.ApplicationTitle;
         }

         if (!settings.QuitKeySet)
         {
            settings.QuitKey = Defaults.QuitKey;
         }

         if (!settings.QuitDelaySet)
         {
            settings.QuitDelay = Defaults.QuitDelay;
         }

         if (settings.TemplateProvider == null)
         {
            settings.TemplateProvider = templateProviderFactoryMethod.Invoke();
         }

         if (settings.Layout == null)
         {
            settings.Layout = new LayoutSettings
            {
               BackgroundColor = Defaults.BackgroundColor,
               TextColor = Defaults.TextColor
            };
         }
         else
         {
            if (!settings.Layout.BackgroundColorSet)
            {
               settings.Layout.BackgroundColor = Defaults.BackgroundColor;
            }

            if (!settings.Layout.TextColorSet)
            {
               settings.Layout.TextColor = Defaults.TextColor;
            }
         }
      }
   }
}
