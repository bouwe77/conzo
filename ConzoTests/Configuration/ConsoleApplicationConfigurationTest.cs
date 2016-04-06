using System;
using Conzo.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conzo.Configuration
{
   [TestClass]
   public class ConsoleApplicationConfigurationTest
   {
      private Command _command;

      [TestInitialize]
      public void TestInitialize()
      {
         _command = new Command(() => "Hello World");
      }

      [TestMethod]
      public void Constructor_Success()
      {
         var config = GetValidConfiguration();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenCommandIsNull()
      {
         var config = new ConsoleApplicationConfiguration(null);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetApplicationTitle_ThrowsException_WhenNull()
      {
         var config = GetValidConfiguration();
         config.ApplicationTitle = null;
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetApplicationTitle_ThrowsException_WhenEmpty()
      {
         var config = GetValidConfiguration();
         config.ApplicationTitle = string.Empty;
      }

      [TestMethod]
      public void SetApplicationTitle_Success()
      {
         var config = GetValidConfiguration();
         config.ApplicationTitle = "ApplicationTitle";
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitDelay_ThrowsException_WhenLessThanZero()
      {
         var config = GetValidConfiguration();
         config.QuitDelay = -1;
      }

      [TestMethod]
      public void SetQuitDelay_Success_WhenZero()
      {
         var config = GetValidConfiguration();
         config.QuitDelay = 0;
         Assert.IsTrue(config.QuitDelaySet);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void SetQuitKey_ThrowsException_WhenKeyNotSupported()
      {
         var config = GetValidConfiguration();
         config.QuitKey = ConsoleKey.PrintScreen;
      }

      [TestMethod]
      public void SetQuitKey_Success_WhenKeySupported()
      {
         var config = GetValidConfiguration();
         config.QuitKey = ConsoleKey.A;
         Assert.IsTrue(config.QuitKeySet);
      }

      private ConsoleApplicationConfiguration GetValidConfiguration()
      {
         return new ConsoleApplicationConfiguration(_command);
      }
   }
}
