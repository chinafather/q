using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// 方法的模块ID属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodModuleIDAttribute : Attribute
    {
        /// <summary>
        /// 获取模块ID
        /// </summary>
        public string ModuleID { get; private set; }

        /// <summary>
        /// 图标资源名
        /// </summary>
        public string IconName { get; set; }

        public MethodModuleIDAttribute(string moduleID)
        {
            this.ModuleID = moduleID;
        }
    }
}
