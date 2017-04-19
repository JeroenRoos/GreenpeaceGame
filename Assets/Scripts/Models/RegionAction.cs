using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//namespace Assets.Scripts.Models
[Serializable]
public class RegionAction //: MonoBehaviour
{
    public string[] name { get; private set; }
    public string[] description { get; private set; }
    public double actionMoneyCost { get; private set; }
    public double afterInvestmentActionMoneyCost { get; private set; }
    public double actionMoneyReward { get; private set; }
    public int actionDuration { get; private set; } //in months
    public string[] possibleSectors { get; private set; }
    public bool[] pickedSectors { get; private set; }
    public int actionCooldown { get; private set; } //in months
    public bool isUnique { get; private set; }
    public int temporaryConsequencesDuration { get; private set; }

    public SectorStatistics actionCosts { get; private set; }
    public SectorStatistics consequences { get; private set; }
    public SectorStatistics afterInvestmentConsequences { get; private set; }
    public SectorStatistics temporaryConsequences { get; private set; }
    public SectorStatistics duringActionConsequences { get; private set; }

    public int startYear { get; private set; }
    public int startMonth { get; private set; }
    public int lastCompleted { get; private set; } //in months
    public bool isActive { get; private set; }
    public int endTemporaryConsequencesMonth { get; private set; }

    private RegionAction() { }

    public void ActivateAction(int startYear, int startMonth, bool[] pickedSectors)
    {
        this.pickedSectors = pickedSectors;
        this.startYear = startYear;
        this.startMonth = startMonth;
        isActive = true;
    }

    public void CompleteAction()
    {
        lastCompleted = startYear * 12 + startMonth + actionDuration;
        endTemporaryConsequencesMonth = lastCompleted + +temporaryConsequencesDuration; 
        startYear = 0;
        startMonth = 0;
        isActive = false;
    }

    public void SetAfterInvestmentActionCost(double modifier)
    {
        afterInvestmentActionMoneyCost -= actionMoneyCost * modifier;
    }

    public void SetAfterInvestmentConsequences(double modifier)
    {
        double incomeChangeValue = consequences.income * modifier;
        double happinessChangeValue = consequences.happiness * modifier;
        double ecoAwarenessChangeValue = consequences.ecoAwareness * modifier;
        double prosperityChangeValue = consequences.prosperity * modifier;
        double airPollutionChangeValue = consequences.pollution.airPollution * modifier;
        double naturePollutionChangeValue = consequences.pollution.naturePollution * modifier;
        double waterPollutionChangeValue = consequences.pollution.waterPollution * modifier;
        double airPollutionIncreaseChangeValue = consequences.pollution.airPollutionIncrease * modifier;
        double naturePollutionIncreaseChangeValue = consequences.pollution.naturePollutionIncrease * modifier;
        double waterPollutionIncreaseChangeValue = consequences.pollution.waterPollutionIncrease * modifier;

        if (incomeChangeValue > 0)
            afterInvestmentConsequences.ModifyIncome(incomeChangeValue);
        else
            afterInvestmentConsequences.ModifyIncome(0 - incomeChangeValue);
        if (happinessChangeValue > 0)
            afterInvestmentConsequences.ModifyHappiness(happinessChangeValue);
        else
            afterInvestmentConsequences.ModifyHappiness(0 - happinessChangeValue);
        if (ecoAwarenessChangeValue > 0)
            afterInvestmentConsequences.ModifyEcoAwareness(ecoAwarenessChangeValue);
        else
            afterInvestmentConsequences.ModifyEcoAwareness(0 - ecoAwarenessChangeValue);
        if (prosperityChangeValue > 0)
            afterInvestmentConsequences.ModifyProsperity(prosperityChangeValue);
        else
            afterInvestmentConsequences.ModifyProsperity(0 - prosperityChangeValue);
        if (airPollutionChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeAirPollution(airPollutionChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeAirPollution(0 - airPollutionChangeValue);
        if (naturePollutionChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeNaturePollution(naturePollutionChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeNaturePollution(0 - naturePollutionChangeValue);
        if (waterPollutionChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeWaterPollution(waterPollutionChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeWaterPollution(0 - waterPollutionChangeValue);
        if (airPollutionIncreaseChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeAirPollutionMutation(airPollutionIncreaseChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeAirPollutionMutation(0 - airPollutionIncreaseChangeValue);
        if (naturePollutionIncreaseChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeNaturePollutionMutation(naturePollutionIncreaseChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeNaturePollutionMutation(0 - naturePollutionIncreaseChangeValue);
        if (waterPollutionIncreaseChangeValue < 0)
            afterInvestmentConsequences.pollution.ChangeWaterPollutionMutation(waterPollutionIncreaseChangeValue);
        else
            afterInvestmentConsequences.pollution.ChangeWaterPollutionMutation(0 - waterPollutionIncreaseChangeValue);
    }
}