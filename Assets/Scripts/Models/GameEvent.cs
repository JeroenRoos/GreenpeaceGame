using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//UNFINISHED
public class GameEvent
{
    public string name { get; private set; } //id
    public string[] description { get; private set; }
    public bool isUnique { get; private set; }

    public int[] eventDuration { get; private set; } //in months
    public int eventCooldown { get; private set; } //in months

    public string[,] choices { get; private set; }
    public RegionStatistics[] consequences { get; private set; }
    public RegionStatistics onEventStartConsequence { get; private set; }
    public double[] eventChoiceMoneyCost { get; private set; }
    
    //started events variables
    public int pickedChoiceNumber { get; private set; }
    public int startYear { get; private set; }
    public int startMonth { get; private set; }
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
        pickedChoiceNumber = -1;
        startYear = -1;
        startMonth = -1;
        lastCompleted = -1;
        idleTurnsLeft = -1;
    }

    public void StartEvent(Region region)
    {
        this.region = region;
        region.ImplementStatisticValues(onEventStartConsequence, true);

        isIdle = true;
        idleTurnsLeft = 100;
    }

    public void SubtractIdleTurnsLeft()
    {
        idleTurnsLeft--;
    }

    public void CompleteEvent()
    {
        region.ImplementStatisticValues(consequences[pickedChoiceNumber], true);

        lastCompleted = startYear * 12 + startMonth + eventCooldown;
        startYear = -1;
        startMonth = -1;
        pickedChoiceNumber = -1;
        isActive = false;
    }

    public void SetPickedChoice(int i, Game game)
    {
        if (game.gameStatistics.money > eventChoiceMoneyCost[i])
        {
            game.gameStatistics.ModifyMoney(-eventChoiceMoneyCost[i]);

            pickedChoiceNumber = i;
            this.startYear = game.currentYear;
            this.startMonth = game.currentMonth;

            isIdle = false;
            idleTurnsLeft = -1;
            isActive = true;

            if (eventDuration[i] == 0)
                CompleteEvent();
        }

        else
        {
            //not enough money popup message?
        }
    }
}

