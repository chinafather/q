using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cares.FidsII.Util
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// 窗体是否有效
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public static bool IsValid(this Form form)
        {
            return form.Created && !form.Disposing && !form.IsDisposed;
        }

        /// <summary>
        /// 同步执行操作
        /// </summary>
        /// <param name="form"></param>
        /// <param name="action"></param>
        public static void DoSync(this Form form, Action action)
        {
            if (form.InvokeRequired)
            {
                if (form.IsHandleCreated)
                {
                    form.Invoke(action);
                }
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// 是否为有效的航班时间
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsValidFlightDateTime(this DateTime dt)
        {
            return dt.Year > 1900;
        }

        /// <summary>
        /// 转换为航班习惯时间，如 2200+
        /// 与今日比
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeZone"></param>
        /// <param name="isUTC">
        /// <param name="format"></param>
        /// 是否为UTC时间
        /// 不判断Kind是因为系统中时间很混乱；不是UTC时间即认为是用户时间
        /// 时间数值用用户时区表示时传false，用UTC表示时传true
        /// </param>
        /// <returns></returns>
        public static string ToFlightString(this DateTime dt, int timeZone, bool isUTC = true, string format = "HHmm")
        {
            DateTime dtUser = dt;

            if (isUTC)
            {
                dtUser = dtUser.AddHours(timeZone);     // 转为用户时间
            }

            DateTime dtUserDate = dtUser.Date;
            DateTime dtToday = DateTime.Today.ToUniversalTime().AddHours(timeZone);    // 转为用户时间
            TimeSpan ts = dtUserDate - dtToday;

            string str = dtUser.ToString(format);

            if (ts.TotalDays >= 1)
            {
                str += "+";
            }
            else if (ts.TotalDays <= -1)
            {
                str += "-";
            }

            if (Math.Abs(ts.TotalDays) >= 2)
            {
                str += (int)Math.Abs(ts.TotalDays);
            }

            return str;
        }

        /// <summary>
        /// 转换为航班习惯时间，如 2200+
        /// 与指定日期比
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timeZone"></param>
        /// <param name="dtCompare">对比的时间</param>
        /// <param name="isUTC">
        /// 是否为UTC时间
        /// 不判断Kind是因为系统中时间很混乱；不是UTC时间即认为是用户时间
        /// 时间数值用用户时区表示时传false，用UTC表示时传true
        /// </param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToFlightString(this DateTime dt, int timeZone, DateTime dtCompare, bool isUTC = true, string format = "HHmm")
        {
            DateTime dtUser = dt;
            if (isUTC)
            {
                dtUser = dtUser.AddHours(timeZone);     // 转为用户时间
            }

            if (dtCompare.Year <= 1900) { dtCompare = DateTime.Today; }

            DateTime dtUserDate = dtUser.Date;
            DateTime dtBase = dtCompare.Date;           // 用于比较的时间不判断时区，直接减
            TimeSpan ts = dtUserDate - dtBase;

            string str = dtUser.ToString(format);

            if (ts.TotalDays >= 1)
            {
                str += "+";
            }
            else if (ts.TotalDays <= -1)
            {
                str += "-";
            }

            if (Math.Abs(ts.TotalDays) >= 2)
            {
                str += (int)Math.Abs(ts.TotalDays);
            }

            return str;
        }

        /// <summary>
        /// 转为服务器时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timezone"></param>
        /// <param name="isUTC">是否为UTC时间，UTC时间则仅置Kind</param>
        /// <returns></returns>
        public static DateTime ToServerTime(this DateTime dt, int timezone, bool isUTC = false)
        {
            return isUTC ? DateTime.SpecifyKind(dt, DateTimeKind.Utc) : DateTime.SpecifyKind(dt.AddHours(-timezone), DateTimeKind.Utc);
        }

        /// <summary>
        /// 转为用户时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="timezone"></param>
        /// <param name="isUTC">是否为UTC时间，非UTC时间则仅置Kind</param>
        /// <returns></returns>
        public static DateTime ToUserTime(this DateTime dt, int timezone, bool isUTC = true)
        {
            return isUTC ? DateTime.SpecifyKind(dt.AddHours(timezone), DateTimeKind.Local) : DateTime.SpecifyKind(dt, DateTimeKind.Local);
        }

        public static DateTime ToUserLocalTime(this DateTime dateTime, int Hours, bool isUTC = true)
        {
            // 服务器端返回时间转换成本地时间
            return TimeZoneInfo.ConvertTimeToUtc(dateTime).AddHours(Hours);
        }
        /// <summary>
        /// 将 "yyyyMMddHHmmssfff" 类型的字符串转成时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToStringDateTime(this String str)
        {
            if (str.Length < 14) return new DateTime();
            String s = String.Format("{0}-{1}-{2} {3}:{4}:{5}", str.Substring(0, 4), str.Substring(4, 2), str.Substring(6, 2), str.Substring(8, 2), str.Substring(10, 2), str.Substring(12, 2));

            DateTime dt = new DateTime();
            if (!DateTime.TryParse(s, out dt))
                return new DateTime();
            else
                return dt;
        }
        /// <summary>
        /// 判断是否  str -strcomp >0 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strComp"></param>
        /// <returns></returns>
        public static int CompareByMinute(this String str, String strComp)
        {
            int ret = 1;
            if (str.IndexOf("-") > -1 && strComp.IndexOf("-") < 0)
                return -1;
            if (str.IndexOf("-") < 0 && strComp.IndexOf("-") > -1)
                return 1;

            if (str.IndexOf("+") < 0 && strComp.IndexOf("+") > -1)
                return -1;
            if (str.IndexOf("+") > -1 && strComp.IndexOf("+") < 0)
                return 1;

            ret = str.CompareTo(strComp);
            return ret;
        }
    }
}
