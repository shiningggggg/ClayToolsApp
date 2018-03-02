namespace SqlExercise.Linq_Exercise
{
    partial class LinqExerciseControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnQuery = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCreateTable = new System.Windows.Forms.Button();
            this.btnInsertData = new System.Windows.Forms.Button();
            this.btnShowData = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnNonQuery = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(16, 13);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(170, 35);
            this.btnQuery.TabIndex = 0;
            this.btnQuery.Text = "开始联查";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 165);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(425, 408);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnCreateTable
            // 
            this.btnCreateTable.Location = new System.Drawing.Point(193, 13);
            this.btnCreateTable.Name = "btnCreateTable";
            this.btnCreateTable.Size = new System.Drawing.Size(161, 35);
            this.btnCreateTable.TabIndex = 2;
            this.btnCreateTable.Text = "创建表";
            this.btnCreateTable.UseVisualStyleBackColor = true;
            this.btnCreateTable.Click += new System.EventHandler(this.btnCreateTable_Click);
            // 
            // btnInsertData
            // 
            this.btnInsertData.Location = new System.Drawing.Point(361, 13);
            this.btnInsertData.Name = "btnInsertData";
            this.btnInsertData.Size = new System.Drawing.Size(154, 35);
            this.btnInsertData.TabIndex = 3;
            this.btnInsertData.Text = "插入数据";
            this.btnInsertData.UseVisualStyleBackColor = true;
            this.btnInsertData.Click += new System.EventHandler(this.btnInsertData_Click);
            // 
            // btnShowData
            // 
            this.btnShowData.Location = new System.Drawing.Point(522, 13);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(160, 35);
            this.btnShowData.TabIndex = 4;
            this.btnShowData.Text = "显示数据";
            this.btnShowData.UseVisualStyleBackColor = true;
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(456, 165);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(434, 408);
            this.dataGridView2.TabIndex = 5;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(909, 165);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowTemplate.Height = 30;
            this.dataGridView3.Size = new System.Drawing.Size(449, 408);
            this.dataGridView3.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 54);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1342, 105);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "2";
            // 
            // btnNonQuery
            // 
            this.btnNonQuery.Location = new System.Drawing.Point(689, 13);
            this.btnNonQuery.Name = "btnNonQuery";
            this.btnNonQuery.Size = new System.Drawing.Size(158, 35);
            this.btnNonQuery.TabIndex = 8;
            this.btnNonQuery.Text = "执行非查询操作";
            this.btnNonQuery.UseVisualStyleBackColor = true;
            this.btnNonQuery.Click += new System.EventHandler(this.btnNonQuery_Click);
            // 
            // LinqExerciseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNonQuery);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnShowData);
            this.Controls.Add(this.btnInsertData);
            this.Controls.Add(this.btnCreateTable);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnQuery);
            this.Name = "LinqExerciseControl";
            this.Size = new System.Drawing.Size(1374, 603);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCreateTable;
        private System.Windows.Forms.Button btnInsertData;
        private System.Windows.Forms.Button btnShowData;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnNonQuery;
    }
}
