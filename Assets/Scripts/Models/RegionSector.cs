using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RegionSector
{
    public string[] sectorName { get; protected set; }
    public SectorStatistics statistics { get; protected set; }

    public RegionSector() { }

    public RegionSector(string[] sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }

    public void ImplementBuildingStatistics(Building activeBuilding, bool isAdded)
    {
        double statisticIncome = statistics.income * (activeBuilding.incomeModifier / 100);

        if (isAdded)
        {
            statistics.ModifyIncome(statisticIncome);
            statistics.ModifyHappiness(activeBuilding.happinessModifier);
            statistics.pollution.ChangeAirPollutionMutation(activeBuilding.pollutionModifier);
            statistics.pollution.ChangeNaturePollutionMutation(activeBuilding.pollutionModifier);
            statistics.pollution.ChangeWaterPollutionMutation(activeBuilding.pollutionModifier);
        }

        else
        {
            statistics.ModifyIncome(0 - statisticIncome);
            statistics.ModifyHappiness(0 - activeBuilding.happinessModifier);
            statistics.pollution.ChangeAirPollutionMutation(0 - activeBuilding.pollutionModifier);
            statistics.pollution.ChangeNaturePollutionMutation(0 - activeBuilding.pollutionModifier);
            statistics.pollution.ChangeWaterPollutionMutation(0 - activeBuilding.pollutionModifier);

        }
    }

    public void ImplementStatisticValues(SectorStatistics statistics, bool isAdded) //if a statistic is removed for example, isAdded is false
    {
        if (isAdded)
        {
            this.statistics.ModifyIncome(statistics.income);
            this.statistics.ModifyHappiness(statistics.happiness);
            this.statistics.ModifyEcoAwareness(statistics.ecoAwareness);
            this.statistics.ModifyProsperity(statistics.prosperity);
            
            this.statistics.pollution.ChangeAirPollution(statistics.pollution.airPollution);
            this.statistics.pollution.ChangeNaturePollution(statistics.pollution.naturePollution);
            this.statistics.pollution.ChangeWaterPollution(statistics.pollution.waterPollution);
            
            this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
        }

        else
        {
            this.statistics.ModifyIncome(0 - statistics.income);
            this.statistics.ModifyHappiness(0 - statistics.happiness);
            this.statistics.ModifyEcoAwareness(0 - statistics.ecoAwareness);
            this.statistics.ModifyProsperity(0 - statistics.prosperity);

            this.statistics.pollution.ChangeAirPollution(0 - statistics.pollution.airPollution);
            this.statistics.pollution.ChangeNaturePollution(0 - statistics.pollution.naturePollution);
            this.statistics.pollution.ChangeWaterPollution(0 - statistics.pollution.waterPollution);

            this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
        }
    }
}
