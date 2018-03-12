namespace SqlExercise.Procedure
{
    partial class ProcedureControl
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
            this.btnInProcedure = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnInOutProcedure = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInProcedure
            // 
            this.btnInProcedure.Location = new System.Drawing.Point(15, 24);
            this.btnInProcedure.Name = "btnInProcedure";
            this.btnInProcedure.Size = new System.Drawing.Size(135, 36);
            this.btnInProcedure.TabIndex = 1;
            this.btnInProcedure.Text = "带入参数存过";
            this.btnInProcedure.UseVisualStyleBackColor = true;
            this.btnInProcedure.Click += new System.EventHandler(this.btnInProcedure_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(15, 83);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(932, 455);
            this.textBox1.TabIndex = 2;
            // 
            // btnInOutProcedure
            // 
            this.btnInOutProcedure.Location = new System.Drawing.Point(157, 24);
            this.btnInOutProcedure.Name = "btnInOutProcedure";
            this.btnInOutProcedure.Size = new System.Drawing.Size(164, 36);
            this.btnInOutProcedure.TabIndex = 3;
            this.btnInOutProcedure.Text = "带入带出参数存过";
            this.btnInOutProcedure.UseVisualStyleBackColor = true;
            this.btnInOutProcedure.Click += new System.EventHandler(this.btnInOutProcedure_Click);
            // 
            // ProcedureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnInOutProcedure);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnInProcedure);
            this.Name = "ProcedureControl";
            this.Size = new System.Drawing.Size(964, 554);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnInProcedure;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnInOutProcedure;
    }
}
