using System;
using System.Collections.Generic;
using Conzo.Keys;
using Conzo.Utilities;

namespace Conzo.Screens
{
   public class ScreenConfiguration
   {
      private readonly Dictionary<ConsoleKey, Screen> _commands = new Dictionary<ConsoleKey, Screen>();

      /// <summary>
      /// Initializes a new instance of the <see cref="ScreenConfiguration"/> class.
      /// Prevents creating new instances from outside the project.
      /// </summary>
      internal ScreenConfiguration()
      {
      }

      public ScreenConfiguration AddCommand(ConsoleKey key, Screen screenToShow)
      {
         SupportedKeys.Validate(key);
         Enforce.DictionaryKeyDoesNotExist(_commands, key, "Dictionary _commands already contains key" + key);
         Enforce.ArgumentNotNull(screenToShow, "screenToShow can not be null");

         _commands.Add(key, screenToShow);
         return this;
      }

      internal Screen GetScreen(ConsoleKey consoleKey)
      {
         Screen screen = null;
         if (_commands.ContainsKey(consoleKey))
         {
            screen = _commands[consoleKey];
         }

         return screen;
      }

      internal IEnumerable<Screen> GetAllScreens()
      {
         return _commands.Values;
      }
   }
}
