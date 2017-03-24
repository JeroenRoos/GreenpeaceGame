﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameEvent
{
    public string description { get; private set; }
    public string[] choices { get; private set; }
    public RegionStatistics[] consequences { get; private set; }
    public RegionStatistics pickedChoice { get; private set; }
    public int pickedChoiceNumber { get; private set; }
    public List<GameEvent> previousEvents { get; private set; }
    public int eventDuration { get; private set; } //in months
    public int startYear { get; private set; }
    public int startMonth { get; private set; }

    public bool isActive { get; private set; }

    public Region region { get; private set; }

    public GameEvent(string description, int eventDuration, string[] choices, RegionStatistics[] consequences)
    {
        this.description = description;
        this.eventDuration = eventDuration;
        this.choices = choices;
        this.consequences = consequences;
    }

    public void ActivateEvent(int startYear, int startMonth, Region region)
    {
        //Console.Clear();
        this.startYear = startYear;
        this.startMonth = startMonth;
        this.region = region;

        isActive = true;
        //DisplayEvent();
    }

    public void CompleteEvent()
    {
        region.ImplementStatisticValues(pickedChoice, true);
        isActive = false;
    }

    /*
    public void DisplayEvent()
    {
        Console.WriteLine("Region: {0}", region.name);
        Console.WriteLine(description);
        Console.WriteLine();
        Console.WriteLine("What will you do?");
        int i = 0;
        foreach (string choice in choices)
        {
            Console.WriteLine("{0}: {1}", i, choice);
            i++;
        }
    }
        */
        
    public void SetPickedChoice(int i) //string = Choice (class)
    {
        this.pickedChoice = consequences[i];
        this.pickedChoiceNumber = i;
    }
}
