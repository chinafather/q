using System;
using System.Collections;
using System.Data;
using System.Text.RegularExpressions;

namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// Query 的摘要说明。
	/// </summary>
	public class Query
	{
		private ArrayList m_params;
		private String m_sql;
		private int m_type;
		private String m_column = "";
		private ArrayList m_filters;
		private String[] esfilters = {"MYBADGE", "MYDEPID", "MYJOBID", "EMPF", "DEPF", "JOBF", "EXTF1", "EXTF2"};

		//当needSelect = true时，需要
		public Query(String p)
		{
			m_params = new ArrayList();
			m_filters = new ArrayList();
			m_sql = p.ToUpper();
			if(m_sql.StartsWith("SELECT EXEC"))
			{
				//去掉'SELECT EXEC("'...'")'
				m_sql = m_sql.Substring(13, m_sql.Length - 15);
			}
			if(m_sql.StartsWith("EXEC"))
			{
				//去掉'EXEC("'...'")'
				m_sql = m_sql.Substring(6, m_sql.Length - 8);
			}
			//select
			if(m_sql.IndexOf("SELECT") != -1 || m_sql.StartsWith("UPDATE") || m_sql.StartsWith("DELETE") || m_sql.StartsWith("INSERT"))
			{
				/*bool containFilter = false;
				for(int i = 0; i < esfilters.Length; i ++)
				{
					if(m_sql.IndexOf("{" + esfilters[i] + "}") != -1)
					{
						containFilter = true;
						break;
					}
				}*/
				//if(containFilter)
				//{
					ArrayList tempArray = new ArrayList();
					//with filter
					//m_type = 7;
					m_type = 0;

					//将select * from abc where a in ({filter.xxx})改为select * from abc where a in ({0})
					//并将其他{}存入m_params（用于GetParameters()）
					int i = 1;
					int j = 0;
					//将select * from abc where a = {xx}改为select * from abc where a = @1
					while(m_sql.IndexOf("{") != -1)
					{
						//将select * from abc where a = {xx}改为select * from abc where a = @1
						int start = m_sql.IndexOf("{");
						int end = m_sql.IndexOf("}");
						String temp = m_sql.Substring(start, end - start + 1);
						int k = filterPos(temp);
						//mybadge, mydepid, myjobid或参数
						if(k < 3)
						{
							m_sql = m_sql.Replace(temp, "@" + i);
							tempArray.Add(temp);
							i ++;
						}
						else
						{
							m_filters.Add(new Query(temp));
							m_sql = m_sql.Replace(temp, "$" + j + "$");
							j ++;
							m_type = 7;
						}
					}
					for(i = 0; i < m_filters.Count; i ++)
					{
						m_sql = m_sql.Replace("$" + i + "$", "{" + i + "}");
					}
				if(m_type == 7)
				{
					//替代回来，以便下一步递归时仍能得到参数
					for(i = tempArray.Count; i > 0; i --)
					{
						m_sql = m_sql.Replace("@" + i, (String)tempArray[i - 1]);
					}
					tempArray.Clear();
				}
				else
				{
					for(i = 0; i < tempArray.Count; i ++)
					{
						m_params.Add(new Query((String)tempArray[i]));
					}
				}
				//}
				/*else
				{
					//select
					m_type = 0;
					int i = 1;
					//将select * from abc where a = {xx}改为select * from abc where a = @1
					while(m_sql.IndexOf("{") != -1)
					{
						//将select * from abc where a = {xx}改为select * from abc where a = @1
						int start = m_sql.IndexOf("{");
						int end = m_sql.IndexOf("}");
						String temp = m_sql.Substring(start, end - start + 1);
						Query q = new Query(temp);
						m_sql = m_sql.Replace(temp, "@" + i);
						m_params.Add(new Query(temp));
						i ++;
					}
					
				}*/
			}
			else
			{
				if(m_sql.IndexOf("{") != -1)
				{
					if(m_sql.IndexOf("[") == - 1)
					{
						//parameter
						m_type = 2;
					}
						//数据源中获取
					else
					{
						//get from datasource
						if(m_sql.IndexOf(".") > -1)
						{
							m_type = 9;
						}
							//get from current datasource
						else
						{
							m_type = 1;
						}
						m_column = m_sql.ToUpper();
					}
				}
				else
				{
					//常数
					m_type = 4;
				}
			}


		}
		//execute sql
		public Query(String p, int type)
		{
			m_type = 3;
			m_params = new ArrayList();
			m_filters = new ArrayList();
			m_sql = p.ToUpper();
			bool containFilter = false;
			for(int i = 0; i < esfilters.Length; i ++)
			{
				if(m_sql.IndexOf("{" + esfilters[i] + "}") != -1)
				{
					containFilter = true;
					break;
				}
			}
			if(containFilter)
			{
				//with filter
				m_type = 8;
				//将select * from abc where a in ({filter.xxx})改为select * from abc where a in ({0})
				//并将其他{}存入m_params（用于GetParameters()）
				//?表示最小匹配，这样不会返回'{filter.empf} and badge = {badge}'的形式而只会返回{filter.empf}
				Regex regex = new Regex("{*?}");
				System.Text.RegularExpressions.Match match = regex.Match(m_sql);
				for(int i = 0; match.Groups[i].Value != ""; i ++)
				{
					String temp = match.Groups[i].Value;
					bool isFilter = false;
					for(int k = 0; k < esfilters.Length; k ++)
					{
						if(temp.ToUpper() == esfilters[k])
						{
							isFilter = true;
							break;
						}
					}
					if(isFilter)
					{
						m_filters.Add(new Query(temp));
						m_sql = m_sql.Replace(temp, "{" + i + "}");
					}
					else
					{
						m_params.Add(new Query(temp));
					}
				}
			}
			else
			{
				//select
				m_type = 3;
				int i = 1;
				//将select * from abc where a = {xx}改为select * from abc where a = @1
				while(m_sql.IndexOf("{") != -1)
				{
					//将select * from abc where a = {xx}改为select * from abc where a = @1
					int start = m_sql.IndexOf("{");
					int end = m_sql.IndexOf("}");
					String temp = m_sql.Substring(start, end - start + 1);
					Query q = new Query(temp);
					m_sql = m_sql.Replace(temp, "@" + i);
					m_params.Add(new Query(temp));
					i ++;
				}
					
			}
		}

		public ArrayList getParameters()
		{
			return m_params;
		}

		public String GetSql()
		{
			return m_sql;
		}

		public Object GetValue(DBConfig cfg, IContext ctx)
		{
			switch(m_type)
			{
				case 0:
					ArrayList param = new ArrayList();
					for(int i = 0; i < m_params.Count; i ++)
					{
						Query q = (Query)m_params[i];
						param.Add(q.GetValue(cfg, ctx));
					}
					Database db = cfg.GetDatabase();
					DataView dv = db.Query(m_sql, param);
					if(dv.Count == 0)
					{
						return null;
					}
					else
					{
						return dv[0][0];
					}
				case 1:
					return ctx.GetValue(cfg, m_sql);
				case 2:
					return ctx.GetValue(cfg, m_sql);
				case 4:
					return m_sql;
				case 9:
					return ctx.GetValue(cfg, m_sql);
				case 7:
					String sql = m_sql;
					for(int i = 0; i < m_filters.Count; i ++)
					{
						String temp = "{" + i + "}";
						Query q = (Query)m_filters[i];
						String filter = (String)q.GetValue(cfg, ctx);
						sql.ToUpper().Replace(temp, filter);
					}
					Query q1 = new Query(sql);
					return q1.GetValue(cfg, ctx);
				default:
					return null;
			}
		}

		public DataView GetData(DBConfig cfg, IContext ctx)
		{
			Database db = cfg.GetDatabase();
			if(m_type == 0)
			{
				ArrayList param = new ArrayList();
				for(int i = 0; i < m_params.Count; i ++)
				{
					Query q = (Query)m_params[i];
					param.Add(q.GetValue(cfg, ctx));
				}
				DataView dv = db.Query(m_sql, param);
				return dv;
			}
			else if(m_type == 7)
			{
				String sql = m_sql;
				for(int i = 0; i < m_filters.Count; i ++)
				{
					String temp = "{" + i + "}";
					Query q = (Query)m_filters[i];
					String filter = (String)q.GetValue(cfg, ctx);
					if(filter == null)
					{
						filter = "null";
					}
					sql = sql.ToUpper().Replace(temp, filter);
				}
				Query q1 = new Query(sql);
				return q1.GetData(cfg, ctx);
			}
			else
			{
				return null;
			}
		}

		public void Execute(DBConfig cfg, IContext ctx, ArrayList par)
		{
			Database db = cfg.GetDatabase();
			if(m_type != 7)
			{
				ArrayList param = new ArrayList();
				for(int i = 0; i < m_params.Count; i ++)
				{
					Query q = (Query)m_params[i];
					param.Add(q.GetValue(cfg, ctx));
				}
				param.AddRange(par);
				db.Execute(m_sql, param);
			}
			else if(m_type == 7)
			{
				String sql = m_sql;
				for(int i = 0; i < m_filters.Count; i ++)
				{
					String temp = "{" + i + "}";
					Query q = (Query)m_filters[i];
					String filter = (String)q.GetValue(cfg, ctx);
					sql.ToUpper().Replace(temp, filter);
				}
				Query q1 = new Query(sql);
				q1.Execute(cfg, ctx, par);
			}
		}

		public new int GetType()
		{
			return m_type;
		}

		/// <summary>
		/// 递归获取某种类型的参数，用于控件连锁的情况
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public ArrayList GetSelectedParameters(int type)
		{
			ArrayList ret = new ArrayList();
			if(m_type == 7 || m_type == 8 || m_type == 3 || m_type == 0)
			{
				for(int i = 0; i < m_params.Count; i ++)
				{
					Query q = (Query)m_params[i];
					ret.AddRange(q.GetSelectedParameters(type));
				}
			}
			else
			{
				if(m_type == type)
				{
					ret.Add(m_sql.ToUpper());
				}
			}
			return ret;
		}

		//是否是esfilters中的一个，是就返回对应的需要，否就返回-1
		private int filterPos(String s)
		{
			for(int k = 0; k < esfilters.Length; k ++)
			{
				if(s.ToUpper() == "{" + esfilters[k] + "}")
				{
					return k;
				}
			}
			return -1;
		}

	}
}
