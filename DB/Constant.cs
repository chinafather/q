using System;
using System.Configuration;

namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// Constant 的摘要说明。
	/// </summary>
	public class Constant
	{
		
			public Constant()
			{
				//
				// TODO: 在此处添加构造函数逻辑
				//
			}
			public static String GetDBConn()
			{
				//string ConnString = Crypto.DecryptString(ConfigurationSettings.AppSettings["dsn"]);
				String ret = "";
                ConnectionStringSettings connObj = ConfigurationManager.ConnectionStrings["localSqlServer"];
                ret = connObj.ConnectionString;
                return ret;
				//return "server=129.1.1.10;uid=sa;Password=;database=HRV61Test";
			}
			

			public static String GetDBType()
			{
                String ret = ConfigurationManager.AppSettings["dbtype"];
				return ret;
			}

			public static String GetDBServer()
			{
                String ret = ConfigurationManager.AppSettings["server"];
				return ret;
			}

			public static String GetDBName()
			{
                String ret = ConfigurationManager.AppSettings["dbname"];
				return ret;
			}

			public static bool IsNTTrust()
			{
                String ret = ConfigurationManager.AppSettings["NTTrustLogin"];
				if(ret == null)
				{
					return false;
				}
				else if(ret.ToUpper() == "TRUE")
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public static String GetTimeout()
			{
                String ret = ConfigurationManager.AppSettings["timeout"];
				if(ret == null)
				{
					return "30";
				}
				return ret;
			}

			public static bool GetShowMessage()
			{
                String ret = ConfigurationManager.AppSettings["showMessage"];
				if(ret == null)
				{
					return false;
				}
				else if(ret.ToUpper() == "TRUE")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
	}
}
