using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KInt 的摘要说明。
	/// </summary>
	public class KInt : IType
	{
		public KInt()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回null
		/// </summary>
		public Object StringToObject(String v)
		{
			if(v == "")
			{
				return 0;
			}
			try
			{
				return int.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate integer, please make sure your input is valid", v));
			}
		}

		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回null
		/// </summary>
		public Object StringToObjectRaw(String v)
		{
			if(v == "")
			{
				return null;
			}
			try
			{
				return int.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate integer, please make sure your input is valid", v));
			}
		}
		/// <summary>
		/// 将对象的值转换为字符串，如果对象为null，则返回空
		/// format为字符串显示格式，同skyobjitems中的format类型
		/// </summary>
		public String ObjectToString(Object o, String format)
		{
			if(o == null)
			{
				return "";
			}
			return String.Format("{0:" + format + "}", (int)o);
		}
		/// <summary>
		/// 将数据库对象转换为C#对象，如果对象为DBNull，则返回默认值
		/// </summary>
		public Object DBToObject(Object o)
		{
			if(o.GetType().Name == "DBNull")
			{
				return Default();
			}
			else
			{
				return int.Parse(o.ToString());
			}
		}
		/// <summary>
		/// 将数据库对象转换为字符串，，如果对象为DBNull，则返回空
		/// </summary>
		public String DBToString(Object o, String format)
		{
			if(o.GetType().Name == "DBNull")
			{
				return "";
			}
			else
			{
				return ObjectToString(DBToObject(o), format);
			}
		}
		/// <summary>
		/// 获取默认值
		/// </summary>
		public Object Default()
		{
			return 0;
		}
		public String StringToDBString(String v)
		{
			return v;
		}
		public String GetName()
		{
			return "Int";
		}
		public virtual object ObjectToDBValue(object o)
		{
			if (o == null || o.ToString().Trim() == "")
				return DBNull.Value;
			else
				return Convert.ToInt32(o);
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            if (v == null || v.ToString().Trim() == "")
                return null;
            else
                return Convert.ToString(v);
        }
	}
}
