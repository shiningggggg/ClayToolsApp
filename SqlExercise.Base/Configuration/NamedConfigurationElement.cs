using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExercise.Base.Configuration
{
    /// <summary>
    /// 以名字为键值的配置项
    /// </summary>
    public class NamedConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name",IsKey =true,IsRequired =true)]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
        }
        [ConfigurationProperty("description",DefaultValue ="")]
        public string Description
        {
            get
            {
                return this["description"].ToString();
            }
        }
        [ConfigurationProperty("uri")]
        public string Uri
        {
            get
            {
                return this["uri"].ToString();
            }
        }
    }
}
