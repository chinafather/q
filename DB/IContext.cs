using System;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// IContext ��ժҪ˵����
	/// </summary>
	public interface IContext
	{
		Object GetValue(DBConfig cfg, String s);
		Object GetValueRaw(DBConfig cfg, String s);
	}
}
