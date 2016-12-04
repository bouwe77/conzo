//using System;
//using Conzo.Helpers;

//namespace Conzo.Commands
//{
//   internal class InternalCommand
//   {
//      public CommandBase Command { get; private set; }
//      public Func<bool> Condition { get; private set; }

//      public InternalCommand(CommandBase command, Func<bool> condition)
//      {
//         Command = Enforce.ArgumentNotNull(command, "Command can not be null");
//         Condition = condition;
//      }

//      public InternalCommand(CommandBase command)
//         : this(command, null)
//      {
//      }
//   }
//}
