using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SpeedDevelopTool
{
    public class MyEventArgs:EventArgs
    {
        public EventInfo EventName { get; set; }

        public MyEventArgs(EventInfo eventName)
        {
            this.EventName = eventName;
        }
    }
}
