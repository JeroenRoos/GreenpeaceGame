using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

[Serializable]
public class GameEvent
{
    public string name { get; private set; } //id
    public string[] publicEventName { get; private set; }

    public string[] description { get; private set; }
    public string[] choicesDutch { get; private set; }
    public string[] choicesEnglish { get; private set; }
    public bool isUnique { get; private set; }
    public bool isGlobal { get; private set; }
    public int eventStartChance { get; private set; } //chance to actually get the event when rolled (0-100)
    public int eventIdleDuration { get; private set; } //in months
    public int eventCooldown { get; private set; } //in months
    public int[] eventDuration { get; private set; } //in months
    public int[] temporaryConsequencesDuration { get; private set; }
    public double[] eventChoiceMoneyCost { get; private set; }
    public double[] afterInvestmentEventChoiceMoneyCost { get; private set; }
    public double[] eventChoiceMoneyReward { get; private set; }
    public int[] eventChoiceEventStartChanceModifier { get; private set; }
    public string[] possibleRegions { get; private set; }
    public int[] successChance { get; private set; } //chance to succesfully perform the choice
    public int[] increasedConsequencesModifierChance { get; private set; }
    public string[] possibleSectors { get; private set; }

    public SectorStatistics[] consequences { get; private set; }
    public SectorStatistics[] afterInvestmentConsequences { get; private set; }
    public SectorStatistics[] temporaryConsequences { get; private set; }
    public SectorStatistics[] afterInvestmentTemporaryConsequences { get; private set; }

    //choice picked events variables
    public int pickedChoiceNumber { get; private set; }
    public int pickedChoiceStartYear { get; private set; }
    public int pickedChoiceStartMonth { get; private set; }
    public int lastCompleted { get; private set; }
    public bool isIdle { get; private set; }
    public int idleTurnsLeft { get; private set; }
    public bool isActive { get; private set; }
    public int onEventStartMonth { get; private set; }
    public int onEventStartYear { get; private set; }
    public bool isFinished { get; private set; }

    public SectorStatistics[] pickedConsequences;
    public SectorStatistics[] pickedTemporaryConsequences;

    public bool isOwnEvent;

    public GameEvent() { }
    
    //method used for copying GameEvent without reference
    public GameEvent(GameEvent e)
    {
        name = e.name;
        publicEventName = (string[])e.publicEventName.Clone();
        description = (string[])e.description.Clone();
        choicesDutch = (string[])e.choicesDutch.Clone();
        choicesEnglish = (string[])e.choicesEnglish.Clone();
        isUnique = e.isUnique;
        isGlobal = e.isGlobal;
        eventStartChance = e.eventStartChance;
        eventIdleDuration = e.eventIdleDuration;
        eventCooldown = e.eventCooldown;
        eventDuration = (int[])e.eventDuration;
        temporaryConsequencesDuration = (int[])e.temporaryConsequencesDuration.Clone();
        eventChoiceMoneyCost = (double[])e.eventChoiceMoneyCost.Clone();
        afterInvestmentEventChoiceMoneyCost = (double[])e.afterInvestmentEventChoiceMoneyCost.Clone();
        eventChoiceMoneyReward = (double[])e.eventChoiceMoneyReward.Clone();
        eventChoiceEventStartChanceModifier = (int[])e.eventChoiceEventStartChanceModifier.Clone();
        possibleRegions = (string[])e.possibleRegions.Clone();
        successChance = (int[])e.successChance.Clone();
        increasedConsequencesModifierChance = (int[])e.increasedConsequencesModifierChance.Clone();
        possibleSectors = (string[])e.possibleSectors.Clone();
        pickedChoiceNumber = e.pickedChoiceNumber;
        pickedChoiceStartYear = e.pickedChoiceStartYear;
        pickedChoiceStartMonth = e.pickedChoiceStartMonth;
        lastCompleted = e.lastCompleted;
        isIdle = e.isIdle;
        idleTurnsLeft = e.idleTurnsLeft;
        isActive = e.isActive;
        onEventStartMonth = e.onEventStartMonth;
        onEventStartYear = e.onEventStartYear;
        isFinished = e.isFinished;
        consequences = new SectorStatistics[3] { new SectorStatistics(e.consequences[0]), new SectorStatistics(e.consequences[0]), new SectorStatistics(e.consequences[0]) };
        afterInvestmentConsequences = new SectorStatistics[3] { new SectorStatistics(e.afterInvestmentConsequences[0]), new SectorStatistics(e.afterInvestmentConsequences[0]), new SectorStatistics(e.afterInvestmentConsequences[0]) };
        temporaryConsequences = new SectorStatistics[3] { new SectorStatistics(e.temporaryConsequences[0]), new SectorStatistics(e.temporaryConsequences[0]), new SectorStatistics(e.temporaryConsequences[0]) };
        afterInvestmentTemporaryConsequences = new SectorStatistics[3] { new SectorStatistics(e.afterInvestmentTemporaryConsequences[0]), new SectorStatistics(e.afterInvestmentTemporaryConsequences[0]), new SectorStatistics(e.afterInvestmentTemporaryConsequences[0]) };
        pickedConsequences = new SectorStatistics[3] { new SectorStatistics(e.pickedConsequences[0]), new SectorStatistics(e.pickedConsequences[0]), new SectorStatistics(e.pickedConsequences[0]) };
        pickedTemporaryConsequences = new SectorStatistics[3] { new SectorStatistics(e.pickedTemporaryConsequences[0]), new SectorStatistics(e.pickedTemporaryConsequences[0]), new SectorStatistics(e.pickedTemporaryConsequences[0]) };
    }

