using System;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// IErrorManager 的摘要说明。
	/// </summary>
	public interface IErrorManager
	{
		void Warning(object form,string message);
		void ExceptionProcess(object form,Exception err);
	}
}
