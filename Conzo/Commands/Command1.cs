using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   public class Command1 : CommandBase
   {
      public Command1(Func<string> action)
      {
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      internal Func<string> Action { get; private set; }
   }
}
