using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// 异步执行体
    /// </summary>
    public class AsyncExecuter
    {
        /// <summary>
        /// 是否正在查询中
        /// </summary>
        protected volatile bool _isQuering = false;

        /// <summary>
        /// 获取一个值，指示是否正在查询中
        /// </summary>
        public bool IsQuering
        {
            get { return this._isQuering; }
        }

        /// <summary>
        /// 日志标题
        /// </summary>
        public string LogTitle { get; set; }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        public event Action<bool> SetControlState;

        /// <summary>
        /// 异步执行
        /// </summary>
        public void AsyncExecute(Action action)
        {
            ThreadPool.QueueUserWorkItem((WaitCallback)(obj =>
            {
                this.Execute(action);
            }));
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="action"></param>
        protected virtual void Execute(Action action)
        {
            if (action == null) { return; }

            if (this._isQuering)
            {
                return;
            }

            this._isQuering = true;

            if (this.SetControlState != null)
            {
                try
                {
                    this.SetControlState(false);
                }
                catch (Exception ex)
                {
                    LogUtil.WriteErr(string.Format("[{0}]开始加载数据前异常：{1}", this.LogTitle, ex));
                }
            }

            try
            {
                action();
            }
            catch (Exception ex)
            {
                LogUtil.WriteErr(string.Format("[{0}]异步加载数据时异常：{1}", this.LogTitle, ex));
            }
            finally
            {
                this._isQuering = false;

                if (this.SetControlState != null)
                {
                    try
                    {
                        this.SetControlState(true);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.WriteErr(string.Format("[{0}]加载数据完成后异常：{1}", this.LogTitle, ex));
                    }
                }
            }
        }
    }
}

