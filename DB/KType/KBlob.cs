using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// Bolb 的摘要说明。
	/// </summary>
	public class KBlob : IType
	{
		public KBlob()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		///  将字符串转换为对象，如果字符串为空，则返回0
		/// </summary>
		public Object StringToObject(String v)
		{
			return "";
		}

		/// <summary>
		///  将字符串转换为对象，如果字符串为空，则返回null
		/// </summary>
		public Object StringToObjectRaw(String v)
		{
			if(v == "")
			{
				return null;
			}
			try
			{
				return null;
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate decimal, please make sure your input is valid", v));
			}
		}
		public String ObjectToString(Object o, String format)
		{
			if(o == null)
			{
				return "";
			}
			return String.Format("{0:" + format + "}", (Byte[])o);
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            return null;
        }
		public Object DBToObject(Object o)
		{
			if(o.GetType().Name == "DBNull")
			{
				return null;
			}
			else
			{
				return o;
			}
		}
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
		public Object Default()
		{
			return null;
		}
		public String StringToDBString(String v)
		{
			return null;
		}
		public String GetName()
		{
			return "Blob";
		}
		public virtual object ObjectToDBValue(object o)
		{
			if (o != null)
				return o;
			else
				return DBNull.Value;
		}
	}
}
