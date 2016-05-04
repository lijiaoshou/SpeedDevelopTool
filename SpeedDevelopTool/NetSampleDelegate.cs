using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UFIDA.U8.Portal.Common.Core;
using UFIDA.U8.Portal.Framework.Actions;

namespace SpeedDevelopTool
{
    public class NetSampleDelegate : IActionDelegate
    {
        #region IActionDelegate 成员

        /// <summary>
        /// 实现绑定的操作
        /// </summary>
        /// <param name="action"></param>
        public void Run(IAction action)
        {
            switch (action.Id)
            {
                case "sss":
                    {
                        MessageBox.Show("SSSS按钮");
                        return;
                    }
            }
        }

        public void SelectionChanged(IAction action, UFIDA.U8.Portal.Common.Core.ISelection selection)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
