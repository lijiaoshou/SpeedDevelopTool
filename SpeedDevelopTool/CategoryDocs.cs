using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class CategoryDocs : Form
    {
        private string choiceOpiton;

        public CategoryDocs()
        {
            InitializeComponent();
        }

        public CategoryDocs(string choice)
        {
            this.choiceOpiton = choice;
            InitializeComponent();
        }

        private void CategoryDocs_Load(object sender, EventArgs e)
        {
            string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");
            //得到对应公共控件类别下的相关文档文件夹下的（包括子文件夹）的文件
            List<FileInfo> fileInfo = Common.GetAllFilesInDirectory(AppDomain.CurrentDomain.BaseDirectory + categoryPath + @"相关文档");
            for (int i = 0; i < fileInfo.Count; i++)
            {
                LinkLabel lab = new LinkLabel();

                //展示样式设置
                lab.Width = this.Width;
                lab.Text = fileInfo[i].Name;
                lab.ForeColor = Color.Blue;
                lab.Left = 15;
                lab.Top = lab.Height * i + 40;

                //给文字绑定点击时触发的委托方法
                lab.Click += new EventHandler(StartProcessDoc);

                //将文档生成的可操作内容加载到父控件中
                this.Controls.Add(lab);
            }
        }

        private void StartProcessDoc(object sender, EventArgs e)
        {
            //得到界面显示的名称（包括后缀名）
            Label lab = sender as Label;

            //是label控件则查找和label控件文字相同的文档
            if (lab != null)
            {
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + choiceOpiton + @"\相关文档\" + lab.Text);
            }
        }
    }
}
