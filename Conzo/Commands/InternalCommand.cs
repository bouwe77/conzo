using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   internal class InternalCommand
   {
      public Command Command { get; private set; }
      public Func<bool> Condition { get; private set; }

      public InternalCommand(Command command, Func<bool> condition)
      {
         Command = Enforce.ArgumentNotNull(command, "Command can not be null");
         Condition = Enforce.ArgumentNotNull(condition, "Condition can not be null");
      }

      public InternalCommand(Command command)
      {
         Command = Enforce.ArgumentNotNull(command, "Command can not be null");
      }
   }
}
