using System;

namespace Weegle.FrameWork.DB.KType.Oracle
{
	/// <summary>
	/// ONVarChar ��ժҪ˵����
	/// </summary>
    public class ONVarChar : Weegle.FrameWork.DB.KType.KVarChar
	{
		public ONVarChar()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public String StringToDBString(String v)
		{
			return String.Format("'{0}'", v.Replace("'", "''"));
		}
	}
}
