using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeedDevelopTool
{
    public class Middle
    {
        public delegate void SendMessage(string str);
        public static event SendMessage sendEvent;
        public static void DoSendMessage(string str)
        {
            sendEvent(str);
        }
    }
}
