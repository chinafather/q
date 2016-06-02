using System;


namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KNVarChar 的摘要说明。
	/// </summary>
	public class KNVarChar : IType
	{
		public KNVarChar()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public Object StringToObject(String v)
		{
			if(v == "")
			{
				return "";
			}
			return (Object)v;
		}
		public Object StringToObjectRaw(String v)
		{
			if(v == "")
			{
				return null;
			}
			return (Object)v;
		}
		public String ObjectToString(Object o,String format)
		{
			if(o == null)
			{
				return "" ;
			}
			else
			{
				return  String.Format("{0:" + format + "}", o.ToString());
			}
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            if (v != null)
                return String.Format("N'{0}'", v.ToString().Replace("'", "''"));
            else
                return null;
        }
		public Object DBToObject(Object o)
		{
			if(o.GetType().Name == "DBNull")
			{
				return Default();
			}
			else
			{
				return o;
			}
		}
		public String DBToString(Object o,String format)
		{
			Object temp ;
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
			return "";
		}
		public String StringToDBString(String v)
		{
			return String.Format("N'{0}'", v.Replace("'", "''"));
		}
		public String GetName()
		{
			return "String";
		}
		public virtual object ObjectToDBValue(object o)
		{
			if (o == null )
				return DBNull.Value;
			else
				return Convert.ToString(o);
		}
	}
}
