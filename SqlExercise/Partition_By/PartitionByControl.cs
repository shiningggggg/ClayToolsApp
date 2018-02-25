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

namespace SqlExercise.Partition_By
{
    public partial class PartitionByControl : UserControl
    {
        public PartitionByControl()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select *,Row_Number() over( partition by Name order by Score) 排名 from StudentGrade";
            ClearDataGridView();
            System.Data.SqlClient.SqlDataReader reader = SqlHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = reader[0];
                this.dataGridView1.Rows[index].Cells[1].Value = reader[1];
                this.dataGridView1.Rows[index].Cells[2].Value = reader[2];
                this.dataGridView1.Rows[index].Cells[3].Value = reader[3];
                this.dataGridView1.Rows[index].Cells[4].Value = reader[4];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select *,Row_Number() over(partition by Course order by Score) 排名 from StudentGrade";
            ClearDataGridView();
            System.Data.SqlClient.SqlDataReader reader = SqlHelper.ExecuteReader(sql);
            while (reader.Read())
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = reader[0];
                this.dataGridView1.Rows[index].Cells[1].Value = reader[1];
                this.dataGridView1.Rows[index].Cells[2].Value = reader[2];
                this.dataGridView1.Rows[index].Cells[3].Value = reader[3];
                this.dataGridView1.Rows[index].Cells[4].Value = reader[4];
            }
        }
        private void ClearDataGridView()
        {
            this.dataGridView1.Rows.Clear();
        }
    }
}
