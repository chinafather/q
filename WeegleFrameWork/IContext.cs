using System;
using System.Collections;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// IContext ��ժҪ˵����
	/// </summary>
	public interface IContext
	{
		IEnumerable Current
		{
			set;
		}
		string Prex
		{
			set;
			get;
		}
		object GetValue( string s);
	}
}
