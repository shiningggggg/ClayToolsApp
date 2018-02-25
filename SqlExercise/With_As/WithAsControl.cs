using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SqlExercise.With_As
{
    public partial class WithAsControl : UserControl
    {
        public WithAsControl()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            StringBuilder sbuilder = new StringBuilder();
            sbuilder.Append("先看一个嵌套的查询语句\r\n");
            sbuilder.Append("select * from person.StateProvince where CountryRegionCode in\r\n");
            sbuilder.Append("(select CountryRegionCode from person.CountryRegion where Name like 'C%')\r\n");
            sbuilder.Append("如果嵌套的层次过多，会使SQL语句难以阅读和维护\r\n");
            sbuilder.Append("declare @t table(CountryRegionCode nvarchar(3))\r\n");
            sbuilder.Append("insert into @t(CountryRegionCode)(select CountryRegionCode from person.CountryRegion where Name like 'C%')\r\n");
            sbuilder.Append("select * from person.StateProvince where CountryRegionCode in (select * from @t)\r\n");
            sbuilder.Append("表变量使SQL更容易维护，但会带来性能损失，表变量实际上使用了临时表，增加了I/O开销，因此不太适合数据量大且频繁查询的情况");
            this.txtBox.Text = sbuilder.ToString();
            StringBuilder sbuilder2 = new StringBuilder();
            sbuilder2.Append("CTE时SQL语句可维护性增高，同时比表变量的效率高的多--CTE:公共表表达式\r\n");
            sbuilder2.Append("下面使用CTE来解决上面的问题\r\n");
            sbuilder2.Append("with cr as \r\n");
            sbuilder2.Append("(select CountryRegionCode from person.CountryRegionCode where Name like 'C%')\r\n");
            sbuilder2.Append("select * from person.StateProvince where CountryRegionCode in (select * from cr)\r\n");
            sbuilder2.Append("cr就是一个CTE，CTE后面必须直接跟使用CTE的SQL语句\r\n");
            sbuilder2.Append("CTE后面可以跟其他CTE，但只能用一个with,多个CTE中间用逗号分隔\r\n");
            sbuilder2.Append("如果CTE的表达式名称与某个数据表或视图重名，则紧跟CTE后面的sql语句使用的仍然是CTE\r\n");
            sbuilder2.Append("如果CTE时一个SQL语句的一部分，则with前面的语句必须以分号结尾\r\n");
            this.txtBoxCTE.Text = sbuilder2.ToString();
        }
    }
}
