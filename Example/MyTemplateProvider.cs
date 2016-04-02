using System;
using Conzo.Templates;

namespace Example
{
   internal class MyTemplateProvider : ITemplateProvider
   {
      private ConsoleKey _quitKey;
      private string _applicationTitle;

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
            "{0}{1}{2}{3}{4}{5}{6}",
            Environment.NewLine,
            Environment.NewLine,
            Environment.NewLine,
            "-------------------------------------------------------",
            Environment.NewLine,
            "Press " + _quitKey + " to quit",
            Environment.NewLine);

         return footer;
      }

      public string GetRenderedTemplate(string stuff)
      {
         string renderedTemplate = string.Format("{0}{1}{2}", GetHeader(), stuff, GetFooter());
         return renderedTemplate;
      }

      public MyTemplateProvider(ConsoleKey quitKey, string applicationTitle)
      {
         _quitKey = quitKey;
         _applicationTitle = applicationTitle;
      }
   }
}
