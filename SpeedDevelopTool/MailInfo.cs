using CommonLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SpeedDevelopTool
{
    public partial class MailInfo : Form
    {
        public MailInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //问题模块
            string module = txtModule.Text;
            //简要描述
            string desc = txtDesc.Text;
            //联系方式
            string connect = txtConnect.Text;

            //验证是否输全必要参数
            if (string.IsNullOrEmpty(module.Trim())||string.IsNullOrEmpty(desc.Trim())||string.IsNullOrEmpty(connect.Trim()))
            {
                MessageBox.Show("请输入必填信息！");
                return;
            }

            //通过<br/>实现邮件中内容的换行
            string msg = @"所属模块：" + module + "<br />" +
                          "简要描述：" + desc + "<br />" + 
                          "客户联系方式：" + connect + "<br />";

            //发送邮件并判断是否发送成功
            bool result=Common.SendEmail(msg);

            if (result)
            {
                MessageBox.Show("邮件发送成功!");
                this.Close();
            }
            else
            {
                MessageBox.Show("邮件发送失败，稍后再试~");
                this.Close();
            }
        }
    }
}
