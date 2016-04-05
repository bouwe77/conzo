using System;
using Conzo.Utilities;

namespace Conzo.Commands
{
   public class Command
   {
      public Command(Func<string> action)
      {
         Id = Guid.NewGuid().ToString("N");
         Action = Enforce.ArgumentNotNull(action, "action can not be null");
      }

      public string Id { get; private set; }
      public Func<string> Action { get; private set; } 

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
         return (Id != null ? Id.GetHashCode() : 0);
      }
   }
}
