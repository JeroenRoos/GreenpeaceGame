using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class PollutionAdvisor : Advisor
{
    public override string[] name { get; protected set; }
    public override string[] displayMessage { get; protected set; } //dutch/english display message
    public override string dutchStatusMessages { get; protected set; }
    public override string englishStatusMessages { get; protected set; }


    public PollutionAdvisor()
    {
        name = new string[2] { "Vervuiling adviseur", "Pollution advisor" };
        dutchStatusMessages = "";
        englishStatusMessages = "";
        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }

    public override void DetermineDisplayMessage(int currentYear, int currentMonth, double avgPollution)
    {
        double calcValue = 45 - currentYear * 1.5 - currentMonth * (1.5 / 12);
        if (calcValue < 0)
            calcValue = 0;
        dutchStatusMessages = "De gemiddelde vervuiling is nu " + avgPollution.ToString("0") + "% per maand." +
            "De richtlijn is " + calcValue.ToString("0") + "% per maand.";
        englishStatusMessages = "The average pollution is now " + avgPollution.ToString("0") + "% per month." +
            "The guideline is " + calcValue.ToString("0") + "% per month.";

        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }
}