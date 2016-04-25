using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class Command2 : Command
   {
      public Command2(Func<ConsoleKey, string> action)
      {
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      internal Func<ConsoleKey, string> Action { get; private set; }
   }
}
