using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KSmallDataTime 的摘要说明。
	/// </summary>
	public class KDateTime : IType
	{
		public KDateTime()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回当天
		/// </summary>
		public Object StringToObject(String v)
		{
			if(v == null || v == "")
			{
				return null;
			}
			try
			{
				return DateTime.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate datatime,please make sure your input is datatime",v));
			}
		}
		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回null
		/// </summary>
		public Object StringToObjectRaw(String v)
		{
			if(v == null || v == "")
			{
				return null;
			}
			try
			{
				return DateTime.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate datatime,please make sure your input is datatime",v));
			}
		}
		/// <summary>
		/// 将对象的值转换为字符串，如果对象为null，则返回空
		/// </summary>
		public String ObjectToString(Object o, String format)
		{
			if( o == null)
			{
				return "";
			}
			DateTime i = (DateTime)o;
			return i.ToString(format);
		}
		/// <summary>
		/// 将数据库对象转换为C#对象，如果对象为DBNull，则返回默认值
		/// </summary>
		public Object DBToObject(Object o)
		{
			if(o.GetType().Name =="DBNull")
			{
				return null;
			}
			else
			{
				return o;
			}
		}
		/// <summary>
		/// 将数据库类型转换为字符串，如果对象为DBNull，则返回空
		/// </summary>
		public String DBToString(Object o, String format)
		{
			Object temp;
			if(o.GetType().Name == "DBNull")
			{
				temp = null;
			}
			else
			{
				temp = o;
			}
			return ObjectToString(temp,format);
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            if (v != null && v.ToString().Trim() != "")
                return String.Format("'{0}'", v);
            else
                return null;
        }
		/// <summary>
		/// 取得默认值
		/// </summary>
		public Object Default()
		{
			return DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
		}
		public String StringToDBString(String v)
		{
			return String.Format("'{0}'", v);
		}
		public String GetName()
		{
			return "DateTime";
		}

		public virtual object ObjectToDBValue(object o)
		{
			if (o != null && o.ToString().Trim() != "")
				return Convert.ToDateTime(o);
			else
				return DBNull.Value;
		}
	}
}
