using System;

//TODO RazorEngine https://antaris.github.io/RazorEngine/

namespace Conzo.Templates
{
   internal class DefaultTemplateProvider : ITemplateProvider
   {
      private readonly ConsoleKey _quitKey;
      private readonly string _applicationTitle;

      public DefaultTemplateProvider(ConsoleKey quitKey, string applicationTitle)
      {
         _quitKey = quitKey;
         _applicationTitle = applicationTitle;
      }

      private string GetHeader()
      {
         string header = string.Format(
            "{0}{1}{2}{3}{4}{5}",
            Environment.NewLine,
            _applicationTitle,
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
            _quitKey.ToString().ToUpper(),
            " to quit",
            Environment.NewLine);

         return footer;
      }

      public string GetRenderedTemplate(string stuff)
      {
         string renderedTemplate = string.Format("{0}{1}{2}", GetHeader(), stuff, GetFooter());
         return renderedTemplate;
      }
   }
}
