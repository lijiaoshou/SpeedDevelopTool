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
using System.Text.RegularExpressions;
using WinForm_Test;
using System.Threading;
using System.Net;
using System.Web;

namespace SpeedDevelopTool
{
    public partial class MainForm : UserControl, INetUserControl
    {
        //ICSharpCode.TextEditor.TextEditorControl txtContent = new ICSharpCode.TextEditor.TextEditorControl();
        CodeRegion.MainForm txtContentForm = new CodeRegion.MainForm();
        public CommonAnswer cmomonAnswer;
        AskQuestion askQuestion;
        MyQuestion myQuestion;
        WebLogin webLogin;
        CategoryDocs docs;
        private int cmomonAnswerX=0;

        public string userEmail
        {
            get;set;
        }

        private bool canUse = true;
        public int iniMainFormWidth = 0;

        //internal ICSharpCode.SharpDevelop.Dom.ProjectContentRegistry pcRegistry;
        internal ICSharpCode.SharpDevelop.Dom.DefaultProjectContent myProjectContent;
        internal ICSharpCode.SharpDevelop.Dom.ParseInformation parseInformation = new ICSharpCode.SharpDevelop.Dom.ParseInformation();
        //ICSharpCode.SharpDevelop.Dom.ICompilationUnit lastCompilationUnit;
        //Thread parserThread;

        //public static bool IsVisualBasic = false;

        //public const string DummyFileName = "edited.cs";

        //static readonly ICSharpCode.SharpDevelop.Dom.LanguageProperties CurrentLanguageProperties = IsVisualBasic ? ICSharpCode.SharpDevelop.Dom.LanguageProperties.VBNet : ICSharpCode.SharpDevelop.Dom.LanguageProperties.CSharp;

        AppDomain ad;

        public string choiceOpiton { get; set; }

        private bool codeIsModified = false;

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
            ProcessStart(sender);
        }

        /// <summary>
        /// 搜索常见问题委托绑定的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartProcessPro(object sender, EventArgs e)
        {
            ProcessStart(sender);
        }

