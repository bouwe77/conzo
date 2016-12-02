using System;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Configuration
{
   internal static class Settings
   {
      private static ConsoleKey _quitKey;
      private static int _quitDelay;
      private static LayoutSettings _layout;

      static Settings()
      {
         SettingsManager.SetDefaults();
      }

      /// <summary>
      /// Gets or sets the key that makes the application quit.
      /// </summary>
      public static ConsoleKey QuitKey
      {
         get { return _quitKey; }
         set
         {
            SupportedKeys.Validate(value);
            _quitKey = value;
         }
      }

      /// <summary>
      /// Gets or sets the quit delay in milliseconds.
      /// Use this if you want to display a command when hitting the <see cref="QuitKey"/>, 
      /// because then you need a delay so the user will at least see the output of the command.
      /// </summary>
      public static int QuitDelay
      {
         get { return _quitDelay; }
         set
         {
            _quitDelay = Enforce.Condition(value, value >= 0, "QuitDelay must 0 or greater");
         }
      }


      public static LayoutSettings Layout
      {
         get { return _layout; }
         set { _layout = Enforce.ArgumentNotNull(value, "Layout can not be null"); }
      }
   }
}
