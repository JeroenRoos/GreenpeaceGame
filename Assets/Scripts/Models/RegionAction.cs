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

    public SectorStatistics consequences { get; private set; }
    public SectorStatistics afterInvestmentConsequences { get; private set; }
    public SectorStatistics temporaryConsequences { get; private set; }
    public SectorStatistics afterInvestmentTemporaryConsequences { get; private set; }

    public int startYear { get; private set; }
    public int startMonth { get; private set; }
    public int lastCompleted { get; private set; } //in months
    public bool isActive { get; private set; }
    public int endTemporaryConsequencesMonth { get; private set; }

    //action availability conditions
    public bool isAvailable { get; private set; }
    public bool conditionsAreRegional { get; private set; } //false = global, true = regional
    public int startAvailableYear { get; private set; }
    public int startAvailableMonth { get; private set; }
    public int endAvailableYear { get; private set; }
    public int endAvailableMonth { get; private set; }
    public SectorStatistics availableConditionsMinimum { get; private set; }
    public SectorStatistics availableConditionsMaximum { get; private set; }

    //multiplayer
    public bool isOwnAction;

    private RegionAction() { }

    public RegionAction(RegionAction r)
    {
        name = (string[])r.name.Clone();
        description = (string[])r.description.Clone();
        actionMoneyCost = r.actionMoneyCost;
        afterInvestmentActionMoneyCost = r.afterInvestmentActionMoneyCost;
        actionMoneyReward = r.actionMoneyReward;
        actionDuration = r.actionDuration;
        possibleSectors = (string[])r.possibleSectors.Clone();
        pickedSectors = (bool[])r.pickedSectors.Clone();
        actionCooldown = r.actionCooldown;
        isUnique = r.isUnique;
        temporaryConsequencesDuration = r.temporaryConsequencesDuration;

        consequences = new SectorStatistics(r.consequences);
        afterInvestmentConsequences = new SectorStatistics(r.afterInvestmentConsequences);
        temporaryConsequences = new SectorStatistics(r.temporaryConsequences);
        afterInvestmentTemporaryConsequences = new SectorStatistics(r.afterInvestmentTemporaryConsequences);

        startYear = r.startYear;
        startMonth = r.startMonth;
        lastCompleted = r.lastCompleted;
        isActive = r.isActive;
        endTemporaryConsequencesMonth = r.endTemporaryConsequencesMonth;
        isAvailable = r.isAvailable;
        conditionsAreRegional = r.conditionsAreRegional;
        startAvailableYear = r.startAvailableYear;
        startAvailableMonth = r.startAvailableMonth;
        endAvailableYear = r.endAvailableYear;
        endAvailableMonth = r.endAvailableMonth;
        availableConditionsMinimum = new SectorStatistics(r.availableConditionsMinimum);
        availableConditionsMaximum = new SectorStatistics(r.availableConditionsMaximum);
}

    public void GetAvailableActions(Game game, RegionStatistics rs)
    {
        if (conditionsAreRegional)
            GetRegionalAvailability(game.currentYear, game.currentMonth, rs);
        else
            GetGlobalAvailability(game.currentYear, game.currentMonth, game.gameStatistics);
    }

    public void GetRegionalAvailability(int currentYear, int currentMonth, RegionStatistics rs)
    {
        int monthTotal = currentYear * 12 + currentMonth;
        int startTimeCondition = startAvailableYear * 12 + startAvailableMonth;
        int endTimeCondition = endAvailableYear * 12 + endAvailableMonth;

        if ((monthTotal >= startTimeCondition && monthTotal < endTimeCondition) &&
            (availableConditionsMinimum.income > rs.income || availableConditionsMinimum.income == 0) &&
            (availableConditionsMinimum.happiness > rs.happiness || availableConditionsMinimum.happiness == 0) &&
            (availableConditionsMinimum.ecoAwareness > rs.ecoAwareness || availableConditionsMinimum.ecoAwareness == 0) &&
            (availableConditionsMinimum.prosperity > rs.prosperity || availableConditionsMinimum.prosperity == 0) &&
            (availableConditionsMinimum.pollution.airPollution > rs.avgAirPollution || availableConditionsMinimum.pollution.airPollution == 0) &&
            (availableConditionsMinimum.pollution.naturePollution > rs.avgNaturePollution || availableConditionsMinimum.pollution.naturePollution == 0) &&
            (availableConditionsMinimum.pollution.waterPollution > rs.avgWaterPollution || availableConditionsMinimum.pollution.waterPollution == 0) &&
            (availableConditionsMaximum.income < rs.income || availableConditionsMaximum.income == 0) &&
            (availableConditionsMaximum.happiness < rs.happiness || availableConditionsMaximum.happiness == 0) &&
            (availableConditionsMaximum.ecoAwareness < rs.ecoAwareness || availableConditionsMaximum.ecoAwareness == 0) &&
            (availableConditionsMaximum.prosperity < rs.prosperity || availableConditionsMaximum.prosperity == 0) &&
            (availableConditionsMaximum.pollution.airPollution < rs.avgAirPollution || availableConditionsMaximum.pollution.airPollution == 0) &&
            (availableConditionsMaximum.pollution.naturePollution < rs.avgNaturePollution || availableConditionsMaximum.pollution.naturePollution == 0) &&
            (availableConditionsMaximum.pollution.waterPollution < rs.avgWaterPollution || availableConditionsMaximum.pollution.waterPollution == 0))
            isAvailable = true;
        else
            isAvailable = false;
    }

    public void GetGlobalAvailability(int currentYear, int currentMonth, GameStatistics gs)
    {
        int monthTotal = currentYear * 12 + currentMonth;
        int startTimeCondition = startAvailableYear * 12 + startAvailableMonth;
        int endTimeCondition = endAvailableYear * 12 + endAvailableMonth;
        double pollutionAvgMin = (availableConditionsMinimum.pollution.airPollution +
            availableConditionsMinimum.pollution.naturePollution + availableConditionsMinimum.pollution.waterPollution) / 3;
        double pollutionAvgMax = (availableConditionsMaximum.pollution.airPollution +
            availableConditionsMaximum.pollution.naturePollution + availableConditionsMaximum.pollution.waterPollution) / 3;

        if ((monthTotal >= startTimeCondition && monthTotal < endTimeCondition) &&
            (availableConditionsMinimum.income > gs.income || availableConditionsMinimum.income == 0) &&
            (availableConditionsMinimum.happiness > gs.happiness || availableConditionsMinimum.happiness == 0) &&
            (availableConditionsMinimum.ecoAwareness > gs.ecoAwareness || availableConditionsMinimum.ecoAwareness == 0) &&
            (availableConditionsMinimum.prosperity > gs.prosperity || availableConditionsMinimum.prosperity == 0) &&
            (availableConditionsMinimum.pollution.airPollution > gs.pollution || availableConditionsMinimum.pollution.airPollution == 0) &&
            (availableConditionsMaximum.income < gs.income || availableConditionsMaximum.income == 0) &&
            (availableConditionsMaximum.happiness < gs.happiness || availableConditionsMaximum.happiness == 0) &&
            (availableConditionsMaximum.ecoAwareness < gs.ecoAwareness || availableConditionsMaximum.ecoAwareness == 0) &&
            (availableConditionsMaximum.prosperity < gs.prosperity || availableConditionsMaximum.prosperity == 0) &&
            (availableConditionsMaximum.pollution.airPollution < gs.pollution || availableConditionsMaximum.pollution.airPollution == 0))
            isAvailable = true;
        else
            isAvailable = false;

    }

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

    public void SetAfterInvestmentTemporaryConsequences(double modifier)
    {
        double incomeChangeValue = temporaryConsequences.income * modifier;
        double happinessChangeValue = temporaryConsequences.happiness * modifier;
        double ecoAwarenessChangeValue = temporaryConsequences.ecoAwareness * modifier;
        double prosperityChangeValue = temporaryConsequences.prosperity * modifier;
        double airPollutionChangeValue = temporaryConsequences.pollution.airPollution * modifier;
        double naturePollutionChangeValue = temporaryConsequences.pollution.naturePollution * modifier;
        double waterPollutionChangeValue = temporaryConsequences.pollution.waterPollution * modifier;
        double airPollutionIncreaseChangeValue = temporaryConsequences.pollution.airPollutionIncrease * modifier;
        double naturePollutionIncreaseChangeValue = temporaryConsequences.pollution.naturePollutionIncrease * modifier;
        double waterPollutionIncreaseChangeValue = temporaryConsequences.pollution.waterPollutionIncrease * modifier;

        if (incomeChangeValue > 0)
            afterInvestmentTemporaryConsequences.ModifyIncome(incomeChangeValue);
        else
            afterInvestmentTemporaryConsequences.ModifyIncome(0 - incomeChangeValue);
        if (happinessChangeValue > 0)
            afterInvestmentTemporaryConsequences.ModifyHappiness(happinessChangeValue);
        else
            afterInvestmentTemporaryConsequences.ModifyHappiness(0 - happinessChangeValue);
        if (ecoAwarenessChangeValue > 0)
            afterInvestmentTemporaryConsequences.ModifyEcoAwareness(ecoAwarenessChangeValue);
        else
            afterInvestmentTemporaryConsequences.ModifyEcoAwareness(0 - ecoAwarenessChangeValue);
        if (prosperityChangeValue > 0)
            afterInvestmentTemporaryConsequences.ModifyProsperity(prosperityChangeValue);
        else
            afterInvestmentTemporaryConsequences.ModifyProsperity(0 - prosperityChangeValue);
        if (airPollutionChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeAirPollution(airPollutionChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeAirPollution(0 - airPollutionChangeValue);
        if (naturePollutionChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeNaturePollution(naturePollutionChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeNaturePollution(0 - naturePollutionChangeValue);
        if (waterPollutionChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeWaterPollution(waterPollutionChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeWaterPollution(0 - waterPollutionChangeValue);
        if (airPollutionIncreaseChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeAirPollutionMutation(airPollutionIncreaseChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeAirPollutionMutation(0 - airPollutionIncreaseChangeValue);
        if (naturePollutionIncreaseChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeNaturePollutionMutation(naturePollutionIncreaseChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeNaturePollutionMutation(0 - naturePollutionIncreaseChangeValue);
        if (waterPollutionIncreaseChangeValue < 0)
            afterInvestmentTemporaryConsequences.pollution.ChangeWaterPollutionMutation(waterPollutionIncreaseChangeValue);
        else
            afterInvestmentTemporaryConsequences.pollution.ChangeWaterPollutionMutation(0 - waterPollutionIncreaseChangeValue);
    }
}