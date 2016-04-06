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
         var command = new Command(() => "Hello World");
         Assert.IsNotNull(command);
         Assert.IsTrue(!string.IsNullOrEmpty(command.Id));
         Assert.IsNotNull(command.Action);
      }

      [TestMethod]
      [ExpectedException(typeof (ArgumentException))]
      public void Constructor_ThrowsException_WhenActionIsNull()
      {
         var command = new Command(null);
      }
   }
}
