using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Commands
{
   [TestClass]
   public class CommandTest
   {
      [TestMethod]
      public void Constructor_Success()
      {
         var command = new Command(() => "Hello World");
         Assert.IsNotNull(command);
         Assert.IsTrue(!string.IsNullOrEmpty(command.Id));
      }
   }
}
