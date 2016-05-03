using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class StartForm : Form
    {

        public StartForm()
        {
            InitializeComponent();
        }

        #region old

        ///// <summary>
        ///// 栏目
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("LanMu");
        //    mainForm.ShowDialog();
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{

        //}

        ///// <summary>
        ///// 过滤
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Filter");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 参照
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Ref");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 工具栏控件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("ToolControl");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 门户菜单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("PortalMenu");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 登录相关
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button6_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Login");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 数据权限
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button12_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Permission");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 单据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button11_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Voucher");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 表单
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button10_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Reciept");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 报表
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button9_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("Report");
        //    mainForm.ShowDialog();

        //}

        ///// <summary>
        ///// EAI+API
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button8_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("EAI");
        //    mainForm.ShowDialog();
        //}

        ///// <summary>
        ///// 工作流
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void button7_Click(object sender, EventArgs e)
        //{
        //    MainForm mainForm = new MainForm("WorkFlow");
        //    mainForm.ShowDialog();
        //}

        #endregion

        private void StartForm_Load(object sender, EventArgs e)
        {
            //读取配置文件，获取已有功能配置信息
            string[] categories = CommonLib.Config.GetCategories().Split(';');
            int j = 0;

            //根据配置文件信息自动生成按钮，加载按钮到界面上
            for (int i = 0; i < categories.Length; i++)
            {
                //获取该分类的中文名称
                string cateName = CommonLib.Config.GetValueByKey(categories[i], "ChineseName");

                //生成按钮并加载到界面上
                Button btnGenerate = new Button();

                #region 控制生成按钮的样式
                //父容器
                groupBox2.Controls.Add(btnGenerate);
                //大小
                btnGenerate.Width = 75;
                btnGenerate.Height = 23;
                //位置
                btnGenerate.Left = 75*i+10;
                btnGenerate.Top = 30;
                if (btnGenerate.Left + 75 > groupBox2.Width)
                {
                    //记录换行时的i值
                    j = i;
                    btnGenerate.Left = 75 * (i-j) + 10;
                    //如果最后一个按钮的排版已经超出，则换行
                    btnGenerate.Top = 53 + i / j * 30;
                }
                #endregion

                #region 给生成的按钮绑定点击事件

                //把功能名称赋值给按钮
                btnGenerate.Name = categories[i];
                btnGenerate.Text = cateName;
                btnGenerate.Click += new EventHandler(BindClick);

                #endregion

            }
            //并且给每个按钮绑定各自的点击事件(反射)
        }

        private void BindClick(object sender,EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                MainForm mainForm = new MainForm(btn.Name);
                mainForm.ShowDialog();
            }
        }
    }
}
