using System;
using Conzo.Commands;
using Conzo.Configuration;
using Conzo.Console;
using Conzo.Keys;
using Conzo.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Conzo
{
   [TestClass]
   public partial class ConsoleApplicationTest
   {
      private Mock<IConsoleWriter> _consoleWriterMock;
      private Mock<IKeyboardListener> _keyboardListenerMock;
      private Mock<ICommandManager> _commandManagerMock;
      private Mock<IConsoleApplication> _consoleApplicationMock;
      private Mock<ITemplateProvider> _templateProviderMock;
      private Settings _settings;
      private Command _command;

      [TestInitialize]
      public void TestInitialize()
      {
         _consoleWriterMock = new Mock<IConsoleWriter>();
         _keyboardListenerMock = new Mock<IKeyboardListener>();
         _commandManagerMock = new Mock<ICommandManager>();
         _consoleApplicationMock = new Mock<IConsoleApplication>();
         _templateProviderMock = new Mock<ITemplateProvider>();
         _command = new Command(() => "dummy");
         _settings = GetSettings();
         ConsoleApplication.Reset();
      }

      [TestCleanup]
      public void TestCleanup()
      {
         _consoleWriterMock.VerifyAll();
         _keyboardListenerMock.VerifyAll();
         _commandManagerMock.VerifyAll();
         _consoleApplicationMock.VerifyAll();
         _templateProviderMock.VerifyAll();
      }

      [TestMethod]
      public void Constructor_Success()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         Assert.IsNotNull(consoleApplication);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenSettingsNull()
      {
         var consoleApplication = new ConsoleApplication(null, _consoleWriterMock.Object, _keyboardListenerMock.Object, _commandManagerMock.Object);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenConsoleWriterNull()
      {
         var consoleApplication = new ConsoleApplication(_settings, null, _keyboardListenerMock.Object, _commandManagerMock.Object);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenKeyboardListenerNull()
      {
         var consoleApplication = new ConsoleApplication(_settings, _consoleWriterMock.Object, null, _commandManagerMock.Object);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Constructor_ThrowsException_WhenCommandManagerNull()
      {
         var consoleApplication = new ConsoleApplication(_settings, _consoleWriterMock.Object, _keyboardListenerMock.Object, null);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void Configure_Success_WhenCommandIsNull()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.Configure(null);
      }

      [TestMethod]
      public void Configure_ThrowsException_WhenCommandIsNotNull()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();

         _commandManagerMock.Setup(x => x.Configure(It.IsAny<Command>())).Returns(new CommandConfiguration());

         var commandConfiguration = consoleApplication.Configure(_command);

         Assert.IsNotNull(commandConfiguration);
      }

      [TestMethod]
      public void Start_Success()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.Run();
      }

      [TestMethod]
      [ExpectedException(typeof(Exception))]
      public void Start_ThrowsException_WhenStartedMultipleTimes()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.Run();
         consoleApplication.Run();
      }

      [TestMethod]
      public void Stop_Success()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.Run();
         consoleApplication.Stop();
      }

      [TestMethod]
      [ExpectedException(typeof(Exception))]
      public void Stop_ThrowsException_WhenNotStarted()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.Stop();
      }

      [TestMethod]
      public void AddGlobalCommand_Success()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.AddGlobalCommand(ConsoleKey.A, _command);
      }

      [TestMethod]
      public void AddGlobalCommand_Success_WhenCommandNull()
      {
         var consoleApplication = GetConsoleApplicationWithMocks();
         consoleApplication.AddGlobalCommand(ConsoleKey.A, null);
      }

      private IConsoleApplication GetConsoleApplicationWithMocks()
      {
         return new ConsoleApplication(_settings, _consoleWriterMock.Object, _keyboardListenerMock.Object, _commandManagerMock.Object);
      }

      private Settings GetSettings()
      {
         var settings = new Settings(_command)
         {
            TemplateProvider = new Mock<ITemplateProvider>().Object
         };

         return settings;
      }
   }
}
