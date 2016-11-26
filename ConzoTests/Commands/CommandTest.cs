using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Commands
{
   [TestClass]
   public class CommandTest
   {
      [TestMethod]
      public void Constructor_Success()
      {
         var command = CommandFactory.Create(() => "Hello World");
         Assert.IsNotNull(command);
         Assert.IsTrue(!string.IsNullOrEmpty(command.Id));
      }
   }
}
