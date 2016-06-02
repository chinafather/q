using System;
using System.Web;
using System.Collections.Specialized;
using System.Web.SessionState;


namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// HTTP处理类
	/// </summary>
	public sealed class HttpHelper
	{
		public HttpHelper()
		{
		}

		/// <summary>
		/// 得到相应key的session值
		/// </summary>
		/// <param name="session"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		public static Object GetSessionAttribute(HttpSessionState session, string key)
		{
			Object obj = session[key];
			if(obj == null)
			{
				throw new Exception(string.Format("Session {0} is not exists",key));
			}
			return obj;
		}

		/// <summary>
		/// Request的url处理
		/// </summary>
		public static string GetUrlOfRequest(HttpRequest request)
		{
			string m_url = request.Path;
			string[] temp = m_url.Split('/');
			m_url = temp[temp.Length - 1];

			NameValueCollection m_params = request.QueryString;

			string param = "";
			string[] keys = m_params.AllKeys;
			for(int i = 0; i < keys.Length; i ++)
			{
				if(i == 0)
				{
					param += "?" + HttpUtility.UrlEncode(keys[i]) + "=" + HttpUtility.UrlEncode(m_params[keys[i]]);
				}
				else
				{
					param += "&" + HttpUtility.UrlEncode(keys[i]) + "=" + HttpUtility.UrlEncode(m_params[keys[i]]);
				}
			}
			return m_url + param;
		}
	}
}
