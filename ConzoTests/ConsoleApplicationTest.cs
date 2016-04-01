using Conzo.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conzo
{
   [TestClass]
   public class ConsoleApplicationTest
   {
      private Mock<ITemplateProvider> _templateProviderMock;

      [TestInitialize]
      public void TestInitialize()
      {
         _templateProviderMock = new Mock<ITemplateProvider>();
      }
   }
}
