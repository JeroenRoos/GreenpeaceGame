using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class HappinessAnalyst : Advisor
{
    public override string[] name { get; protected set; }
    public override string[] displayMessage { get; protected set; } //dutch/english display message
    public override string dutchStatusMessages { get; protected set; }
    public override string englishStatusMessages { get; protected set; }


    public HappinessAnalyst()
    {
        name = new string[2] { "Tevredenheid analyst", "Happiness analyst" };
        dutchStatusMessages = "";
        englishStatusMessages = "";
        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }

    public override void DetermineDisplayMessage(int currentYear, int currentMonth, double happiness)
    {
        double calcValue = happiness - 50;
        if (calcValue >= 0)
        {
            dutchStatusMessages = "Door de tevredenheid zijn de consequenties van acties en events " + calcValue.ToString("0.00") + "% beter";
            englishStatusMessages = "Because of the happiness the consequences for actions and events are " + calcValue.ToString("0.00") + "% better";
        }
        else
        {
            dutchStatusMessages = "Door de tevredenheid zijn de consequenties van acties en events " + calcValue.ToString("0.00") + "% slechter";
            englishStatusMessages = "Because of the happiness the consequences for actions and events are " + calcValue.ToString("0.00") + "% worse";
        }
        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }
}