using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SectorStatistics
{
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }
    public double airPollutionContribution { get; private set; }
    public double naturePollutionContribution { get; private set; }
    public double waterPollutionContribution { get; private set; }

    public SectorStatistics(double happiness, double ecoAwareness, double prosperity, double airPollutionContribution,
                            double naturePollutionContribution, double waterPollutionContribution)
    {
        this.happiness = happiness;
        this.ecoAwareness = ecoAwareness;
        this.prosperity = prosperity;
        this.airPollutionContribution = airPollutionContribution;
        this.naturePollutionContribution = naturePollutionContribution;
        this.waterPollutionContribution = waterPollutionContribution;
    }

    //happiness = prosperity - pollution *formule*
    public void ModifyHappiness(double changeValue)
    {
        happiness += changeValue;
    }

    public void ModifyEcoAwareness(double changeValue)
    {
        ecoAwareness += changeValue;
    }

    public void ModifyProsperity(double changeValue)
    {
        prosperity += changeValue;
    }
}
