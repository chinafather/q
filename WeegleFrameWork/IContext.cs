using System;
using System.Collections;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// IContext 的摘要说明。
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
