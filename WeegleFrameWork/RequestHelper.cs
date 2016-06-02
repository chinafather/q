using System;
using System.Web;
namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// RequestHelper 的摘要说明。
	/// 这个类主要是用来方便处理Request是对各个类型的操作的
	/// 主要包括了整数和字符串类型布尔型
	/// </summary>
	public class RequestHelper
	{
		HttpRequest _request;
		public RequestHelper(HttpRequest request)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this._request = request;  
		}
		/// <summary>
		/// 获得字符串类型的方法
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public String GetString(String name)
		{
			if (this._request[name] != null)
			{
				return Convert.ToString(this._request[name]);
			}
			return "";
		}
 
		/// <summary>
		/// 获得整数类型,如果为空则返回期望值
		/// </summary>
		/// <param name="name"></param>
		/// <param name="def"></param>
		/// <returns></returns>
		public int GetInt(string name, int def)
		{
			if ((this._request[name] != null) && (this._request[name] != ""))
			{
				return int.Parse(this._request[name]);
			}
			return def;
		}

		/// <summary>
		/// 获得整数类型,如果为空则返回0
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public int GetInt(string name)
		{
			if ((this._request[name] != null) && (this._request[name] != ""))
			{
				return int.Parse(this._request[name]);
			}
			return 0;
		}

		/// <summary>
		/// 获得布而类型的值
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool GetBool(string name)
		{
			if (Convert.ToString(this._request[name]).ToUpper() == "TRUE")
			{
				return true;
			}
			return false;
		}
 



	}
}
