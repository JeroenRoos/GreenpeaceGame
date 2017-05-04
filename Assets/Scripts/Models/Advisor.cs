using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public abstract class Advisor
{
    protected enum statisticStatus { good, bad };

    abstract public string[] name { get; protected set; }
    abstract public string[] displayMessage { get; protected set; } //dutch/english display message
    abstract public string dutchStatusMessages { get; protected set; }
    abstract public string englishStatusMessages { get; protected set; }

    public abstract void DetermineDisplayMessage(int currentYear, int currentMonth, double calculateValue);
}