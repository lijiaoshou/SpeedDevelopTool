using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class MyQuestion : Form
    {
        private string category;
        private Point point;

        public MyQuestion()
        {
            InitializeComponent();
        }

        public MyQuestion(string category)
        {
            this.category = category;
            InitializeComponent();
        }

        private void AskQuestion_Load(object sender, EventArgs e)
        {
            point = this.Location;
            this.Move += new EventHandler(CommonAnswer_Move);
            webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=my&v=0");
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            this.Location = point;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }
    }
}
