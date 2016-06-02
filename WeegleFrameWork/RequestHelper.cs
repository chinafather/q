using System;
using System.Web;
namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// RequestHelper ��ժҪ˵����
	/// �������Ҫ���������㴦��Request�ǶԸ������͵Ĳ�����
	/// ��Ҫ�������������ַ������Ͳ�����
	/// </summary>
	public class RequestHelper
	{
		HttpRequest _request;
		public RequestHelper(HttpRequest request)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			this._request = request;  
		}
		/// <summary>
		/// ����ַ������͵ķ���
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
		/// �����������,���Ϊ���򷵻�����ֵ
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
		/// �����������,���Ϊ���򷵻�0
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
		/// ��ò������͵�ֵ
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
