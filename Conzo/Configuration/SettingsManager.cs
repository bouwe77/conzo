namespace Conzo.Configuration
{
   internal class SettingsManager
   {
      public static void SetDefaults()
      {
         Settings.QuitKey = Defaults.QuitKey;

         Settings.QuitDelay = Defaults.QuitDelay;

         Settings.Layout = new LayoutSettings
         {
            BackgroundColor = Defaults.BackgroundColor,
            TextColor = Defaults.TextColor
         };
      }
   }
}

