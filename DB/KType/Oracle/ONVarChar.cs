using System;

namespace Weegle.FrameWork.DB.KType.Oracle
{
	/// <summary>
	/// ONVarChar 的摘要说明。
	/// </summary>
    public class ONVarChar : Weegle.FrameWork.DB.KType.KVarChar
	{
		public ONVarChar()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public String StringToDBString(String v)
		{
			return String.Format("'{0}'", v.Replace("'", "''"));
		}
	}
}
