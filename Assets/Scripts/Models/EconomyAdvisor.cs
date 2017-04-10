using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class EconomyAdvisor : Advisor
{
    public override string[] name { get; protected set; }
    public override string[] displayMessage { get; protected set; } //dutch/english display message
    public override string[] dutchStatusMessages { get; protected set; }
    public override string[] englishStatusMessages { get; protected set; }


    public EconomyAdvisor()
    {
        name = new string[2] { "Economie adviseur", "Economy advisor" };
        dutchStatusMessages = new string[2] { "Het inkomen is hoog genoeg op het moment.", "Het inkomen is te laag op het moment." };
        englishStatusMessages = new string[2] { "The income is high enough at the moment", "The income is too low at the moment." };
        displayMessage = new string[2] { dutchStatusMessages[1], englishStatusMessages[1] };
    }

    public override void DetermineDisplayMessage(int currentYear, int currentMonth, double avgIncome)
    {
        if (avgIncome < (1000 + currentYear * 48 + currentMonth * 4) * 4)
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