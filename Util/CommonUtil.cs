using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cares.FidsII.Util
{
    public sealed class CommonUtil
    {
        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ShallowCopy<T>(T t)
        {
            if (Comparer<T>.Default.Compare(t, default(T)) == 0) { return default(T); }

            MethodInfo mi = typeof(T).GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic);

            return (T)mi.Invoke(t, null);
        }
    }
}
