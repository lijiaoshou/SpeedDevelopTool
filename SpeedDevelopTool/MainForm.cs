using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using System.Diagnostics;
using System.Reflection;
using ICSharpCode.TextEditor.Document;
using System.IO;
using UFIDA.U8.Portal.Proxy.editors;
using UFIDA.U8.Portal.Framework.MainFrames;
using UFIDA.U8.Portal.Proxy.Actions;
using UFSoft.U8.Framework.Login.UI;
using UFIDA.U8.Portal.Framework.Actions;

namespace SpeedDevelopTool
{
    public partial class MainForm : UserControl, INetUserControl
    {
        ICSharpCode.TextEditor.TextEditorControl txtContent = new ICSharpCode.TextEditor.TextEditorControl();

        public string choiceOpiton { get; set; }

        public IEditorPart EditorPart { get; set; }

        public IEditorInput EditorInput{ get; set; }

        public string Title{ get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 搜索相关文档委托绑定的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartProcessDoc(object sender, EventArgs e)
        {
            //得到界面显示的名称（包括后缀名）
            Label lab = sender as Label;

            //是label控件则查找和label控件文字相同的文档
            if (lab != null)
            {
                Process.Start(System.Environment.CurrentDirectory + @"\" + choiceOpiton + @"\相关文档\" + lab.Text);
            }
        }

        /// <summary>
        /// 搜索常见问题委托绑定的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartProcessPro(object sender, EventArgs e)
        {
            //得到界面现实的名称（包括后缀名）
            Label lab = sender as Label;

            //是label控件则查找和label控件文字相同的文档
            if (lab != null)
            {
                Process.Start(System.Environment.CurrentDirectory + @"\" + choiceOpiton + @"\常见问题\" + lab.Text);
            }
        }

        /// <summary>
        /// 开发模式下用.cs文件生成xml文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Common.GenerateXmlDocument(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + choiceOpiton + ".xml");
        }


        /// <summary>
        /// 实时求助按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MailInfo mainForm = new MailInfo();
            mainForm.ShowDialog();
        }

        /// <summary>
        /// 复制选中代码到黏贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtContent.ActiveTextAreaControl.SelectionManager.SelectedText);
            MessageBox.Show("选中内容已经复制到黏贴板！");
        }


        /// <summary>
        /// 复制全部代码到黏贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtContent.Text);
            MessageBox.Show("全部内容已经复制到黏贴板！");
        }

        /// <summary>
        /// 主界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm1_Load(object sender, EventArgs e)
        {
            #region 初始化功能演示区

            #region old
            ////根据初始化的choiceOption参数决定动态加载哪个用户控件
            //switch (choiceOpiton)
            //{
            //    case "LanMu":
            //        LanMu lanmuForm = new LanMu();
            //        //动态加载用户控件到主界面中
            //        groupBox1.Controls.Add(lanmuForm);

            //        //控件用户控件在主界面中的位置
            //        lanmuForm.Top = 20;
            //        lanmuForm.Left = 10;
            //        break;
            //    case "Filter":
            //        Filter filterForm = new Filter();
            //        groupBox1.Controls.Add(filterForm);
            //        filterForm.Top = 20;
            //        filterForm.Left = 10;
            //        break;
            //    case "Ref":
            //        Ref refForm = new Ref();
            //        groupBox1.Controls.Add(refForm);
            //        refForm.Top = 20;
            //        refForm.Left = 10;
            //        break;
            //    case "ToolControl":
            //        ToolControl toolForm = new ToolControl();
            //        groupBox1.Controls.Add(toolForm);
            //        toolForm.Top = 20;
            //        toolForm.Left = 10;
            //        break;
            //    case "PortalMenu":
            //        PortalMenu portalForm = new PortalMenu();
            //        groupBox1.Controls.Add(portalForm);
            //        portalForm.Top = 20;
            //        portalForm.Left = 10;
            //        break;
            //    case "Login":
            //        Login loginForm = new Login();
            //        groupBox1.Controls.Add(loginForm);
            //        loginForm.Top = 20;
            //        loginForm.Left = 10;
            //        break;
            //    case "Permission":
            //        Permission permissionForm = new Permission();
            //        groupBox1.Controls.Add(permissionForm);
            //        permissionForm.Top = 20;
            //        permissionForm.Left = 10;
            //        break;
            //    case "Voucher":
            //        Voucher voucherForm = new Voucher();
            //        groupBox1.Controls.Add(voucherForm);
            //        voucherForm.Top = 20;
            //        voucherForm.Left = 10;
            //        break;
            //    case "Receipt":
            //        Reciept recieptForm = new Reciept();
            //        groupBox1.Controls.Add(recieptForm);
            //        recieptForm.Top = 20;
            //        recieptForm.Left = 10;
            //        break;
            //    case "Report":
            //        Report reportForm = new Report();
            //        groupBox1.Controls.Add(reportForm);
            //        reportForm.Top = 20;
            //        reportForm.Left = 10;
            //        break;
            //    case "EAI":
            //        EAI eaiForm = new EAI();
            //        groupBox1.Controls.Add(eaiForm);
            //        eaiForm.Top = 20;
            //        eaiForm.Left = 10;
            //        break;
            //    case "WorkFlow":
            //        WorkFlow workflowForm = new WorkFlow();
            //        groupBox1.Controls.Add(workflowForm);
            //        workflowForm.Top = 20;
            //        workflowForm.Left = 10;
            //        break;
            //}
            #endregion

            #region new

            //获取按钮控件类的全名称
            string fullName = Config.GetValueByKey(this.choiceOpiton, "fullClassName");

            //获取dll所在路径
            string dllPath = Config.GetValueByKey(this.choiceOpiton, "dllPath");

            //获取dll名称/exe名称
            string dllName = Config.GetValueByKey(this.choiceOpiton, "dllName");

            //加载程序集(dll文件地址)，使用Assembly类   
            Assembly assembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + dllPath + dllName);

