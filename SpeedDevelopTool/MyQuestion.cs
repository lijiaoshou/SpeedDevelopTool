﻿using System;
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
        public Point point { get; set; }

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
            webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=my&v=0");
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            this.Location = point;
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
