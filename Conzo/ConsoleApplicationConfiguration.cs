using System;
using System.Collections.Generic;
using Conzo.Keys;
using Conzo.Screens;
using Conzo.Templates;
using Conzo.Utilities;

namespace Conzo
{
   public class ConsoleApplicationConfiguration
   {
      private string _applicationTitle;
      private ConsoleKey _quitKey;
      private int _quitDelay;

      /// <summary>
      /// Gets or sets the application title which is displayed by the <see cref="ITemplateProvider"/>.
      /// </summary>
      public string ApplicationTitle
      {
         internal get { return _applicationTitle; }
         set { _applicationTitle = Enforce.StringNotNullOrEmpty(value, "ApplicationTitle can not be empty"); }
      }

      /// <summary>
      /// Gets or sets the key that makes the application quit.
      /// </summary>
      public ConsoleKey QuitKey
      {
         internal get { return _quitKey; }
         set
         {
            SupportedKeys.Validate(value);
            _quitKey = value;
            QuitKeySet = true;
         }
      }

      /// <summary>
      /// Gets or sets the quit delay in milliseconds.
      /// Use this if you want to display a screen when hitting the <see cref="QuitKey"/>, because then you need a delay so the user will at least see the screen.
      /// </summary>
      public int QuitDelay
      {
         internal get { return _quitDelay; }
         set
         {
            _quitDelay = Enforce.Condition(value, value >= 0, "QuitDelay must 0 or greater");
            QuitDelaySet = true;
         }
      }

      internal bool QuitDelaySet { get; private set; }
      internal bool QuitKeySet { get; private set; }

      public ITemplateProvider TemplateProvider { internal get; set; }

      internal Screen StartScreen { get; private set; }

      public ConsoleApplicationConfiguration(Screen startScreen)
      {
         StartScreen = startScreen;
      }
   }
}
