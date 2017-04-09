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
    public int onEventStartTemporaryConsequenceDuration { get; private set; }
    public double[] eventChoiceMoneyCost { get; private set; }
    public double[] eventChoiceMoneyReward { get; private set; }
    public int[] eventChoiceEventStartChanceModifier { get; private set; }
    public string[] possibleRegions { get; private set; }
    public int[] successChance { get; private set; } //chance to succesfully perform the choice
    public int[] increasedConsequencesModifierChance { get; private set; }
    public string[] possibleSectors { get; private set; }

    public SectorStatistics[] consequences { get; private set; }
    public SectorStatistics[] temporaryConsequences { get; private set; }
    public SectorStatistics[] duringEventProgressConsequences { get; private set; } //consequences after choosing an option until the event is completed
    public SectorStatistics onEventStartConsequence { get; private set; }
    public SectorStatistics onEventStartTemporaryConsequence { get; private set; }

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
    public bool[] pickedSectors { get; private set; }

    private GameEvent() { }
    
    public void StartEvent(int currentYear, int currentMonth)
    {
        onEventStartYear = currentYear;
        onEventStartMonth = currentMonth;

        isIdle = true;
        idleTurnsLeft = eventIdleDuration;
        isFinished = false;
    }

    public void pickEventSector(System.Random rnd)
    {
        pickedSectors[rnd.Next(0, possibleSectors.Count())] = true;
    }

    public void SubtractIdleTurnsLeft()
    {
        idleTurnsLeft--;
    }
    
    public void FinishEvent()
    {
        pickedChoiceStartYear = 0;
        pickedChoiceStartMonth = 0;
        onEventStartYear = 0;
        onEventStartMonth = 0;
        pickedChoiceNumber = 0;
        for (int i = 0; i < possibleSectors.Count(); i++)
            pickedSectors[i] = false;

        isFinished = true;
    }

    public void CompleteEvent(Game game)
    {
        isActive = false;
        eventStartChance += eventChoiceEventStartChanceModifier[pickedChoiceNumber];
        game.gameStatistics.ModifyMoney(eventChoiceMoneyReward[pickedChoiceNumber], true);
        lastCompleted = pickedChoiceStartYear * 12 + pickedChoiceStartMonth + eventCooldown;
    }

    public void SetPickedChoice(int i, Game game, Region region)
    {
        pickedChoiceNumber = i;
        pickedChoiceStartYear = game.currentYear;
        pickedChoiceStartMonth = game.currentMonth;
        isIdle = false;
        idleTurnsLeft = 0;
        isActive = true;

        game.gameStatistics.ModifyMoney(eventChoiceMoneyCost[pickedChoiceNumber], false);
        region.ImplementEventConsequences(this, duringEventProgressConsequences[pickedChoiceNumber], true);

        if (eventDuration[pickedChoiceNumber] == 0)
            region.CompleteEvent(this, game);
    }
}

