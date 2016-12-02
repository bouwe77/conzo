namespace Conzo.Configuration
{
   internal class SettingsManager
   {
      public static void SetDefaults()//Func<ITemplateProvider> templateProviderFactoryMethod)
      {
         Settings.QuitKey = Defaults.QuitKey;

         Settings.QuitDelay = Defaults.QuitDelay;

         Settings.Layout = new LayoutSettings
         {
            BackgroundColor = Defaults.BackgroundColor,
            TextColor = Defaults.TextColor
         };

         Settings.Layout.BackgroundColor = Defaults.BackgroundColor;

         Settings.Layout.TextColor = Defaults.TextColor;
      }
   }
}

