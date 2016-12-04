using System;

namespace Conzo.Exceptions
{
   public class ConzoException : Exception
   {
      public ConzoException(string message)
         : base(message)
      {
      }

      public ConzoException(string message, Exception innerException)
         : base(message, innerException)
      {
      }
   }
}
