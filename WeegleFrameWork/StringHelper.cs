 //****************************************************************************
 //** 文件名: GUIDHelp.cs	                                          
 //**         Copyright 2005 by Shanghai HJ Software System Co.,Ltd            
 //**         All Rights Reserved 	                                          
 //** 创建人: Hovering	                                                      
 //** 日 期 : 2006-2-7 
 //** 修改人:	                                                              
 //** 日 期 :	                                                              
 //** 描 述 :	                                                              
 //**       
 //** 版 本 :		                                                              
 //****************************************************************************
using System;
using System.Web;
namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// StringHelper 的摘要说明。
	/// </summary>
	public class StringHelper
	{
		public StringHelper()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
        /// <summary>
        /// 防Sql注入式攻击
        /// </summary>
        /// <param name="strValue">sql脚本中的内容，不是sql脚本本身</param>
        /// <returns>0：正常; 1：不能使用系统保留字符; 2：不能使用非法字符 </returns>
        public static int ProtectSql(String strValue)
        {
            String[] strKeys = new String[] { "and", "exec", "insert", "select", "delete", "update", "count", "chr", "mid", "master", "truncate", "char", "declare"};
            String[] strInvaild = new String[] { "*", "%", "'" }; 
            for (int j = 0; j < strKeys.Length; j++)
            {
                if (strValue.ToLower().IndexOf(strKeys[j]) >= 0)
                {
                    return 1;
                }
            }
            for (int j = 0; j < strInvaild.Length; j++)
            {
                if (strValue.ToLower().IndexOf(strInvaild[j]) >= 0)
                {
                    return 2;
                }
            }
            return 0;
        }

        /// <summary>
        /// 对页面提交的内容进行防止sql注入式攻击判断
        /// </summary>
        /// <param name="request">页面的request</param>
        /// <param name="response">页面的response</param>
        public static void ProtectInput(HttpRequest request,HttpResponse response)
        {
            try
            {
                String[] strKeys = new String[] { "exec ", "insert ", "select ", "delete ", "update ", "count", "chr(", "mid(", "master ", "truncate " };
                String[] strInvaild = new String[] { "*", "%", "'" };

                for (int i = 0; i < request.QueryString.Count; i++)
                {
                    for (int j = 0; j < strKeys.Length; j++)
                    {
                        if (request.QueryString[i].ToLower().IndexOf(strKeys[j]) >= 0)
                        {
                            response.Write("<script>alert('您输入的字符中包含了系统保留字符');document.location=document.location;</script>");
                            response.End();
                            return;
                        }
                    }
                }
                for (int i = 0; i < request.QueryString.Count; i++)
                {
                    for (int j = 0; j < strInvaild.Length; j++)
                    {
                        if (request.QueryString[i].ToLower().IndexOf(strInvaild[j]) >= 0)
                        {
                            response.Write("<script>alert(\"您输入的字符中包含了非法字符,如：* % '\");document.location=document.location;</script>");
                            response.End();
                            return;
                        }
                    }
                }
                for (int i = 0; i < request.Form.Count; i++)
                {
                    for (int j = 0; j < strKeys.Length; j++)
                    {
                        if (request.Form[i].ToLower().IndexOf(strKeys[j]) >= 0)
                        {
                            response.Write("<script>alert('您输入的字符中包含了系统保留字符');document.location=document.location;</script>");
                            response.End();
                            return;
                        }
                    }
                }
                for (int i = 0; i < request.Form.Count; i++)
                {
                    for (int j = 0; j < strInvaild.Length; j++)
                    {
                        if (request.Form[i].ToLower().IndexOf(strInvaild[j]) >= 0)
                        {
                            response.Write("<script>alert(\"您输入的字符中包含了非法字符,如：* % '\");document.location=document.location;</script>");
                            response.End();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
            
           
            return ;
        }
	}
}
