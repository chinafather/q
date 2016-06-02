using System;
using System.Collections;
using System.Text.RegularExpressions ;
using System.Data;
using Weegle.FrameWork.WeegleFrameWork ;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// SqlAnalyse 的摘要说明。
	/// </summary>
	public class SqlAnalyse : ISqlAnalyse
	{
		protected string _sql = "";
		protected ArrayList _value = new ArrayList();
		protected int _type = 0;

		public SqlAnalyse(string sql)
		{
			//0为sql语句，1为有参数的句子，2为常量
			_sql = sql;
			sql = sql.Trim().ToLower();
			if (sql.StartsWith("exec")  || sql.StartsWith("update") || sql.StartsWith("delete") || sql.StartsWith("declare"))
			{
				_type = 3;
			}
			else if(sql.StartsWith("select"))
			{
				_type = 0;
			}
			else if( sql.IndexOf("{") != -1)
			{
				_type = 1;
			}
			else
			{
				_type = 2;
			}
            
		}

		public SqlAnalyse(string sql,int type)
		{
			_sql = sql;
			_type = type;
		}

        public virtual DataView GetData(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
		{
			if(_type == 0)
			{
				Database db = DatabaseFactory.GetDatabase() ;
				string sql = AnalyseSql( cfg,ctx ) ;
				if( _value.Count > 0 )
				{
					return db.Query( sql , _value ) ;
				}
				else if( sql.ToLower().IndexOf( "select") != -1)
				{
					return db.Query( sql);
				}
				else
				{
					return new DataView();
				}
			}
			else
			{
				return new DataView();
			}
		}
        public virtual DataSet GetQueryData(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
        {
            if (_type == 0)
            {
                Database db = DatabaseFactory.GetDatabase();
                string sql = AnalyseSql(cfg, ctx);
                String[] temp = sql.Split(';');
                DataSet ds = new DataSet();
                if (sql.ToLower().IndexOf("select") != -1)
                {
                    return db.GetDataSet(temp);
                }
                else
                {
                    return new DataSet();
                }
            }
            else
            {
                return new DataSet();
            }
        }
        public virtual DataView GetDataSchema(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
        {
            if (_type == 0)
            {
                Database db = DatabaseFactory.GetDatabase();
                string sql = AnalyseSql(cfg, ctx);

                if (_value.Count > 0)
                {
                    return db.GetDataTableSchema(CommandType.Text, sql, _value, SchemaType.Source).DefaultView; 
                    //return db.Query(sql, _value);
                }
                else if (sql.ToLower().IndexOf("select") != -1)
                {
                    //return db.Query(sql);
                    return db.GetDataTableSchema(CommandType.Text, sql, new ArrayList(), SchemaType.Source).DefaultView; 
                }
                else
                {
                    return new DataView();
                }
            }
            else
            {
                return new DataView();
            }
        }

        public virtual object ExcuteSql(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
		{
			if(_type == 3)
			{
				Database db = DatabaseFactory.GetDatabase() ;
				string sql = AnalyseSql(cfg,ctx ) ;
				if( _value.Count > 0 )
				{
					return db.Execute(sql , _value) ;
				}
				else 
				{
					return db.Execute( sql) ;
				}
			}
			else
			{
				return -1;
			}		
		}

		/// <summary>
		/// {xxx} 为从全局信息类如session里来的值
		/// {[xxx]}为从当前操作的页面来的值
		/// </summary>
		/// <param name="key"></param>
		/// <param name="ctx"></param>
		/// <returns></returns>
        public virtual object GetValue(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
		{
			object getValue = null ;
			switch( _type)
			{
				case 0:
					Database db = DatabaseFactory.GetDatabase() ;
					string sql = AnalyseSql (cfg,ctx ) ;
					if( _value.Count > 0 )
					{
						return db.GetObject( sql , _value ) ;
					}
					else if( sql.ToLower().IndexOf( "select") != -1)
					{
						return db.GetObject( sql);
					}
					else
					{
						return null;
					}
				case 1:
					Regex regex = new Regex( @"{\[?\w+\]?}" );
					System.Text.RegularExpressions.Match match = regex.Match( _sql );
					string temp = "";
					while (true)
					{
						temp = match.Value;
						if(temp == "") break;
						_sql = _sql.Replace( temp ,Convert.ToString(ctx.GetValue(temp)) ) ;
						match = match.NextMatch();
					}
					return _sql;
				case 2:
					return _sql;
				case 3:
					return ExcuteSql(cfg,ctx);
				default:
					return getValue;
			}
		}

        protected virtual string AnalyseSql(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx)
		{
			string sql = _sql;
			Regex regex = new Regex( @"{\[?\w+\]?}" );
			System.Text.RegularExpressions.Match match = regex.Match( sql );
			string temp = "";
			int j = 1;
			_value.Clear();
			while (true)
			{
				temp = match.Value;
				if(temp == "") break;
				sql = sql.Replace( temp ,"@" + j ) ;
				_value.Add( ctx.GetValue(temp) ) ;
				match = match.NextMatch();
				j ++ ;
			}
			return sql;
		}
	}
}
