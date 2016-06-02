using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KBit 的摘要说明。
	/// </summary>
	public class KBit:IType
	{
		public KBit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public Object StringToObject(String v)
		{
			if(v == "")
			{
				return false;
			}
			if(v == null)
			{
				return false;
			}
			//在变态的.net中bool在request中为on表示true;
			if(v.ToUpper() == "ON")
			{
				return true;
			}
			try
			{
				return Boolean.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate boolean, please make sure your input is valid",v));
			}
		}
		public Object StringToObjectRaw(String v)
		{
			if(v == "")
			{
				return null;
			}
			if(v == null)
			{
				return null;
			}
			//在变态的.net中bool在request中为on表示true;
			if(v.ToUpper() == "ON")
			{
				return true;
			}
			try
			{
				return Boolean.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate boolean, please make sure your input is valid",v));
			}
		}
		public String ObjectToString(Object o,String format)
		{
			if(o == null)
			{
				return String.Format("{0:" + format + "}", Boolean.FalseString);
			}
			else
			{
				return String.Format("{0:" + format + "}", ((Boolean)o).ToString());
			}
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            if (v == null || v.ToString().Trim() == "")
            {
                return null;
            }
            else if (Convert.ToBoolean(v))
            {
                return "1";
            }
            else
                return "0";
        }
		public virtual Object DBToObject(Object o)
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
		public virtual String DBToString(Object o,String format)
		{
			if(o.GetType().Name == "DBNull")
			{
				return String.Format("{0:"+format+"}",Boolean.FalseString);
			}
			else
			{
				string temp = "";
				switch(bool.Parse(o.ToString()))
				{
					case false:
						temp = String.Format("{0:"+format+"}",Boolean.FalseString);
						break;
					case true:
						temp = String.Format("{0:"+format+"}",Boolean.TrueString);
						break;
				}
				return temp;
			}
		}
		public Object Default()
		{
			return false;
		}
		public String StringToDBString(String v)
		{
			if(v.ToUpper() == "TRUE" || v.ToUpper() == "1")
			{
				return "1";
			}
			else
			{
				return "0";
			}
		}
		public String GetName()
		{
			return "Bool";
		}
		public virtual object ObjectToDBValue(object o)
		{
			if( o == null || o.ToString().Trim() == "" )
			{
				return DBNull.Value;
			}
			else
			{
				if( o.ToString().Equals("1") || o.ToString().Equals("0"))
				{
					return o;
				}
				else if (Convert.ToBoolean(o))
				{
					return 1;
				}
				else
					return 0;
			}
		}


	}
}
