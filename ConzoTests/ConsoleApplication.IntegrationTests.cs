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

      [TestInitialize]
      public void TestInitialize()
      {
         _consoleWriterMock = new Mock<IConsoleWriter>();
         _keyboardListenerMock = new Mock<IKeyboardListener>();

         ConsoleApplication.Reset();
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

         var settings = new Settings(startCommand);

         string expectedTextToWriteToConsole = "-Hello World-";
         SetupConsoleWriterMock(expectedTextToWriteToConsole);

         var commandConfigurationManager = new CommandConfigurationManager(settings);

         Func<IConsoleApplication> consoleApplicationFactoryMethod = () => new ConsoleApplication(
            commandConfigurationManager,
            new CommandManager(settings, _consoleWriterMock.Object, commandConfigurationManager, _keyboardListenerMock.Object));

         Func<ITemplateProvider> templateProviderFactoryMethod = () => new TemplateProviderStub();
         var consoleApplication = ConsoleApplication.Create(settings, consoleApplicationFactoryMethod, templateProviderFactoryMethod);

         consoleApplication.Run();
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

   class TemplateProviderStub : ITemplateProvider
   {
      public string GetRenderedTemplate(string stuff)
      {
         return $"-{stuff}-";
      }
   }
}
