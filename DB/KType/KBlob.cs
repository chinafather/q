using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// Bolb ��ժҪ˵����
	/// </summary>
	public class KBlob : IType
	{
		public KBlob()
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
			return "";
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
