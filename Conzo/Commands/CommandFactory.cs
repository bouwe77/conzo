using System;

namespace Conzo.Commands
{
   public static class CommandFactory
   {
      public static CommandBase Create(Func<string> action)
      {
         return new Command(action);
      }

      public static CommandBase Create(Func<ConsoleKey, string> action)
      {
         return new CommandWithPressedKey(action);
      }
   }
}
