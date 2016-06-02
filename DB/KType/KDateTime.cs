using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KSmallDataTime ��ժҪ˵����
	/// </summary>
	public class KDateTime : IType
	{
		public KDateTime()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻ص���
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
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�null
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
		/// �������ֵת��Ϊ�ַ������������Ϊnull���򷵻ؿ�
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
		/// �����ݿ����ת��ΪC#�����������ΪDBNull���򷵻�Ĭ��ֵ
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
		/// �����ݿ�����ת��Ϊ�ַ������������ΪDBNull���򷵻ؿ�
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
		/// ȡ��Ĭ��ֵ
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
