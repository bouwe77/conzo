using System;
using System.Collections.Generic;

namespace Conzo.Utilities
{
   internal static class Enforce
   {
      public static T ArgumentNotNull<T>(T argument, string description) where T : class
      {
         if (argument == null)
         {
            throw new ArgumentException(description);
         }

         return argument;
      }

      public static string StringNotNullOrEmpty(string argument, string description)
      {
         if (string.IsNullOrEmpty(argument))
         {
            throw new ArgumentException(description);
         }

         return argument;
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
