/* 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001
 *
 *  Project: Tumblr Tools - Image parser and downloader from Tumblr blog system
 *
 *  Author: Shino Amakusa
 *
 *  Created: 2013
 *
 *  Last Updated: March, 2016
 *
 * 01010011 01101000 01101001 01101110 01101111  01000001 01101101 01100001 01101011 01110101 01110011 01100001 */

using System;

namespace Tumblr_Tool.Helpers
{
    /// <summary>
    /// Converts from Unix-type timestamp to <see cref="DateTime"/> and vice-versa.
    /// </summary>
	public static class DateTimeHelper
    {
        /// <summary>
        /// Converts from a timestamp to a <see cref="DateTime"/>. The result is in local time.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp.
        /// </param>
        /// <returns>
        /// The equivalent <see cref="DateTime"/> in local time.
        /// </returns>
		public static DateTime FromTimestamp(Int64 timestamp)
        {
            return FromTimestamp((double)timestamp);
        }

        /// <summary>
        /// Converts from a timestamp to a <see cref="DateTime"/>. The result is in local time.
        /// </summary>
        /// <param name="timestamp">
        /// The timestamp.
        /// </param>
        /// <returns>
        /// The equivalent <see cref="DateTime"/> in local time.
        /// </returns>
		public static DateTime FromTimestamp(Double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp).ToLocalTime();
        }

        /// <summary>
        /// Converts from a <see cref="DateTime"/> to a timestamp.
        /// </summary>
        /// <param name="date">
        /// The <see cref="DateTime"/>.
        /// </param>
        /// <returns>
        /// The timestamp.
        /// </returns>
		public static double ToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}