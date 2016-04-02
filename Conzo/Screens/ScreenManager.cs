using System;
using System.Collections.Generic;
using System.Linq;
using Conzo.Utilities;

namespace Conzo.Screens
{
   internal class ScreenManager : IScreenManager
   {
      private readonly Dictionary<Screen, ScreenConfiguration> _configuredScreens;
      private Screen _startScreen;
      //private Screen _currentScreen;

      internal ScreenManager()
      {
         _configuredScreens = new Dictionary<Screen, ScreenConfiguration>();
      }

      public ScreenConfiguration AddOrUpdateScreen(Screen screen)
      {
         Enforce.ArgumentNotNull(screen, "screen can not be null");

         ScreenConfiguration configuration;
         if (!_configuredScreens.ContainsKey(screen))
         {
            configuration = new ScreenConfiguration();
            _configuredScreens.Add(screen, configuration);
         }
         else
         {
            configuration = _configuredScreens[screen];
         }

         if (StartScreen == null)
         {
            StartScreen = screen;
         }

         //if (CurrentScreen == null)
         //{
         //   CurrentScreen = screen;
         //}

         return configuration;
      }

      public Screen GetNewCurrentScreen(Screen currentScreen, ConsoleKey key)
      {
         Screen newCurrentScreen = null;

         if (_configuredScreens.ContainsKey(currentScreen))
         {
            var configuration = _configuredScreens[currentScreen];
            newCurrentScreen = configuration.GetScreen(key);
         }

         if (newCurrentScreen == null)
         {
            newCurrentScreen = currentScreen;
         }

         return newCurrentScreen;
      }

      public void Validate()
      {
         // At least one screen is required. This typically is the start screen.
         if (!_configuredScreens.Any())
         {
            throw new Exception("No screens configured");
         }

         //TODO refactor this:

         var screensThatHaveCommandPointingToIt = new List<Screen>();
         foreach (var screenConfiguration in _configuredScreens.Values)
         {
            screensThatHaveCommandPointingToIt.AddRange(screenConfiguration.GetAllScreens());
         }

         bool isOrphaned = false;

         // Screens that are configured must not be "orphans", i.e. they must be either the start screen or there must be a command pointing to it.
         foreach (var configuredScreen in _configuredScreens)
         {
            isOrphaned = !configuredScreen.Key.Equals(_startScreen);
            if (isOrphaned)
            {
               foreach (var screen in screensThatHaveCommandPointingToIt)
               {
                  if (screen.Equals(configuredScreen.Key))
                  {
                     isOrphaned = false;
                     break;
                  }
               }
            }

            if (isOrphaned)
            {
               break;
            }
         }

         if (isOrphaned)
         {
            throw new Exception("You can not configure a orphaned screen, i.e. a screen that has no command pointing to it");            
         }
      }

      public Screen StartScreen
      {
         get
         {
            return _startScreen;
         }
         set
         {
            Enforce.ArgumentNotNull(value, "startScreen can not be null");
            Enforce.DictionaryKeyExists(_configuredScreens, value, "startScreen does not exist");
            _startScreen = value;
         }
      }

      //public Screen CurrentScreen
      //{
      //   get
      //   {
      //      return _currentScreen;
      //   }
      //   set
      //   {
      //      Enforce.ArgumentNotNull(value, "currentScreen can not be null");
      //      _currentScreen = value;
      //   }
      //}
   }
}
