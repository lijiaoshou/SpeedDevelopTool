using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        private const string REGISTKEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe";

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //RegistryKey rkey = Registry.LocalMachine;
         
            //RegistryKey rkey1 = rkey.OpenSubKey(REGISTKEY, false);
            //string visualStudioPath = rkey1.GetValue("").ToString();

            //ProcessStartInfo startInfo =new ProcessStartInfo(visualStudioPath);
            //startInfo.Arguments = "/rebuild debug "+ @"G:\C#Program\U8Code\栏目\LanMu\LanMu.sln";

            //Process process = new Process();
            //process.StartInfo = startInfo;

            //process.Start();
            //process.WaitForExit();

            //MessageBox.Show("编译完成");

            button1.Click += new EventHandler(test);
        }

        protected static void test(object sender, EventArgs e)
        {
            MessageBox.Show("adf");
        }
    }
}
