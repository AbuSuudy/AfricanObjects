using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AfricanObjects
{

    static class ExtensionsClass
    {

        public static void Shuffle<T>(this IList<T> list)
        {
         
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = RandomNumberGenerator.GetInt32(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

}
