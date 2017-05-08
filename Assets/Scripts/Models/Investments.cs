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

    public double investmentModifier { get; private set; }
    public double investmentCost { get; private set; }

    public Investments()
    {
        actionCostReduction = new bool[5]{ false, false, false, false, false };
        betterActionConsequences = new bool[5] { false, false, false, false, false };
        gameEventCostReduction = new bool[5] { false, false, false, false, false };
        betterGameEventConsequences = new bool[5] { false, false, false, false, false };

        investmentModifier = 0.1;
        investmentCost = 10000;
    }

    public void InvestInActionCostReduction(List<Region> regions)
    {
        for (int i = 0; i < actionCostReduction.Length; i++)
        {
            if (!actionCostReduction[i])
            {
                actionCostReduction[i] = true;
                foreach (Region region in regions)
                {
                    foreach (RegionAction ra in region.actions)
                    {
                        ra.SetAfterInvestmentActionCost(investmentModifier);
                    }
                }
                break;
            }
        }
    }

    public void InvestInBetterActionConsequences(List<Region> regions)
    {
        for (int i = 0; i < betterActionConsequences.Length; i++)
        {
            if (!betterActionConsequences[i])
            {
                betterActionConsequences[i] = true;
                foreach (Region region in regions)
                {
                    foreach (RegionAction ra in region.actions)
                    {
                        ra.SetAfterInvestmentConsequences(investmentModifier);
                        ra.SetAfterInvestmentTemporaryConsequences(investmentModifier);
                    }
                }
                break;
            }
        }
    }

    public void InvestInGameEventCostReduction(List<GameEvent> events)
    {
        for (int i = 0; i < gameEventCostReduction.Length; i++)
        {
            if (!gameEventCostReduction[i])
            {
                gameEventCostReduction[i] = true;
                foreach (GameEvent ge in events)
                {
                    ge.SetAfterInvestmentEventChoiceMoneyCost(investmentModifier);
                }
                break;
            }
        }
    }

    public void InvestInBetterGameEventConsequences(List<GameEvent> events)
    {
        for (int i = 0; i < betterGameEventConsequences.Length; i++)
        {
            if (!betterGameEventConsequences[i])
            {
                betterGameEventConsequences[i] = true;
                foreach (GameEvent ge in events)
                {
                    ge.SetAfterInvestmentConsequences(investmentModifier);
                }
                break;
            }
        }
    }
}
