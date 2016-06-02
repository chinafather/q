using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// MessageBox帮助类
    /// </summary>
    public static class MsgBox
    {
        /// <summary>
        /// 获取或设置显示标题
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static string Caption { get; set; }

        static MsgBox()
        {
            Caption = "华东凯亚提示";
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="text"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult ShowInfomation(string text, IWin32Window owner = null)
        {
            if (owner == null)
            {
                return MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return MessageBox.Show(owner, text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="text"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult ShowWarning(string text, IWin32Window owner = null)
        {
            if (owner == null)
            {
                return MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                return MessageBox.Show(owner, text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="text"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult ShowError(string text, IWin32Window owner = null)
        {
            if (owner == null)
            {
                return MessageBox.Show(text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                return MessageBox.Show(owner, text, Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="text"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(string text, IWin32Window owner = null)
        {
            if (owner == null)
            {
                return MessageBox.Show(text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else
            {
                return MessageBox.Show(owner, text, Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }
    }
}
