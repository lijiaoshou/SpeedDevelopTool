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
    public partial class AskQuestion : Form
    {
        private string category;
        public Point point { get; set; }

        public AskQuestion()
        {
            InitializeComponent();
        }

        public AskQuestion(string category)
        {
            this.category = category;
            InitializeComponent();
        }

        private void AskQuestion_Load(object sender, EventArgs e)
        {
            point = this.Location;
            this.Move += new EventHandler(CommonAnswer_Move);
            webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/add.aspx?v=0");
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