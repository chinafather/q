using System;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// IContext 的摘要说明。
	/// </summary>
	public interface IContext
	{
		Object GetValue(DBConfig cfg, String s);
		Object GetValueRaw(DBConfig cfg, String s);
	}
}
