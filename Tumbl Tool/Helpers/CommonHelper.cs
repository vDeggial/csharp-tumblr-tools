/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2017
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System.Collections.Generic;
using System.Linq;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Common helper
    /// </summary>
    public static class CommonHelper
    {
        public static HashSet<T> ReverseHashSet<T>(this HashSet<T> set)
        {
            try
            {
                return set?.Reverse().ToHashSet();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert to Hashset
        /// </summary>
        /// <typeparam name="T">Type for hashset items</typeparam>
        /// <param name="obj">Object to convert to hashset</param>
        /// <returns>Hasheset of objects of the type T</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> obj)
        {
            try
            {
                return new HashSet<T>(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}