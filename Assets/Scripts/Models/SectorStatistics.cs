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
    }

    /*public SectorStatistics(double happiness, double ecoAwareness, double prosperity, double airPollutionContribution,
                            double naturePollutionContribution, double waterPollutionContribution)
    {
    }*/

    public void ModifyIncome(double changeValue)
    {
        income += changeValue;
    }
    
    public void ModifyHappiness(double changeValue)
    {
        happiness += changeValue;
    }

    public void ModifyEcoAwareness(double changeValue)
    {
        ecoAwareness += changeValue;

        double pollutionChangeValue = 0 - (changeValue / 10);
        pollution.ChangeAirPollutionMutation(pollutionChangeValue);
        pollution.ChangeNaturePollutionMutation(pollutionChangeValue);
        pollution.ChangeWaterPollutionMutation(pollutionChangeValue);
    }

    public void ModifyProsperity(double changeValue)
    {
        prosperity += changeValue;

        double happinessChangeValue = changeValue / 10;
        ModifyHappiness(happinessChangeValue);
    }

    public void mutateTimeBasedStatistics()
    {
        pollution.mutateTimeBasedStatistics();
    }
}
