using System;
using Conzo.Screens;
using Conzo.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conzo
{
   [TestClass]
   public class ConsoleApplicationTest
   {
      private string _validApplicationTitle;
      private Screen _validScreen;
      private Mock<ITemplateProvider> _templateProviderMock;

      [TestInitialize]
      public void TestInitialize()
      {
         _validApplicationTitle = "My Application";
         _validScreen = new Screen(() => "Hello World");
         _templateProviderMock = new Mock<ITemplateProvider>();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor1_ThrowsArgumentException_WhenStartScreenIsNull()
      {
         var consoleApplication = new ConsoleApplication(null, _validApplicationTitle);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor1_ThrowsArgumentException_WhenApplicationTitleIsNull()
      {
         string emptyApplicationTitle = null;
         var consoleApplication = new ConsoleApplication(_validScreen, emptyApplicationTitle);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor2_ThrowsArgumentException_WhenStartScreenIsNull()
      {
         var consoleApplication = new ConsoleApplication(null, _templateProviderMock.Object);
      }

      [TestMethod]
      public void Constructor2_DoesNotThrowException_WhenTemplateProviderIsNull()
      {
         ITemplateProvider emptyTemplateProvider = null;
         var consoleApplication = new ConsoleApplication(_validScreen, emptyTemplateProvider);
      }
   }
}
