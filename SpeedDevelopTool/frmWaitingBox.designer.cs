namespace WinForm_Test
{
    partial class frmWaitingBox
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labTimer = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labTimer
            // 
            this.labTimer.AutoSize = true;
            this.labTimer.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labTimer.ForeColor = System.Drawing.Color.Red;
            this.labTimer.Location = new System.Drawing.Point(159, 76);
            this.labTimer.Name = "labTimer";
            this.labTimer.Size = new System.Drawing.Size(30, 16);
            this.labTimer.TabIndex = 9;
            this.labTimer.Text = "0秒";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Silver;
            //this.pictureBox1.Image = global::SpeedDevelopTool.Properties.Resources.加载2;
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(143, 133);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // labMessage
            // 
            this.labMessage.AutoSize = true;
            this.labMessage.Font = new System.Drawing.Font("Tahoma", 9.5F);
            this.labMessage.Location = new System.Drawing.Point(156, 53);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(170, 16);
            this.labMessage.TabIndex = 7;
            this.labMessage.Text = "正在处理数据，请稍后...";
            // 
            // frmWaitingBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(346, 151);
            this.Controls.Add(this.labTimer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWaitingBox";
            this.Opacity = 0.01D;
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "frmWaitingBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWaitingBox_FormClosing);
            this.Shown += new System.EventHandler(this.frmWaitingBox_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labTimer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labMessage;
    }
}