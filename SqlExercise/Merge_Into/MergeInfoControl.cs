using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqlExercise.Base.SqlMethod;
using System.Data.SqlClient;

namespace SqlExercise.Merge_Into
{
    public partial class MergeInfoControl : UserControl
    {
        public MergeInfoControl()
        {
            InitializeComponent();
            string source = "select * from SourceTable";
            string target = "select * from TargetTable";
            this.dataGridView1.DataSource = SqlHelper.GetDataSet(source).Tables[0];
            this.dataGridView2.DataSource = SqlHelper.GetDataSet(target).Tables[0];
        }

        /// <summary>
        /// 创建合并表所需要的表和数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            string createSourceTable = "Create Table SourceTable(id INT,[DESC] varchar(50))";
            string createTargetTable = "Create Table TargetTable(id INT,[DESC] varchar(50))";
                  
            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader("select * from dbo.sys.objects where object_id=object_id('SourceTable')");
                if (reader.HasRows)
                {
                    MessageBox.Show("表已经存在！");
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(createSourceTable);
                    SqlHelper.ExecuteNonQuery(createTargetTable);
                    MessageBox.Show("创建成功！");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnMergeInto_Click(object sender, EventArgs e)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("Merge Into TargetTable AS T ");
            sbuilder.Append("using SourceTable As S ");
            sbuilder.Append("on T.id = S.id ");
            sbuilder.Append("When matched ");
            sbuilder.Append("Then Update set T.[DESC]=S.[DESC] ");
            sbuilder.Append("When not matched ");
            sbuilder.Append("Then insert Values(S.id,S.[DESC]) ");
            sbuilder.Append("When not matched by Source ");
            sbuilder.Append("Then Delete; ");//目标表中存在，源表中不存在，则删除
            try
            {
                if (SqlHelper.ExecuteNonQuery(sbuilder.ToString()) > 0)
                {
                    MessageBox.Show("合并成功！");
                    this.dataGridView3.DataSource = SqlHelper.GetDataSet("select * from TargetTable ").Tables[0];
                }
                else
                {
                    MessageBox.Show("合并失败！");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            StringBuilder sSourceBuilder = new StringBuilder();
            sSourceBuilder.Append("insert into dbo.SourceTable(id,[DESC]) values (1,'描述1');");
            sSourceBuilder.Append("insert into dbo.SourceTable(id,[DESC]) values (2,'描述2');");
            sSourceBuilder.Append("insert into dbo.SourceTable(id,[DESC]) values (3,'描述3');");
            sSourceBuilder.Append("insert into dbo.SourceTable(id,[DESC]) values (4,'描述4')");
            StringBuilder sTargetBuilder = new StringBuilder();
            sTargetBuilder.Append("insert into dbo.TargetTable(id,[DESC]) values(1,'在源表里面存在，将会被更新');");
            sTargetBuilder.Append("insert into dbo.TargetTable(id,[DESC]) values(2,'在源表里面存在，将会被更新');");
            sTargetBuilder.Append("insert into dbo.TargetTable(id,[DESC]) values(5,'在源表里面不存在，将会被删除');");
            sTargetBuilder.Append("insert into dbo.TargetTable(id,[DESC]) values(6,'在源表里面不存在，将会被删除');");
            try
            {
                SqlHelper.ExecuteNonQuery("truncate table dbo.SourceTable;truncate table dbo.TargetTable;");
                SqlHelper.ExecuteNonQuery(sSourceBuilder.ToString());
                SqlHelper.ExecuteNonQuery(sTargetBuilder.ToString());
                MessageBox.Show("插入成功！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = SqlHelper.GetDataSet("select * from dbo.SourceTable").Tables[0];
            this.dataGridView2.DataSource = SqlHelper.GetDataSet("select * from dbo.TargetTable").Tables[0];
            DataTable table = SqlHelper.GetDataSet("select * from dbo.TargetTable").Tables[0];
            var model = from d1 in table.AsEnumerable()
                        select new
                        {
                            Id = d1.Field<string>("ID"),
                        };
            this.dataGridView3.DataSource = null;
        }

        private void btnMergeOutput_Click(object sender, EventArgs e)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("Merge Into TargetTable As T ");
            sbuilder.Append("Using SourceTable As S ");
            sbuilder.Append("ON T.id = S.id ");
            sbuilder.Append("When Matched ");
            sbuilder.Append("Then Update Set T.[DESC] = S.[DESC] ");
            sbuilder.Append("When not Matched ");
            sbuilder.Append("Then Insert Values(S.id,S.[DESC]) ");
            sbuilder.Append("When not Matched By Source ");
            sbuilder.Append("Then Delete ");
            sbuilder.Append("Output $Action As [Action], Inserted.id As 插入的id,");
            sbuilder.Append("Inserted.[DESC] As 插入的DESC,");
            sbuilder.Append("Deleted.id As 删除的id,");
            sbuilder.Append("Deleted.[DESC] As 删除的DESC;");
            try
            {
                this.dataGridView3.DataSource = SqlHelper.GetDataSet(sbuilder.ToString()).Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            string strSql = "Merge Top (2)  TargetTable As T ";
            strSql += "Using SourceTable As S On T.id = S.id ";
            strSql += "When Matched Then Update  Set T.[DESC]=S.[DESC] ";
            strSql += "When not Matched Then Insert Values(S.id,S.[DESC]) ";
            strSql += "When not Matched by Source Then Delete ";
            strSql += "Output $Action As Action,Inserted.id AS 插入的ID,Inserted.[DESC] AS 插入的DESC, " +
                "Deleted.id As 删除的ID,Deleted.[DESC] AS 删除的DESC; ";
            try
            {
                this.dataGridView3.DataSource = SqlHelper.GetDataSet(strSql.ToString()).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAnd_Click(object sender, EventArgs e)
        {
            string strSql = "Merge TargetTable As T ";
            strSql += "Using SourceTable As S on T.id = S.id ";
            strSql += "When not Matched And S.id = 3 ";//加入了ID=3的限制条件
            strSql += "Then Insert Values(S.id,S.[DESC]) ";
            strSql += "OUTPUT $Action AS [Action],Inserted.id As 插入的ID,Inserted.[DESC] AS 插入的DESC,Deleted.id As 删除的ID,Deleted.[DESC] AS 删除的DESC; ";
            try
            {
                this.dataGridView3.DataSource = SqlHelper.GetDataSet(strSql).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
