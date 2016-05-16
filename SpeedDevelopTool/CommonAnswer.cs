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
    public partial class CommonAnswer : Form
    {
        private string category;

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
            webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=iszhishi&v=0&key="+this.category);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.Url.ToString() != "http://u8dev.yonyou.com/" && webBrowser1.Url.ToString() != "http://u8dev.yonyou.com/register.aspx"
                && webBrowser1.Url.ToString() != "http://u8dev.yonyou.com/GetPassword.aspx")
            {
                //隐藏搜索框
                webBrowser1.Document.GetElementById("filterIpt").SetAttribute("style", "display:none;");

                //隐藏搜索按钮
                HtmlElementCollection collection = webBrowser1.Document.GetElementsByTagName("img");
                foreach (HtmlElement element in collection)
                {
                    if (element.GetAttribute("title") == "查询")
                    {
                        element.SetAttribute("style", "display:none;");
                    }
                }

                //隐藏返回上一页
                HtmlElementCollection collection1 = webBrowser1.Document.GetElementsByTagName("a");
                foreach (HtmlElement element in collection1)
                {
                    if (element.GetAttribute("title") == "返回上一页")
                    {
                        element.SetAttribute("style", "display:none;");
                    }
                }


            }
        }
    }
}
