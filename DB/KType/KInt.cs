using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// KInt ��ժҪ˵����
	/// </summary>
	public class KInt : IType
	{
		public KInt()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�null
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
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�null
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
		/// �������ֵת��Ϊ�ַ������������Ϊnull���򷵻ؿ�
		/// formatΪ�ַ�����ʾ��ʽ��ͬskyobjitems�е�format����
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
		/// �����ݿ����ת��ΪC#�����������ΪDBNull���򷵻�Ĭ��ֵ
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
		/// �����ݿ����ת��Ϊ�ַ��������������ΪDBNull���򷵻ؿ�
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
		/// ��ȡĬ��ֵ
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
