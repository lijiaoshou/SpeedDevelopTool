using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class WebLogin : Form
    {
        private string NavigateUrl { get; set; }

        public WebLogin()
        {
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

                U8DevDocs.u8DevServiceSoapClient client = new U8DevDocs.u8DevServiceSoapClient();
                DataSet ds = client.getAskCount();

                //CookieContainer myCookieContainer = new CookieContainer();

                //string cookieStr = this.webBrowser1.Document.Cookie;
                //string[] cookstr = cookieStr.Split(';');
                //foreach (string str in cookstr)
                //{
                //    string[] cookieNameValue = str.Split('=');
                //    Cookie ck = new Cookie(cookieNameValue[0].Trim().ToString(), cookieNameValue[1].Trim().ToString());
                //    ck.Domain = "u8dev.yonyou.com";
                //    myCookieContainer.Add(ck);
                //}
            }
        }
    }
}
