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
   /// <summary>
   /// Integration tests by using the <see cref="ConsoleApplication"/> class.
   /// </summary>
   [TestClass]
   public class ConsoleApplicationIntegrationTest
   {
      private Mock<IConsoleWriter> _consoleWriterMock;
      private Mock<IKeyboardListener> _keyboardListenerMock;
      private Mock<ICommandManager> _commandManagerMock;
      private Mock<ITemplateProvider> _templateProviderMock;

      [TestInitialize]
      public void TestInitialize()
      {
         _consoleWriterMock = new Mock<IConsoleWriter>();
         _keyboardListenerMock = new Mock<IKeyboardListener>();
         _commandManagerMock = new Mock<ICommandManager>();
         _templateProviderMock = new Mock<ITemplateProvider>();
         ConsoleApplication.Reset();
      }

      [TestCleanup]
      public void TestCleanup()
      {
         _consoleWriterMock.VerifyAll();
         _keyboardListenerMock.VerifyAll();
         _commandManagerMock.VerifyAll();
         _templateProviderMock.VerifyAll();
      }

      [TestMethod]
      public void ConsoleApplication_Start()
      {
         const string text = "Hello World";

         bool startCommandInvoked = false;
         var startCommand = new Command(() =>
         {
            startCommandInvoked = true;
            return text;
         });

         var settings = new Settings(startCommand);

         SetupTemplateProviderMock(text);
         SetupConsoleWriterMock(text);
         SetupCommandManagerMock(startCommand);

         Func<IConsoleApplication> consoleApplicationFactoryMethod = () => new ConsoleApplication(
            settings,
            _consoleWriterMock.Object,
            _keyboardListenerMock.Object,
            _commandManagerMock.Object);

         Func<ITemplateProvider> templateProviderFactoryMethod = () => _templateProviderMock.Object;
         var consoleApplication = ConsoleApplication.Create(settings, consoleApplicationFactoryMethod, templateProviderFactoryMethod);

         consoleApplication.Run();

         _commandManagerMock.Verify(x => x.Validate(), Times.Once);
         _keyboardListenerMock.Verify(x => x.Start(), Times.Once);
         Assert.IsTrue(startCommandInvoked);
      }

      private void SetupCommandManagerMock(Command startCommand)
      {
         // Set up the command manager so it asserts that the Configure method is called for the start command.
         _commandManagerMock
            .Setup(x => x.Configure(It.IsAny<Command>()))
            .Callback<Command>(actualArgument => Assert.AreEqual(startCommand, actualArgument));
      }

      //TODO test commands with conditions
      //TODO Raise event _keyboardListenerMock.Raise(x => x.KeyPressed += null, EventArgs.Empty);

      private void SetupTemplateProviderMock(string renderedTemplateToReturn)
      {
         // Set up the template provider so it will return the supplied argument.
         _templateProviderMock
            .Setup(x => x.GetRenderedTemplate(It.IsAny<string>()))
            .Returns(renderedTemplateToReturn);
      }

      private void SetupConsoleWriterMock(string expectedTextToWriteToConsole)
      {
         // Set up the console writer so it asserts whether the expectedText argument is supplied as argument for the WriteToConsole method.
         _consoleWriterMock
            .Setup(x => x.WriteToConsole(It.IsAny<string>()))
            .Callback<string>(actualArgument => Assert.AreEqual(expectedTextToWriteToConsole, actualArgument));
      }
   }
}
