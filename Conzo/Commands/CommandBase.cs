using System;

namespace Conzo.Commands
{
   public abstract class CommandBase
   {
      protected CommandBase()
      {
         Id = Guid.NewGuid().ToString("N");
      }

      internal string Id { get; }

      protected bool Equals(CommandBase other)
      {
         return string.Equals(Id, other.Id);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((CommandBase) obj);
      }

      public override int GetHashCode()
      {
         return Id?.GetHashCode() ?? 0;
      }

      internal Func<bool> Condition { get; private set; }
   }
}
