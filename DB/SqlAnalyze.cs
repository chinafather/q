using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Weegle.FrameWork.DB
{
	public	struct SelAlias
	{
		public string tablename;//表名
		public string col;//列名
		public string asAlias;//列的别名
		public SelAlias(string a,string b,string c)
		{
			tablename = a;
			col = b;
			asAlias =c;
		}
		public void settable(string x)
		{
			tablename = x;
		}
		public void setcol(string x)
		{
			col=x;
		}
	} 
	/// <summary>
	/// SqlAnalyze 的摘要说明。
	/// </summary>
	public class SqlAnalyze
	{
		public SqlAnalyze()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		public  ArrayList AnalyzeSelect(string sqlstr)
		{
			string str = sqlstr.ToUpper();
			int start = 7;
			int end = str.IndexOf(" FROM ");
			string WhatSelect = str.Substring(start,end-start);
			int havewhere = str.LastIndexOf("WHERE");
			int haveorder = str.LastIndexOf("ORDER BY");
			string from_where = "";
			if(havewhere != -1)
			{//有where的情况
				from_where = str.Substring(end+6,havewhere-1-end-6);
			}
			else
			{//没有where的情况
				if(haveorder != -1)
				{//有order by 的情况
					from_where = str.Substring(end+6,haveorder-1-end-6);
				}
				else
				{//没有order by的情况
					from_where = str.Substring(end+6,str.Length-end-6);
				}
			}
			string[] fromtable = from_where.Split(',');
			Hashtable TableAlias = new Hashtable();
			for(int j = 0;j<fromtable.Length;j++)
			{
				int havealias = fromtable[j].Trim().IndexOf(' ');
				if(havealias != -1)
				{//有别名
					string[] table = fromtable[j].Trim().Split(' ');
					TableAlias.Add((Object)table[1],(Object)table[0]);
				}
				else
				{//无别名
					TableAlias.Add((Object)fromtable[j],(Object)fromtable[j]);
				}
			}
			//string WhatSelect = str.Substring(start,end-start);
			string[] Select = WhatSelect.Trim().Split(',');
			ArrayList  tt = new ArrayList();
			for(int i=0;i<Select.Length;i++)
			{
				int isas = Select[i].IndexOf(" as ");
				if(isas != -1 )
				{
					string asfirst = Select[i].Substring(0,isas);
					string asname = Select[i].Substring(isas+3,Select[i].Length-isas-4);
					string[] Col = asfirst.Split('.');
					SelAlias sa;
					if(Col.Length > 1)
					{
						sa = new SelAlias(Col[0],Col[1],asname);
					}
					else
					{
						if(fromtable.Length > 1)
						{
							throw new Exception("无法区分column:" + Col[0] + "所属的table!");
						}
						else
						{
							string[] table = fromtable[0].Trim().Split(' ');
							sa = new SelAlias(table[0],Col[0],asname);
						}
					}
					tt.Add(selecttable(sa,TableAlias));
				}
				else
				{
					string[] Col = Select[i].Split('.');
					SelAlias sa;
					//有前缀
					if(Col.Length > 1)
					{
						sa = new SelAlias(Col[0],Col[1],"");
					}
					else
					{
						if(fromtable.Length > 1)
						{
							throw new Exception("无法区分column:" + Col[0] + "所属的table!");
						}
						else
						{
							string[] table = fromtable[0].Trim().Split(' ');
							sa = new SelAlias(table[0],Col[0],"");
						}
					}

					tt.Add(selecttable(sa,TableAlias));
				}
			}
			ArrayList result = new ArrayList();
			for(int i=0;i<tt.Count;i++)
			{
				if(((SelAlias)tt[i]).col.ToString().Trim()=="*")
				{
					ArrayList tempx = GetColumn(((SelAlias)tt[i]).tablename.ToString().Trim());
					for(int j=0;j<tempx.Count;j++)
					{
						result.Add(new SelAlias(((SelAlias)tt[i]).tablename.ToString().Trim(),tempx[j].ToString().Trim(),((SelAlias)tt[i]).asAlias.ToString().Trim()));
					}
				}
				else
				{
					result.Add(tt[i]);
				}
			}
			return result;//返回一个SelAlias结构类型的ArrayList
		}
		private static SelAlias selecttable(SelAlias name,Hashtable ht)
		{//将列的别名改成表名

			IDictionaryEnumerator myEnumerator = ht.GetEnumerator();
			while(myEnumerator.MoveNext())
			{
				if(name.tablename.ToString() == myEnumerator.Key.ToString().Trim())
				{
					name.settable(myEnumerator.Value.ToString().Trim());
				}
			}
			return name;
		}
		private static ArrayList GetColumn(string tablename)//取得*的所有列名
		{
			ArrayList r = new ArrayList();
		    Database db = DatabaseFactory.GetDatabase();
			DataView dv = db.Query("select xColumn from objitems where objname='"+tablename+"'");
			foreach(DataRowView drv in dv)
			{
				r.Add(drv[0].ToString());
			}
			return r;

		}
	}
}
