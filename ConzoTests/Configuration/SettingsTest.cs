using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Configuration
{
   [TestClass]
   public class SettingsTest
   {
      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitDelay_ThrowsException_WhenLessThanZero()
      {
         Settings.QuitDelay = -1;
      }

      [TestMethod]
      public void SetQuitDelay_Success_WhenZero()
      {
         Settings.QuitDelay = 0;
         Assert.AreEqual(0, Settings.QuitDelay);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitKey_ThrowsException_WhenKeyNotSupported()
      {
         Settings.QuitKey = ConsoleKey.PrintScreen;
      }

      [TestMethod]
      public void SetQuitKey_Success_WhenKeySupported()
      {
         Settings.QuitKey = ConsoleKey.A;
         Assert.AreEqual(ConsoleKey.A, Settings.QuitKey);
      }
   }
}
