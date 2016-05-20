using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using ImageMagick;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private BackgroundWorker worker = new BackgroundWorker();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.webBrowser1.ScriptErrorsSuppressed = true;

            //this.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=iszhishi&v=0&key="+HttpUtility.UrlEncode("过滤"));

            // Write to stream
            MagickReadSettings settings = new MagickReadSettings();
            // Tells the xc: reader the image to create should be 800x600
            settings.Width = 200;
            settings.Height = 200;

            using (MemoryStream memStream = new MemoryStream())
            {
                // Create image that is completely purple and 800x600
                using (MagickImage image = new MagickImage(AppDomain.CurrentDomain.BaseDirectory+"\\test.jpg", settings))
                {
                    // Sets the output format to png
                    image.Format = MagickFormat.Png;
                    // Write the image to the memorystream
                    image.Write(memStream);

                    
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }
    }
}
