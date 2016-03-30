using System;
using Conzo.Templates;

namespace Example
{
   internal class MyTemplateProvider : ITemplateProvider
   {
      public string GetHeader()
      {
         string header = string.Format(
            "{0}{1}{2}{3}{4}{5}",
            Environment.NewLine,
            "Hello World, this is my EXAMPLE application",
            Environment.NewLine,
            "-------------------------------------------------------",
            Environment.NewLine,
            Environment.NewLine);
         return header;
      }

      public string GetFooter()
      {
         string footer = string.Format(
            "{0}{1}{2}{3}{4}{5}{6}",
            Environment.NewLine,
            Environment.NewLine,
            Environment.NewLine,
            "-------------------------------------------------------",
            Environment.NewLine,
            "Press Q to quit",
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
