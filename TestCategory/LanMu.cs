using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using U8ColumnSet;
using CommonLib;

namespace TestCategory
{
    public partial class LanMu : UserControl
    {
        clsColSet colSet;

        public LanMu()
        {
            InitializeComponent();
            colSet = new clsColSet();
            string connstr = @"Provider=SQLOLEDB.1;Password=sa123456;User ID=SA;Initial Catalog=UFDATA_999_2014;
                               Data Source =.; Current Language = Simplified Chinese; Use Procedure for Prepare = 1; Auto Translate = True;
                               Packet Size = 4096; Workstation ID = lichaor; Use Encryption for Data = False;
                               Tag with column collation when possible = False";
            colSet.Init(connstr, "demo");
            colSet.setColMode("001", 1);


            //colSet.Init(Config.ConnectString, Config.UserName);
            //colSet.setColMode(Config.LanMuKey,1);
        }

        #region 暂时给一个入口，后边可以把这些东西写在配置文件中
        public LanMu(string connstr, string username, string key)
        {
            InitializeComponent();

            colSet = new clsColSet();
            colSet.Init(connstr, username);
            colSet.setColMode(key, 1);
        }
        #endregion

        /// <summary>
        /// 是否排除扩展字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                colSet.IsNotIncludeExtendField = true;
            }
            else
            {
                colSet.IsNotIncludeExtendField = false;
            }
        }

        /// <summary>
        /// 是否显示标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                colSet.isShowTitle = true;
            }
            else
            {
                colSet.isShowTitle = false;
            }
        }

        /// <summary>
        /// 是否支持双重表头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                colSet.DoubleHead = true;
            }
            else
            {
                colSet.DoubleHead = false;
            }
        }

        /// <summary>
        /// 查看汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                colSet.set_ShowSumType(true);
            }
            else
            {
                colSet.set_ShowSumType(false);
            }
        }

        /// <summary>
        /// 是否支持新增删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                colSet.AllowAddDeleteUpdate = true;
            }
            else
            {
                colSet.AllowAddDeleteUpdate = false;
            }
        }

        /// <summary>
        /// 是否允许调整栏目位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                colSet.AllowUpDown = true;
            }
            else
            {
                colSet.AllowUpDown = false;
            }
        }

        /// <summary>
        /// 弹出栏目窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLanMu_Click(object sender, EventArgs e)
        {
            colSet.setCol();
            txtSle.Text = colSet.GetSqlString();
            txtHZ.Text = colSet.GetSumStringKCGL();
            txtob.Text = colSet.GetOrderString();
            txtgb.Text = colSet.GetSqlGroupString();
            txtXml.Text = colSet.getColInfo();
        }
    }
}
