using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class WebLogin : Form
    {
        private string UserEmail = "";
        public Point point { get; set; }

        //public int MainFormWidth { get; set; }

        //public int MainFormX { get; set; }

        internal string NavigateUrl { get; set; }

        public WebLogin()
        {
            InitializeComponent();
        }

        public WebLogin(string email,int length,int locationX,string navigateUrl)
        {
            this.UserEmail = email;
            this.NavigateUrl = navigateUrl;
            //this.MainFormWidth = length;
            //this.MainFormX = locationX;
            InitializeComponent();
        }

        public WebLogin(string navigateUrl)
        {
            InitializeComponent();
            
            this.NavigateUrl = navigateUrl;
        }

        private void WebLogin_Load(object sender, EventArgs e)
        {
            //this.Width = MainFormWidth - 35;
            //this.point = new Point(MainFormX, this.Location.Y);
            this.Location = point;

            webBrowser1.Navigate(NavigateUrl);
            this.Move += new EventHandler(CommonAnswer_Move);
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            this.Location = point;
        }

        private void WebDocumentClick(object sender,HtmlElementEventArgs e) 
        {
            //HtmlElement loginButton= webBrowser1.Document.All["submit"];
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (webBrowser1.Url.ToString() == "http://u8dev.yonyou.com/home/ask/index.aspx?r=my")
            {
                //刷新主窗体中的两个webbrowser控件
                Middle.DoSendMessage("");

                this.Close();
            }
            else
            {
                Config.RemoveChildNode(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.xml", "UserEmail");
                Config.AddChildNode(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.xml", "Email", UserEmail);
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string userEmail = "";

            try
            {
                userEmail = webBrowser1.Document.GetElementById("TxtLogEmail").GetAttribute("value");
            }
            catch (Exception ex)
            {
               
            }

            UserEmail = userEmail;
        }
    }
}
