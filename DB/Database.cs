using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// 封装了数据库的操作
	/// </summary>
	public interface Database
	{

		void BeginTransaction();

		void EndTransaction();
	
		DataView Query(String SqlText);
		DataView Query(String SqlText,int Top);
		DataView Query(String sqlText, ArrayList pars);
		DataView Query(String sqlText, ArrayList pars,int Top);
		
		int Execute(String sqlText, ArrayList pars);
        int Execute(String sqlText, IList<SqlParameter> pars);
		int Execute(String sqlText);

		Object ExecProc(string procname);

		//Object ExecProc(string procname,ArrayList pars);
		
		Object ExecProc(string procname,Hashtable pars);
		
		Object ExecProc(string procname,ArrayList paraname,ArrayList paravalue,ArrayList sqldbtype);
        IDataReader ExecuteReader(String sqlText, ArrayList pars);
        IDataReader ExecuteReader(String sqlText);

		DataView Query(String sqlText, ArrayList pars, int start, int length);

		DataSet GetDataSet(string[] sqlText);
		DataSet GetDataSet(string[] sqlText,ArrayList[] pars);

		Object GetObject(string sqlText);
		Object GetObject(string sqlText,ArrayList pars);

		XmlReader GetXmlReader(string sqlText);
		XmlReader GetXmlReader(string sqlText,ArrayList pars);


        DataTable GetPagedRecords(string strSelectSql, int startIndex, int recordCount);
        DataTable GetDataTable(CommandType cmdType, string strSelectSql,ArrayList sqlParams,  int startIndex, int recordCount);
        DataTable GetDataTable(CommandType cmdType, string strSelectSql,ArrayList sqlParams);
        DataTable GetDataTable(CommandType cmdType, string strSelectSql);
        DataTable GetDataTableSchema(CommandType cmdType, string strSelectSql, ArrayList sqlParams, SchemaType schemaType);
        int ExecuteByParametersNonQuery(CommandType cmdType, ArrayList sqlParams, string strSql, bool EnableTransaction);
        DataSet GetMultiDataTable(CommandType cmdType, ArrayList sqlParams, string strSelectSql);

	}
}
