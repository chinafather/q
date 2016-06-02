using System;
using System.Configuration;
using Weegle.FrameWork.DB.KType.Oracle;
using Weegle.FrameWork.DB.KType.SqlServer;


namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// STypeFactory 的摘要说明。
	/// </summary>
	public class KTypeFactory : ITypeFactory
	{
		public KTypeFactory()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public IType GetType(int type)
		{
			IType temp = null;
			switch(Constant.GetDBType().ToUpper())
			{
				case "SQLSERVER":
					if(type > 100 && type < 199)
					{
						temp = new SDateTime();
					}
					else if(type == 202)
					{
						temp = temp = new SBit();
					}
					else if(type > 200 && type < 210)
					{
						temp = new SInt();
					}
					else if(type > 210 && type < 300)
					{
						temp = new SDecimal();
					}
					else if(type > 300 && type < 400)
					{
						temp = new SVarChar();
					}
					else if(type > 400 && type < 500)
					{
						temp = new SNVarChar();
					}
					else if(type > 500 && type < 600)
					{
						temp = new SBlob();
					}
					break;
				case "ORACLE":
					if(type > 100 && type < 199)
					{
						temp = new ODateTime();
					}
					else if(type == 202)
					{
						temp = temp = new OBit();
					}
					else if(type > 200 && type < 210)
					{
						temp = new OInt();
					}
					else if(type > 210 && type < 300)
					{
						temp = new ODecimal();
					}
					else if(type > 300 && type < 400)
					{
						temp = new OVarChar();
					}
					else if(type > 400 && type < 500)
					{
						temp = new ONVarChar();
					}
					else if(type > 500 && type < 600)
					{
						temp = new OBlob();
					}
					break;
					
			}
			return temp;
		}


	}
}
