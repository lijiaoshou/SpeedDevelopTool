namespace SpeedDevelopTool
{
    partial class CategoryDocs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DocumentName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.FileSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PostTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DocumentName,
            this.FileSize,
            this.PostTime,
            this.FilePath});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(538, 384);
            this.dataGridView1.TabIndex = 0;
            // 
            // DocumentName
            // 
            this.DocumentName.HeaderText = "文档名";
            this.DocumentName.Name = "DocumentName";
            this.DocumentName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DocumentName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.DocumentName.Width = 300;
            // 
            // FileSize
            // 
            this.FileSize.HeaderText = "文档大小";
            this.FileSize.Name = "FileSize";
            this.FileSize.Width = 60;
            // 
            // PostTime
            // 
            this.PostTime.HeaderText = "上传时间";
            this.PostTime.Name = "PostTime";
            this.PostTime.Width = 200;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "路径";
            this.FilePath.Name = "FilePath";
            this.FilePath.Visible = false;
            // 
            // CategoryDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 384);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CategoryDocs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "相关文档";
            this.Load += new System.EventHandler(this.CategoryDocs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewLinkColumn DocumentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn PostTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
    }
}