            ////获取类型，参数（名称空间+类）   
            //Type type = assembly.GetType(fullName);

            //创建该对象的实例，object类型，参数（名称空间+类）   
            object instance = assembly.CreateInstance(fullName);

            Form uForm = instance as Form;

            if (uForm != null)
            {
                //去掉uForm的边框
                uForm.FormBorderStyle = FormBorderStyle.None;
                //设置窗体为非顶级控件
                uForm.TopLevel = false;

                //动态加载用户控件到主界面中
                groupBox1.Controls.Add(uForm);
                uForm.Show();

                //控件用户控件在主界面中的位置
                uForm.Top = 20;
                uForm.Left = 10;
            }
            else //同时支持form和usercontrol
            {
                UserControl usercontrol = instance as UserControl;
                if(usercontrol != null)
                {
                    //动态加载用户控件到主界面中
                    groupBox1.Controls.Add(usercontrol);

                    //控件用户控件在主界面中的位置
                    usercontrol.Top = 20;
                    usercontrol.Left = 10;
                }
            }
            #endregion

            #endregion

            #region 初始化相关文档
            string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");
            //得到对应公共控件类别下的相关文档文件夹下的（包括子文件夹）的文件
            List<FileInfo> fileInfo = CommonLib.Common.GetAllFilesInDirectory(System.Environment.CurrentDirectory  + categoryPath + @"相关文档");
            for (int i = 0; i < fileInfo.Count; i++)
            {
                LinkLabel lab = new LinkLabel();

                //展示样式设置
                lab.Width = groupBox3.Width;
                lab.Text = fileInfo[i].Name;
                lab.ForeColor = Color.Blue;
                lab.Left = 15;
                lab.Top = lab.Height * i + 40;

                //给文字绑定点击时触发的委托方法
                lab.Click += new EventHandler(StartProcessDoc);

                //将文档生成的可操作内容加载到父控件中
                groupBox3.Controls.Add(lab);
            }
            #endregion

            #region 初始化常见问题
            //得到对应公共空间类别下的常见问题文件夹下的（包括子文件夹）的文件
            List<FileInfo> fileInfoP = CommonLib.Common.GetAllFilesInDirectory(System.Environment.CurrentDirectory +categoryPath + @"常见问题");
            for (int i = 0; i < fileInfoP.Count; i++)
            {
                LinkLabel lab = new LinkLabel();

                //展示样式设置
                lab.Width = groupBox4.Width;
                lab.Text = fileInfoP[i].Name;
                lab.ForeColor = Color.Blue;
                lab.Left = 15;
                lab.Top = lab.Height * i + 40;

                //给文字绑定点击时触发的委托方法
                lab.Click += new EventHandler(StartProcessPro);

                //将文档生成的可操作内容加载到父控件中
                groupBox4.Controls.Add(lab);
            }
            #endregion

            #region 初始化代码控件

            txtContent.Width = groupBox2.Width-20;
            txtContent.Height = groupBox2.Height-10;
            txtContent.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txtContent.Encoding = Encoding.Default;
            txtContent.Location = new Point(-3, 50);
            txtContent.AutoScroll = false;
            //txtContent.AutoScrollMargin = new Size(1, 1);
            txtContent.Text = "";
            groupBox2.Controls.Add(txtContent);
            #endregion

