using System;

namespace Conzo.Commands
{
   public abstract partial class Command
   {
      public static Command Create(Func<string> action)
      {
         return new Command1(action);
      }

      public static Command Create(Func<ConsoleKey, string> action)
      {
         return new Command2(action);
      }
   }
}
