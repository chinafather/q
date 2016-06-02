using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// IType用于数据在Web页面，C#程序，Database中的相互转换，
	/// 转换的原则是在不发生异常的情况下
	/// String c = "xxx";
	/// Object o = StringToObject(c);
	/// SaveToDatabase as Object d...
	/// Object b = DBToObject(d);
	/// String f = ObjectToString(b);
	/// c == f;
	/// 注意b不一定等于o,因为可能返回默认值！
	/// </summary>
	public interface IType
	{
		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回默认值
		/// </summary>
		Object StringToObject(String v);
		/// <summary>
		/// 将字符串转换为对象，如果字符串为空，则返回null
		/// </summary>
		Object StringToObjectRaw(String v);
		/// <summary>
		/// 将对象的值转换为字符串，如果对象为null，则返回空
		/// format为字符串显示格式，同skyobjitems中的format类型
		/// </summary>
		String ObjectToString(Object o, String format);
        /// <summary>
        /// 将对象转化为数据库数据
        /// </summary>
        string ObjectToDBFormat(Object v);
		/// <summary>
		/// 将数据库对象转换为C#对象，如果对象为DBNull，则返回默认值
		/// </summary>
		Object DBToObject(Object o);
		/// <summary>
		/// 将数据库对象转换为字符串
		/// </summary>
		String DBToString(Object o, String format);
		/// <summary>
		/// 获取默认值
		/// </summary>
		Object Default();
		/// <summary>
		/// 将字符串转换为sql语句中的字符串，如int型的1转换为1，String型的1转换为'1'，String型的ab'c转换为ab''c
		/// </summary>
		String StringToDBString(String v);
		/// <summary>
		/// 类型名称，用于判断对象的类型，仅在KType中有实现（不要在OType和SType中实现）!
		/// </summary>
		String GetName();

		Object ObjectToDBValue(object o);
	}
}
