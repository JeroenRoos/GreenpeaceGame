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
    public bool isUnique { get; private set; }

    public int eventIdleDuration { get; private set; } //in months
    public int eventCooldown { get; private set; } //in months
    public string[] choicesDutch { get; private set; }
    public string[] choicesEnglish { get; private set; }
    public int[] eventDuration { get; private set; } //in months
    public double[] eventChoiceMoneyCost { get; private set; }

    public SectorStatistics[] consequences { get; private set; }
    public SectorStatistics[] temporaryConsequences { get; private set; }
    public int[] temporaryConsequencesDuration { get; private set; }

    public SectorStatistics[] duringEventProgressConsequences { get; private set; } //consequences after choosing an option until the event is completed

    public SectorStatistics onEventStartConsequence { get; private set; }
    public SectorStatistics onEventStartTemporaryConsequence { get; private set; }
    public int onEventStartTemporaryConsequenceDuration { get; private set; }
    public int onEventStartMonth { get; private set; }
    public int onEventStartYear { get; private set; }
    
    //choice picked events variables
    public int pickedChoiceNumber { get; private set; }
    public int pickedChoiceStartYear { get; private set; }
    public int pickedChoiceStartMonth { get; private set; }
    public int lastCompleted { get; private set; }
    public bool isIdle { get; private set; }
    public int idleTurnsLeft { get; private set; } 
    public bool isActive { get; private set; }
    
    public bool isFinished { get; private set; }
    public string[] possibleRegions { get; private set; }
    public bool isGlobal { get; private set; }
    public int[] successChance { get; private set; }
    public int[] increasedConsequencesModifierChance { get; private set; }
    
    public string[] possibleSectors { get; private set; }
    public bool[] pickedSectors { get; private set; }

    private GameEvent()
    {
        possibleSectors = new string[] { "Huishoudens", "Bedrijven", "Landbouw" };
        for (int i = 0; i < possibleSectors.Count(); i++)
            pickedSectors[i] = false;
    }

    /*public GameEvent(string name, string[] description, int[] eventDuration, string[,] choices, SectorStatistics[] consequences,
                    SectorStatistics onEventStartConsequence, double[] eventChoiceMoneyCost, int eventCooldown, bool isUnique)
    {
        this.name = name;
        this.description = description;
        this.eventDuration = eventDuration;
        this.choicesDutch = new string[3] { choices[0, 0], choices[0, 1], choices[0, 2] };
        this.choicesEnglish = new string[3] { choices[1, 0], choices[1, 1], choices[1, 2] };
        this.consequences = consequences;
        this.onEventStartConsequence = onEventStartConsequence;
        this.eventChoiceMoneyCost = eventChoiceMoneyCost;
        this.eventCooldown = eventCooldown;
        this.isUnique = isUnique;

        isActive = false;
        isIdle = false;
        isFinished = true;
        pickedChoiceNumber = 0;
        pickedChoiceStartYear = 0;
        pickedChoiceStartMonth = 0;
        lastCompleted = 0;
        idleTurnsLeft = 0;
        possibleRegions = new string[] { "Noord Nederland", "Oost Nederland", "Zuid Nederland", "West Nederland" };
        isGlobal = false;
        successChance = new int[] { 100, 100, 100 };
        increasedConsequencesModifierChance = new int[] { 0, 0, 0 };

        //temporary anti error code
        eventIdleDuration = 100;
        onEventStartMonth = 0;
        onEventStartYear = 0;
        onEventStartTemporaryConsequence = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        temporaryConsequencesDuration = new int[] { 0, 0, 0 };
        temporaryConsequences = new RegionStatistics[] {
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0),
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0),
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0) };
        
        duringEventProgressConsequences = new RegionStatistics[] {
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0),
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0),
            new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0) };
    }
    */

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

    public void CompleteEvent()
    {
        isActive = false;
        lastCompleted = pickedChoiceStartYear * 12 + pickedChoiceStartMonth + eventCooldown;
    }

    public void SetPickedChoice(int i, Game game)
    {
        if (game.gameStatistics.money > eventChoiceMoneyCost[i])
        {
            game.gameStatistics.ModifyMoney(-eventChoiceMoneyCost[i]);

            pickedChoiceNumber = i;
            this.pickedChoiceStartYear = game.currentYear;
            this.pickedChoiceStartMonth = game.currentMonth;

            isIdle = false;
            idleTurnsLeft = 0;
            isActive = true;
            
            if (eventDuration[i] == 0)
            {
                CompleteEvent();
            }
        }

        else
        {
            //not enough money popup message?
        }
    }
}

