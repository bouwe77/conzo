using System;
using Conzo.Commands;
using Conzo.Configuration;
using Conzo.Console;
using Conzo.Keys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conzo
{
   /// <summary>
   /// Integration tests by using the <see cref="ConzoApplication"/> class.
   /// </summary>
   [TestClass]
   public class ConsoleApplicationIntegrationTest
   {
      private Mock<IConsoleWriter> _consoleWriterMock;
      private Mock<IKeyboardListener> _keyboardListenerMock;

      [TestInitialize]
      public void TestInitialize()
      {
         _consoleWriterMock = new Mock<IConsoleWriter>();
         _keyboardListenerMock = new Mock<IKeyboardListener>();
      }

      [TestCleanup]
      public void TestCleanup()
      {
         _consoleWriterMock.VerifyAll();
         _keyboardListenerMock.VerifyAll();
      }

      [TestMethod]
      public void ConsoleApplication_Start()
      {
         const string text = "Hello World";

         bool startCommandInvoked = false;
         var startCommand = CommandFactory.Create(() =>
         {
            startCommandInvoked = true;
            return text;
         });

         string expectedTextToWriteToConsole = "-Hello World-";
         SetupConsoleWriterMock(expectedTextToWriteToConsole);

         var consoleApplication = new ConzoApplication(startCommand);

         consoleApplication.Start();
         Assert.IsTrue(startCommandInvoked, "startCommand was not invoked");
      }

      //TODO test commands with conditions
      //TODO Raise event _keyboardListenerMock.Raise(x => x.KeyPressed += null, EventArgs.Empty);

      private void SetupConsoleWriterMock(string expectedTextToWriteToConsole)
      {
         // Set up the console writer so it asserts whether the expectedText argument is supplied as argument for the WriteToConsole method.
         _consoleWriterMock
            .Setup(x => x.WriteToConsole(It.IsAny<string>()))
            .Callback<string>(actualArgument => Assert.AreEqual(expectedTextToWriteToConsole, actualArgument));
      }
   }
}
