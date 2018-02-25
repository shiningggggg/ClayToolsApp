using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExercise.Base.Configuration
{
    /// <summary>
    /// 类型信息的配置项
    /// </summary>
    public class TypeConfigurationElement:NamedConfigurationElement
    {
        /// <summary>
        /// 类型描述信息
        /// </summary>
        /// <remarks>一般采用QualifiedName (QualifiedTypeName, AssemblyName) 方式</remarks>
        [ConfigurationProperty("type",IsRequired =true)]
        public virtual string Type
        {
            get
            {
                return this["type"].ToString();
            }
        }
        /// <summary>
        /// 建立对象的实例
        /// </summary>
        /// <param name="ctorParams">创建实例的初始化参数</param>
        /// <returns>运用晚绑定方式动态创建一个实例</returns>
        public object CreateInstance(params object[] ctorParams)
        {
            return TypeCreator.CreateInstance(Type, ctorParams);
        }
    }
}
