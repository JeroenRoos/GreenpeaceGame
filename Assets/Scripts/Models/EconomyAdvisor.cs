using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class EconomyAdvisor : Advisor
{
    public override string[] name { get; protected set; }
    public override string[] displayMessage { get; protected set; } //dutch/english display message
    public override string dutchStatusMessages { get; protected set; }
    public override string englishStatusMessages { get; protected set; }


    public EconomyAdvisor()
    {
        name = new string[2] { "Economie adviseur", "Economy advisor" };
        dutchStatusMessages = "";
        englishStatusMessages = "";
        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }

    public override void DetermineDisplayMessage(int currentYear, int currentMonth, double income)
    {
        double calcValue = (1000 + currentYear * 48 + currentMonth * 4) * 4;
        dutchStatusMessages = "Het inkomen is nu " + income.ToString("0") + " per maand." +
            "De richtlijn is " + calcValue.ToString("0") + " per maand.";
        englishStatusMessages = "The income is now " + income.ToString("0") + " per month." +
            "The guideline is " + calcValue.ToString("0") + " per month.";

        displayMessage = new string[2] { dutchStatusMessages, englishStatusMessages };
    }
}