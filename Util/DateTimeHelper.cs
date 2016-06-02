using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// 时间、日期帮助类
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// 获取距离给定时间的天数偏离字符串
        /// </summary>
        /// <param name="dtCompareTo">比较的基准时间</param>
        /// <param name="dtCompare">要比较的时间</param>
        /// <param name="diff">时差</param>
        /// <returns></returns>
        public static string GetDateDiff(DateTime dtCompareTo, DateTime dtCompare, int diff)
        {
            DateTime dtGMT = dtCompare.AddHours(diff);
            TimeSpan ts = dtGMT.Date - dtCompareTo.Date;
            String strTs = "";
            if (ts.Days == -1)
            {
                strTs = "-";
            }
            else if (ts.Days == 1)
            {
                strTs = "";
            }
            else
            {
                strTs = ts.Days.ToString();
            }
            return dtGMT.ToString("HHmm") + (ts.Days == 0 ? string.Empty : ((ts.Days > 0 ? "+" : string.Empty) + strTs));
        }
        /// <summary>
        /// 获取距离给定时间的天数偏离字符串
        /// </summary>
        /// <param name="dtCompareTo">比较的基准时间</param>
        /// <param name="dtCompare">要比较的时间</param>
        /// <param name="diff">时差</param>
        /// <returns></returns>
        public static string GetDateDiff(DateTime dtCompareTo, TimeSpan dtCompare, int diff)
        {
            DateTime dtGMT = dtCompareTo.Add(dtCompare);
            TimeSpan ts = dtGMT.Date - dtCompareTo.Date;

            String strTs = "";
            if (ts.Days < 0)
            {
                strTs = "-";
            }
            else if (ts.Days >= 1)
            {
                strTs = "";
            }
            else
            {
                strTs = ts.Days.ToString();
            }
            return dtCompare.ToString("hhmm") + (ts.Days == 0 ? string.Empty : ((ts.Days > 0 ? "+" : string.Empty) + strTs));
        }

        /// <summary>
        /// 从航班格式时间取时间，时间必须至少为4位HHmm
        /// 不涉及时区转换
        /// 格式不正确则返回DateTime.MinValue
        /// </summary>
        /// <param name="dtBase"></param>
        /// <param name="strDate">航班格式时间：HHmm[+|-[n]]</param>
        /// <returns></returns>
        public static DateTime DateTimeFromFlightTimeString(DateTime dtBase, string strDate)
        {
            if (string.IsNullOrWhiteSpace(strDate)) { return DateTime.MinValue; }

            strDate = strDate.Trim();
            const string strRegex = @"((([0-1][0-9])|(2[0-3]))([0-5][0-9]))(([\+\-]([0-9]*))?)";

            if (!Regex.IsMatch(strDate, strRegex)) { return DateTime.MinValue; }

            string time = strDate.Substring(0, 4);

            try
            {
                int diff = 0;
                if (strDate.Length > 4)
                {
                    diff = strDate[4] == '+' ? 1 : -1;

                    if (strDate.Length > 5)
                    {
                        diff *= int.Parse(strDate.Substring(5));
                    }
                }

                return dtBase.Date.AddDays(diff) + DateTime.ParseExact(time, "HHmm", null).TimeOfDay;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetDateTime(DateTime dtBase, String strDate, int diff)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            DateTime dtRet = new DateTime();
            if (strDate.Trim().Length == 4)//如0123
            {
                DateTime dt4 = DateTime.Parse(dtBase.ToString("yyyy-MM-dd ")
                        + strDate.Trim().Substring(0, 2) + ":"
                        + strDate.Trim().Substring(2, 2) + ":00");
                dtRet = dt4.AddHours(-diff);
            }
            else if (strDate.Trim().Length == 5)//如0123+
            {
                if (!(strDate.Trim().Contains("+") || strDate.Trim().Contains("-")))
                {
                    throw new Exception();
                }
                DateTime dt5 = DateTime.Parse(dtBase.ToString("yyyy-MM-dd ")
                       + strDate.Trim().Substring(0, 2) + ":"
                        + strDate.Trim().Substring(2, 2) + ":00");
                dt5 = strDate.Trim().Substring(4, 1) == "+" ? dt5.AddDays(1) : dt5.AddDays(-1);
                dtRet = dt5.AddHours(-diff);

            }
            else
            {
                DateTime dt = DateTime.Parse(strDate);
            }
            return dtRet;
        }
        /// <summary>
        /// 获取GMT时间区间
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="diff"></param>
        /// <returns></returns>
        public static string GetGMTDateRange(DateTime dtBegin, DateTime dtEnd, int diff, string split = " - ")
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}{1}{2:yyyy-MM-dd HH:mm:ss}", dtBegin.AddHours(-diff), string.IsNullOrEmpty(split) ? " " : split, dtEnd.AddHours(-diff));
        }
    }
}
