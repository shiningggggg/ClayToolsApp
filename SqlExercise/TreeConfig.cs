using SqlExercise.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SqlExercise
{
    public class TreeConfig : CustomConfigBase
    {
        #region 私有成员

        //配置文件的Xml引用信息
        private XmlDocument _document = null;
        private XmlNamespaceManager _nsmsg = null;
        private XmlNode _root = null;

        //接卸出的全部配置信息
        private List<SqlExerciseItem> _items;
        #endregion

        #region 构造函数
        
        /// <summary>
        ///根据配置文件的据对路径名初始化实例，并解析装在全部配置项 
        /// </summary>
        /// <param name="filePath">完整文件路径</param>
        public TreeConfig(string filePath) : base(filePath, ConfigType.XmlDocument)
        {
            InitConfigFile();
            LoadConfigItems();
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 获取配置信息集合
        /// </summary>
        public List<SqlExerciseItem> Items
        {
            get
            {
                return this._items;
            }
        }

        /// <summary>
        /// 根据配置项Name查找配置项
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SqlExerciseItem GetItemByName(string name)
        {
            SqlExerciseItem result = null;
            foreach (var item in this._items)
            {
                if (item.Name.Equals(name))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据配置项的Title查找配置项
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public SqlExerciseItem GetItemByTitle(string title)
        {
            SqlExerciseItem result = null;
            foreach (SqlExerciseItem item in this._items)
            {
                if (item.Title.Equals(title))
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 保存更改的原文件
        /// </summary>
        public void Save()
        {
            this.SaveConfigFile();
        }

        #endregion

        #region 私有方法

        private void InitConfigFile()
        {
            this._items = new List<SqlExerciseItem>();

            //装在配置文件
            _document = base.XmlConfig;

            //构建一个Xml命名控件，由于所有节点没有明确指定节点名前缀，因此需要构建一个临时的命名空间
            _nsmsg = new XmlNamespaceManager(_document.NameTable);
            _nsmsg.AddNamespace("xx", _document.DocumentElement.NamespaceURI);

            //根节点
            _root = _document.DocumentElement;
        }

        /// <summary>
        /// 当配置文件改变后自动激活
        /// </summary>
        protected override void FileChanged()
        {
            InitConfigFile();
            LoadConfigItems();
        }

        /// <summary>
        /// 从配置文件中解析全部配置项
        /// </summary>
        private void LoadConfigItems()
        {
            XmlNodeList treeNodes = _root.SelectNodes("//xx:TreeNodes/xx:TreeNode", _nsmsg);
            foreach (XmlNode treeNode in treeNodes)
            {
                string name = this.GetXmlNodeText(treeNode, "xx:name");
                if (string.IsNullOrEmpty(name) == false)
                {
                    string title = GetXmlNodeText(treeNode, "xx:title");
                    string description = GetXmlNodeText(treeNode, "xx:description");
                    SqlExerciseItem item = new SqlExerciseItem(name, title, description);

                    this._items.Add(item);
                }
            }
        }
        
        /// <summary>
        /// 根据xpath获取一个节点的文字内容
        /// </summary>
        /// <param name="rootNode"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        private string GetXmlNodeText(XmlNode rootNode, string xpath)
        {
            XmlNode node = rootNode.SelectSingleNode(xpath,_nsmsg);
            return node != null ? node.InnerText : string.Empty;
        }

        /// <summary>
        /// 保存更改到原文件
        /// </summary>
        private void SaveConfigFile()
        {
            lock (SyncObj)
            {
                foreach (SqlExerciseItem item in this.Items)
                {
                    if (!item.Changed) continue;

                    XmlNode treeNode = _root.SelectSingleNode(string.Format("//xx:TreeNode[xx:name=\"{0}\"]", item.Name), _nsmsg);
                    if (treeNode != null)
                    {
                        SetXmlNodeText(treeNode, "name", item.Name);
                        SetXmlNodeText(treeNode, "title", item.Title);
                        SetXmlNodeText(treeNode, "description", item.Description);
                    }
                    else
                    {
                        throw new Exception(string.Format("保存调度配置文件失败，配置文件中找不到配置项'{0}'的触发器片段", item.Name));
                    }
                }
                try
                {
                    //保存文件
                    _document.Save(base.FilePathName);

                    //调整Changed属性
                    foreach (SqlExerciseItem item in this._items)
                    {
                        if (item.Changed)
                        {
                            item.Changed = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// 设置一个子节点的文本内容，如果该节点不存在，则新建一个
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNodeName"></param>
        /// <param name="text"></param>
        public void SetXmlNodeText(XmlNode parentNode, string childNodeName, string text)
        {
            string xpath = "xx:" + childNodeName;
            XmlNode childNode = parentNode.SelectSingleNode(xpath, _nsmsg);
            if (childNode == null)
            {
                childNode = _document.CreateElement(childNodeName, _root.NamespaceURI);
                parentNode.AppendChild(childNode);
            }
            childNode.InnerText = text;
        }
        #endregion
    }
}
