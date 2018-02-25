using SqlExercise.Base;
using SqlExercise.Base.Configuration;
using System;
using System.Windows.Forms;

namespace SqlExercise
{
    public partial class Form1 : Form
    {
        private TreeConfig tree_config;
        private TreeNode tree_TnSummary, tree_TnConfig;
        public Form1()
        {
            InitializeComponent();
            LoadConfigFile();
            InitTreeNode();
        }
        private void LoadConfigFile()
        {
            try
            {
                this.tree_config = new TreeConfig((ServiceMainSettings.GetConfigs()).TreeConfig());
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("读取配置文件{0}出错\n\n{1}", (ServiceMainSettings.GetConfigs()).TreeConfigFile, ex.Message));
            }
        }
        private void InitTreeNode()
        {
            tree_TnSummary = new TreeNode("摘要");
            tree_TnConfig = new TreeNode("Sql练习列表");
            foreach (SqlExerciseItem item in this.tree_config.Items)
            {
                TreeNode treeNode = new TreeNode(item.Name);
                tree_TnConfig.Nodes.Add(treeNode);
            }
            tree_TnSummary.Nodes.Add(tree_TnConfig);
            treeView.Nodes.Add(tree_TnSummary);
            treeView.ExpandAll();
        }
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == tree_TnSummary || e.Node == tree_TnConfig)
            {

            }
            else
            {
                SqlExerciseItem item = this.tree_config.GetItemByTitle(e.Node.Text);
                if (item != null)
                {
                    if (tabControl1.TabPages.Contains(tabPage2) == false)
                    {
                        tabControl1.TabPages.Add(tabPage2);
                    }
                    if (tabControl1.TabPages.Contains(tabPage3) == false)
                    {
                        tabControl1.TabPages.Add(tabPage3);
                    }

                    this.FillConfigPanel(item);
                }
            }
        }

        private void FillConfigPanel(SqlExerciseItem item)
        {
            CreateTabControl(ServiceMainSettings.GetConfigs().AddinConfigs, item, tabPage1);
        }
        private void CreateTabControl(TypeConfigurationCollection configs, SqlExerciseItem item, TabPage tab)
        {
            tab.Controls.Clear();
            try
            {
                if (configs.ContainsKey(item.Name))
                {
                    TypeConfigurationElement typeElement = configs[item.Name];
                    UserControl uc = typeElement.CreateInstance() as UserControl;
                    if (uc != null)
                    {
                        tab.Controls.Clear();
                        uc.Dock = DockStyle.Fill;
                        tab.Controls.Add(uc);
                    }
                    else
                    {
                        tabControl1.TabPages.Remove(tab);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("加载扩展配置控件失败，异常信息：{0}", ex.Message));
                //移除扩展配置项
                tabControl1.TabPages.Remove(tab);
            }
        }
    }
}
