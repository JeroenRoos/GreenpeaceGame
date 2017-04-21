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

    public void ImplementStatisticValues(SectorStatistics statistics, bool isAdded, double globalHappiness) //if a statistic is removed for example, isAdded is false
    {
        double modifiedIncome = ModifyIncomeFromBuilding(statistics.income);
        modifiedIncome = ModifyIncomeFromHappiness(modifiedIncome, globalHappiness);
        double modifiedHappiness = ModifyHappinessFromHappiness(statistics.happiness, globalHappiness);
        double modifiedEcoAwareness = ModifyEcoAwarenessFromHappiness(statistics.ecoAwareness, globalHappiness);
        double modifiedProsperity = ModifyProsperityFromHappiness(statistics.prosperity, globalHappiness);
        double modifiedAirPollution = ModifyAirPollutionFromHappiness(statistics.pollution.airPollution, globalHappiness);
        double modifiedNaturePollution = ModifyAirPollutionFromHappiness(statistics.pollution.naturePollution, globalHappiness);
        double modifiedWaterPollution = ModifyAirPollutionFromHappiness(statistics.pollution.waterPollution, globalHappiness);
        double modifiedAirPollutionIncrease = ModifyAirPollutionFromHappiness(statistics.pollution.airPollutionIncrease, globalHappiness);
        double modifiedNaturePollutionIncrease = ModifyAirPollutionFromHappiness(statistics.pollution.naturePollutionIncrease, globalHappiness);
        double modifiedWaterPollutionIncrease = ModifyAirPollutionFromHappiness(statistics.pollution.waterPollutionIncrease, globalHappiness);

        if (isAdded)
        {
            this.statistics.ModifyIncome(modifiedIncome);
            this.statistics.ModifyHappiness(modifiedHappiness);
            this.statistics.ModifyEcoAwareness(modifiedEcoAwareness);
            this.statistics.ModifyProsperity(modifiedProsperity);
            
            this.statistics.pollution.ChangeAirPollution(modifiedAirPollution);
            this.statistics.pollution.ChangeNaturePollution(modifiedNaturePollution);
            this.statistics.pollution.ChangeWaterPollution(modifiedWaterPollution);
            
            this.statistics.pollution.ChangeAirPollutionMutation(modifiedAirPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(modifiedNaturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(modifiedWaterPollutionIncrease);
        }

        else
        {
            this.statistics.ModifyIncome(0 - modifiedIncome);
            this.statistics.ModifyHappiness(0 - modifiedHappiness);
            this.statistics.ModifyEcoAwareness(0 - modifiedEcoAwareness);
            this.statistics.ModifyProsperity(0 - modifiedProsperity);

            this.statistics.pollution.ChangeAirPollution(0 - modifiedAirPollution);
            this.statistics.pollution.ChangeNaturePollution(0 - modifiedNaturePollution);
            this.statistics.pollution.ChangeWaterPollution(0 - modifiedWaterPollution);

            this.statistics.pollution.ChangeAirPollutionMutation(0 - modifiedAirPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(0 - modifiedNaturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(0 - modifiedWaterPollutionIncrease);
        }
    }

    public double ModifyIncomeFromBuilding(double oldIncome)
    {
        if (oldIncome > 0)
            return oldIncome + oldIncome * incomeModifier;
        else
            return oldIncome - oldIncome * incomeModifier;
    }

    public double ModifyIncomeFromHappiness(double oldIncome, double happiness)
    {
        if (oldIncome > 0)
            return oldIncome + oldIncome * ((happiness - 50) * 100);
        else
            return oldIncome - oldIncome * ((happiness - 50) * 100);
    }

    public double ModifyHappinessFromHappiness(double oldHappiness, double happiness)
    {
        if (oldHappiness > 0)
            return oldHappiness + oldHappiness * ((happiness - 50) * 100);
        else
            return oldHappiness - oldHappiness * ((happiness - 50) * 100);
    }

    public double ModifyEcoAwarenessFromHappiness(double oldEcoAwareness, double happiness)
    {
        if (oldEcoAwareness > 0)
            return oldEcoAwareness + oldEcoAwareness * ((happiness - 50) * 100);
        else
            return oldEcoAwareness - oldEcoAwareness * ((happiness - 50) * 100);
    }

    public double ModifyProsperityFromHappiness(double oldProsperity, double happiness)
    {
        if (oldProsperity > 0)
            return oldProsperity + oldProsperity * ((happiness - 50) * 100);
        else
            return oldProsperity - oldProsperity * ((happiness - 50) * 100);
    }

    public double ModifyAirPollutionFromHappiness(double oldAirPollution, double happiness)
    {
        if (oldAirPollution > 0)
            return oldAirPollution - oldAirPollution * ((happiness - 50) * 100);
        else
            return oldAirPollution + oldAirPollution * ((happiness - 50) * 100);
    }

    public double ModifyNaturePollutionFromHappiness(double oldNaturePollution, double happiness)
    {
        if (oldNaturePollution > 0)
            return oldNaturePollution - oldNaturePollution * ((happiness - 50) * 100);
        else
            return oldNaturePollution + oldNaturePollution * ((happiness - 50) * 100);
    }

    public double ModifyWaterPollutionFromHappiness(double oldWaterPollution, double happiness)
    {
        if (oldWaterPollution > 0)
            return oldWaterPollution - oldWaterPollution * ((happiness - 50) * 100);
        else
            return oldWaterPollution + oldWaterPollution * ((happiness - 50) * 100);
    }

    public double ModifyAirPollutionIncreaseFromHappiness(double oldAirPollutionIncrease, double happiness)
    {
        if (oldAirPollutionIncrease > 0)
            return oldAirPollutionIncrease - oldAirPollutionIncrease * ((happiness - 50) * 100);
        else
            return oldAirPollutionIncrease + oldAirPollutionIncrease * ((happiness - 50) * 100);
    }

    public double ModifyNaturePollutionIncreaseFromHappiness(double oldNaturePollutionIncrease, double happiness)
    {
        if (oldNaturePollutionIncrease > 0)
            return oldNaturePollutionIncrease - oldNaturePollutionIncrease * ((happiness - 50) * 100);
        else
            return oldNaturePollutionIncrease + oldNaturePollutionIncrease * ((happiness - 50) * 100);
    }

    public double ModifyWaterPollutionIncreaseFromHappiness(double oldWaterPollutionIncrease, double happiness)
    {
        if (oldWaterPollutionIncrease > 0)
            return oldWaterPollutionIncrease - oldWaterPollutionIncrease * ((happiness - 50) * 100);
        else
            return oldWaterPollutionIncrease + oldWaterPollutionIncrease * ((happiness - 50) * 100);
    }
}