        /// <summary>
        /// 处理用户查看文档的事件
        /// </summary>
        /// <param name="sender">点击控件</param>
        private void ProcessStart(object sender)
        {
            try
            {
                //得到界面显示的名称（包括后缀名）
                Label lab = sender as Label;

                //是label控件则查找和label控件文字相同的文档
                if (lab != null)
                {
                    Process.Start(System.Environment.CurrentDirectory + @"\" + choiceOpiton + @"\相关文档\" + lab.Text);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
            Clipboard.SetDataObject(txtContentForm.textEditorControl1.ActiveTextAreaControl.SelectionManager.SelectedText);
            MessageBox.Show("选中内容已经复制到黏贴板！");
        }


        /// <summary>
        /// 复制全部代码到黏贴板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(txtContentForm.textEditorControl1.Text);
            MessageBox.Show("全部内容已经复制到黏贴板！");
        }

        /// <summary>
        /// 主界面加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm1_Load(object sender, EventArgs e)
        {
            //Config.RemoveChildNode(AppDomain.CurrentDomain.BaseDirectory + "SpeedDevelopTool.xml", "UserEmail");

            ShowNoLoginImages();

            timer1.Enabled = true;

            Middle.sendEvent += new Middle.SendMessage(this.DoMethod);

            string category = Config.GetValueByKey(this.choiceOpiton, "ChineseName");

            cmomonAnswer = new CommonAnswer(category);
            cmomonAnswer.webBrowser1.ScriptErrorsSuppressed = true;
            askQuestion = new AskQuestion(category);
            myQuestion = new MyQuestion(category);
            docs = new CategoryDocs(this.choiceOpiton);
            webLogin = new WebLogin(this.Size.Width, cmomonAnswer.Location.X, "http://u8dev.yonyou.com/");


            //获取相关配置信息
            string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");
            string dllName = Config.GetValueByKey(this.choiceOpiton, "dllName");
            string basePath = AppDomain.CurrentDomain.BaseDirectory + categoryPath;
           
            try
            {
                // 初始化DEMO的dll或者exe
                File.Copy(basePath + @"\backup\" + dllName, basePath + dllName, true);

                //框架换肤
                SkinSE.SkinSE_Net skinStructure = new SkinSE.SkinSE_Net();
                skinStructure.Init_NET(this, 1);

                //初始化功能演示区
                InitFunctionalDemonstrationRegion();

                #region old wait to delete
                //初始化相关文档
                //IniDocAndQuestionRegion(basePath, "相关文档", groupBox3);

                //初始化常见问题
                //IniDocAndQuestionRegion(basePath, "常见问题", groupBox4);
                #endregion

                //初始化代码控件
                IniCodeRegion();

                //跟踪功能区代码
                TraceOperate();

                //初始化“源码_修改”文件夹
                Common.copyDirectory(basePath + "源码", basePath + "源码_修改");

                iniMainFormWidth = this.Size.Width;
                cmomonAnswerX = cmomonAnswer.Location.X;

                progressBar1.Visible = false;
                this.BackColor = Color.White;

                MainForm_SizeChanged(this,null);
                //进来默认点一次登录
                //pictureBox3_Click(pictureBox3, null);
                MainFormLogin();

                //userEmail = Config.GetValueByKey("UserEmail", "Email");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MainFormLogin()
        {
            webLogin.Location =webLogin.point;

            webLogin.webBrowser1.Navigate(webLogin.NavigateUrl);
            this.Move += new EventHandler(CommonAnswer_Move);
        }

        public void CommonAnswer_Move(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl != null)
            {
                ctrl.Location = webLogin.point;
            }
        }

        public void DoMethod(string getstr)
        {
            //if (!string.IsNullOrEmpty(getstr))
            //{
                this.cmomonAnswer.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=iszhishi&v=0");
                this.askQuestion.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/add.aspx?v=0");
                this.myQuestion.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=my&v=0");

                this.pictureBox1.BackgroundImage = Properties.Resources.提问图标;
                this.pictureBox2.BackgroundImage = Properties.Resources.信息;
                this.pictureBox5.BackgroundImage = Properties.Resources.常见问题;

                this.label5.ForeColor = Color.Black;
                this.label6.ForeColor = Color.Black;
           // }
            //else
            //{
            //    this.pictureBox1.BackgroundImage = Properties.Resources.提问图标_未登录;
            //    this.pictureBox2.BackgroundImage = Properties.Resources.信息_未登录;
            //    this.pictureBox5.BackgroundImage = Properties.Resources.常见问题_未登录;

            //    this.label5.ForeColor = Color.LightGray;
            //    this.label6.ForeColor = Color.LightGray;
            //}
            ChargeMessageStatus();
        }

        public void ChargeMessageStatus()
        {
            U8DevDocs.u8DevServiceSoapClient client = new U8DevDocs.u8DevServiceSoapClient();
            DataSet ds = client.getAskCount(Config.GetValueByKey("UserEmail", "Email"));
            if (ds != null&&ds.Tables.Count>0)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    //有消息
                    this.pictureBox2.BackgroundImage = Properties.Resources.未读信息;
                }
                else
                {
                    //无消息
                }
            }
            else
            {
                //无消息
               
            }
        }

        private void ShowNoLoginImages()
        {
            this.pictureBox1.BackgroundImage = Properties.Resources.提问图标_未登录;
            this.pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            this.pictureBox2.BackgroundImage = Properties.Resources.信息_未登录;
            this.pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            this.pictureBox5.BackgroundImage = Properties.Resources.常见问题_未登录;
            this.pictureBox5.BackgroundImageLayout = ImageLayout.Stretch;

            this.label5.ForeColor = Color.LightGray;
            this.label6.ForeColor = Color.LightGray;
        }

        /// <summary>
        /// 初始化相关文档和常见问题区域
        /// </summary>
        /// <param name="basePath">类别路径</param>
        /// <param name="DirectoryName">文件夹名称</param>
        /// <param name="groupBox">目标groupBox</param>
        private void IniDocAndQuestionRegion(string basePath,string DirectoryName, GroupBox groupBox)
        {
            try
            {
                //得到对应公共空间类别下的常见问题文件夹下的（包括子文件夹）的文件
                List<FileInfo> fileInfoP = Common.GetAllFilesInDirectory(basePath + DirectoryName);
                for (int i = 0; i < fileInfoP.Count; i++)
                {
                    LinkLabel lab = new LinkLabel();

                    //展示样式设置
                    lab.Width = groupBox.Width;
                    lab.Text = fileInfoP[i].Name;
                    lab.ForeColor = Color.Blue;
                    lab.Left = 15;
                    lab.Top = lab.Height * i + 40;

                    //给文字绑定点击时触发的委托方法
                    lab.Click += new EventHandler(StartProcessPro);

                    //将文档生成的可操作内容加载到父控件中
                    groupBox.Controls.Add(lab);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 初始化代码区
        /// </summary>
        private void IniCodeRegion()
        {
            try
            {
                txtContentForm.Width = groupBox2.Width;
                txtContentForm.Height = groupBox2.Height;
                txtContentForm.textEditorControl1.Text = "";
                txtContentForm.textEditorControl1.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
                txtContentForm.textEditorControl1.Encoding = Encoding.Default;
                txtContentForm.Location = new Point(5, 45);
                //txtContent.AutoScroll = false;
                txtContentForm.FormBorderStyle = FormBorderStyle.None;
                txtContentForm.textEditorControl1.Dock = DockStyle.Fill;
                //txtContent.AutoScrollMargin = new Size(1, 1);
                //txtContent.Text = "";
                //txtContentForm.statusStrip1.Width = txtContentForm.Width - 2;
                txtContentForm.TopLevel = false;
                txtContentForm.BackColor = Color.White;
                txtContentForm.Show();
                groupBox2.Controls.Add(txtContentForm);

                #region 代码区控件换肤
                SkinSE.SkinSE_Net skinCode = new SkinSE.SkinSE_Net();
                skinCode.Init_NET(txtContentForm.textEditorControl1, 1);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 给功能演示区的操作绑定实时监控
        /// </summary>
        private void TraceOperate()
        {
            try
            {
                //获取groupBox1下的所有控件
                Control.ControlCollection controls = groupBox1.Controls;
                foreach (Control ctrl in controls)
                {
                    //获取用户控件
                    if ((ctrl is UserControl) || (ctrl is Form))
                    {
                        LoopCtrl(ctrl);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 循环用户控件内部的所有控件，并为需要的控件绑定监听
        /// </summary>
        /// <param name="ctrl">控件</param>
        private void LoopCtrl(Control ctrl)
        {
            try
            {
                //循环对用户控件上的每一个控件进行操作
                foreach (Control ctrl1 in ctrl.Controls)
                {
                    OperateForEachControl(ctrl1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        /// <summary>
        /// 对每一个控件进行处理
        /// </summary>
        /// <param name="ctrl">控件</param>
        private void OperateForEachControl(Control ctrl)
        {
            try
            {
               
                //获得控件上的所有事件
                EventInfo[] events = GetAllEventsOnControl(ctrl);

                ProcessEachEvent(ctrl, events);

                //如果用户控件上的控件仍然是容器控件，递归遍历容器中的控件
                if (ctrl.Controls != null)
                {
                    foreach (Control ctrl2 in ctrl.Controls)
                    {
                        ProcessChildControl(ctrl2);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理指定控件的所有事件
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="events">事件集合</param>
        private void ProcessEachEvent(Control ctrl,EventInfo[] events)
        {
            try
            {
                for (int i = 0; i < events.Length; i++)
                {
                    string funcNames = GetBindFuncNamesOnControl(ctrl, events[i]);

                    if (string.IsNullOrEmpty(funcNames))
                    {
                        continue;
                    }

                    AddMonitorForControl(ctrl, events[i]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 获取控件上指定事件的绑定函数名
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="eventInfo">事件信息</param>
        private string GetBindFuncNamesOnControl(Control ctrl, EventInfo eventInfo)
        {
            string funcNames = "";
            try
            {
                funcNames = Common.GetFunctionNames(ctrl, eventInfo);
                funcNames = GetFunctionNamesReplaceLoop(funcNames);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return funcNames;
        }

        /// <summary>
        /// 给已经绑定操作方法的控件添加监视功能
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <param name="eventInfo">事件信息</param>
        private void AddMonitorForControl(Control ctrl, EventInfo eventInfo)
        {
            try
            {
                //自定义事件参数类，传递事件名称
                MyEventArgs args = new MyEventArgs(eventInfo);

                //获取事件处理类型，事件处理类型有很多种，EventHandler,MouseEventHandler.....
                string handleType = eventInfo.EventHandlerType.Name;

                //暂时只考虑处理句柄为EventHandler的
                if (handleType == "EventHandler")
                {
                    //针对控件的每一个事件进行绑定
                    eventInfo.AddEventHandler(ctrl, new EventHandler((object sender, EventArgs e) => { TraceMethod(sender, args); }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 处理子控件
        /// </summary>
        /// <param name="ctrl">子控件</param>
        private void ProcessChildControl(Control ctrl)
        {
            if (ctrl != null)
            {
                LoopCtrl(ctrl);
            }
        }

        /// <summary>
        /// 获取指定控件上的所有事件
        /// </summary>
        /// <param name="ctrl">控件</param>
        /// <returns></returns>
        private EventInfo[] GetAllEventsOnControl(Control ctrl)
        {
            try
            {
                PropertyInfo propertyInfo = ctrl.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                EventHandlerList eventHandlerList = propertyInfo.GetValue(ctrl, new object[] { }) as EventHandlerList;

                Type contorlType = ctrl.GetType();

                //获得控件上的所有事件
                return contorlType.GetEvents();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 监控执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TraceMethod(object sender, EventArgs e)
        {
            try
            {
                MyEventArgs mye = e as MyEventArgs;
                if (mye == null)
                {
                    return;
                }
                Control control = sender as Control;

                ShowCodesIntoCodeRegion(control, mye);
            }
            catch (Exception ex)
            {
                throw ex;
            } 

        }

        /// <summary>
        /// 获取源代码并展示到代码区域
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="mye">自定义参数</param>
        private void ShowCodesIntoCodeRegion(Control control,MyEventArgs mye)
        {
            try
            {
                if (control != null)
                {
                    StringBuilder sbCodes = new StringBuilder();

                    string functionNames = GetBindFuncNamesOnControl(control, mye.EventName);

                    if (string.IsNullOrEmpty(functionNames))
                    {
                        return;
                    }

                    //查出所有方法名的方法体（源代码）
                    sbCodes.Append("//" + mye.EventName.Name + "事件代码：" + "\r\n" + Common.GetFunctionBodys(functionNames, choiceOpiton, codeIsModified));

                    //加载查询到的源码到源码展示控件上
                    txtContentForm.textEditorControl1.Text = sbCodes.ToString();//.Replace("{", "{" + "\r\n").Replace("}", "}" + "\r\n").Replace(";", ";" + "\r\n")

                    //给代码控件赋值后需要刷新下，否则如果后赋值的代码行数小于前边已有代码行数，会出现代码错误。
                    txtContentForm.textEditorControl1.Refresh();
                }
                else
                {
                    txtContentForm.textEditorControl1.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        /// <summary>
        /// 打开解决方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                //获取类别的路径
                string categroyPath = Config.GetValueByKey(choiceOpiton, "categoryPath");

                //获取解决方案路径
                string filePath = AppDomain.CurrentDomain.BaseDirectory + categroyPath + @"源码\";

                OpenSolution(filePath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 打开指定路径下的解决方案
        /// </summary>
        /// <param name="filePath">路径</param>
        private void OpenSolution(string filePath)
        {
            try
            {
                //获取解决方案路径下后缀名为.sln的文件
                List<FileInfo> listFileInfo = Common.GetAllFilesInDirectory(filePath);
                for (int i = 0; i < listFileInfo.Count; i++)
                {
                    //只针对.sln文件
                    if (listFileInfo[i].Extension == ".sln")
                    {
                        //打开该文件
                        Process.Start(listFileInfo[i].FullName);

                        //找到一个.sln就返回
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// 保存用户自定义代码的功能
        /// 目前考虑采用（动态编译(改变控件handler绑定的方法)/全体编译 两种办法）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click_1(object sender, EventArgs e)
        {
            #region old wait to delete
            ////弹出等待框，进行修改->编译->替换过程
            //frmWaitingBox f = new frmWaitingBox((obj, args) =>
            //{
            //    //替换原始代码
            //    ReplaceCodes(codesText);

            //    //编译用户修改后的解决方案，复制并替换默认dll或者exe,并重新加载功能演示区
            //    CompileAfterReplaceCodes();

            //}, 20, "处理中，请稍后...", false, true);
            //f.ShowDialog(this);

            ////重新加载填充代码演示区域
            //InitFunctionalDemonstrationRegion();

            ////跟踪功能区代码
            //TraceOperate();

            ////弹窗提示用户
            //MessageBox.Show("用户修改已保存且编译完毕");
            #endregion

            #region ProgressBar

            progressBar1.Visible = true;
            canUse = true;
            EditProgressBar();
            ReplaceSolution();

            #endregion
        }

        private void ReplaceSolution()
        {
            //代码已修改
            this.codeIsModified = true;

            //获取用户修改后的代码
            string codesText = txtContentForm.textEditorControl1.Text;

            bool chargeResult=ChargeAlteredCodes(codesText);
            if (!chargeResult)
            {
                canUse = false;
                this.progressBar1.Value = 0;
                return;
            }

            //替换原始代码
            ReplaceCodes(codesText);
            this.progressBar1.Value += 10;

            //编译用户修改后的解决方案，复制并替换默认dll或者exe,并重新加载功能演示区
            CompileAfterReplaceCodes();
            this.progressBar1.Value += 10;

            //重新加载填充代码演示区域
            InitFunctionalDemonstrationRegion();
            this.progressBar1.Value += 10;

            //跟踪功能区代码
            TraceOperate();
            this.progressBar1.Value = 100;

            //弹窗提示用户
            MessageBox.Show("用户修改已保存且编译完毕");
            this.progressBar1.Visible = false;
            this.progressBar1.Value = 0;
        }

        /// <summary>
        /// 判断用户修改后的代码
        /// </summary>
        /// <param name="codesText">用户修改后的代码</param>
        private bool ChargeAlteredCodes(string codesText)
        {
            if (string.IsNullOrEmpty(codesText))
            {
                MessageBox.Show("代码不可为空，请输入正确代码");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 使用用户修改的代码替换源码文件中的代码
        /// </summary>
        /// <param name="codesText">用户修改后的代码</param>
        private void ReplaceCodes(string codesText)
        {
            //替换“源码_修改”中的代码
            string pattern = @"//--------------------";
            Regex regex = new Regex(pattern);
            string[] contensArray = regex.Split(codesText.Remove(codesText.LastIndexOf(pattern)));
            //没有找到对应的方法，直接返回空
            if (contensArray.Length < 2)
            {
                MessageBox.Show("请输入正确代码格式，并且不要去掉原有注释符号");
            }

            //获取分隔后的数组（分隔的数组中既有代码，偶数索引项为注释，奇数索引项为代码）
            string[] codes = contensArray;

            for (int i = 1; i < codes.Length; i++)
            {
                string[] resultInfo=SplitContentArray(codes[i]);

                bool result = Common.ReplaceSourceDoucmentCodes(resultInfo[2], choiceOpiton, codes[i], resultInfo[0], resultInfo[1]);
                if (!result)
                {
                    MessageBox.Show("替换代码失败！");
                    return;
                }

                //只有奇数为索引代码，所以再次++
                i++;
            }
        }

        /// <summary>
        /// 获取分隔“访问修饰符”+“返回类型”+“代码”的数组
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private string[] SplitContentArray(string code)
        {
            //分隔出“访问修饰符+返回类型+函数名”
            string pattern1 = @"\s*\(\s*object\s*sender\s*,\s*EventArgs\s*e\s*\)";
            Regex regex1 = new Regex(pattern1);
            string functionsInfo = regex1.Split(code)[0].Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("\t", "");

            //空格分隔出 访问修饰符[0] 返回类型[1] 函数名[2]
            string[] resultInfo = functionsInfo.Split(' ');

            return resultInfo;
        }

        /// <summary>
        /// 编译修改后的解决方案
        /// </summary>
        private void CompileAfterReplaceCodes()
        {
            bool compileResult = CompileAndReplace("源码_修改");
            if (!compileResult)
            {
                MessageBox.Show("编译失败，请检查是否有语法错误");
                return;
            }
        }

        /// <summary>
        /// 恢复默认DEMO默认代码（取消用户更改生效的代码）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            #region old wait to delete
            ////打开处理中窗口，友好等待界面
            //frmWaitingBox f = new frmWaitingBox((obj, args) =>
            //{
            //    //恢复默认，代码变为未修改
            //    this.codeIsModified = false;

            //    //初始化“源码_修改”文件夹
            //    string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");

            //    Common.copyDirectory(AppDomain.CurrentDomain.BaseDirectory + categoryPath + "源码", AppDomain.CurrentDomain.BaseDirectory + categoryPath + "源码_修改");

            //    //找到源码文件夹，重新编译，拷贝替换，重新加载
            //    bool compileResult = CompileAndReplace("源码");

            //    if (!compileResult)
            //    {
            //        MessageBox.Show("编译失败,请检查是否改动过源码文件");
            //        return;
            //    }
            //}, 20, "处理中，请稍后...", false, true);
            //f.ShowDialog(this);


            ////重新加载填充代码演示区域
            //InitFunctionalDemonstrationRegion();

            ////跟踪功能区代码
            //TraceOperate();


            ////弹窗提示恢复成功
            //MessageBox.Show("恢复默认成功");
            #endregion

            #region ProgressBar

            progressBar1.Visible = true;
            canUse=true;
            EditProgressBar();
            RecoverSolution();

            #endregion
        }

        private void EditProgressBar()
        {
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
            Thread thread = new Thread(IncreaseBar);
            thread.Start();
        }

        private void IncreaseBar()
        {
            while(this.progressBar1.Value<60&&canUse)
            {
                this.progressBar1.Value += 10;
                Thread.Sleep(1000);
            }
        }

        private void RecoverSolution()
        {
            //恢复默认，代码变为未修改
            this.codeIsModified = false;

            //初始化“源码_修改”文件夹
            string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");

            Common.copyDirectory(AppDomain.CurrentDomain.BaseDirectory + categoryPath + "源码", AppDomain.CurrentDomain.BaseDirectory + categoryPath + "源码_修改");
            this.progressBar1.Value += 10;

            //找到源码文件夹，重新编译，拷贝替换，重新加载
            bool compileResult = CompileAndReplace("源码");
            this.progressBar1.Value += 10;

            if (!compileResult)
            {
                MessageBox.Show("编译失败,请检查是否改动过源码文件");
                this.progressBar1.Value = 0;
                return;
            }

            //重新加载填充代码演示区域
            InitFunctionalDemonstrationRegion();
            this.progressBar1.Value += 10;

            //跟踪功能区代码
            TraceOperate();
            this.progressBar1.Value = 100;

            //弹窗提示恢复成功
            MessageBox.Show("恢复默认成功");
            this.progressBar1.Visible = false;
            this.progressBar1.Value = 0;
        }

        /// <summary>
        /// 加载功能演示区
        /// </summary>
        private void InitFunctionalDemonstrationRegion()
        {
            try
            {
                #region backgroundWorker方法（线程间访问控件）
                //using (BackgroundWorker bw = new BackgroundWorker())
                //{
                //    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                //    bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                //    bw.RunWorkerAsync("Tank");
                //}
                #endregion

                string fullName = Config.GetValueByKey(this.choiceOpiton, "fullClassName");

                //获取dll所在路径
                string dllPath = Config.GetValueByKey(this.choiceOpiton, "dllPath");

                //获取dll名称/exe名称
                string dllName = Config.GetValueByKey(this.choiceOpiton, "dllName");

                #region 不占用dll解决方法（子应用程序域方法）
                //CVST.AppFramework.AssemblyLoader.Loader loader = new CVST.AppFramework.AssemblyLoader.Loader();
                //Assembly assembly = loader.LoadAssembly(AppDomain.CurrentDomain.BaseDirectory + dllPath+dllName);
                #endregion

                #region 不占用dll解决方案（Load(byte[] buffer)方法）
                byte[] buffer = Common.GetByteArrayFromFile(AppDomain.CurrentDomain.BaseDirectory + dllPath + dllName);
                Assembly assembly = Assembly.Load(buffer);
                //Assembly assembly = Assembly.ReflectionOnlyLoad(buffer);
                #endregion

                //创建该对象的实例，object类型，参数（名称空间+类）   
                object instance = assembly.CreateInstance(fullName);

                //将反射出的对象实例添加到groupBox1中（Form/userControl）
                ControlWaitToAdd(instance);

                #region 委托方法（线程间访问控件）
                //Thread groupbox1Thread = new Thread(new ParameterizedThreadStart(UpdateGroupBox1));
                //groupbox1Thread.Start(instance);
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 添加控件到groupBox1中
        /// </summary>
        /// <param name="instance">反射出来的实例</param>
        private void ControlWaitToAdd(object instance)
        {
            Form uForm = instance as Form;

            if (uForm != null)
            {
                //去掉uForm的边框
                uForm.FormBorderStyle = FormBorderStyle.None;
                //设置窗体为非顶级控件
                uForm.TopLevel = false;

                AddControlAndChangeSkin(uForm);
                uForm.Width = groupBox1.Width;
                uForm.Height = groupBox1.Height;
                uForm.BackColor = Color.White;
                uForm.Show();
            }
            else //同时支持form和usercontrol
            {
                UserControl usercontrol = instance as UserControl;
                if (usercontrol != null)
                {
                    AddControlAndChangeSkin(usercontrol);

                    usercontrol.Width = groupBox1.Width;
                    usercontrol.Height = groupBox1.Height;
                    usercontrol.BackColor = Color.White;
                    //控件用户控件在主界面中的位置
                    usercontrol.Top = 20;
                    usercontrol.Left = 10;
                }
            }
        }

        /// <summary>
        /// 加载控件并换肤
        /// </summary>
        /// <param name="control"></param>
        private void AddControlAndChangeSkin(Control control)
        {
            #region 换肤
            SkinSE.SkinSE_Net skinForm = new SkinSE.SkinSE_Net();
            skinForm.Init_NET(control, 1);
            #endregion

            //动态加载用户控件到主界面中
            while (groupBox1.Controls.Count > 0)
            {
                foreach (Control crl in groupBox1.Controls)
                {
                    groupBox1.Controls.Remove(crl);
                    crl.Dispose();
                }
            }

            groupBox1.Controls.Add(control);

            //控件用户控件在主界面中的位置
            control.Top = 20;
            control.Left = 10;
        }

        #region 委托方法（线程间访问控件）
        //private void UpdateGroupBox1(object instance)
        //{
        //    if (groupBox1.InvokeRequired)
        //    {
        //        // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
        //        Action<string> actionDelegate = (x) => { ControlWaitToAdd(instance); };

        //        //this.label2.Invoke(actionDelegate, str);
        //    }
        //    else
        //    {
        //        //this.label2.Text = str.ToString();
        //        ControlWaitToAdd(instance);
        //    }
        //}
        #endregion

        #region backgroundWorker方法（线程间访问控件）
        //void bw_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    // 这里是后台线程， 是在另一个线程上完成的
        //    // 这里是真正做事的工作线程
        //    // 可以在这里做一些费时的，复杂的操作
        //    //e.Result = e.Argument + "工作线程完成";
        //    //获取按钮控件类的全名称
        //    string fullName = Config.GetValueByKey(this.choiceOpiton, "fullClassName");

        //    //获取dll所在路径
        //    string dllPath = Config.GetValueByKey(this.choiceOpiton, "dllPath");

        //    //获取dll名称/exe名称
        //    string dllName = Config.GetValueByKey(this.choiceOpiton, "dllName");

        //    #region 不占用dll解决方法（子应用程序域方法）
        //    //CVST.AppFramework.AssemblyLoader.Loader loader = new CVST.AppFramework.AssemblyLoader.Loader();
        //    //Assembly assembly = loader.LoadAssembly(AppDomain.CurrentDomain.BaseDirectory + dllPath+dllName);
        //    #endregion

        //    #region 不占用dll解决方案（Load(byte[] buffer)方法）
        //    byte[] buffer = Common.GetByteArrayFromFile(AppDomain.CurrentDomain.BaseDirectory + dllPath + dllName);
        //    Assembly assembly = Assembly.Load(buffer);
        //    #endregion


        //    //创建该对象的实例，object类型，参数（名称空间+类）   
        //    object instance = assembly.CreateInstance(fullName);

        //    e.Result = instance;
        //}

        //void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了 
        //    //this.label4.Text = e.Result.ToString();

        //    Form uForm = e.Result as Form;

        //    if (uForm != null)
        //    {
        //        #region Form换肤
        //        SkinSE.SkinSE_Net skinForm = new SkinSE.SkinSE_Net();
        //        skinForm.Init_NET(uForm, 1);
        //        #endregion

        //        //去掉uForm的边框
        //        uForm.FormBorderStyle = FormBorderStyle.None;
        //        //设置窗体为非顶级控件
        //        uForm.TopLevel = false;

        //        //动态加载用户控件到主界面中
        //        while (groupBox1.Controls.Count > 0)
        //        {
        //            foreach (Control crl in groupBox1.Controls)
        //            {
        //                groupBox1.Controls.Remove(crl);
        //                crl.Dispose();
        //            }
        //        }

        //        groupBox1.Controls.Add(uForm);
        //        uForm.Show();

        //        //控件用户控件在主界面中的位置
        //        uForm.Top = 20;
        //        uForm.Left = 10;
        //    }
        //    else //同时支持form和usercontrol
        //    {
        //        UserControl usercontrol = e.Result as UserControl;
        //        if (usercontrol != null)
        //        {
        //            #region UserControl换肤
        //            SkinSE.SkinSE_Net skinUserConrol = new SkinSE.SkinSE_Net();
        //            skinUserConrol.Init_NET(usercontrol, 1);
        //            #endregion

        //            //动态加载用户控件到主界面中
        //            while (groupBox1.Controls.Count > 0)
        //            {
        //                foreach (Control crl in groupBox1.Controls)
        //                {
        //                    groupBox1.Controls.Remove(crl);
        //                    crl.Dispose();
        //                }
        //            }

        //            groupBox1.Controls.Add(usercontrol);

        //            //控件用户控件在主界面中的位置
        //            usercontrol.Top = 20;
        //            usercontrol.Left = 10;
        //        }
        //    }
        //}
        #endregion

        /// <summary>
        /// 编译，拷贝替换二开的dll或者exe,并且初始化功能演示区
        /// </summary>
        /// <param name="sourceName">查找文件夹名称（源码/源码_修改）</param>
        private bool CompileAndReplace(string sourceName)
        {
            try
            {
                //重新编译sourceName文件夹下的解决方案
                string categoryPath = Config.GetValueByKey(this.choiceOpiton, "categoryPath");

                List<FileInfo> fileInfo = Common.GetAllFilesInDirectory(AppDomain.CurrentDomain.BaseDirectory + categoryPath + sourceName);
                for (int i = 0; i < fileInfo.Count; i++)
                {
                    //找到第一个.sln文件(解决方案文件)
                    if (fileInfo[i].Extension == ".sln")
                    {
                       //编译解决方案
                       bool compileresult = Common.CompileSolution(fileInfo[i].FullName);

                        if (!compileresult)
                        {
                            MessageBox.Show("编译失败，请检查是否有语法错误");
                            return false;
                        }
                    }
                }

                //遍历编译后的sourceName文件夹
                List<FileInfo> fileInfoAfter = Common.GetAllFilesInDirectory(AppDomain.CurrentDomain.BaseDirectory + categoryPath + sourceName);
                List<FileInfo> fileInfoDllOrExe = new List<FileInfo>();

                //获取dll所在路径
                string dllPath = Config.GetValueByKey(this.choiceOpiton, "dllPath");

                //获取dll名称/exe名称
                string dllName = Config.GetValueByKey(this.choiceOpiton, "dllName");

                for (int i = 0; i < fileInfo.Count; i++)
                {
                    //找到所有名称是dllName的dll或者exe
                    if (fileInfo[i].Name == dllName)
                    {
                        fileInfoDllOrExe.Add(fileInfo[i]);
                    }
                }

                //拿到最新日期的dll或者exe文件
                FileInfo latelyFile = Common.GetLatelyFile(fileInfoDllOrExe);

                //将编译生成的dll或者exe拷贝到对应位置
                bool copyResult=Common.CopyFile(latelyFile.FullName, AppDomain.CurrentDomain.BaseDirectory + dllPath + dllName);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据给定的方法名去掉快速开发工具给控件绑定的方法名
        /// </summary>
        /// <param name="functionNames"></param>
        /// <returns></returns>
        private static string GetFunctionNamesReplaceLoop(string functionNames)
        {
            if (!functionNames.Contains(";"))
            {
                return functionNames;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                string[] functionArray = functionNames.Split(';');
                for (int i = 0; i < functionArray.Length; i++)
                {
                    if (functionArray[i].Contains("SpeedDevelopTool.<>c__DisplayClass"))
                    {
                        continue;
                    }
                    else if (functionArray[i].Contains("Crownwood.DotNetMagic.Controls"))
                    {
                        continue;
                    }
                    else
                    {
                        sb.Append(functionArray[i] + ";");
                    }
                }

                return sb.ToString().TrimEnd(';');
            }
        }

        /// <summary>
        /// 相关文档链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            CategoryDocs docs = new CategoryDocs(this.choiceOpiton);
            docs.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //获取分类
            //string category = Config.GetValueByKey(this.choiceOpiton, "ChineseName");

            //CommonAnswer answer = new CommonAnswer(category);

            cmomonAnswer.ShowDialog();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        //private void button10_Click(object sender, EventArgs e)
        //{
        //    WebLogin loginForm = new WebLogin("http://u8dev.yonyou.com");
        //    loginForm.ShowDialog();
        //}

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                MessageBox.Show("请先登录");
                return;
            }

            askQuestion.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //每隔15分钟刷新提问，常见问题，保持session
            this.cmomonAnswer.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=iszhishi&v=0");
            this.askQuestion.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/add.aspx?v=0");
            this.myQuestion.webBrowser1.Navigate("http://u8dev.yonyou.com/home/ask/index.aspx?r=my&v=0");

            ChargeMessageStatus();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                MessageBox.Show("请先登录");
                return;
            }
            myQuestion.ShowDialog();
            pictureBox2.BackgroundImage = Properties.Resources.信息;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //int nowWidth = this.Size.Width;
            int diffWidth = this.Size.Width - iniMainFormWidth;
            //iniMainFormWidth = this.Size.Width;

            ////只隐藏左边小框
            //if (this.Size.Width > (iniMainFormWidth+50) && this.Size.Width < (iniMainFormWidth+100))
            //{
            //    IniBegin();
            //    ChangeLayout(68);
            //}
            ////只隐藏左边大框
            //else if (this.Size.Width > (iniMainFormWidth + 100) && this.Size.Width < (iniMainFormWidth + 260))
            //{
            //    IniBegin();
            //    ChangeLayout(240);
            //}
            ////两个边框都隐藏
            //else if (this.Size.Width > (iniMainFormWidth + 260))
            //{
            //    IniBegin();
            //    ChangeLayout(308);
            //}
            ////都不隐藏，不需要做什么
            //else
            //{
            //    IniBegin();
            //}

            //只隐藏左边小框
            if (diffWidth > 50 && diffWidth < 100)
            {
                IniBegin();
                ChangeLayout(68);
            }
            //只隐藏左边大框
            else if (diffWidth >  100 && diffWidth < 260)
            {
                IniBegin();
                ChangeLayout(240);
            }
            //两个边框都隐藏
            else if (diffWidth > 260)
            {
                IniBegin();
                ChangeLayout(308);
            }
            //都不隐藏，不需要做什么
            else
            {
                IniBegin();
            }
        }

        private void IniBegin()
        {
            #region old wait delete
            //pictureBox6.Width = iniMainFormWidth - 70;
            //pictureBox7.Width = iniMainFormWidth - 70;

            //label5.Left = iniMainFormWidth-215;
            //label6.Left = iniMainFormWidth-162;
            //label7.Left = iniMainFormWidth - 105;
            //pictureBox1.Left = iniMainFormWidth-183;
            //pictureBox2.Left = iniMainFormWidth-132;
            //pictureBox3.Left = iniMainFormWidth - 76;

            //progressBar1.Width = iniMainFormWidth-70;

            //groupBox1.Width = iniMainFormWidth - 70;
            //groupBox2.Width = iniMainFormWidth - 70;

            //pictureBox4.Left = iniMainFormWidth - 47;
            //pictureBox5.Left = iniMainFormWidth - 47;

            //txtContentForm.Width = iniMainFormWidth - 70;
            //ControlCollection controls = groupBox1.Controls;
            //controls[0].Width = iniMainFormWidth - 70;

            //askQuestion.Width = iniMainFormWidth - 35;
            //askQuestion.point = new Point(cmomonAnswer.Location.X,askQuestion.Location.Y);
            //askQuestion.Left = 310;

            //cmomonAnswer.Width = iniMainFormWidth - 35;
            //cmomonAnswer.point = new Point(cmomonAnswer.Location.X, cmomonAnswer.Location.Y);
            //cmomonAnswer.Left = 310;

            //myQuestion.Width = iniMainFormWidth - 35;
            //myQuestion.point = new Point(cmomonAnswer.Location.X, myQuestion.Location.Y);
            //myQuestion.Left = 310;

            //docs.Width = iniMainFormWidth - 35;
            //docs.point = new Point(cmomonAnswer.Location.X, cmomonAnswer.Location.Y);
            //docs.Left = 310;

            //webLogin.Width = iniMainFormWidth - 35;
            //webLogin.point = new Point(cmomonAnswer.Location.X, cmomonAnswer.Location.Y);
            //webLogin.Left = 310;
            #endregion

            CancleBindNotMove();

            int mainFormX = this.Location.X;
            int mainFormY = this.Location.Y;
            //int mainFormLeft = this.Left;

            pictureBox6.Width = iniMainFormWidth - 70;
            pictureBox7.Width = iniMainFormWidth - 70;

            label5.Left = iniMainFormWidth - 215;
            label6.Left = iniMainFormWidth - 162;
            label7.Left = iniMainFormWidth - 105;
            pictureBox1.Left = iniMainFormWidth - 183;
            pictureBox2.Left = iniMainFormWidth - 132;
            pictureBox3.Left = iniMainFormWidth - 76;

            progressBar1.Width = iniMainFormWidth - 70;

            groupBox1.Width = iniMainFormWidth - 70;
            groupBox2.Width = iniMainFormWidth - 70;

            pictureBox4.Left = iniMainFormWidth - 47;
            pictureBox5.Left = iniMainFormWidth - 47;

            txtContentForm.Width = iniMainFormWidth - 70;
            ControlCollection controls = groupBox1.Controls;
            controls[0].Width = iniMainFormWidth - 70;

            askQuestion.Width = iniMainFormWidth - 35;
            askQuestion.Left = 310;
            askQuestion.Top = 57;
            askQuestion.point = new Point(askQuestion.Location.X, askQuestion.Location.Y);

            cmomonAnswer.Width = iniMainFormWidth - 35;
            cmomonAnswer.Left = 310;
            cmomonAnswer.Top = 57;
            cmomonAnswer.point = new Point(cmomonAnswer.Location.X, cmomonAnswer.Location.Y);

            myQuestion.Width = iniMainFormWidth - 35;
            myQuestion.Left = 310;
            myQuestion.Top = 57;
            myQuestion.point = new Point(myQuestion.Location.X, myQuestion.Location.Y);

            docs.Width = iniMainFormWidth - 35;
            docs.Left = 310;
            docs.Top = 57;
            docs.point = new Point(docs.Location.X, docs.Location.Y);

            webLogin.Width = iniMainFormWidth - 35;
            webLogin.Left = 310;
            webLogin.Top = 57;
            webLogin.point = new Point(webLogin.Location.X, webLogin.Location.Y);

            BindNotMove();
        }

        private void BindNotMove()
        {
            askQuestion.Move += new EventHandler(CommonAnswer_Move);
            cmomonAnswer.Move += new EventHandler(CommonAnswer_Move);
            myQuestion.Move += new EventHandler(CommonAnswer_Move);
            docs.Move += new EventHandler(CommonAnswer_Move);
            webLogin.Move += new EventHandler(CommonAnswer_Move);
        }

        private void CancleBindNotMove()
        {
            askQuestion.Move -= new EventHandler(CommonAnswer_Move);
            cmomonAnswer.Move -= new EventHandler(CommonAnswer_Move);
            myQuestion.Move -= new EventHandler(CommonAnswer_Move);
            docs.Move -= new EventHandler(CommonAnswer_Move);
            webLogin.Move -= new EventHandler(CommonAnswer_Move);
        }

        private void ChangeLayout(int size)
        {
            //pictureBox6.Width += size;
            //pictureBox7.Width += size;

            //label5.Left += size;
            //label6.Left += size;
            //label7.Left += size-5;
            //pictureBox1.Left += size;
            //pictureBox2.Left += size;
            //pictureBox3.Left += size-5;

            //progressBar1.Width += size;

            //pictureBox4.Left += size+1;
            //pictureBox5.Left += size+1;

            //groupBox1.Width += size;
            //groupBox2.Width += size;

            //txtContentForm.Width += size;
            //ControlCollection controls = groupBox1.Controls;
            //controls[0].Width += size;

            //askQuestion.Width += size;
            //askQuestion.point = new Point(askQuestion.Location.X - size, askQuestion.Location.Y);
            //askQuestion.Left -= size;

            //cmomonAnswer.Width += size;
            //cmomonAnswer.point = new Point(cmomonAnswer.Location.X - size, cmomonAnswer.Location.Y);
            //cmomonAnswer.Left -= size;

            //myQuestion.Width += size;
            //myQuestion.point = new Point(myQuestion.Location.X - size, myQuestion.Location.Y);
            //myQuestion.Left -= size;

            //docs.Width += size;
            //docs.point = new Point(cmomonAnswer.Location.X - size, cmomonAnswer.Location.Y);
            //docs.Left -= size;

            //webLogin.Width += size;
            //webLogin.point = new Point(cmomonAnswer.Location.X - size, cmomonAnswer.Location.Y);
            //webLogin.Left -= size;
            CancleBindNotMove();

            int nowWidth = this.Size.Width;
            int diffWidth = nowWidth - iniMainFormWidth+15;
            int mainFormX = this.Location.X;
            int mainFormY = this.Location.Y;
            //int mainFormLeft = this.Left;

            pictureBox6.Width = nowWidth - 70;
            pictureBox7.Width = nowWidth - 70;
            progressBar1.Width = pictureBox6.Width;
            groupBox1.Width = pictureBox6.Width;
            groupBox2.Width = pictureBox6.Width;

            label5.Left = nowWidth-190;
            label6.Left = nowWidth-140;
            label7.Left = nowWidth - 85;
            pictureBox1.Left = nowWidth-165;
            pictureBox2.Left = nowWidth-115 ;
            pictureBox3.Left = nowWidth-61;

            pictureBox4.Left = nowWidth-35;
            pictureBox5.Left = nowWidth - 35;

            txtContentForm.Width = pictureBox6.Width-10;
            ControlCollection controls = groupBox1.Controls;
            controls[0].Width = pictureBox6.Width-10;

            askQuestion.Width = nowWidth-30;
            askQuestion.Left -= diffWidth;
            askQuestion.Top = 57;
            askQuestion.point = new Point(askQuestion.Location.X, askQuestion.Location.Y);

            cmomonAnswer.Width = nowWidth - 30;
            cmomonAnswer.Left -= diffWidth;
            cmomonAnswer.Top = 57;
            cmomonAnswer.point = new Point(cmomonAnswer.Location.X, cmomonAnswer.Location.Y);

            myQuestion.Width = nowWidth - 30;
            myQuestion.Left -= diffWidth;
            myQuestion.Top = 57;
            myQuestion.point = new Point(myQuestion.Location.X, myQuestion.Location.Y);

            docs.Width = nowWidth - 30;
            docs.Left -= diffWidth;
            docs.Top = 57;
            docs.point = new Point(docs.Location.X, docs.Location.Y);

            webLogin.Width = nowWidth - 30;
            webLogin.Left -= diffWidth;
            webLogin.Top = 57;
            webLogin.point = new Point(webLogin.Location.X, webLogin.Location.Y);

            BindNotMove();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            docs.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                MessageBox.Show("请先登录");
                return;
            }
            cmomonAnswer.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            webLogin.ShowDialog();
        }


        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackgroundImage = Properties.Resources.相关文档;
            Cursor.Current = Cursors.Arrow;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox5.BackgroundImage = Properties.Resources.常见问题;
            }
            else
            {
                pictureBox5.BackgroundImage = Properties.Resources.常见问题_未登录;
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox4.BackgroundImage = Properties.Resources.相关文档_鼠标上来;
                Cursor.Current = Cursors.Hand;
            }
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox5.BackgroundImage = Properties.Resources.常见问题_鼠标上来;
                Cursor.Current = Cursors.Hand;
            }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Properties.Resources.登录_鼠标上来;
            Cursor.Current = Cursors.Hand;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = Properties.Resources.登录;
            Cursor.Current = Cursors.Arrow;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.提问图标_鼠标上来;
                Cursor.Current = Cursors.Hand;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox1.BackgroundImage = Properties.Resources.提问图标;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.提问图标_未登录;
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox2.BackgroundImage = Properties.Resources.信息_鼠标上来;
                Cursor.Current = Cursors.Hand;
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.GetValueByKey("UserEmail", "Email")))
            {
                pictureBox2.BackgroundImage = Properties.Resources.信息;
            }
            else
            {
                pictureBox2.BackgroundImage = Properties.Resources.信息_未登录;
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.Height += 2;
            label5.Width += 2;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.Height -= 2;
            label5.Width -= 2;
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.Height += 2;
            label6.Width += 2;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.Height -= 2;
            label6.Width -= 2;
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.Height += 2;
            label7.Width += 2;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.Height -= 2;
            label7.Width -= 2;
        }
    }
}
