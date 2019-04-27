using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SqlExercise.Binary_Operate
{
    public partial class BinaryDataOperateControl : UserControl
    {
        public BinaryDataOperateControl()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string content = this.txtContent.Text.ToString().Trim();
            if (content.Length > 0)
            {
                try
                {
                    Stream stream = new FileStream("mydata", FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8);
                    bw.Write(content);
                    bw.Close();
                    stream.Close();
                    MessageBox.Show("OK");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        } 

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                Stream stream = new FileStream("mydata", FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(stream, Encoding.UTF8);
                string str = br.ReadString();
                this.txtBinaryContent.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
