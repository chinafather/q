using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Cares.FidsII.Util
{
    /// <summary>
    /// P/Invoke
    /// </summary>
    public static class Native
    {
        public const int WM_CHAR = 0x102;

        public const int WM_KEYDOWN = 0x100;

        public const int WM_SYSKEYUP = 0x105;

        public const int KEYDOWN_MASK = 0x40000000;

        public const int WS_EX_TOOLWINDOW = 0x80;

        public const int WM_SYSCOMMAND = 0x112;

        public const int SC_CLOSE = 0xF060;

        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENT
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hwndTrack;
            public uint dwHoverTime;
        }

        public const uint TME_CANCEL = 0x80000000;

        public const int TME_HOVER = 0x1;

        public const int TME_LEAVE = 0x2;

        public const int TME_NONCLIENT = 0x10;

        public const int TME_QUERY = 0x40000000;

        [DllImport("user32.dll")]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT tme);

        [DllImport("user32.dll ")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);

        [DllImport("user32.dll ")]
        public static extern int ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        
        public const int HTCAPTION = 2;

        public const int HTBOTTOMRIGHT = 17;
    }
}
