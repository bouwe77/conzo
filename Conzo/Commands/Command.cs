using System;
using Conzo.Helpers;

namespace Conzo.Commands
{
   public class Command //: Command
   {
      internal string Id { get; }

      public Command(Func<string> action)
         : this(action, null)
      {
      }

      public Command(Func<ConsoleKey, string> actionWithPressedKey)
         : this(null, actionWithPressedKey)
      {
      }

      private Command(Func<string> action, Func<ConsoleKey, string> actionWithPressedKey)
      {
         Action = action;
         ActionWithPressedKey = actionWithPressedKey;
         Id = Guid.NewGuid().ToString("N");
      }

      protected bool Equals(Command other)
      {
         return string.Equals(Id, other.Id);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((Command)obj);
      }

      public override int GetHashCode()
      {
         return Id?.GetHashCode() ?? 0;
      }

      internal Func<bool> Condition { get; set; }

      internal Func<string> Action { get; private set; }

      internal Func<ConsoleKey, string> ActionWithPressedKey { get; private set; }
   }
}
