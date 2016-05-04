using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UFIDA.U8.Portal.Proxy.editors;
using UFIDA.U8.Portal.Proxy.supports;

namespace SpeedDevelopTool
{
    public class MenuClass: NetLoginable
    {
        public override object CallFunction(string cMenuId, string cMenuName, string cAuthId, string cCmdLine)
        {
            MainForm mycontrol = new MainForm();

            ///页签标题
            mycontrol.Title = CommonLib.Config.GetValueByKey(cCmdLine, "ChineseName");
            mycontrol.choiceOpiton = cCmdLine;
            
            base.ShowEmbedControl(mycontrol, cMenuId, true);
            return null;
        }
        public override bool SubSysLogin()
        {
            return base.SubSysLogin();
        }

        public override bool SubSysLogOff()
        {
            return base.SubSysLogOff();
        }
    }
}
