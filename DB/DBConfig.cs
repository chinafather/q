using System;
using System.Configuration;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// Config 的摘要说明。
	/// </summary>
	[Serializable]
	public class DBConfig
	{
		private String m_user;
		private String m_password;
		private String m_server;
		private String m_database;
		private String m_dbType = string.Empty;
		private String m_timeout = "30";
		private String m_dbConn;

		public String DbType
		{
			get{return m_dbType;}
		}

		public string DbUser
		{
			get{return m_user;}
		}
		public DBConfig(String user, String password, String server, String database, String type, String timeout)
		{
			m_user = user;
			m_password = password;
			m_server = server;
			m_database = database;
			m_dbType = type;
			m_timeout = timeout;
		}
		/// <summary>
		/// 通过输入关键子拼成配置
		/// </summary>
		/// <param name="dbServer"></param>
		/// <param name="dbName"></param>
		/// <param name="dbUser"></param>
		/// <param name="dbPassword"></param>
		/// <param name="timeout"></param>
		/// <param name="dbType"></param>
		public DBConfig(string dbUser,string dbPassword)
		{
			if(m_dbType == string.Empty)
			{
				GetConfigure();
			}
			m_user = dbUser;
			//m_dbType = stDbType;
			switch (m_dbType)
			{
				case "sqlserver":
					m_dbConn = string.Format ( "server={0};uid={1};password={2};database={3};connection timeout={4}" , m_server ,m_user ,m_password ,m_database , m_timeout) ;
					break;
				case "oracle":
					m_dbConn = string.Format( "Provider=MSDAORA.2;Data Source={0};User ID={1};Password={2};connection timeout={3}" , m_server ,m_user ,dbPassword ,m_database , m_timeout) ;
					break;
				case "access":
					break;
			}
		}

		protected  void GetConfigure()
		{
			if ( !GetWebConfigure())
				throw new Exception("Can't read configure or configure is error,you must config 'dsn','dbserver' and 'dbname'!");
		}

		/// <summary>
		/// 得到web程序的配置
		/// </summary>
		/// <returns></returns>
		protected bool GetWebConfigure()
		{
			try
			{
				//dsn一定要配置，但dbtype 不一定要配置
                string temp = ConfigurationManager.AppSettings["dsn"];
				if(temp == null || temp.Trim().Equals(string.Empty)) return false;
                string temp2 = ConfigurationManager.AppSettings["dbserver"];
				if(temp2 == null || temp2.Trim().Equals(string.Empty)) return false;
                string temp3 = ConfigurationManager.AppSettings["dbname"];
				if(temp3 == null || temp3.Trim().Equals(string.Empty)) return false;
                string temp4 = ConfigurationManager.AppSettings["password"];
				if(temp4 == null || temp4.Trim().Equals(string.Empty)) return false;
				m_dbConn = temp;
                string temp1 = ConfigurationManager.AppSettings["dbtype"];
				if (temp1 != null && temp1 != string.Empty)
					m_dbType = temp1;
				else
					m_dbType = "sqlserver";
                if ((m_timeout = Convert.ToString(ConfigurationManager.AppSettings["timeout"])) == "0")
					m_timeout = "30";
				m_server = temp2;
				m_database = temp3;
				m_password = temp4;
				return true;
			}
			catch
			{
				return false;
			}
		}

		private String ConnStr()
		{
			if(m_dbType == "sqlserver")
			{
				return String.Format("server={0};uid={1};password={2};database={3};Connect Timeout={4}",m_server,m_user,m_password,m_database, m_timeout);
			}
			else
			{
				//Modify by YangWJ 2003-10-20 for Oracle
				//return String.Format("Provider=MSDAORA.1;Data Source={0};User ID={1};Password={2}", m_database, m_user, m_password);
				//return String.Format("Provider=MSDAORA.2;Data Source={0};User ID={1};Password={2};Connect Timeout={3}", m_server, m_user, m_password, m_timeout);
				
				return String.Format("Provider=MSDAORA.2;Data Source={0};User ID={1};Password={2};Connection Timeout={3}", m_server, m_user, m_password,m_timeout);
			}
		}

		public Database GetDatabase()
		{
			if(m_dbType == "sqlserver")
			{
				
				return new SqlDatabase(ConnStr());
			}
			else
			{
				//return new OracleDatabase(ConnStr());
				return new SqlDatabase(ConnStr());
			}
		}

		public String User
		{
			get
			{
				return m_user;
			}
		}
	}
}
