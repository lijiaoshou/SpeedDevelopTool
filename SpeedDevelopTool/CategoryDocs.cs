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

        private Point point;

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
            point = this.Location;
            this.Move += new EventHandler(CategoryDocs_Move);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(DataGridView1_CellClick);

            string category = Config.GetValueByKey(this.choiceOpiton, "ChineseName");
            string FilePath = "";
            int FileSize = 0;
            string FileSizeStr = "";
            string PostTime = "";
            string ExName = "";//后缀名
            string DocumentName = "";//不带后缀名
            string OldName = "";//带后缀名
 
            U8DevDocs.u8DevServiceSoapClient u8devClient = new U8DevDocs.u8DevServiceSoapClient();
            DataSet dsDocs = new DataSet();
            DataTable dtDocs = new DataTable();

            dsDocs = u8devClient.getDocument(category);
            if (dsDocs != null && dsDocs.Tables.Count > 0)
            {
                dtDocs = dsDocs.Tables[0];
                dataGridView1.Rows.Add(dtDocs.Rows.Count);
                if (dtDocs != null && dtDocs.Rows.Count>0)
                {
                    for (int i = 0; i < dtDocs.Rows.Count; i++)
                    {
                        FilePath= dtDocs.Rows[i]["FilePath"].ToString();

                        FileSize= Convert.ToInt32(dtDocs.Rows[i]["FileSize"])/1000;
                        if (FileSize == 0)
                        {
                            FileSizeStr = dtDocs.Rows[i]["FileSize"].ToString() + "KB";
                        }
                        else
                        {
                            FileSizeStr = FileSize.ToString() + "MB";
                        }

                        PostTime= dtDocs.Rows[i]["PostTime"].ToString();
                        ExName= dtDocs.Rows[i]["ExName"].ToString();
                        DocumentName= dtDocs.Rows[i]["DocumentName"].ToString();
                        OldName= dtDocs.Rows[i]["OldName"].ToString();

                        dataGridView1.Rows[i].Cells[0].Value = DocumentName;
                        dataGridView1.Rows[i].Cells[1].Value = FileSizeStr;
                        dataGridView1.Rows[i].Cells[2].Value = PostTime;
                        dataGridView1.Rows[i].Cells[3].Value = FilePath;

                    }
                }
            }
        }

        public void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "DocumentName")
            {
                string target = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                Process.Start("IEXPLORE.EXE", "http://" + target);
            }
        }

        public void CategoryDocs_Move(object sender, EventArgs e)
        {
            this.Location = point;
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
