using System;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// IErrorManager ��ժҪ˵����
	/// </summary>
	public interface IErrorManager
	{
		void Warning(object form,string message);
		void ExceptionProcess(object form,Exception err);
	}
}
