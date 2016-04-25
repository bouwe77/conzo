using System;
using Conzo.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Configuration
{
   [TestClass]
   public class SettingsTest
   {
      private Command _command;

      [TestInitialize]
      public void TestInitialize()
      {
         _command = Command.Create(() => "Hello World");
      }

      [TestMethod]
      public void Constructor_Success()
      {
         var settings = GetValidSettings();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenCommandIsNull()
      {
         var settings = new Settings(null);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetApplicationTitle_ThrowsException_WhenNull()
      {
         var settings = GetValidSettings();
         settings.ApplicationTitle = null;
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetApplicationTitle_ThrowsException_WhenEmpty()
      {
         var settings = GetValidSettings();
         settings.ApplicationTitle = string.Empty;
      }

      [TestMethod]
      public void SetApplicationTitle_Success()
      {
         var settings = GetValidSettings();
         settings.ApplicationTitle = "ApplicationTitle";
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitDelay_ThrowsException_WhenLessThanZero()
      {
         var settings = GetValidSettings();
         settings.QuitDelay = -1;
      }

      [TestMethod]
      public void SetQuitDelay_Success_WhenZero()
      {
         var settings = GetValidSettings();
         settings.QuitDelay = 0;
         Assert.IsTrue(settings.QuitDelaySet);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitKey_ThrowsException_WhenKeyNotSupported()
      {
         var settings = GetValidSettings();
         settings.QuitKey = ConsoleKey.PrintScreen;
      }

      [TestMethod]
      public void SetQuitKey_Success_WhenKeySupported()
      {
         var settings = GetValidSettings();
         settings.QuitKey = ConsoleKey.A;
         Assert.IsTrue(settings.QuitKeySet);
      }

      private Settings GetValidSettings()
      {
         return new Settings(_command);
      }
   }
}