    #region EventStatusMethods
    public void StartEvent(int currentYear, int currentMonth)
    {
        onEventStartYear = currentYear;
        onEventStartMonth = currentMonth;

        isIdle = true;
        idleTurnsLeft = eventIdleDuration;
        isFinished = false;
    }

    public void SubtractIdleTurnsLeft()
    {
        idleTurnsLeft--;
    }

    public void SetPickedChoice(int i, Game game, MapRegion region)
    {
        pickedChoiceNumber = i;
        pickedChoiceStartYear = game.currentYear;
        pickedChoiceStartMonth = game.currentMonth;
        isIdle = false;
        idleTurnsLeft = 0;
        isActive = true;

        if (!ApplicationModel.multiplayer || isOwnEvent)
            game.gameStatistics.ModifyMoney(afterInvestmentEventChoiceMoneyCost[pickedChoiceNumber], false);

        EventManager.CallDestroySprite(this);

        if (eventDuration[pickedChoiceNumber] == 0)
        {
            game.AddCompletedEventToReports(region, this);
            region.CompleteEvent(this, game);
        }
    }

    public void CompleteEvent(Game game)
    {
        isActive = false;
        eventStartChance += eventChoiceEventStartChanceModifier[pickedChoiceNumber];
        if (!ApplicationModel.multiplayer || isOwnEvent)
            game.gameStatistics.ModifyMoney(eventChoiceMoneyReward[pickedChoiceNumber], true);
        lastCompleted = pickedChoiceStartYear * 12 + pickedChoiceStartMonth + eventCooldown + temporaryConsequencesDuration[pickedChoiceNumber];
    }

    public void FinishEvent()
    {
        isFinished = true;
    }
    #endregion

    #region UpdateEventVariablesMethods
    //changes the costs of the choices
    public void SetAfterInvestmentEventChoiceMoneyCost(double modifier)
    {
        for (int i = 0; i < afterInvestmentEventChoiceMoneyCost.Length; i++)
        {
            afterInvestmentEventChoiceMoneyCost[i] -= eventChoiceMoneyCost[i] * modifier;
        }
    }

