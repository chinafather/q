using System;
using System.Collections;
using System.Data;
using Weegle.FrameWork.WeegleFrameWork;
namespace Weegle.FrameWork.DB
{
	/// <summary>
	/// ISqlAnalyse 的摘要说明。
	/// </summary>
	public interface ISqlAnalyse
	{
        DataView GetData(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx);
        DataView GetDataSchema(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx);
        object GetValue(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx);
        object ExcuteSql(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx);
        DataSet GetQueryData(DBConfig cfg, Weegle.FrameWork.WeegleFrameWork.IContext ctx);
	}
}
