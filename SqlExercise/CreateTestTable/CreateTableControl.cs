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

namespace SqlExercise.CreateTestTable
{
    public partial class CreateTableControl : UserControl
    {
        public CreateTableControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "create table StudentGrade(Id int primary key identity not null,Name nvarchar(50) null,Course varchar(50) null,Score int null)";
            try
            {
                SqlDataReader reader = SqlHelper.ExecuteReader("select * from sys.objects where object_id = object_id('StudentGrade')");
                if (reader.HasRows)
                {
                    MessageBoxButtons mbBottons = MessageBoxButtons.OKCancel;
                    DialogResult dr = MessageBox.Show("是否删除原表!","删除原表",mbBottons);
                    if (dr == DialogResult.OK)
                    {
                        SqlHelper.ExecuteNonQuery("drop table StudentGrade");
                        SqlHelper.ExecuteNonQuery(sql);
                    }
                }
                else
                {
                    SqlHelper.ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnShowData_Click(object sender, EventArgs e)
        {
            //表从起始行显示在dataGridView里
            DataTable table = SqlHelper.GetDataSet("select * from StudentGrade").Tables[0];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                int index = this.dataGridViewTable.Rows.Add();
                this.dataGridViewTable.Rows[index].Cells[0].Value = table.Rows[i][0];
                this.dataGridViewTable.Rows[index].Cells[1].Value = table.Rows[i][1];
                this.dataGridViewTable.Rows[index].Cells[2].Value = table.Rows[i][2];
                this.dataGridViewTable.Rows[index].Cells[3].Value = table.Rows[i][3];
            }
        }
    }
}
