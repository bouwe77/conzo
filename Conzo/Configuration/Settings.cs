using System;
using Conzo.Keys;
using Conzo.Commands;
using Conzo.Templates;
using Conzo.Utilities;

namespace Conzo.Configuration
{
   public class Settings
   {
      private string _applicationTitle;
      private ConsoleKey _quitKey;
      private int _quitDelay;

      /// <summary>
      /// Gets or sets the application title which is displayed by the <see cref="ITemplateProvider"/>.
      /// </summary>
      public string ApplicationTitle
      {
         get { return _applicationTitle; }
         set { _applicationTitle = Enforce.StringNotNullOrEmpty(value, "ApplicationTitle can not be empty"); }
      }

      /// <summary>
      /// Gets or sets the key that makes the application quit.
      /// </summary>
      public ConsoleKey QuitKey
      {
         get { return _quitKey; }
         set
         {
            SupportedKeys.Validate(value);
            _quitKey = value;
            QuitKeySet = true;
         }
      }

      /// <summary>
      /// Gets or sets the quit delay in milliseconds.
      /// Use this if you want to display a command when hitting the <see cref="QuitKey"/>, because then you need a delay so the user will at least see the command.
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

      public LayoutSettings Layout { internal get; set; }

      internal Command StartCommand { get; private set; }

      public Settings(Command startCommand)
      {
         StartCommand = Enforce.ArgumentNotNull(startCommand, "startCommand can not be null");
      }
   }
}
