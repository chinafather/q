 //****************************************************************************
 //** �ļ���: GUIDHelp.cs	                                          
 //**         Copyright 2005 by Shanghai HJ Software System Co.,Ltd            
 //**         All Rights Reserved 	                                          
 //** ������: Hovering	                                                      
 //** �� �� : 2006-2-7 
 //** �޸���:	                                                              
 //** �� �� :	                                                              
 //** �� �� :	                                                              
 //**       
 //** �� �� :		                                                              
 //****************************************************************************
using System;
using System.Web;
namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// StringHelper ��ժҪ˵����
	/// </summary>
	public class StringHelper
	{
		public StringHelper()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        /// <summary>
        /// ��Sqlע��ʽ����
        /// </summary>
        /// <param name="strValue">sql�ű��е����ݣ�����sql�ű�����</param>
        /// <returns>0������; 1������ʹ��ϵͳ�����ַ�; 2������ʹ�÷Ƿ��ַ� </returns>
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
        /// ��ҳ���ύ�����ݽ��з�ֹsqlע��ʽ�����ж�
        /// </summary>
        /// <param name="request">ҳ���request</param>
        /// <param name="response">ҳ���response</param>
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
                            response.Write("<script>alert('��������ַ��а�����ϵͳ�����ַ�');document.location=document.location;</script>");
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
                            response.Write("<script>alert(\"��������ַ��а����˷Ƿ��ַ�,�磺* % '\");document.location=document.location;</script>");
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
                            response.Write("<script>alert('��������ַ��а�����ϵͳ�����ַ�');document.location=document.location;</script>");
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
                            response.Write("<script>alert(\"��������ַ��а����˷Ƿ��ַ�,�磺* % '\");document.location=document.location;</script>");
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
