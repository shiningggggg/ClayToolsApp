using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlExercise.Procedure
{
    public partial class ProcedureControl : UserControl
    {
        public ProcedureControl()
        {
            InitializeComponent();
        }

        private void btnInProcedure_Click(object sender, EventArgs e)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("use pubs\r\n");
            sbuilder.Append("go\r\n");
            sbuilder.Append("create proc P_Titles_ByTitleID_SelectPrice\r\n");
            sbuilder.Append("@title_id varchar(6)\r\n");
            sbuilder.Append("as\r\n");
            sbuilder.Append("select price from titles where title_id=@title_id\r\n");
            sbuilder.Append("执行\r\n");
            sbuilder.Append("exec P_Titles_ByTitleID_SelectPrice 'BU1032'\r\n");
            this.textBox1.Text = sbuilder.ToString();
        }

        private void btnInOutProcedure_Click(object sender, EventArgs e)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("create proc P_Titles_ByTitleID_SelectPrice\r\n");
            sbuilder.Append("@title_id varchar(6),--入参\r\n");
            sbuilder.Append("@price money output  --出参【出参加标识(output)】\r\n");
            sbuilder.Append("as\r\n");
            sbuilder.Append("select @price=price from titles where \r\n");
            sbuilder.Append("title_id=@title_id\r\n");
            sbuilder.Append("出参的@在=左边");
            sbuilder.Append("执行\r\n");
            sbuilder.Append("declare @price2 money--先声明变量\r\n");
            sbuilder.Append("exec P_Titles_ByTitleID_SelectPrice--之后再调用\r\n");
            sbuilder.Append("@title_id='BU1032'\r\n");
            sbuilder.Append("@price=@price2 output\r\n");
            sbuilder.Append("select @price2---再之后在查声明变量\r\n");
            sbuilder.Append("出参要声明，配参后面要加output标识，之后再查声明变量。");
            this.textBox1.Text = sbuilder.ToString();

        }
    }
}
