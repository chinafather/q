using System;

namespace Weegle.FrameWork.DB.KType
{
	/// <summary>
	/// TypeFactory ��ժҪ˵����
	/// </summary>
	public interface ITypeFactory
	{
		IType GetType(int type);
	}
}
