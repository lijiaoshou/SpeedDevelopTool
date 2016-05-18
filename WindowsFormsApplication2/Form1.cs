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

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        private BackgroundWorker worker = new BackgroundWorker();
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public Form1()
        {

            InitializeComponent();

            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = 200;
            this.progressBar1.Step = 1;
            timer.Interval = 100;
            //timer.Tick += new EventHandler(timer_Tick);
            worker.WorkerReportsProgress = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
            timer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        //void timer_Tick(object sender, EventArgs e)
        // {
        //    if (this.progressBar1.Value< this.progressBar1.Maximum)
        //   {
        //        this.progressBar1.PerformStep();
        //     }
        // }

         void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {
            timer.Stop();
             this.progressBar1.Value = this.progressBar1.Maximum;
            MessageBox.Show("Complete!");
        }

         void worker_DoWork(object sender, DoWorkEventArgs e)
         {
            int count = 100;
            for (int i = 0; i<count; i++)
             {
                 Thread.Sleep(100);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabel1.Links[linkLabel1.Links.IndexOf(e.Link)].Visited = true;

            string target = e.Link.LinkData as string;
            if (null != target)//&& target.StartsWith("www")
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE","http://"+target);
            }
            else
            {
                MessageBox.Show("Item clicked: " + target);
            }
            //System.Diagnostics.Process.Start("u8dev.yonyou.com/uploads/document/2014/12/22/201412221000147OdU6.docx");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            progressBar1.Value += 2;
        }
    }
}
