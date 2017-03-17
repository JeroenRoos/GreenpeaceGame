using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

class GameTimer : Timer
{
    public void StartTimeflowTimer()
    {
        Interval = 10;
        Enabled = true;
    }
    
}
