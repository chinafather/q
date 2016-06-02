using System;

namespace Weegle.FrameWork.DB.KType.Oracle
{
	/// <summary>
	/// ONumber 的摘要说明。
	/// </summary>
    public class OBit : Weegle.FrameWork.DB.KType.KBit
	{
		public OBit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public override Object DBToObject(Object o)
		{
			if(o.GetType().Name == "DBNull")
			{
				return Default();
			}
			else
			{	
				//modify by sdh
				if (o.GetType().Name=="Boolean"){
					return o;
				}
				else{
					if(int.Parse(o.ToString()) == 0) {
						return false;
					}
					else {
						return true;
					}
				}
			}
		}
		public override String DBToString(Object o,String format)
		{
			if(o.GetType().Name == "DBNull")
			{
				return String.Format("{0:"+format+"}",Boolean.FalseString);
			}
			else
			{
				if(int.Parse(o.ToString()) == 0)
				{
					return String.Format("{0:"+format+"}",Boolean.FalseString);
				}
				else
				{
					return String.Format("{0:"+format+"}",Boolean.TrueString);
				}
			}
		}
	}
}
