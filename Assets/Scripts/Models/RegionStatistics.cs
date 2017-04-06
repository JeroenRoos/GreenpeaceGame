using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RegionStatistics
{
    public double income { get; private set; }

    //averages of sectors in the region
    public double happiness { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }
    public double avgPollution { get; private set; }

    //public double publicTransport { get; private set; } //public transport class
    //public double cityEnvironment { get; private set; } //city environment class

    private RegionStatistics()
    {
        this.income = 0;
        this.happiness = 0;
        this.ecoAwareness = 0;
        this.prosperity = 0;
        this.avgPollution = 0;
    }

    public void UpdateSectorAvgs(Region region)
    {
        income = 0;
        happiness = 0;
        avgPollution = 0;
        ecoAwareness = 0;
        prosperity = 0;

        int divisionValue = 0;

        foreach (RegionSector sector in region.sectors)
        {
            income += sector.statistics.income;
            happiness += sector.statistics.happiness;
            avgPollution += sector.statistics.pollution.avgPullution;
            ecoAwareness += sector.statistics.ecoAwareness;
            prosperity += sector.statistics.prosperity;

            divisionValue++;
        }

        income /= divisionValue;
        happiness /= divisionValue;
        avgPollution /= divisionValue;
        ecoAwareness /= divisionValue;
        prosperity /= divisionValue;
    }
}

