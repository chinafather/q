using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// TypeFactory 的摘要说明。
	/// </summary>
	public interface ITypeFactory
	{
		IType GetType(int type);
	}
}
