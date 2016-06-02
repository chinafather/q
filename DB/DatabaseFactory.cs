using System;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// DatabaseFactory ��ժҪ˵����
	/// </summary>
	public class DatabaseFactory
	{	//��������������ͨ��Web.config�ļ�������ֵ��ȡ�ö����
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
        /// ���ݴ������Ӵ���ȡ���ݿ�
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static Database GetDatabase(String conn)
        {
            return new SqlDatabase(conn);
        }

        /// <summary>
        /// �������ݿ����;������ݿ�����
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
