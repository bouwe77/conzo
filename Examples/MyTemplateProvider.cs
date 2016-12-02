//using System;
//using Conzo.Templates;

//namespace Example
//{
//   internal class MyTemplateProvider : ITemplateProvider
//   {
//      private ConsoleKey _quitKey;
//      private string _applicationTitle;

//      private string GetHeader()
//      {
//         string header = $"{Environment.NewLine}{_applicationTitle}{Environment.NewLine}{"-------------------------------------------------------"}{Environment.NewLine}{Environment.NewLine}";
//         return header;
//      }

//      private string GetFooter()
//      {
//         string footer =
//            $"{Environment.NewLine}{Environment.NewLine}{Environment.NewLine}{"-------------------------------------------------------"}{Environment.NewLine}{"Press " + _quitKey + " to quit"}{Environment.NewLine}";

//         return footer;
//      }

//      public string GetRenderedTemplate(string stuff)
//      {
//         string renderedTemplate = $"{GetHeader()}{stuff}{GetFooter()}";
//         return renderedTemplate;
//      }

//      public MyTemplateProvider(ConsoleKey quitKey, string applicationTitle)
//      {
//         _quitKey = quitKey;
//         _applicationTitle = applicationTitle;
//      }
//   }
//}
