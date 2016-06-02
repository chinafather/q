using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KDecimal ��ժҪ˵����
	/// </summary>
	public class KDecimal : IType
	{
		public KDecimal()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		///  ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�0
		/// </summary>
		public Object StringToObject(String v)
		{
			if(v == "")
			{
				return 0;
			}
			try
			{
				return Decimal.Parse(v);
			}
			catch(Exception exp)
			{
				throw new DataFormatException(String.Format("{0} is not a validate decimal, please make sure your input is valid", v));
			}
		}

		/// <summary>
		///  ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�null
		/// </summary>
		public Object StringToObjectRaw(String v)
		{
			if(v == "")
			{
				return null;
			}
			try
			{
				return Decimal.Parse(v);
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
			return String.Format("{0:" + format + "}", (Decimal)o);
		}
        public virtual string ObjectToDBFormat(Object v)
        {
            if (v != null && v.ToString().Trim() != "")
                return Convert.ToString(v);
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
			return 0.0;
		}
		public String StringToDBString(String v)
		{
			return v;
		}
		public String GetName()
		{
			return "Decimal";
		}
		public virtual object ObjectToDBValue(object o)
		{
			if ( o != null && o.ToString().Trim() != "")
				return Convert.ToDecimal(o);
			else
				return DBNull.Value;
		}
	}
}
