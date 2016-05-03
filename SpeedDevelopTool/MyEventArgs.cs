using System;
using System.Collections.Generic;
using System.Text;

namespace SpeedDevelopTool
{
    public class MyEventArgs:EventArgs
    {
        public string EventName { get; set; }

        public MyEventArgs(string eventName)
        {
            this.EventName = eventName;
        }
    }
}
