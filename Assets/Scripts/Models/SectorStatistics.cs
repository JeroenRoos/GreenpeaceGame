using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class SectorStatistics
{
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }
    public double airPollutionContribution { get; private set; }
    public double naturePollutionContribution { get; private set; }
    public double waterPollutionContribution { get; private set; }

    public SectorStatistics() { }

    public SectorStatistics(double happiness, double ecoAwareness, double prosperity, double airPollutionContribution,
                            double naturePollutionContribution, double waterPollutionContribution)
    {
        this.happiness = 0;
        this.ecoAwareness = 0;
        this.prosperity = 0;
        this.airPollutionContribution = 0;
        this.naturePollutionContribution = 0;
        this.waterPollutionContribution = 0;

        ModifyHappiness(happiness);
        ModifyEcoAwareness(ecoAwareness);
        ModifyProsperity(prosperity);
        ModifyAirPollutionContribution(airPollutionContribution);
        ModifyNaturePollutionContribution(naturePollutionContribution);
        ModifyWaterPollutionContribution(waterPollutionContribution);
    }

    //happiness = prosperity - pollution *formule*
    public void ModifyHappiness(double changeValue)
    {
        happiness += changeValue;
    }

    public void ModifyEcoAwareness(double changeValue)
    {
        ecoAwareness += changeValue;

        double pollutionChangeValue = 0 - (changeValue / 10);
        ModifyAirPollutionContribution(pollutionChangeValue);
        ModifyNaturePollutionContribution(pollutionChangeValue);
        ModifyWaterPollutionContribution(pollutionChangeValue);
    }

    public void ModifyProsperity(double changeValue)
    {
        prosperity += changeValue;

        double happinessChangeValue = changeValue / 10;
        ModifyHappiness(happinessChangeValue);
    }

    public void ModifyAirPollutionContribution(double changevalue)
    {
        airPollutionContribution += changevalue;
    }

    public void ModifyNaturePollutionContribution(double changevalue)
    {
        naturePollutionContribution += changevalue;
    }

    public void ModifyWaterPollutionContribution(double changevalue)
    {
        waterPollutionContribution += changevalue;
    }
}
