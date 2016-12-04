using System;
using System.Collections.Generic;

namespace Conzo.Helpers
{
   internal static class Enforce
   {
      public static T Condition<T>(T argument, bool condition, string description)
      {
         if (!condition)
         {
            throw new ArgumentException(description);
         }

         return argument;
      }

      public static T ArgumentNotNull<T>(T argument, string description) where T : class
      {
         return Condition(argument, argument != null, description);
      }

      public static string StringNotNullOrEmpty(string argument, string description)
      {
         return Condition(argument, !string.IsNullOrEmpty(argument), description);
      }

      public static void DictionaryKeyDoesNotExist<T1, T2>(Dictionary<T1, T2> dictionary, T1 key, string description)
      {
         if (dictionary.ContainsKey(key))
         {
            throw new ArgumentException(description);
         }
      }

      public static void DictionaryKeyExists<T1, T2>(Dictionary<T1, T2> dictionary, T1 key, string description)
      {
         if (!dictionary.ContainsKey(key))
         {
            throw new ArgumentException(description);
         }
      }
   }
}
