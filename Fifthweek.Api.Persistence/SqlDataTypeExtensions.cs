namespace Fifthweek.Api.Persistence
{
    using System;

    using Fifthweek.Api.Core;

    public static class SqlDataTypeExtensions
    {
        public static string ToSqlUtcString(this DateTime dateTime)
        {
            dateTime.AssertUtc("dateTime");
            return string.Format("CONVERT(datetime, '{0}', 127)", dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
        }
    }
}