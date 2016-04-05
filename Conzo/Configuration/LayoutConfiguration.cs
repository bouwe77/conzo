using System;

namespace Conzo.Configuration
{
   public class LayoutConfiguration
   {
      public LayoutConfiguration(ConsoleColor backgroundColor, ConsoleColor textColor)
      {
         BackgroundColor = backgroundColor;
         TextColor = textColor;
      }

      public ConsoleColor BackgroundColor { get; private set; }
      public ConsoleColor TextColor { get; private set; }
   }
}
