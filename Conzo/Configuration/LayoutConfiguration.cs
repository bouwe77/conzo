using System;

namespace Conzo.Configuration
{
   public class LayoutConfiguration
   {
      private ConsoleColor _backgroundColor;
      private ConsoleColor _textColor;

      public ConsoleColor BackgroundColor
      {
         get
         {
            return _backgroundColor;
         }
         set
         {
            _backgroundColor = value;
            BackgroundColorSet = true;
         }
      }

      public ConsoleColor TextColor
      {
         get
         {
            return _textColor;
         }
         set
         {
            _textColor = value;
            TextColorSet = true;
         }
      }

      internal bool BackgroundColorSet { get; private set; }
      internal bool TextColorSet { get; private set; }
   }
}
