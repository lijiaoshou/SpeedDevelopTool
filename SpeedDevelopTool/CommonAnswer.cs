using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class CommonAnswer : Form
    {
        private string category;
        public Point point { get; set; }

        public CommonAnswer()
        {
            InitializeComponent();
        }

        public CommonAnswer(string category)
        {
            this.category = category;
            InitializeComponent();
        }

        private void CommonAnswer_Load(object sender, EventArgs e)
        {
            //point = this.Location;
            //this.Move += new EventHandler(CommonAnswer_Move);
            string url = "http://u8dev.yonyou.com/home/ask/index.aspx?r=iszhishi&v=0&key=" + HttpUtility.UrlEncode(this.category);
            webBrowser1.Navigate(url);
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            this.Location = point;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error+= new HtmlElementErrorEventHandler(Window_Error);
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }
}
}
