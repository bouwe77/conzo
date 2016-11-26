using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class Command : CommandBase
   {
      public Command(Func<string> action)
      {
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      internal Func<string> Action { get; private set; }
   }
}
