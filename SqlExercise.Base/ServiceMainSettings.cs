using SqlExercise.Base.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExercise.Base
{
    /// <summary>
    /// 这个类映射了App.config中配置的section，section中的name对应自定义配置节点。
    /// 
    /// </summary>
    public sealed class ServiceMainSettings : ConfigurationSection
    {
        private const string configSection = "serviceMainConfig";

        public static ServiceMainSettings GetConfigs()
        {
            ServiceMainSettings result = (ServiceMainSettings)ConfigurationManager.GetSection(configSection);
            if (result == null)
                result = new ServiceMainSettings();
            return result;
        }

        [ConfigurationProperty("treeConfigFile")]
        public string TreeConfigFile
        {
            get
            {
                string ss = this["treeConfigFile"].ToString();
                return this["treeConfigFile"].ToString();
            }
        }
        [ConfigurationProperty("treeConfigPath", DefaultValue = "")]
        public string TreeConfigPath
        {
            get
            {
                if (this["treeConfigPath"] == null || string.IsNullOrEmpty(this["treeConfigPath"].ToString()))
                    return AppDomain.CurrentDomain.BaseDirectory;
                return this["treeConfigPath"].ToString();
            }
        }
        [ConfigurationProperty("serviceName", DefaultValue = "SqlExercise")]
        public string ServiceName
        {
            get { return this["serviceName"].ToString(); }
        }
        public string TreeConfig()
        {
            if (TreeConfigPath.EndsWith(@"\"))
                return string.Format("{0}{1}", TreeConfigPath, TreeConfigFile);
            return string.Format(@"{0}\{1}", TreeConfigPath, TreeConfigFile);
        }
        [ConfigurationProperty("extendConfigs")]
        public TypeConfigurationCollection ExtendConfig
        {
            get
            {
                return (TypeConfigurationCollection)this["extendConfigs"];
            }
        }
        [ConfigurationProperty("addinConfigs")]
        public TypeConfigurationCollection AddinConfigs
        {
            get
            {
                return (TypeConfigurationCollection)this["addinConfigs"];
            }
        }
    }
}
