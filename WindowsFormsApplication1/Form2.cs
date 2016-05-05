using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("我是原有事件");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string function = textBox1.Text;

            // 检索按钮的事件，这里单击事件的名字是EventClick，要注意的
            Delegate[] invokeList = GetObjectEventList(this.button1, "EventClick");
            if (invokeList != null)
            {
                foreach (Delegate del in invokeList)
                {
                    // 我已经测试，事件被全部取消了，没有问题。
                    typeof(Button).GetEvent("Click").RemoveEventHandler(this.button1, del);
                }
            }

            this.button1.Click += new EventHandler(testFunction);
            this.button1.Click += new EventHandler((ss, ee)=>{ });
        }

        private void testFunction(object sender, EventArgs e)
        {
            MessageBox.Show("我是更新后的事件");
        }

        /// <summary>
        /// 获取控件事件  
        /// </summary>
        /// <param name="p_Control">对象</param>
        /// <param name="p_EventName">事件名 EventClick EventDoubleClick </param>
        /// <returns>委托列</returns>
        public Delegate[] GetObjectEventList(Control p_Control, string p_EventName)
        {
            PropertyInfo _PropertyInfo = p_Control.GetType().GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            if (_PropertyInfo != null)
            {
                object _EventList = _PropertyInfo.GetValue(p_Control, null);
                if (_EventList != null && _EventList is EventHandlerList)
                {
                    EventHandlerList _List = (EventHandlerList)_EventList;
                    FieldInfo _FieldInfo = (typeof(Control)).GetField(p_EventName, BindingFlags.Static | BindingFlags.NonPublic);
                    if (_FieldInfo == null) return null;
                    Delegate _ObjectDelegate = _List[_FieldInfo.GetValue(p_Control)];
                    if (_ObjectDelegate == null) return null;
                    return _ObjectDelegate.GetInvocationList();
                }
            }
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // 检索按钮的事件，这里单击事件的名字是EventClick，要注意的
            Delegate[] invokeList = GetObjectEventList(this.button1, "EventClick");
            if (invokeList != null)
            {
                foreach (Delegate del in invokeList)
                {
                    // 我已经测试，事件被全部取消了，没有问题。
                    typeof(Button).GetEvent("Click").RemoveEventHandler(this.button1, del);
                }
            }

            //动态编译，重新加载一遍
            //this.InitializeComponent();
            this.button1.Click += new EventHandler(button1_Click);
            MessageBox.Show("成功");
        }
    }
}
