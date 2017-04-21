using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RegionSector
{
    public string[] sectorName { get; protected set; }
    public SectorStatistics statistics { get; protected set; }
    public double incomeModifier { get; protected set; }
    public double pollutionModifier { get; protected set; }
    public double happinessModifier { get; protected set; }

    public RegionSector() { }

    public void TempMethod()
    {
        incomeModifier = 0;
        pollutionModifier = 0;
        happinessModifier = 0;
    }

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
            incomeModifier = activeBuilding.incomeModifier;
            pollutionModifier = activeBuilding.incomeModifier;
            happinessModifier = activeBuilding.incomeModifier;

            if (statisticIncome > 0)
                statistics.ModifyIncome(statisticIncome);
            else
                statistics.ModifyIncome(0 - statisticIncome);

            statistics.ModifyHappiness(activeBuilding.happinessModifier);
            statistics.pollution.ChangeAirPollutionMutation(activeBuilding.pollutionModifier);
            statistics.pollution.ChangeNaturePollutionMutation(activeBuilding.pollutionModifier);
            statistics.pollution.ChangeWaterPollutionMutation(activeBuilding.pollutionModifier);
        }

        else
        {
            if (statisticIncome > 0)
            statistics.ModifyIncome(0 - statisticIncome);
            else
                statistics.ModifyIncome(statisticIncome);

            statistics.ModifyHappiness(0 - activeBuilding.happinessModifier);
            statistics.pollution.ChangeAirPollutionMutation(0 - activeBuilding.pollutionModifier);
            statistics.pollution.ChangeNaturePollutionMutation(0 - activeBuilding.pollutionModifier);
            statistics.pollution.ChangeWaterPollutionMutation(0 - activeBuilding.pollutionModifier);

            incomeModifier = 0;
            pollutionModifier = 0;
            happinessModifier = 0;

        }
    }

    public void ImplementStatisticValues(SectorStatistics statistics, bool isAdded) //if a statistic is removed for example, isAdded is false
    {
        double modifiedIncome = ModifyIncomeFromBuilding(statistics.income);
        /*double modifiedHappiness = ModifyHappinessFromBuilding(statistics.happiness);
        double modifiedAirPollution = ModifyAirPollutionFromBuilding(statistics.pollution.airPollution);
        double modifiedNaturePollution = ModifyAirPollutionFromBuilding(statistics.pollution.naturePollution);
        double modifiedWaterPollution = ModifyAirPollutionFromBuilding(statistics.pollution.waterPollution);
        double modifiedAirPollutionIncrease = ModifyAirPollutionFromBuilding(statistics.pollution.airPollutionIncrease);
        double modifiedNaturePollutionIncrease = ModifyAirPollutionFromBuilding(statistics.pollution.naturePollutionIncrease);
        double modifiedWaterPollutionIncrease = ModifyAirPollutionFromBuilding(statistics.pollution.waterPollutionIncrease);*/

        if (isAdded)
        {
            this.statistics.ModifyIncome(modifiedIncome);
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
            this.statistics.ModifyIncome(0 - modifiedIncome);
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

    public double ModifyIncomeFromBuilding(double oldIncome)
    {
        if (oldIncome > 0)
            return oldIncome + oldIncome * incomeModifier;
        else
            return oldIncome - oldIncome * incomeModifier;
    }

    /*public double ModifyHappinessFromBuilding(double oldHappiness)
    {
        if (oldHappiness > 0)
            return oldHappiness + oldHappiness * happinessModifier;
        else
            return oldHappiness - oldHappiness * happinessModifier;
    }

    public double ModifyAirPollutionFromBuilding(double oldAirPollution)
    {
        if (oldAirPollution > 0)
            return oldAirPollution - oldAirPollution * pollutionModifier;
        else
            return oldAirPollution + oldAirPollution * pollutionModifier;
    }

    public double ModifyNaturePollutionFromBuilding(double oldNaturePollution)
    {
        if (oldNaturePollution > 0)
            return oldNaturePollution - oldNaturePollution * pollutionModifier;
        else
            return oldNaturePollution + oldNaturePollution * pollutionModifier;
    }

    public double ModifyWaterPollutionFromBuilding(double oldWaterPollution)
    {
        if (oldWaterPollution > 0)
            return oldWaterPollution - oldWaterPollution * pollutionModifier;
        else
            return oldWaterPollution + oldWaterPollution * pollutionModifier;
    }

    public double ModifyAirPollutionIncreaseFromBuilding(double oldAirPollutionIncrease)
    {
        if (oldAirPollutionIncrease > 0)
            return oldAirPollutionIncrease - oldAirPollutionIncrease * pollutionModifier;
        else
            return oldAirPollutionIncrease + oldAirPollutionIncrease * pollutionModifier;
    }

    public double ModifyNaturePollutionIncreaseFromBuilding(double oldNaturePollutionIncrease)
    {
        if (oldNaturePollutionIncrease > 0)
            return oldNaturePollutionIncrease - oldNaturePollutionIncrease * pollutionModifier;
        else
            return oldNaturePollutionIncrease + oldNaturePollutionIncrease * pollutionModifier;
    }

    public double ModifyWaterPollutionIncreaseFromBuilding(double oldWaterPollutionIncrease)
    {
        if (oldWaterPollutionIncrease > 0)
            return oldWaterPollutionIncrease - oldWaterPollutionIncrease * pollutionModifier;
        else
            return oldWaterPollutionIncrease + oldWaterPollutionIncrease * pollutionModifier;
    }*/
}
