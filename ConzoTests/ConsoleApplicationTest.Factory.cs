//using System;
//using Conzo.Configuration;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace Conzo
//{
//   public partial class ConsoleApplicationTest
//   {
//      [TestMethod]
//      public void Create_Success()
//      {
//         var consoleApplication = ConsoleApplication.Create(_settings, () => _consoleApplicationMock.Object, () => _templateProviderMock.Object);

//         Assert.IsNotNull(consoleApplication);
//      }

//      [TestMethod]
//      [ExpectedException(typeof(Exception))]
//      public void Create_ThrowsException_WhenCreatedMultipleTimes()
//      {
//         var consoleApplication = ConsoleApplication.Create(_settings, () => _consoleApplicationMock.Object, () => _templateProviderMock.Object);
//         consoleApplication = ConsoleApplication.Create(_settings, () => _consoleApplicationMock.Object, () => _templateProviderMock.Object);
//      }

//      [TestMethod]
//      [ExpectedException(typeof(ArgumentException))]
//      public void Create_ThrowsException_WhenSettingsNull()
//      {
//         var consoleApplication = ConsoleApplication.Create(null, () => _consoleApplicationMock.Object, () => _templateProviderMock.Object);
//      }

//      [TestMethod]
//      public void Create_SettingsGetsDefaultValues()
//      {
//         var settings = new Settings(_command)
//         {
//            Layout = null,
//            QuitDelay = 0,
//            TemplateProvider = null
//         };

//         var consoleApplication = ConsoleApplication.Create(settings, () => _consoleApplicationMock.Object, () => _templateProviderMock.Object);

//         Assert.IsNotNull(consoleApplication);
//         Assert.IsNotNull(settings);
//         Assert.IsTrue(!string.IsNullOrEmpty(settings.ApplicationTitle));
//         Assert.IsNotNull(settings.Layout);
//         Assert.IsNotNull(settings.TemplateProvider);
//         Assert.IsTrue(settings.QuitKeySet);
//         Assert.IsTrue(settings.QuitDelaySet);
//      }
//   }
//}
