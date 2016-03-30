using System;
using Conzo.Utilities;

namespace Conzo.Screens
{
   public class Screen
   {
      public Screen(Func<string> getScreenContents)
      {
         Id = Guid.NewGuid().ToString("N");
         GetScreenContents = Enforce.ArgumentNotNull(getScreenContents, "getScreenContents can not be null");
      }

      //TODO GetScreenContents hernoemen omdat niet alleen screen contents worden teruggegeven, er wordt ook meestal iets "uitgevoerd"

      public string Id { get; private set; }
      public Func<string> GetScreenContents { get; private set; } 

      protected bool Equals(Screen other)
      {
         return string.Equals(Id, other.Id);
      }

      public override bool Equals(object obj)
      {
         if (ReferenceEquals(null, obj)) return false;
         if (ReferenceEquals(this, obj)) return true;
         if (obj.GetType() != GetType()) return false;
         return Equals((Screen) obj);
      }

      public override int GetHashCode()
      {
         return (Id != null ? Id.GetHashCode() : 0);
      }
   }
}
