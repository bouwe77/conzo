using System;

namespace Conzo.Templates
{
   public interface ITemplateProvider
   {
      string GetRenderedTemplate(string stuff);
      //ConsoleKey QuitKey { get; set; }
      //string ApplicationTitle { get; set; }
   }
}
