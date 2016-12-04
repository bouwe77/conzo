using System;
using Conzo.Commands;
using Conzo.Console;
using Conzo.Exceptions;
using Conzo.Keys;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conzo
{
   [TestClass]
   public class ConsoleApplicationTest
   {
      private IConzoApplication _consoleApplication;
      private Mock<IConsoleWriter> _consoleWriterMock;
      private Mock<IKeyboardListener> _keyboardListenerMock;
      private Mock<ICommandManager> _commandManagerMock;
      private Command _command;

      [TestInitialize]
      public void TestInitialize()
      {
         _consoleWriterMock = new Mock<IConsoleWriter>();
         _keyboardListenerMock = new Mock<IKeyboardListener>();
         _commandManagerMock = new Mock<ICommandManager>();
         _command = new Command(() => "dummy");
         CommandRepository.Clear();
         _consoleApplication = new ConzoApplication(_command, _commandManagerMock.Object);
      }

      [TestCleanup]
      public void TestCleanup()
      {
         _consoleWriterMock.VerifyAll();
         _keyboardListenerMock.VerifyAll();
         _commandManagerMock.VerifyAll();
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Configure_ThrowsException_WhenCommandIsNull()
      {
         _consoleApplication.Configure(null);
      }

      [TestMethod]
      public void Configure_Success_WhenCommandIsNotNull()
      {
         var commandConfiguration = _consoleApplication.Configure(_command);

         Assert.IsNotNull(commandConfiguration);
      }

      [TestMethod]
      public void Run_Success()
      {
         _consoleApplication.Start();
      }

      [TestMethod]
      [ExpectedException(typeof(ConzoException))]
      public void Run_ThrowsException_WhenStartedMultipleTimes()
      {
         try
         {
            _consoleApplication.Start();
         }
         catch
         {
            Assert.Fail("There is no exception expected here");
         }

         _consoleApplication.Start();
      }

      [TestMethod]
      public void Stop_Success()
      {
         _consoleApplication.Start();
         _consoleApplication.Stop();
      }

      [TestMethod]
      [ExpectedException(typeof(ConzoException))]
      public void Stop_ThrowsException_WhenNotStarted()
      {
         _consoleApplication.Stop();
      }

      [TestMethod]
      public void AddGlobalCommand_Success()
      {
         _consoleApplication.AddGlobalCommand(ConsoleKey.A, _command);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void AddGlobalCommand_ThrowsException_WhenCommandNull()
      {
         _consoleApplication.AddGlobalCommand(ConsoleKey.A, null);
      }
   }
}
