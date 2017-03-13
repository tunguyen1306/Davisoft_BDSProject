using System;
using Davisoft_BDSProject.Web.Infrastructure.Utility;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    /// <summary>
    /// Summary description for DateTimeHelper
    /// </summary>
    public static class DateTimeHelper
    {
        private const string InvalidUnixEpochErrorMessage = "Unix epoc starts January 1st, 1970";

        /// <summary>
        ///   Convert a long into a DateTime
        /// </summary>
        public static DateTime FromUnixTime(Int64 self)
        {
            var ret = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return ret.AddSeconds(self);
        }

        /// <summary>
        ///   Convert a DateTime into a long
        /// </summary>
        public static Int64 ToUnixTime(DateTime self)
        {
            if (self == DateTime.MinValue)
                return 0;

            var epoc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var delta = self - epoc;

            if (delta.TotalSeconds < 0)
                throw new ArgumentOutOfRangeException(InvalidUnixEpochErrorMessage);

            return (long)delta.TotalSeconds;
        }

        //public static DateTime ToUserLocalDateTime(this DateTime self)
        //{
        //    var dealerActive = CurrentUser.DealerActive();
        //    return self.AddHours(dealerActive.TimeZone);
        //}
    }
}