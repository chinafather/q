using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

using Pixysoft.Framework.Reflection;
using Pixysoft.Framework.Reflection.Core;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// 简单的对象帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SimpleObjectHelper<T>
    {
        /// <summary>
        /// IDynamicPropertyInfo属性列表
        /// </summary>
        private Dictionary<string, IDynamicPropertyInfo> _dicPropes = new Dictionary<string, IDynamicPropertyInfo>();

        /// <summary>
        /// 用于对象比较的IDynamicPropertyInfo属性列表
        /// </summary>
        private Dictionary<string, IDynamicPropertyInfo> _dicComparePropes = null;

        /// <summary>
        /// 比较对象时的属性过滤器
        /// </summary>
        private Predicate<string> _propertyCompareFilter = null;

        /// <summary>
        /// T是否为值类型
        /// </summary>
        private bool _isValueType = false;

        /// <summary>
        /// 属性名列表
        /// </summary>
        public string[] PropertyNames
        {
            get
            {
                return this._dicPropes.Keys.ToArray();
            }
        }

        /// <summary>
        /// 获取或设置比较对象时的属性过滤器
        /// </summary>
        public Predicate<string> PropertyCompareFilter
        {
            get
            {
                return this._propertyCompareFilter;
            }
            set
            {
                if (this._propertyCompareFilter == value)
                {
                    return;
                }

                this._propertyCompareFilter = value;

                if (this._propertyCompareFilter != null)
                {
                    this._dicComparePropes.Clear();

                    foreach (var pair in this._dicPropes)
                    {
                        if (this._propertyCompareFilter(pair.Key))
                        {
                            this._dicComparePropes.Add(pair.Key, pair.Value);
                        }
                    }
                }
            }
        }

        public SimpleObjectHelper()
        {
            this._isValueType = typeof(T).IsValueType;

            IDynamicPropertyInfo[] propArray = new DynamicType(typeof(T)).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (propArray == null || propArray.Length == 0)
            {
                return;
            }

            foreach (IDynamicPropertyInfo prop in propArray)
            {
                this._dicPropes.Add(prop.Name, prop);
            }

            this._dicComparePropes = new Dictionary<string, IDynamicPropertyInfo>(this._dicPropes);
        }

        /// <summary>
        /// 比较
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public Dictionary<string, PropertyCompareResult> Compare(T t1, T t2)
        {
            if (!this._isValueType && (t1 == null || t2 == null))
            {
                return null;
            }

            Dictionary<string, PropertyCompareResult> dic = new Dictionary<string, PropertyCompareResult>();

            object o1 = null;
            object o2 = null;
            Type type = null;

            PropertyCompareResult result = null;

            foreach (var prop in this._dicComparePropes.Values)
            {
                o1 = prop.GetValue(t1, null);
                o2 = prop.GetValue(t2, null);

                result = new PropertyCompareResult { PropertyName = prop.Name, ValueFirst = o1, ValueSecond = o2 };

                if (o1 == null && o2 == null)
                {
                    result.IsEqual = true;
                }
                else if (o1 == null || o2 == null)
                {
                    result.IsEqual = false;
                }
                else
                {
                    type = o1.GetType();

                    if (type.IsValueType)
                    {
                        result.IsEqual = o1.Equals(o2);
                    }
                    else if (o1 is string)
                    {
                        result.IsEqual = string.Compare(o1 as string, o2 as string, false) == 0;
                    }
                    else
                    {
                        result.IsEqual = object.ReferenceEquals(o1, o2);
                    }
                }

                dic.Add(prop.Name, result);
            }

            return dic;
        }

        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        public object GetPropertyValue(T src, string propName)
        {
            if (!this._dicPropes.ContainsKey(propName))
            {
                return null;
            }

            return this._dicPropes[propName].GetValue(src, null);
        }

        /// <summary>
        /// 设置对象的属性值
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propName"></param>
        /// <param name="value"></param>
        public void SetPropertyValue(T src, string propName, object value)
        {
            IDynamicPropertyInfo prop = this._dicPropes[propName];

            if (prop == null)
            {
                return;
            }

            prop.SetValue(src, value, null);
        }
    }

    /// <summary>
    /// 属性比较结果
    /// </summary>
    public class PropertyCompareResult
    {
        /// <summary>
        /// 获取属性名
        /// </summary>
        public string PropertyName { get; internal set; }

        /// <summary>
        /// 获取一个值，指示比较结果是否相等
        /// </summary>
        public bool IsEqual { get; internal set; }

        /// <summary>
        /// 获取第一个对象的属性值
        /// </summary>
        public object ValueFirst { get; internal set; }

        /// <summary>
        /// 获取第二个对象的属性值
        /// </summary>
        public object ValueSecond { get; internal set; }
    }
}
