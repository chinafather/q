using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Util
{
    #region ===Obsolete===

    /*
    public class Consts
    {
        private static string _currentIpAddr;
        public static string CurrentIpAddr
        {
            get {
                
                if (!String.IsNullOrEmpty(_currentIpAddr) && _currentIpAddr.EndsWith("/"))
                {
                    _currentIpAddr = _currentIpAddr.Substring(0, _currentIpAddr.Length - 1);
                }
                 
                return _currentIpAddr;
            }
            set { _currentIpAddr = value; }
        }

        private static string _defaultServer;
        public static string DefaultServer
        {
            get
            {
                if (_defaultServer == null)
                    _defaultServer = System.Configuration.ConfigurationManager.AppSettings["DefaultServer"].ToString();
                return _defaultServer;
            }
            private set { _defaultServer = value; }
        }

        private static string _currentUrl;
        public static string CurrentURL
        {
            get
            {
                if (_currentUrl == null)
                {
                    if (DefaultServer == "InternalURL")
                    {
                        _currentUrl = InternalURL;
                    }
                    else
                    {
                        _currentUrl = ExternalURL;
                    }
                }
                return _currentUrl;
            }
            private set { _currentUrl = value; }
        }

        public static void ChangeCurrentURL()
        {
            if (DefaultServer == "InternalURL")
            {
                CurrentURL = ExternalURL;
                DefaultServer = "ExternalURL";
            }
            else
            {
                CurrentURL = InternalURL;
                DefaultServer = "InternalURL";
            }
        }

        private static string _internalURL;
        private static string InternalURL
        {
            get
            {
                if (_internalURL == null)
                {
                    _internalURL = System.Configuration.ConfigurationManager.AppSettings["InternalURL"].ToString().Trim();
                    if (_internalURL.EndsWith("/"))
                    {
                        _internalURL = _internalURL.Substring(0, _internalURL.Length - 1);
                    }
                }
                return _internalURL;
            }
        }

        private static string _externalURL;
        private static string ExternalURL
        {
            get
            {
                if (_externalURL == null)
                {
                    _externalURL = System.Configuration.ConfigurationManager.AppSettings["ExternalURL"].ToString().Trim();
                    if (_externalURL.EndsWith("/"))
                    {
                        _externalURL = _externalURL.Substring(0, _externalURL.Length - 1);
                    }
                }
                return _externalURL;
            }
        }

        /// <summary>
        /// 是否为内网连接
        /// </summary>
        public static bool IsInternal
        {
            get
            {
                return string.Compare(Consts.DefaultServer, "InternalURL", true) == 0;
            }
        }
    }
    */

    #endregion
}
