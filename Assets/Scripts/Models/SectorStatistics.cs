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

    public SectorStatistics() { }

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

        //happiness decay
        happiness *= (1 - 0.05 / 12);
    }

    public void ModifyEcoAwareness(double changeValue)
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

        double pollutionChangeValue = 0 - (changeValue / 20);
        pollution.ChangeAirPollutionMutation(pollutionChangeValue);
        pollution.ChangeNaturePollutionMutation(pollutionChangeValue);
        pollution.ChangeWaterPollutionMutation(pollutionChangeValue);
    }

    public void ModifyProsperity(double changeValue)
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

        double incomeChangeValue = changeValue * 5;
        ModifyIncome(incomeChangeValue);
    }

    public void mutateTimeBasedStatistics()
    {
        pollution.mutateTimeBasedStatistics();
    }
}
