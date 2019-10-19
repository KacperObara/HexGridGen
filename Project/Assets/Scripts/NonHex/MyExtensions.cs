using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        /// <summary>
        /// Checks if value is even. Uses bit operations to compare last bit of the number
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool IsEven(this int i)
        {
            return (i & 1) == 0;
        }
    }
}
