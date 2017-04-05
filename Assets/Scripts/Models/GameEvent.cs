using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;


//UNFINISHED
public class GameEvent
{
    public string name { get; private set; } //id
    
    public string[] description { get; private set; }
    public bool isUnique { get; private set; }

    public int eventIdleDuration { get; private set; } //in months
    public int eventCooldown { get; private set; } //in months

    public string[,] choices { get; private set; }
    
    public int[] eventDuration { get; private set; } //in months
    public double[] eventChoiceMoneyCost { get; private set; }

    public RegionStatistics[] consequences { get; private set; }
    public RegionStatistics[] temporaryConsequences { get; private set; }
    public int[] temporaryConsequencesDuration { get; private set; }

    public RegionStatistics[] duringEventProgressConsequences { get; private set; } //consequences after choosing an option until the event is completed


    public RegionStatistics onEventStartConsequence { get; private set; }
    public RegionStatistics onEventStartTemporaryConsequence { get; private set; }
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

    public Region region { get; private set; }

    public GameEvent(string name, string[] description, int[] eventDuration, string[,] choices, RegionStatistics[] consequences, 
                    RegionStatistics onEventStartConsequence, double[] eventChoiceMoneyCost, int eventCooldown, bool isUnique, Region region)
    {
        this.name = name;
        this.description = description;
        this.eventDuration = eventDuration;
        this.choices = choices;
        this.consequences = consequences;
        this.onEventStartConsequence = onEventStartConsequence;
        this.eventChoiceMoneyCost = eventChoiceMoneyCost;
        this.eventCooldown = eventCooldown;
        this.isUnique = isUnique;

        isActive = false;
        isIdle = false;
        this.region = region; //temporary fix
        pickedChoiceNumber = 0;
        pickedChoiceStartYear = 0;
        pickedChoiceStartMonth = 0;
        lastCompleted = 0;
        idleTurnsLeft = 0;

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

    public void StartEvent(Region region, int currentYear, int currentMonth)
    {
        this.region = region;
        region.ImplementStatisticValues(onEventStartConsequence, true);
        region.ImplementStatisticValues(onEventStartTemporaryConsequence, true);
        onEventStartYear = currentYear;
        onEventStartMonth = currentMonth;

        isIdle = true;
        idleTurnsLeft = eventIdleDuration;
    }

    public void SubtractIdleTurnsLeft()
    {
        idleTurnsLeft--;
    }

    public void EndTemporaryOnEventStartConsequences(RegionStatistics consequence)
    {
        region.ImplementStatisticValues(consequence, false);
    }

    //andere oplossing vinden voor temporary consequenties bijhouden!
    public void EndTemporaryConsequences(RegionStatistics consequence)
    {
        region.ImplementStatisticValues(consequence, false);

        pickedChoiceStartYear = 0;
        pickedChoiceStartMonth = 0;
        onEventStartYear = 0;
        onEventStartMonth = 0;
        pickedChoiceNumber = 0;
        isActive = false;
    }

    public void CompleteEvent()
    {
        Debug.Log("PickedChoiceNumber: " + pickedChoiceNumber);
        region.ImplementStatisticValues(consequences[pickedChoiceNumber], true);
        region.ImplementStatisticValues(duringEventProgressConsequences[pickedChoiceNumber], false);
        region.ImplementStatisticValues(temporaryConsequences[pickedChoiceNumber], true);

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

            region.ImplementStatisticValues(duringEventProgressConsequences[pickedChoiceNumber], true);

            if (eventDuration[i] == 0)
            {
                Debug.Log("In de SetPickedChoice method: " + i);
                CompleteEvent();
            }
        }

        else
        {
            //not enough money popup message?
        }
    }
}

