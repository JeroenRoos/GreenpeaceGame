using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SectorStatistics
{    
    public double income { get; private set; }
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }

    public Pollution pollution { get; private set; }

    public SectorStatistics()
    {
        pollution = new Pollution();
    }

    //method used for copying Sectorstatistics without reference
    public SectorStatistics(SectorStatistics sectorStatistics)
    {
        income = sectorStatistics.income;
        happiness = sectorStatistics.happiness;
        ecoAwareness = sectorStatistics.ecoAwareness;
        prosperity = sectorStatistics.prosperity;
        pollution = new Pollution(sectorStatistics.pollution);
    }

    #region ModifyVariablesMethods
    public void ModifyIncome(double changeValue)
    {
        income += changeValue;
    }
    
    public void ModifyHappiness(double changeValue)
    {
        if (happiness + changeValue > 100)
        {
            changeValue = 100 - happiness;
            happiness = 100;
        }

        else if (happiness + changeValue < 0)
        {
            happiness = changeValue - happiness;
            happiness = 0;
        }

        else
            happiness += changeValue;
    }

    public void ModifyEcoAwareness(double changeValue , bool isRewarded)
    {
        if (ecoAwareness + changeValue > 100)
        {
            changeValue = 100 - ecoAwareness;
            ecoAwareness = 100;
        }

        else if (ecoAwareness + changeValue < 0)
        {
            changeValue = changeValue - ecoAwareness;
            ecoAwareness = 0;
        }

        else
            ecoAwareness += changeValue;

        if (isRewarded)
        {
            double pollutionChangeValue = 0 - (changeValue / 20);
            pollution.ChangeAirPollutionMutation(pollutionChangeValue);
            pollution.ChangeNaturePollutionMutation(pollutionChangeValue);
            pollution.ChangeWaterPollutionMutation(pollutionChangeValue);

            double happinessChangeValue = changeValue / 10;
            ModifyHappiness(happinessChangeValue);
        }
    }

    public void ModifyProsperity(double changeValue, bool isRewarded)
    {
        if (prosperity + changeValue > 100)
        {
            changeValue = 100 - prosperity;
            prosperity = 100;
        }

        else if (prosperity + changeValue < 0)
        {
            changeValue = changeValue - prosperity;
            prosperity = 0;
        }

        else
            prosperity += changeValue;

        if (isRewarded)
        {
            double incomeChangeValue = changeValue * 5;
            ModifyIncome(incomeChangeValue);

            double happinessChangeValue = changeValue / 10;
            ModifyHappiness(happinessChangeValue);
        }
    }
    #endregion

    #region GameEventMethods
    public void SetPickedConsequences(SectorStatistics s, double[] modifiers, System.Random rnd)
    {
        income = s.income * modifiers[rnd.Next(0, modifiers.Length)];
        happiness = s.happiness * modifiers[rnd.Next(0, modifiers.Length)];
        ecoAwareness = s.ecoAwareness * modifiers[rnd.Next(0, modifiers.Length)];
        prosperity = s.prosperity * modifiers[rnd.Next(0, modifiers.Length)];

        pollution.SetPickedConsequences(s, modifiers, rnd);
    }

    public void SetPickedConsequencesMultiplayer(double[] consequences)
    {
        income = consequences[0];
        happiness = consequences[1];
        ecoAwareness = consequences[2];
        prosperity = consequences[3];

        pollution.SetPickedConsequencesMultiplayer(consequences);
    }
    #endregion
}
