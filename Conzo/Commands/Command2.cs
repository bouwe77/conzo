using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   public class Command2 : CommandBase
   {
      public Command2(Func<string> action)
      {
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      internal Func<string, ConsoleKey> Action { get; private set; }
   }
}
