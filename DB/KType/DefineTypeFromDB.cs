using System;
using Weegle.FrameWork.DB.KType.SqlServer;
using Weegle.FrameWork.DB;
namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// DefineTypeFromDB 的摘要说明。
	/// </summary>
	public sealed class DefineTypeFromDB
	{
		public DefineTypeFromDB()
		{
		}
		public static IType GetDefineType(int type)
		{
			switch (DatabaseFactory.GetDBType())
			{
				case "oracle":
					return GetDoNetTypeInOracle(type);
				case "access":
					return GetDoNetTypeInAccess(type);
				default:
					return GetDoNetTypeInSqlServer(type);
			}
		}


		private static IType GetDoNetTypeInSqlServer(int type)
		{
			if (type == 58 || type == 61 || type == 189)
				return new SDateTime();
			else if (type == 104)
				return new SBit();
			else if (type == 48 || type == 52 || type == 56 || type == 127)
				return new SInt();
			else if (type == 59 || type == 60 || type == 62 || type == 106 || type == 108 || type == 122)
				return new SDecimal();
			else if (type == 35 || type == 167 || type == 175)
				return new SVarChar();
			else if (type == 99 || type == 231 || type == 239)
				return new SNVarChar();
			else if (type == 34 || type == 165 || type == 173)
				return new SBlob();
			else
				return null;
		}

		private static IType GetDoNetTypeInOracle(int type)
		{
			return null;
		}
	
		private static IType GetDoNetTypeInAccess(int type)
		{
			return null;
		}
	}
}
