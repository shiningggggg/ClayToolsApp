using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SqlExercise
{
    public enum ConfigType
    {
        None,
        Exe,
        XmlDocument,
        Xelement
    }
    public abstract class CustomConfigBase
    {
        #region 私有成员

        private string filePathName = string.Empty;

        private System.Configuration.Configuration m_ExecConfig;

        private XmlDocument m_XmlConfig;

        private XElement m_XmlConfigElement;

        private ConfigType m_configType = ConfigType.None;

        //侦听文件系统更改通知，并在目录或目录中的文件发生更改时引发事件
        private FileSystemWatcher m_FileWatcher;
        #endregion


        #region 保护成员

        /// <summary>
        /// 配置文件读写时使用的锁对象
        /// 注：对配置文件的读写一定要锁定本对象
        /// </summary>
        protected object SyncObj = new object();
        #endregion

        #region 构造方法

        /// <summary>
        /// 使用一个XML文件的名字生成实例
        /// </summary>
        /// <param name="filePathName"></param>
        /// <param name="type"></param>
        public CustomConfigBase(string filePathName, ConfigType type)
        {
            this.filePathName = filePathName;
            this.m_configType = type;

            this.OpenConfigFile(type);
        }

        /// <summary>
        /// 使用一个可执行文件(.exe或.dll)生成实例
        /// </summary>
        /// <param name="assembly"></param>
        public CustomConfigBase(System.Reflection.Assembly assembly)
        {
            this.filePathName = assembly.Location + ".config";

            this.m_configType = ConfigType.Exe;

            this.OpenConfigFile(this.m_configType);
        }
        
        /// <summary>
        /// 垃圾回收器决定合适调用析构函数。
        /// 垃圾回收器检查是否存在应用程序不再使用的对象。
        /// 如果垃圾回收器认为某个对象符合析构，则调用析构函数，并回收用来存储此对象的内存
        /// </summary>
        ~CustomConfigBase()
        {
            if (m_FileWatcher != null)
            {
                m_FileWatcher.Dispose();
            }
        }
        #endregion

        #region 公共属性

        /// <summary>
        /// 获取文件完整路径名
        /// </summary>
        public string FilePathName
        {
            get
            {
                return this.filePathName;
            }
        }

        /// <summary>
        /// 获取配置文件的类型(true: 可执行文件配套，false: XML文件)
        /// </summary>
        public bool IsExecConfig
        {
            get
            {
                return this.m_configType == ConfigType.Exe;
            }
        }

        /// <summary>
        /// 获取可执行程序的配置。仅当采用assembly实例化才有效。
        /// </summary>
        public System.Configuration.Configuration ExecConfig
        {
            get
            {
                return this.m_ExecConfig;
            }
        }

        /// <summary>
        /// 获取XML文件格式的配置。仅当采用filePathName实例化才有效
        /// </summary>
        public XmlDocument XmlConfig
        {
            get
            {
                return this.m_XmlConfig;
            }
        }

        /// <summary>
        /// 获取XML文件格式的配置。仅当采用filePathName实例化才有效
        /// </summary>
        public XElement XmlConfigElement
        {
            get
            {
                return this.m_XmlConfigElement;
            }
        }

        #endregion

        #region 公共方法
        /// <summary>
        /// 获取ExecConfig中的一个appSetting的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSetting(string key)
        {
            string result = string.Empty;
            if (this.m_ExecConfig != null)
            {
                KeyValueConfigurationElement item = this.m_ExecConfig.AppSettings.Settings[key];
                result = item != null ? item.Value : string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 获取指定config文件中的一个配置的连接串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetConnectionString(string name)
        {
            string result = string.Empty;
            if (this.m_ExecConfig != null)
            {
                ConnectionStringSettings item = this.m_ExecConfig.ConnectionStrings.ConnectionStrings[name];
                result = item != null ? item.ConnectionString : string.Empty;
            }
            return result;
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 装入配置文件
        /// </summary>
        /// <param name="type"></param>
        private void OpenConfigFile(ConfigType type)
        {
            lock (SyncObj) //锁定配置文件锁对象
            {
                switch (type)
                {
                    case ConfigType.Exe:
                    case ConfigType.None:
                        //定义.exe应用程序的配置文件映射
                        ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap
                        {
                            //计算机配置文件，其中包含的完整物理路径的名称
                            ExeConfigFilename = this.filePathName
                        };
                        //将制定客户端文件作为Configuration对象打开，该对象使用指定的文件映射和用户级别
                        this.m_ExecConfig = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
                        break;
                    case ConfigType.XmlDocument:
                        this.m_XmlConfig = new XmlDocument();
                        this.m_XmlConfig.Load(this.filePathName);
                        break;
                    case ConfigType.Xelement:
                        XmlReader reader = XmlReader.Create(this.filePathName);
                        this.m_XmlConfigElement = XElement.Load(reader);
                        break;
                }
                this.InitFileWatcher();
            }
        }

        /// <summary>
        /// 初始化文件监测，当文件被修改时，将激活相应的处理程序
        /// </summary>
        private void InitFileWatcher()
        {
            if (this.m_FileWatcher == null)
            {
                string filePath = Path.GetDirectoryName(this.filePathName);
                string fileName = Path.GetFileName(this.filePathName);
                if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
                {
                    return;
                }
                this.m_FileWatcher = new FileSystemWatcher(filePath, fileName)
                {
                    // | 16进制的或运算，按位或
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName,
                    EnableRaisingEvents = true
                };
                this.m_FileWatcher.Changed += FileWatcherChanged;
            }
        }

        /// <summary>
        /// 文件检测到改变后的处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileWatcherChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                //重新装入配置文件
                OpenConfigFile(this.m_configType);

                //触发文件改变事件
                this.FileChanged();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 接口方法

        /// <summary>
        /// 文件改变后的处理程序，子类必须实现
        /// </summary>
        protected abstract void FileChanged();
        #endregion
    }
}
