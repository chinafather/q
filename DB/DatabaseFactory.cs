using System;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// DatabaseFactory 的摘要说明。
	/// </summary>
	public class DatabaseFactory
	{	//本工厂的依据是通过Web.config文件的配置值来取得对象的
		public DatabaseFactory()
		{
		}

		public static Database GetDatabase()
		{
			String database = Constant.GetDBType();
			if(database == "sqlserver")
			{
				
				return new SqlDatabase(Constant.GetDBConn());
			}
			else
			{
				//return new OracleDatabase(Constant.GetDBConn());
				return new SqlDatabase(Constant.GetDBConn());
			}
		}
        /// <summary>
        /// 根据传入连接串获取数据库
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static Database GetDatabase(String conn)
        {
            return new SqlDatabase(conn);
        }

        /// <summary>
        /// 根据数据库类型决定数据库连接
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
		public static Database GetDatabase(String conn, String dbType)
		{
			if(dbType == "sqlserver")
			{
				//return new SqlDatabase(Constant.GetDBConn());
                return new SqlDatabase(conn);
			}
			else
			{
				//return new OracleDatabase(conn);
				return new SqlDatabase(conn);
			}
		}

		public static String  GetDBOwner()
		{
			String database = Constant.GetDBType();
			if(database == "sqlserver")
			{
				return "dbo";
			}
			else
			{
				return "SYS";
			}
		}

		public static String GetDBType()
		{
			return Constant.GetDBType().ToLower();
		}
	}
}
