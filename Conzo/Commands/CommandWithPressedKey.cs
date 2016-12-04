using System;
using Conzo.Helpers;

namespace Conzo.Commands
{
   internal class CommandWithPressedKey : CommandBase
   {
      public CommandWithPressedKey(Func<ConsoleKey, string> action)
      {
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      internal Func<ConsoleKey, string> Action { get; private set; }
   }
}
