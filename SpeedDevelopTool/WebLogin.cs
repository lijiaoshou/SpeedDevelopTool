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
        public Point point { get; set; }


        internal string NavigateUrl { get; set; }

        public WebLogin()
        {
            InitializeComponent();
        }

        public WebLogin(int length,int locationX,string navigateUrl)
        {
            this.NavigateUrl = navigateUrl;
            InitializeComponent();
        }

        public WebLogin(string navigateUrl)
        {
            InitializeComponent();
            
            this.NavigateUrl = navigateUrl;
        }

        private void WebLogin_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(NavigateUrl);
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            this.Location = point;
        }

        private void WebDocumentClick(object sender,HtmlElementEventArgs e) 
        {
            
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            if (webBrowser1.Url.ToString() == "http://u8dev.yonyou.com/home/ask/index.aspx?r=my")
            {
                //刷新主窗体中的两个webbrowser控件
                Middle.DoSendMessage("");

                this.Hide();
            }
            else
            {
                
            }
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string userEmail = "";

            try
            {
                userEmail = webBrowser1.Document.GetElementById("TxtLogEmail").GetAttribute("value");
                Config.RemoveChildNode(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.xml", "UserEmail");
                Config.AddChildNode(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.xml", "Email", userEmail);
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}
