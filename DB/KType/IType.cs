using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// IType����������Webҳ�棬C#����Database�е��໥ת����
	/// ת����ԭ�����ڲ������쳣�������
	/// String c = "xxx";
	/// Object o = StringToObject(c);
	/// SaveToDatabase as Object d...
	/// Object b = DBToObject(d);
	/// String f = ObjectToString(b);
	/// c == f;
	/// ע��b��һ������o,��Ϊ���ܷ���Ĭ��ֵ��
	/// </summary>
	public interface IType
	{
		/// <summary>
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�Ĭ��ֵ
		/// </summary>
		Object StringToObject(String v);
		/// <summary>
		/// ���ַ���ת��Ϊ��������ַ���Ϊ�գ��򷵻�null
		/// </summary>
		Object StringToObjectRaw(String v);
		/// <summary>
		/// �������ֵת��Ϊ�ַ������������Ϊnull���򷵻ؿ�
		/// formatΪ�ַ�����ʾ��ʽ��ͬskyobjitems�е�format����
		/// </summary>
		String ObjectToString(Object o, String format);
        /// <summary>
        /// ������ת��Ϊ���ݿ�����
        /// </summary>
        string ObjectToDBFormat(Object v);
		/// <summary>
		/// �����ݿ����ת��ΪC#�����������ΪDBNull���򷵻�Ĭ��ֵ
		/// </summary>
		Object DBToObject(Object o);
		/// <summary>
		/// �����ݿ����ת��Ϊ�ַ���
		/// </summary>
		String DBToString(Object o, String format);
		/// <summary>
		/// ��ȡĬ��ֵ
		/// </summary>
		Object Default();
		/// <summary>
		/// ���ַ���ת��Ϊsql����е��ַ�������int�͵�1ת��Ϊ1��String�͵�1ת��Ϊ'1'��String�͵�ab'cת��Ϊab''c
		/// </summary>
		String StringToDBString(String v);
		/// <summary>
		/// �������ƣ������ж϶�������ͣ�����KType����ʵ�֣���Ҫ��OType��SType��ʵ�֣�!
		/// </summary>
		String GetName();

		Object ObjectToDBValue(object o);
	}
}