    //changes variables of the consequences
    public void SetAfterInvestmentConsequences(double modifier)
    {
        for (int i = 0; i < consequences.Length; i++)
        {
            double incomeChangeValue = consequences[i].income * modifier;
            double happinessChangeValue = consequences[i].happiness * modifier;
            double ecoAwarenessChangeValue = consequences[i].ecoAwareness * modifier;
            double prosperityChangeValue = consequences[i].prosperity * modifier;
            double airPollutionChangeValue = consequences[i].pollution.airPollution * modifier;
            double naturePollutionChangeValue = consequences[i].pollution.naturePollution * modifier;
            double waterPollutionChangeValue = consequences[i].pollution.waterPollution * modifier;
            double airPollutionIncreaseChangeValue = consequences[i].pollution.airPollutionIncrease * modifier;
            double naturePollutionIncreaseChangeValue = consequences[i].pollution.naturePollutionIncrease * modifier;
            double waterPollutionIncreaseChangeValue = consequences[i].pollution.waterPollutionIncrease * modifier;

            if (incomeChangeValue > 0)
                afterInvestmentConsequences[i].ModifyIncome(incomeChangeValue);
            else
                afterInvestmentConsequences[i].ModifyIncome(0 - incomeChangeValue);
            if (happinessChangeValue > 0)
                afterInvestmentConsequences[i].ModifyHappiness(happinessChangeValue);
            else
                afterInvestmentConsequences[i].ModifyHappiness(0 - happinessChangeValue);
            if (ecoAwarenessChangeValue > 0)
                afterInvestmentConsequences[i].ModifyEcoAwareness(ecoAwarenessChangeValue, false);
            else
                afterInvestmentConsequences[i].ModifyEcoAwareness(0 - ecoAwarenessChangeValue, false);
            if (prosperityChangeValue > 0)
                afterInvestmentConsequences[i].ModifyProsperity(prosperityChangeValue, false);
            else
                afterInvestmentConsequences[i].ModifyProsperity(0 - prosperityChangeValue, false);
            if (airPollutionChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeAirPollution(airPollutionChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeAirPollution(0 - airPollutionChangeValue);
            if (naturePollutionChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeNaturePollution(naturePollutionChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeNaturePollution(0 - naturePollutionChangeValue);
            if (waterPollutionChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeWaterPollution(waterPollutionChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeWaterPollution(0 - waterPollutionChangeValue);
            if (airPollutionIncreaseChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeAirPollutionMutation(airPollutionIncreaseChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeAirPollutionMutation(0 - airPollutionIncreaseChangeValue);
            if (naturePollutionIncreaseChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeNaturePollutionMutation(naturePollutionIncreaseChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeNaturePollutionMutation(0 - naturePollutionIncreaseChangeValue);
            if (waterPollutionIncreaseChangeValue < 0)
                afterInvestmentConsequences[i].pollution.ChangeWaterPollutionMutation(waterPollutionIncreaseChangeValue);
            else
                afterInvestmentConsequences[i].pollution.ChangeWaterPollutionMutation(0 - waterPollutionIncreaseChangeValue);
        }
    }

    //changes variables of the temporary consequences
    public void SetAfterInvestmentTemporaryConsequences(double modifier)
    {
        for (int i = 0; i < consequences.Length; i++)
        {
            double incomeChangeValue = temporaryConsequences[i].income * modifier;
            double happinessChangeValue = temporaryConsequences[i].happiness * modifier;
            double ecoAwarenessChangeValue = temporaryConsequences[i].ecoAwareness * modifier;
            double prosperityChangeValue = temporaryConsequences[i].prosperity * modifier;
            double airPollutionChangeValue = temporaryConsequences[i].pollution.airPollution * modifier;
            double naturePollutionChangeValue = temporaryConsequences[i].pollution.naturePollution * modifier;
            double waterPollutionChangeValue = temporaryConsequences[i].pollution.waterPollution * modifier;
            double airPollutionIncreaseChangeValue = temporaryConsequences[i].pollution.airPollutionIncrease * modifier;
            double naturePollutionIncreaseChangeValue = temporaryConsequences[i].pollution.naturePollutionIncrease * modifier;
            double waterPollutionIncreaseChangeValue = temporaryConsequences[i].pollution.waterPollutionIncrease * modifier;

            if (incomeChangeValue > 0)
                afterInvestmentTemporaryConsequences[i].ModifyIncome(incomeChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].ModifyIncome(0 - incomeChangeValue);
            if (happinessChangeValue > 0)
                afterInvestmentTemporaryConsequences[i].ModifyHappiness(happinessChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].ModifyHappiness(0 - happinessChangeValue);
            if (ecoAwarenessChangeValue > 0)
                afterInvestmentTemporaryConsequences[i].ModifyEcoAwareness(ecoAwarenessChangeValue, false);
            else
                afterInvestmentTemporaryConsequences[i].ModifyEcoAwareness(0 - ecoAwarenessChangeValue, false);
            if (prosperityChangeValue > 0)
                afterInvestmentTemporaryConsequences[i].ModifyProsperity(prosperityChangeValue, false);
            else
                afterInvestmentTemporaryConsequences[i].ModifyProsperity(0 - prosperityChangeValue, false);
            if (airPollutionChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeAirPollution(airPollutionChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeAirPollution(0 - airPollutionChangeValue);
            if (naturePollutionChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeNaturePollution(naturePollutionChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeNaturePollution(0 - naturePollutionChangeValue);
            if (waterPollutionChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeWaterPollution(waterPollutionChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeWaterPollution(0 - waterPollutionChangeValue);
            if (airPollutionIncreaseChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeAirPollutionMutation(airPollutionIncreaseChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeAirPollutionMutation(0 - airPollutionIncreaseChangeValue);
            if (naturePollutionIncreaseChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeNaturePollutionMutation(naturePollutionIncreaseChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeNaturePollutionMutation(0 - naturePollutionIncreaseChangeValue);
            if (waterPollutionIncreaseChangeValue < 0)
                afterInvestmentTemporaryConsequences[i].pollution.ChangeWaterPollutionMutation(waterPollutionIncreaseChangeValue);
            else
                afterInvestmentTemporaryConsequences[i].pollution.ChangeWaterPollutionMutation(0 - waterPollutionIncreaseChangeValue);
        }
    }
    #endregion
}

