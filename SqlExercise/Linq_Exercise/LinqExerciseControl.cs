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

namespace SqlExercise.Linq_Exercise
{
    public partial class LinqExerciseControl : UserControl
    {
        public LinqExerciseControl()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            string test = this.textBox1.Text;
            if (test == "1")
            {
                DataTable headTable = SqlHelper.GetDataSet("SELECT * FROM dtHead").Tables[0];
                DataTable tailTable = SqlHelper.GetDataSet("SELECT * FROM dtTail").Tables[0];
                var query = from dt1 in headTable.AsEnumerable()
                            from dt2 in tailTable.AsEnumerable()
                            where dt1.Field<int>("GoodID") == dt2.Field<int>("GoodID")
                            select new
                            {
                                GoodID = dt1.Field<Int32>("GoodID"),
                                GoodName = dt1.Field<string>("GoodName"),
                                Num = dt2.Field<int>("Num"),
                                Money = dt2.Field<decimal>("Money")
                            };
                var list = query.ToList();
                DataTable dtAll = CreateDataTableSchema();

                foreach (var obj in query)
                {
                    dtAll.Rows.Add(obj.GoodID, obj.GoodName, obj.Num, obj.Money);
                }
                this.dataGridView3.DataSource = dtAll;
            }
            else if (test == "2")
            {
                DataTable headTable = SqlHelper.GetDataSet("SELECT * FROM dtHead").Tables[0];
                DataTable tailTable = SqlHelper.GetDataSet("SELECT * FROM dtTail").Tables[0];
                var query = from htable in headTable.AsEnumerable()
                            join tTail in tailTable.AsEnumerable()
                            on htable.Field<Int32>("GoodID") equals tTail.Field<Int32>("GoodID")
                            select htable.ItemArray.Concat(tTail.ItemArray.Skip(1));
                DataTable dtAll = CreateDataTableSchema();
                foreach (var obj in query)
                {
                    DataRow dr = dtAll.NewRow();
                    dr.ItemArray = obj.ToArray();
                    dtAll.Rows.Add(dr);
                }
                this.dataGridView3.DataSource = dtAll;
            }
            else if (test == "3")
            {
                DataSet ds = new DataSet();
                DataTable headTable = SqlHelper.GetDataSet("SELECT * FROM dtHead").Tables[0];
                DataTable tailTable = SqlHelper.GetDataSet("SELECT * FROM dtTail").Tables[0];
                DataColumn[] leftRelationCols = new DataColumn[headTable.Columns.Count];
                //for (int i = 0; i < headTable.Columns.Count; i++)
                    //leftRelationCols[i] = headTable.Columns[i].ColumnName;
            }
        }
        private DataTable CreateDataTableSchema()
        {
            DataTable dtAll = new DataTable();
            dtAll.Columns.Add("GoodID", typeof(int));
            dtAll.Columns.Add("GoodName", typeof(string));
            dtAll.Columns.Add("Num", typeof(int));
            dtAll.Columns.Add("Money", typeof(decimal));
            return dtAll;
        }
        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            string sql1 = "create table dtHead (GoodID INT primary key Identity not null,GoodName nvarchar(100) null)";
            string sql2 = "create table dtTail (GoodID INT primary key Identity not null,Num int null,Money decimal null)";
            try
            {
                SqlHelper.ExecuteNonQuery(sql1);
                SqlHelper.ExecuteNonQuery(sql2);
                MessageBox.Show("创建成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsertData_Click(object sender, EventArgs e)
        {
            string sql = "insert into dtHead values('青岛纯生'),('哈尔滨啤酒')";
            string sql2 = "insert into dtTail values(10,30),(5,20)";
            try
            {
                SqlHelper.ExecuteNonQuery(sql);
                SqlHelper.ExecuteNonQuery(sql2);
                MessageBox.Show("插入成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            string sqlHead = "select * from dtHead";
            string sqlTail = "select * from dtTail";
            try
            {
                this.dataGridView1.DataSource = SqlHelper.GetDataSet(sqlHead).Tables[0];
                this.dataGridView2.DataSource = SqlHelper.GetDataSet(sqlTail).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 执行Sql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNonQuery_Click(object sender, EventArgs e)
        {
            string sql = this.textBox1.Text;
            if (string.IsNullOrEmpty(sql))
            {
                MessageBox.Show("无sql");
                return;
            }
            try
            {
                SqlHelper.ExecuteNonQuery(sql);
                MessageBox.Show("执行成功！");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
