using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// 封装了数据库的操作
	/// </summary>
	public class SqlDatabase : Database
	{
		private String _sConn;
		private SqlConnection _conn;
		private SqlTransaction _tran;
		private ArrayList _arrsql = new ArrayList();
		private int _transDeep = 0;

		public SqlDatabase(String sConn)
		{
			_sConn = sConn;
		}

        public SqlConnection getConnection()
		{
			if (_conn == null)
			{
				_conn = new SqlConnection(_sConn);
			}
			return _conn;
		}
        /// <summary>
        /// 事务开始
        /// </summary>
		public void BeginTransaction()
		{
			_transDeep ++;
		}
        /// <summary>
        /// 事务结束
        /// </summary>
		public void EndTransaction()
		{

            this._transDeep--;
            if (this._transDeep == 0)
            {
                SqlConnection connection = this.getConnection();
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                this._tran = this.getConnection().BeginTransaction();
                for (int i = 0; i < (this._arrsql.Count); i++)
                {
                    SqlCommand command = null;
                    command = (SqlCommand)this._arrsql[i];
                    command.Transaction = this._tran;
                    command.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException exception)
                    {
                        this._tran.Rollback();
                        this._arrsql.Clear();
                        this.getConnection().Close();
                        throw exception;
                    }
                }
                this._tran.Commit();
                this._arrsql.Clear();
                this.getConnection().Close();
            }

		}

		public DataView Query(String SqlText)
		{
			SqlDataAdapter da;
			DataSet ds;

			da = new SqlDataAdapter(SqlText, getConnection());

			ds = new DataSet();
			try
			{
				da.Fill(ds, "results");
			}
			catch(SqlException exp)
			{
				String errorMsg = exp.Message + " The Original Sql is " + SqlText;
				throw new Exception(errorMsg);
			}
			return ds.Tables["results"].DefaultView;
		}
		
		public DataView Query(String SqlText,int Top)
		{
			SqlDataAdapter da;
			DataSet ds;

			String sql = SqlText;

			//注意此方法有缺陷，sql语句中只能有一个select!
			if(sql.ToUpper().StartsWith("SELECT DISTINCT"))
			{
				string strTop = string.Format("SELECT DISTINCT TOP {0} ",Top);
				sql = sql.Trim().ToUpper().Replace("SELECT DISTINCT ",strTop);
			}
			else
			{
				string strTop = string.Format("SELECT TOP {0} ",Top);
				sql = sql.Trim().ToUpper().Replace("SELECT ",strTop);
			}

			da = new SqlDataAdapter(sql, getConnection());

			ds = new DataSet();
			try
			{
				da.Fill(ds, "results");
			}
			catch(SqlException exp)
			{
				String errorMsg = exp.Message + " The Original Sql is " + SqlText;
				throw new Exception(errorMsg);
			}
			
			return ds.Tables["results"].DefaultView;
		}
		
		public DataView Query(String sqlText, ArrayList pars)
		{
            SqlDataAdapter adapter = new SqlDataAdapter();
            string cmdText = sqlText;
            adapter.SelectCommand = new SqlCommand(cmdText, this.getConnection());
            adapter.SelectCommand.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            for (int i = 1; i < (pars.Count + 1); i++)
            {
                object obj2 = pars[i - 1];
                if (obj2 != null)
                {
                    SqlParameter parameter = new SqlParameter("@" + i, obj2);
                    parameter.IsNullable = true;
                    adapter.SelectCommand.Parameters.Add(parameter);
                }
                else
                {
                    cmdText = this.ReplaceNull(cmdText, "@" + i.ToString());
                }
            }
            adapter.SelectCommand.CommandText = cmdText;
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "result");
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return dataSet.Tables["result"].DefaultView;

		}

		public DataView Query(String sqlText, ArrayList pars,int Top)
		{
            SqlDataAdapter adapter = new SqlDataAdapter();
            string cmdText = sqlText;
            if (cmdText.ToUpper().StartsWith("SELECT DISTINCT"))
            {
                string newValue = string.Format("SELECT DISTINCT TOP {0} ", Top);
                cmdText = cmdText.Trim().ToUpper().Replace("SELECT DISTINCT ", newValue);
            }
            else
            {
                string str3 = string.Format("SELECT TOP {0} ", Top);
                cmdText = cmdText.Trim().ToUpper().Replace("SELECT ", str3);
            }
            adapter.SelectCommand = new SqlCommand(cmdText, this.getConnection());
            adapter.SelectCommand.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            for (int i = 1; i < (pars.Count + 1); i++)
            {
                object obj2 = pars[i - 1];
                if (obj2 != null)
                {
                    SqlParameter parameter = new SqlParameter("@" + i, obj2);
                    parameter.IsNullable = true;
                    adapter.SelectCommand.Parameters.Add(parameter);
                }
                else
                {
                    cmdText = this.ReplaceNull(cmdText, "@" + i.ToString());
                }
            }
            adapter.SelectCommand.CommandText = cmdText;
            DataSet dataSet = new DataSet();
            try
            {
                adapter.Fill(dataSet, "result");
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            return dataSet.Tables["result"].DefaultView;

		}

		public int Execute(String sqlText, ArrayList pars)
		{
            SqlConnection connection = this.getConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int num = 0;
            string cmdText = sqlText;
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            for (int i = 1; i < (pars.Count + 1); i++)
            {
                object obj2 = pars[i - 1];
                if (obj2 != null)
                {
                    SqlParameter parameter = new SqlParameter("@" + i, obj2);
                    command.Parameters.Add(parameter);
                }
                else
                {
                    cmdText = this.ReplaceNull(cmdText, "@" + i.ToString());
                }
            }
            command.CommandText = cmdText;
            if (this._transDeep > 0)
            {
                this._arrsql.Add(command);
                return num;
            }
            try
            {
                num = command.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return num;

			
		}

        public int Execute(String sqlText, IList<SqlParameter> pars)
        {
            SqlConnection connection = this.getConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            int num = 0;
            string cmdText = sqlText;
            SqlCommand command = new SqlCommand(cmdText, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            foreach (var item in pars)
	        {
                command.Parameters.Add(item);
	        }
            
            
            command.CommandText = cmdText;
            if (this._transDeep > 0)
            {
                this._arrsql.Add(command);
                return num;
            }
            try
            {
                num = command.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                throw exception;
            }
            finally
            {
                connection.Close();
            }
            return num;


        }
		/*public Object ExecProc(String procname,ArrayList pars)
		{
			//add by yangwj for return value
			bool _return;

			_return=false;
			SqlCommand comm;
			SqlConnection conn;
			Object result = null;
			conn = getConnection();

			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			comm = new SqlCommand(procname,conn);
			comm.CommandType = CommandType.StoredProcedure;
			if(_isInTransaction)
			{
				comm.Transaction = _tran;
			}
			DataView dv = GetParams(procname);
			int j=0;//输入参数值的第一个
			for(int i=0;i<dv.Count;i++)
			{
			
					if(dv[i]["xcolumn"].ToString().ToUpper()=="@RETVAL")
					{
						//输出参数

						_return=true;

						SqlParameter outpar = new SqlParameter(dv[i]["xcolumn"].ToString(),result);
						outpar.Direction = ParameterDirection.Output;
						comm.Parameters.Add(outpar);
					}
					else
					{
						//输入参数
						Object obj = (Object)pars[j];
						SqlParameter sparam = new SqlParameter(dv[i]["xcolumn"].ToString(),obj);
						sparam.Direction = ParameterDirection.Input;
						comm.Parameters.Add(sparam);
						j++;
					}
			}
			comm.ExecuteNonQuery();

			if (_return==true )
				return (long)comm.Parameters["@RetVal"].Value;
			else
				return result;
		}
		*/
		//add by YangWJ 知道的参数名及其值
		public Object ExecProc(string procname,Hashtable pars)
		{
			//add by yangwj for return value
			bool _return;

			SqlCommand comm;
			SqlConnection conn;
			conn = getConnection();

			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			comm = new SqlCommand(procname,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandTimeout =  int.Parse(Constant.GetTimeout()); 
			_return=CreateParameters(procname,comm,pars);
			
			if(_transDeep > 0)
			{
				
				_arrsql.Add(comm);
				return 0;

			}
			else
			{
				try
				{
					comm.ExecuteNonQuery();
				}
				catch(SqlException exp)
				{
					throw exp;
				}
				finally
				{
					conn.Close();
				}
				if (_return==true)
					if (Convert.IsDBNull(comm.Parameters["@Retval"].Value))
						return -1;
					else
						return (object)comm.Parameters["@Retval"].Value;
				else
					return 0;
			}
		}

		public Object ExecProc(String procname,ArrayList paraname,ArrayList paravalue,ArrayList sqldbtype)
		{
			
			bool _return = false;
			Object _RetVal = null;
			SqlCommand comm;
			SqlConnection conn;
			conn = getConnection();

			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			comm = new SqlCommand(procname,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandTimeout =  int.Parse(Constant.GetTimeout()); 
			
			
			int k = 0;
			SqlParameter outpar = null;
			for(int i = 0 ;i < paraname.Count ;i++)
			{
				if (k == 0)
				{
					if (paraname[i].ToString() != "output")
					{
						SqlParameter sparam = new SqlParameter(paraname[i].ToString(),paravalue[i]);
						sparam.Direction = ParameterDirection.Input;
						//sparam.DbType = sqldbtype[i].ToString() ;
						comm.Parameters.Add(sparam);								
					}
					else
					{
						k = 1;
					}
				}
				else
				{
					outpar = new SqlParameter(paraname[i].ToString(),SqlDbType.NVarChar,50);
					outpar.Direction = ParameterDirection.Output;
					comm.Parameters.Add(outpar);	
					_return = true;		
				}
			}
			
			//_return=CreateParameters(procname,comm,pars);

			
			if(_transDeep > 0)
			{
				
				_arrsql.Add(comm);
				return 0;

			}
			else
			{
				try
				{
					comm.ExecuteNonQuery();
					if (outpar.Value.ToString() != "")
					{
						_RetVal = outpar.Value;
					}
					else
					{
						_RetVal = "";
					}
				}
				catch(SqlException exp)
				{
					throw exp;
				}
				finally
				{
					conn.Close();
				}
				return _RetVal;
//				if (_return==true)
//					if (Convert.IsDBNull(comm.Parameters[paraname[i].ToString()].Value))
//						return -1;
//					else
//						return (object)comm.Parameters["@Retval"].Value;
//				else
//					return 0;
			}
		}

		private bool CreateParameters(string procName,SqlCommand comm,Hashtable ParamValue)
		{
			bool _return;
			string _paraname;
			int _paraSize;
			
			DataView dv=GetParams(procName);
			
			_return=false;
			for (int i=0;i<dv.Count;i++)
			{
				if (dv[i]["xcolumn"].ToString().ToUpper()=="@RETVAL")
				{
					//输出参数
					
					_return=true;

					SqlParameter outpar = new SqlParameter(dv[i]["xcolumn"].ToString(),SqlDbType.Int,4);
					outpar.Direction = ParameterDirection.Output ;
					comm.Parameters.Add(outpar);
				}
				else
				{
					//输入参数
					
					_paraname=dv[i]["xcolumn"].ToString().ToUpper();
					_paraSize=int.Parse(dv[i]["datawidth"].ToString());
					if (_paraSize==0)
						_paraSize=4;
					/// 101	smalldatetime  102	timestamp  103	datetime 
					/// 201	int 202	bit 203	tinyint 204	smallint 205	bigint  211	decimal  212	money  212	smallmoney  213	numeric  214	float  215	real
					/// 301	varchar  302	text   303	char   
					/// 401	nvarchar  402	ntext  403	nchar
					/// 501	image 
					/// 601	binary   602	varbinar
					
					SqlParameter sparam=new SqlParameter();
					sparam.ParameterName=dv[i]["xcolumn"].ToString();
					sparam.Size=_paraSize;
					sparam.Direction= ParameterDirection.Input;

					switch (int.Parse(dv[i]["xdatatype"].ToString()))
					{
						//DataTime 类型
						case 101:
							sparam.SqlDbType=SqlDbType.SmallDateTime;
							sparam.Value=DateTime.Parse(ParamValue[_paraname].ToString());
							break;
						case 102:
							sparam.SqlDbType=SqlDbType.Timestamp;
							sparam.Value=DateTime.Parse(ParamValue[_paraname].ToString());
							break;
						case 103:
							sparam.SqlDbType=SqlDbType.DateTime;
							sparam.Value=DateTime.Parse(ParamValue[_paraname].ToString());
							break;

						//数值类型
						case 201:
							sparam.SqlDbType=SqlDbType.Int;
							sparam.Value=int.Parse(ParamValue[_paraname].ToString());
							break;
						case 202:
							sparam.SqlDbType=SqlDbType.Bit;
							sparam.Value=bool.Parse(ParamValue[_paraname].ToString());
							break;
						case 203:
							sparam.SqlDbType=SqlDbType.TinyInt;
							sparam.Value=int.Parse(ParamValue[_paraname].ToString());
							break;
						case 204:
							sparam.SqlDbType=SqlDbType.SmallInt;
							sparam.Value=int.Parse(ParamValue[_paraname].ToString());
							break;
						case 205:
							sparam.SqlDbType=SqlDbType.BigInt;
							sparam.Value=Int32.Parse(ParamValue[_paraname].ToString());
							break;
						case 211:
							sparam.SqlDbType=SqlDbType.Decimal;
							sparam.Value=decimal.Parse(ParamValue[_paraname].ToString());
							break;
						case 212:
							sparam.SqlDbType=SqlDbType.Money;
							sparam.Value=decimal.Parse(ParamValue[_paraname].ToString());
							break;
						case 213:
							sparam.SqlDbType=SqlDbType.Decimal;
							sparam.Value=decimal.Parse(ParamValue[_paraname].ToString());
							break;
						case 214:
							sparam.SqlDbType=SqlDbType.Float;
							sparam.Value=float.Parse(ParamValue[_paraname].ToString());
							break;
						case 215:
							sparam.SqlDbType=SqlDbType.Real;
							sparam.Value=decimal.Parse(ParamValue[_paraname].ToString());
							break;

						//字符类型
						case 301:
							sparam.SqlDbType=SqlDbType.VarChar;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						case 302:
							sparam.SqlDbType=SqlDbType.Text;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						case 303:
							sparam.SqlDbType=SqlDbType.Char;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						
						//N字符类型
						case 401:
							sparam.SqlDbType=SqlDbType.NVarChar;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						case 402:
							sparam.SqlDbType=SqlDbType.NText;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						case 403:
							sparam.SqlDbType=SqlDbType.NChar;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
						
						//Image类型
						case 501:
							sparam.SqlDbType=SqlDbType.Image;
							sparam.Value=byte.Parse(ParamValue[_paraname].ToString());
							break;

						//Binary类型
						case 601:
							sparam.SqlDbType=SqlDbType.Binary;
							sparam.Value=byte.Parse(ParamValue[_paraname].ToString());
							break;
						case 602:
							sparam.SqlDbType=SqlDbType.VarBinary;
							sparam.Value=byte.Parse(ParamValue[_paraname].ToString());
							break;

						default:
							sparam.SqlDbType=SqlDbType.VarChar;
							sparam.Value=ParamValue[_paraname].ToString();
							break;
					}
					comm.Parameters.Add(sparam);
				}
			}

			return _return;
		}
		public Object ExecProc(String procname)
		{
			//add by yangwj for return value
			bool _return;

			_return=false;
			SqlCommand comm;
			SqlConnection conn;
			conn = getConnection();

			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			comm = new SqlCommand(procname,conn);
			comm.CommandType = CommandType.StoredProcedure;
			comm.CommandTimeout =  int.Parse(Constant.GetTimeout()); 
			DataView dv = GetParams(procname);
			if (dv.Count >0)
			{
				if(dv[0]["xcolumn"].ToString().ToUpper()=="@RETVAL")
				{
					//输出参数

					_return=true;

					SqlParameter outpar = new SqlParameter(dv[0]["xcolumn"].ToString().ToUpper(),SqlDbType.Int);
					
					outpar.Direction = ParameterDirection.Output;
					comm.Parameters.Add(outpar);
				}
			}
			
			if(_transDeep > 0)
			{
				//Modify by YangWJ 2003-11-06
				//comm.Transaction = _tran;
				_arrsql.Add(comm);
				return 0;
			}
			else
			{
				try
				{
					comm.ExecuteNonQuery();
				}
				catch(SqlException exp)
				{
					throw exp;
				}
				finally
				{
					conn.Close();
				}

				if (_return==true )
					return (int)comm.Parameters["@RETVAL"].Value;
				else
					return 0;
			}

		}
		private DataView GetParams(String procname)// 取得sp所有的参数
		{
			Database db = DatabaseFactory.GetDatabase();
			return db.Query("select * from skyobjitems where objname='"+procname+"' order by xorder");
		}
		//modify by yangwj
		//public void ExecProc(string procname)//
		//{
		//	SqlCommand da;
		//	SqlConnection conn;
		//	conn = getConnection();
		//	if(conn.State == ConnectionState.Closed)
		//	{
		//		conn.Open();
		//	}
		//	da = new SqlCommand(procname,conn);
		//	da.CommandType = CommandType.StoredProcedure;
		//	if(_isInTransaction)
		//	{
		//		da.Transaction = _tran;
		//	}
		//	da.ExecuteNonQuery();
		//	if(!_isInTransaction)
		//	{
		//		conn.Close();
		//	}	
		//}

		
		/// <summary>
		/// 执行sql脚本如insertupdatedelete
		/// </summary>
		/// <param name="sqlText"></param>
		/// <returns></returns>
		public int Execute(String sqlText)
		{
            int num = 0;
            SqlConnection connection = this.getConnection();
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand(sqlText, connection);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            if (this._transDeep > 0)
            {
                this._arrsql.Add(command);
                return num;
            }
            try
            {
                num = command.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                string str = sqlText;
                throw new Exception(exception.Message + " The Original Sql is " + str);
            }
            finally
            {
                connection.Close();
            }
            return num;
		}

        public IDataReader ExecuteReader(String sqlText, ArrayList pars)
		{
			SqlConnection conn = getConnection();
			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
			SqlCommand cmd = new SqlCommand(sqlText, conn);
            if (pars != null)//
            {
                for (int i = 1; i < pars.Count + 1; i++)
                {
                    Object obj = (Object)pars[i - 1];
                    cmd.Parameters.Add(new SqlParameter("@" + i, obj));
                }
            }
			return cmd.ExecuteReader();
		}
        public IDataReader ExecuteReader(String sqlText)
        {
            return ExecuteReader(sqlText, null);
        }

		public void CloseReader(IDataReader reader)
		{
			if(reader != null)
			{
				reader.Close();
			}
			getConnection().Close();
		}

		public DataView Query(String sqlText, ArrayList pars, int start, int length)
		{

            DataTable dt = GetDataTable(CommandType.Text, sqlText, pars, start, length);
            return dt.DefaultView; 
            //SqlConnection connection = this.getConnection();
            //if (connection.State == ConnectionState.Closed)
            //{
            //    connection.Open();
            //}
            //SqlCommand command = new SqlCommand(sqlText, connection);
            //command.CommandTimeout = Convert.ToInt32(Constant.GetTimeout());
            //for (int i = 1; i < (pars.Count + 1); i++)
            //{
            //    object obj2 = pars[i - 1];
            //    command.Parameters.Add(new SqlParameter("@" + i, obj2));
            //}
            //SqlDataReader reader = command.ExecuteReader();
            //DataView defaultView = reader.GetSchemaTable().DefaultView;
            //for (int j = 0; j < start; j++)
            //{
            //    if (!reader.Read())
            //    {
            //        return defaultView;
            //    }
            //}
            //for (int k = 0; k < length; k++)
            //{
            //    if (reader.Read())
            //    {
            //        DataRowView view2 = defaultView.AddNew();
                    
            //        for (int m = 0; m < reader.FieldCount; m++)
            //        {
            //            view2[m] =  reader[m];
            //        }
            //    }
            //}
            //reader.Close();
            //this.getConnection().Close();
            //return defaultView;

		}

		public override String ToString()
		{
			return _sConn;
		}


		public virtual DataSet GetDataSet(string[] sqlText)
		{
			try
			{
				getConnection();
				using(DataSet ds = new DataSet())
				{
					using (	SqlDataAdapter da = new SqlDataAdapter())
					{
						using(SqlCommand comm = new SqlCommand())
						{
							for(int i = 0;i < sqlText.Length ; i ++ )
							{
								comm.CommandText = sqlText[i];
								comm.Connection = _conn;
								da.SelectCommand = comm;
								da.Fill(ds, "results" + i);
							}
							return ds;
						}
					}
				}
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
				getConnection().Close();
			}
		}
		public virtual DataSet GetDataSet(string[] sqlText,ArrayList[] pars)
		{
			try
			{
				getConnection();
				using(DataSet ds = new DataSet())
				{
					using (	SqlDataAdapter da = new SqlDataAdapter())
					{
						using(SqlCommand comm = new SqlCommand())
						{
							for(int i = 0;i < sqlText.Length ; i ++ )
							{
								comm.CommandText = sqlText[i];
								comm.Connection = _conn;
								da.SelectCommand = comm;
								SetParameters(comm,pars[i]);
								da.Fill(ds, "results" + i);
							}
							return ds;
						}
					}
				}
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
				getConnection().Close();
			}		
		}

		/// <summary>
		/// 为查询语句获取相关参数
		/// </summary>
		/// <param name="comm"></param>
		/// <param name="pars"></param>
		protected virtual void SetParameters(SqlCommand comm,ArrayList pars)
		{
			for(int i = 1; i < pars.Count + 1; i ++)
			{
				Object obj = (Object)(pars[i - 1]);
				if(obj != null && obj != DBNull.Value)
				{
					SqlParameter par = new SqlParameter("@" + i, obj);
					comm.Parameters.Add(par);
				}
				else
				{
					SqlParameter par = new SqlParameter("@" + i, DBNull.Value);
					par.IsNullable = true;
					comm.Parameters.Add(par);
				}
			}		
		}

		/// <summary>
		/// 得到一个object值，是当前查询结果的第一行第一列的结果，无参数
		/// </summary>
		/// <param name="sqlText"></param>
		/// <returns></returns>
		public virtual Object GetObject(string sqlText)
		{
            IDbConnection con =  getConnection();
			try
			{
				Object result = "";
                con.Open();
				using(SqlCommand comm = new SqlCommand(sqlText,_conn))
				{
					comm.CommandType = CommandType.Text;
					result = comm.ExecuteScalar();
				}
				return result;
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
                con.Close();
			}		
		}

		/// <summary>
		/// 得到一个object值，是当前查询结果的第一行第一列的结果，带参数
		/// </summary>
		/// <param name="sqlText"></param>
		/// <param name="pars"></param>
		/// <returns></returns>
		public virtual Object GetObject(string sqlText,ArrayList pars)
		{
			try
			{
				Object result = "";
				getConnection();
				using(SqlCommand comm = new SqlCommand(sqlText,_conn))
				{
					comm.CommandType = CommandType.Text;
					SetParameters(comm,pars);
					result = comm.ExecuteScalar();
				}
				return result;
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
				getConnection().Close();
			}			
		}

		/// <summary>
		/// 得到一个xml数据
		/// </summary>
		/// <param name="sqlText"></param>
		/// <returns></returns>
		public virtual XmlReader GetXmlReader(string sqlText)
		{
			XmlReader reader = null;
			try
			{
				string temp = sqlText.ToLower().Replace(" ","");
				if (temp.LastIndexOf("forxml") == -1)   //如果sql语句中没有for xml关键字，则加上
					sqlText += " FOR XML AUTO";
				else if(temp.LastIndexOf("auto") == -1)
					sqlText += " AUTO ";
				getConnection();
				using(SqlCommand comm = new SqlCommand(sqlText,_conn))
				{
					comm.CommandType = CommandType.Text;
					reader = comm.ExecuteXmlReader();
				}
				return reader;
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
				if (reader != null)
					reader.Close();
				getConnection().Close();
			}			
		}

		/// <summary>
		///  得到一个xml数据，带参数
		/// </summary>
		/// <param name="sqlText"></param>
		/// <param name="pars"></param>
		/// <returns></returns>
		public virtual XmlReader GetXmlReader(string sqlText,ArrayList pars)
		{
			XmlReader reader = null;
			try
			{
				//如果sql语句中没有for xml auto关键字，则加上
				string temp = sqlText.ToLower().Replace(" ","");
				if (temp.LastIndexOf("forxml") == -1)   
					sqlText += " FOR XML AUTO";
				else if(temp.LastIndexOf("auto") == -1)
					sqlText += " AUTO ";
				getConnection();
				using(SqlCommand comm = new SqlCommand(sqlText,_conn))
				{
					comm.CommandType = CommandType.Text;
					SetParameters(comm,pars);
					reader = comm.ExecuteXmlReader();
				}
				return reader;
			}
			catch(SqlException exp)
			{
				throw exp;
			}
			finally
			{
				if (reader != null)
					reader.Close();
				getConnection().Close();
			}			
		}


        /// <summary>
        /// 获取指定分页的表指定字段或条件的纪录
        /// </summary>
        /// <param name="strSelectSql">自定义的查询语句</param>
        /// <param name="tableName">表名</param>
        /// <param name="startIndex">开始记录的索引号，索引从0开始</param>
        /// <param name="recordCount">要取记录的条数，如果 recordCount 大于剩余行的数目，则仅返回剩余的行并且不引发任何错误。</param>
        /// <returns></returns>
        public DataTable GetPagedRecords(string strSelectSql, int startIndex, int recordCount)
        {
            SqlCommand sqlCommand = new SqlCommand(strSelectSql, getConnection());
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            DataSet ds = new DataSet();
            da.Fill(ds, startIndex, recordCount, "dtPage");

            return ds.Tables[0];
        }


        /// <summary>
        /// 获取指定分页的表指定字段或条件的纪录
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="sqlParams"></param>
        /// <param name="strSelectSql"></param>
        /// <param name="tableName"></param>
        /// <param name="startIndex"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetDataTable(CommandType cmdType, string strSelectSql,ArrayList sqlParams, int startIndex, int recordCount)
        {
            
            SqlCommand sqlCommand = new SqlCommand(strSelectSql, getConnection());
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            sqlCommand.CommandType = cmdType;

            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Count; i++)
                {
                    sqlCommand.Parameters.Add(sqlParams[i]);
                }
            }

            DataSet ds = new DataSet();
            if (recordCount == 0 && startIndex == 0)
                da.Fill(ds, "dtPage");
            else
                da.Fill(ds, startIndex, recordCount, "dtPage");

            return ds.Tables[0];
        }
        public DataTable GetDataTable(CommandType cmdType, string strSelectSql,ArrayList sqlParams)
        {
            return GetDataTable(cmdType, strSelectSql,sqlParams, 0,0);
        }
        public DataTable GetDataTable(CommandType cmdType, string strSelectSql)
        {
            return GetDataTable(cmdType, strSelectSql, new ArrayList());
        }

        public DataTable GetDataTableSchema(CommandType cmdType, string strSelectSql, ArrayList sqlParams, SchemaType schemaType) 
        {
            SqlCommand sqlCommand = new SqlCommand(strSelectSql, getConnection());
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            sqlCommand.CommandType = cmdType;

            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Count; i++)
                {
                    sqlCommand.Parameters.Add(sqlParams[i]);
                }
            }

            DataSet ds = new DataSet();

            da.FillSchema(ds, schemaType);
          
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据命令方式和sql参数对数据库进行更新
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="sqlParams"></param>
        /// <param name="strSql"></param>
        /// <param name="EnableTransaction">是否启动事务</param>
        /// <returns></returns>
        public int ExecuteByParametersNonQuery(CommandType cmdType, ArrayList sqlParams, string strSql, bool EnableTransaction)
        {
            SqlConnection conn;
            conn = getConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(strSql, conn);

            sqlCommand.CommandType = cmdType;

            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Count; i++)
                {
                    sqlCommand.Parameters.Add(sqlParams[i]);
                }
            }
            if (EnableTransaction)
            {
                SqlTransaction sqlTrans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                sqlCommand.Transaction = sqlTrans;
                try
                {
                    int effectRows = sqlCommand.ExecuteNonQuery();
                    sqlTrans.Commit();
                    return effectRows;
                }
                catch (SqlException ex)
                {
                    sqlTrans.Rollback();
                    throw ex;
                }
            }
            else
            {
                return sqlCommand.ExecuteNonQuery();
            }
            
        }

        /// <summary>
        /// 获得多个结果集
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="sqlParams"></param>
        /// <param name="strSelectSql"></param>
        /// <returns></returns>
        public DataSet GetMultiDataTable(CommandType cmdType, ArrayList sqlParams, string strSelectSql)
        {
            SqlConnection conn = getConnection();
            SqlCommand sqlCommand = new SqlCommand(strSelectSql, conn);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            sqlCommand.CommandType = cmdType;

            if (sqlParams != null)
            {
                for (int i = 0; i < sqlParams.Count; i++)
                {
                    sqlCommand.Parameters.Add(sqlParams[i]);
                }
            }

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }


        public string ReplaceNull(string sql, string strpos)
        {
            sql = Regex.Replace(sql, strpos + "([^0-9])", "null$1");
            sql = Regex.Replace(sql, strpos + "$", "null");
            return sql;
        }

 


	}
}
