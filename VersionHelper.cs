using System;
using System.Collections.Generic;
using System.Text;

namespace MRUS.Core
{
    /// <summary>
    /// 给应用的客户端使用，用于获取升级的版本信息。
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// 获取当前客户端的版本号。
        /// </summary>        
        public static int GetCurrentVersion()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "AutoUpdater\\MineRealmsConfig.xml";
            UpdateConfiguration config = (UpdateConfiguration)UpdateConfiguration.Load(path);
            return config.ClientVersion;
        }

        /// <summary>
        /// 从服务端获得最新客户端的版本号。【前提是服务端的配置项RemotingServiceEnabled必需为true】
        /// </summary>
        /// <param name="MRUSServerIP">MRUS服务端的IP</param>
        /// <param name="MRUSServerPort">MRUS服务端的端口</param>        
        public static int GetLatestVersion(string MRUSServerIP, int MRUSServerPort)
        {
            IMRUSService service = (IMRUSService)Activator.GetObject(typeof(IMRUSService), string.Format("tcp://{0}:{1}/MRUSService", MRUSServerIP, MRUSServerPort+2));
            return service.GetLatestVersion();        
        }

        /// <summary>
        /// 是否有新版本？【前提是服务端的配置项RemotingServiceEnabled必需为true】
        /// </summary>
        /// <param name="MRUSServerIP">MRUS服务端的IP</param>
        /// <param name="MRUSServerPort">MRUS服务端的端口</param>        
        public static bool HasNewVersion(string MRUSServerIP, int MRUSServerPort)
        {
            return VersionHelper.GetLatestVersion(MRUSServerIP, MRUSServerPort) > VersionHelper.GetCurrentVersion();
        }
    }
}