            #region 跟踪功能区代码
            TraceOperate();
            #endregion
        }

        /// <summary>
        /// 给功能演示区的操作绑定实时监控
        /// </summary>
        private void TraceOperate()
        {
            //获取groupBox1下的所有控件
            Control.ControlCollection controls = groupBox1.Controls;
            foreach (Control ctrl in controls)
            {
                //获取用户控件
                if ((ctrl is UserControl)||(ctrl is Form))
                {
                    LoopCtrl(ctrl);
                }
            }
        }

        /// <summary>
        /// 循环用户控件内部的所有控件，并为需要的控件绑定监听
        /// </summary>
        /// <param name="ctrl">控件</param>
        private void LoopCtrl(Control ctrl)
        {
            //循环对用户控件上的每一个控件进行操作
            foreach (Control ctrl1 in ctrl.Controls)
            {
                PropertyInfo propertyInfo = ctrl1.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                EventHandlerList eventHandlerList = propertyInfo.GetValue(ctrl1, new object[] { }) as EventHandlerList;

                Type contorlType = ctrl1.GetType();

                //获得控件上的所有事件
                EventInfo[] events = contorlType.GetEvents();
                for (int i = 0; i < events.Length; i++)
                {
                    #region 对于控件上没有绑定操作方法的事件直接返回
                    string funcNames = Common.GetFunctionNames(ctrl1, events[i]);
                    if (string.IsNullOrEmpty(funcNames))
                    {
                        continue;
                    }
                    #endregion

                    #region 给已经绑定操作方法的控件添加监视功能

                    //自定义事件参数类，传递事件名称
                    MyEventArgs args = new MyEventArgs(events[i]);

                    //获取事件处理类型，事件处理类型有很多种，EventHandler,MouseEventHandler.....
                    string handleType = events[i].EventHandlerType.Name;

                    //暂时只考虑处理句柄为EventHandler的
                    if (handleType == "EventHandler")
                    {
                        //针对控件的每一个事件进行绑定
                        events[i].AddEventHandler(ctrl1, new EventHandler((object sender, EventArgs e) => { TraceMethod(sender, args); }));
                    }
                    #endregion
                }

                //如果用户控件上的控件仍然是容器控件，递归遍历容器中的控件
                if (ctrl1.Controls != null)
                {
                    foreach (Control ctrl2 in ctrl1.Controls)
                    {
                        if (ctrl2 != null)
                        {
                            LoopCtrl(ctrl2);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 监控执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceMethod(object sender, EventArgs e)
        {
            MyEventArgs mye = e as MyEventArgs;
            if (mye == null)
            {
                return;
            }
            Control control = sender as Control;
            if (control != null)
            {
                #region old xml文件办法
                //从xml文件中读取对应control的实时代码
                //string code = CommonLib.Common.ReadValueByXml(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + choiceOpiton + ".xml", btn.Name);
                #endregion

                #region 直接分析源文件办法

                StringBuilder sbCodes = new StringBuilder();

                //获取绑定到该控件指定事件上的所有方法名
                string functionNames = Common.GetFunctionNames(control, mye.EventName);
                functionNames = functionNames.Replace(";TraceMethod", "").Replace(";<TraceOperate>b__0", "").Replace(";<LoopCtrl>b__0", "");
                //查出所有方法名的方法体
                sbCodes.Append(mye.EventName.Name + "事件代码：" + "\r\n" + Common.GetFunctionBodys(functionNames, choiceOpiton));

                #endregion


                //加载查询到的源码到源码展示控件上
                txtContent.Text = sbCodes.ToString().Replace("{", "{" + "\r\n").Replace("}", "}" + "\r\n").Replace(";", ";" + "\r\n");
            }
            else
            {
                #region old xml文件办法
                //CheckBox cbx = sender as CheckBox;
                //if (cbx != null)
                //{
                //    string code = CommonLib.Common.ReadValueByXml(AppDomain.CurrentDomain.BaseDirectory + @"xml\" + choiceOpiton + ".xml", cbx.Name);
                //    txtContent.Text = code.Replace("{", "{" + "\r\n").Replace("}", "}" + "\r\n").Replace(";", ";" + "\r\n");
                //}
                #endregion

                #region 直接分析源文件办法
                txtContent.Text = "";
                #endregion

            }
        }

        public bool CloseEvent()
        {
            return true;
        }

        public NetAction[] CreateToolbar(clsLogin login)
        {
            //IActionDelegate nsd = new NetSampleDelegate();

            /////给按钮绑定相关操作
            //NetAction ac = new NetAction("sss", nsd);
            //NetAction[] aclist;
            //aclist = new NetAction[1];

            ////按钮显示文字
            //ac.Text = "Button";
            //ac.Tag = this;
            //aclist[0] = ac;
            //return aclist;
            return null;
        }

        public Control CreateControl(clsLogin login, string MenuID, string Paramters)
        {
            return this;
        }

    }
}
