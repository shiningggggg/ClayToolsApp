using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExercise.Base.Configuration
{
    /// <summary>
    /// 以名字为键值配置项集合
    /// </summary>
    public  class NamedConfigurationElementCollection<T>:ConfigurationElementCollection 
        where T :NamedConfigurationElement,new()
    {
        /// <summary>
        /// 按序号获取指定的配置元素
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>配置元素</returns>
        public virtual T this[int index] { get { return (T)base.BaseGet(index); } }
        /// <summary>
        /// 按名称获取配置元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>配置元素</returns>
        public new T this[string name] { get { return (T)base.BaseGet(name); } }
        /// <summary>
        /// 判断是否包含配置元素
        /// </summary>
        /// <param name="name">配置元素名称</param>
        /// <returns>是否包含</returns>
        public bool ContainsKey(string name) { return base.BaseGet(name) != null; }
        /// <summary>
        /// 得到元素的key值
        /// </summary>
        /// <param name="element">配置元素</param>
        /// <returns>配置元素对应的名称</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Name;
        }
        /// <summary>
        /// 创建新的配置元素
        /// </summary>
        /// <returns>配置元素</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }
        /// <summary>
        /// 通过name在字典内查找数据，如果name不存在，则抛出异常
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>配置元素</returns>
        protected virtual T InnerGet(string name)
        {
            object element = BaseGet(name);
            return (T)element;
        }
    }
}
