using System;

namespace Conzo.Commands
{
   public abstract partial class Command
   {
      internal Command()
      {
         Id = Guid.NewGuid().ToString("N");
      }

      internal string Id { get; }

      protected bool Equals(Command other)
      {
         return string.Equals(Id, other.Id);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((Command) obj);
      }

      public override int GetHashCode()
      {
         return Id?.GetHashCode() ?? 0;
      }
   }
}
