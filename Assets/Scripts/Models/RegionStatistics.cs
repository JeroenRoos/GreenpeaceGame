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
    public double avgAirPollution { get; private set; }
    public double avgNaturePollution { get; private set; }
    public double avgWaterPollution { get; private set; }
    public double avgAirPollutionIncrease { get; private set; }
    public double avgNaturePollutionIncrease { get; private set; }
    public double avgWaterPollutionIncrease { get; private set; }

    //public double publicTransport { get; private set; } //public transport class
    //public double cityEnvironment { get; private set; } //city environment class

    public RegionStatistics()
    {
    }

    public void UpdateSectorAvgs(MapRegion region)
    {
        income = 0;
        happiness = 0;
        ecoAwareness = 0;
        prosperity = 0;
        avgPollution = 0;
        avgAirPollution = 0;
        avgNaturePollution = 0;
        avgWaterPollution = 0;
        avgAirPollutionIncrease = 0;
        avgNaturePollutionIncrease = 0;
        avgWaterPollutionIncrease = 0;

        int divisionValue = 0;

        foreach (RegionSector sector in region.sectors)
        {
            income += sector.statistics.income;
            happiness += sector.statistics.happiness;
            ecoAwareness += sector.statistics.ecoAwareness;
            prosperity += sector.statistics.prosperity;
            avgPollution += sector.statistics.pollution.avgPollution;
            avgAirPollution += sector.statistics.pollution.airPollution;
            avgNaturePollution += sector.statistics.pollution.naturePollution;
            avgWaterPollution += sector.statistics.pollution.waterPollution;
            avgAirPollutionIncrease += sector.statistics.pollution.airPollutionIncrease;
            avgNaturePollutionIncrease += sector.statistics.pollution.naturePollutionIncrease;
            avgWaterPollutionIncrease += sector.statistics.pollution.waterPollutionIncrease;

            divisionValue++;
        }
        
        happiness /= divisionValue;
        ecoAwareness /= divisionValue;
        prosperity /= divisionValue;
        avgPollution /= divisionValue;
        avgAirPollution /= divisionValue;
        avgNaturePollution /= divisionValue;
        avgWaterPollution /= divisionValue;
        avgAirPollutionIncrease /= divisionValue;
        avgNaturePollutionIncrease /= divisionValue;
        avgWaterPollutionIncrease /= divisionValue;
    }
}

