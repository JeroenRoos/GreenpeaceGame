using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Investments
{
    public bool[] actionCostReduction { get; private set; }
    public bool[] betterActionConsequences { get; private set; }
    public bool[] gameEventCostReduction { get; private set; }
    public bool[] betterGameEventConsequences { get; private set; }

    public double investmentCost { get; private set; }

    public Investments()
    {
        actionCostReduction = new bool[5]{ false, false, false, false, false };
        betterActionConsequences = new bool[5] { false, false, false, false, false };
        gameEventCostReduction = new bool[5] { false, false, false, false, false };
        betterGameEventConsequences = new bool[5] { false, false, false, false, false };
    }

    public void InvestInActionCostReduction()
    {
        for (int i = 0; i < actionCostReduction.Length; i++)
        {
            if (!actionCostReduction[i])
            {
                actionCostReduction[i] = true;
                break;
            }
        }
    }

    public void InvestInBetterActionConsequences()
    {
        for (int i = 0; i < betterActionConsequences.Length; i++)
        {
            if (!betterActionConsequences[i])
            {
                betterActionConsequences[i] = true;
                break;
            }
        }
    }

    public void InvestInGameEventCostReduction()
    {
        for (int i = 0; i < gameEventCostReduction.Length; i++)
        {
            if (!gameEventCostReduction[i])
            {
                gameEventCostReduction[i] = true;
                break;
            }
        }
    }

    public void InvestInBetterGameEventConsequences()
    {
        for (int i = 0; i < betterGameEventConsequences.Length; i++)
        {
            if (!betterGameEventConsequences[i])
            {
                betterGameEventConsequences[i] = true;
                break;
            }
        }
    }
}
