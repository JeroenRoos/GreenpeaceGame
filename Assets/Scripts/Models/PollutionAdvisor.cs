using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PollutionAdvisor : Advisor
{
    public override string[] name { get; protected set; }
    public override string[] displayMessage { get; protected set; } //dutch/english display message
    public override string[] dutchStatusMessages { get; protected set; }
    public override string[] englishStatusMessages { get; protected set; }


    public PollutionAdvisor()
    {
        name = new string[2] { "Vervuiling adviseur", "Pollution advisor" };
        dutchStatusMessages = new string[2] { "De vervuiling is laag genoeg op het moment.", "De vervuiling is te hoog op het moment." };
        englishStatusMessages = new string[2] { "The pollution is low enough at the moment", "The pollution is too high at the moment." };
        displayMessage = new string[2] { dutchStatusMessages[1], englishStatusMessages[1] };
    }

    public override void DetermineDisplayMessage(int currentYear, int currentMonth, double avgPollution)
    {
        if (avgPollution > 50 - currentYear * 1.5 - currentMonth * (1.5 / 12))
        {
            displayMessage[0] = dutchStatusMessages[(int)statisticStatus.bad];
            displayMessage[1] = englishStatusMessages[(int)statisticStatus.bad];
        }
        else
        {
            displayMessage[0] = dutchStatusMessages[(int)statisticStatus.good];
            displayMessage[1] = englishStatusMessages[(int)statisticStatus.good];
        }
    }
}