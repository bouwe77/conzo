using System;

//TODO RazorEngine https://antaris.github.io/RazorEngine/

namespace Conzo.Templates
{
   internal class DefaultTemplateProvider : ITemplateProvider
   {
      public DefaultTemplateProvider(ConsoleKey quitKey, string applicationTitle)
      {
         QuitKey = quitKey;
         ApplicationTitle = applicationTitle;
      }

      private string GetHeader()
      {
         string header = string.Format(
            "{0}{1}{2}{3}{4}{5}",
            Environment.NewLine,
            ApplicationTitle,
            Environment.NewLine,
            "-------------------------------------------------------",
            Environment.NewLine,
            Environment.NewLine);
         return header;
      }

      private string GetFooter()
      {
         string footer = string.Format(
            "{0}{1}{2}{3}{4}{5}{6}{7}{8}",
            Environment.NewLine,
            Environment.NewLine,
            Environment.NewLine,
            "-------------------------------------------------------",
            Environment.NewLine,
            "Press ",
            QuitKey.ToString().ToUpper(),
            " to quit",
            Environment.NewLine);

         return footer;
      }

      public string GetRenderedTemplate(string stuff)
      {
         string renderedTemplate = $"{GetHeader()}{stuff}{GetFooter()}";
         return renderedTemplate;
      }

      private ConsoleKey QuitKey { get; }

      private string ApplicationTitle { get; }
   }
}
