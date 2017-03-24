using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

public class Game
{
    //All Regions and region types planned for complete game
    public int currentYear { get; private set; }
    public int currentMonth { get; private set; }
    public Dictionary<string, Region> regions;
    public List<GameEvent> events { get; private set; }
    public System.Random rnd { get; private set; }

    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy

    //public events declareren (unity)


    public Game()
    {
        rnd = new System.Random();
        events = new List<GameEvent>();

        gameStatistics = new GameStatistics(20000, 17000000, new Energy());

        GenerateRegions();
        GenerateGameEvents();


        currentYear = 1;
        currentMonth = 1;

        //DisplayRegion(regions[0]);
    }

    public bool UpdateTime()
    {
        currentMonth++;

        if (currentMonth > 12)
        {
            currentMonth = currentMonth - 12;
            currentYear++;
            ExecuteNewYearMethods();
        }

        bool isNewEvent = ExecuteNewMonthMethods();
        EventManager.CallChangeMonth();
        return isNewEvent;
    }

    private bool ExecuteNewMonthMethods()
    {
        CompletefinishedActions();
        CompleteFinishedEvents();

        int activeCount = getActiveEventCount();

        if (rnd.Next(1, 61) <= 20 && activeCount < 3)
        {
            EventManager.CallShowEvent();
            return true;
        }

        else
        {
            /*Console.Clear();
            DisplayRegion(regions[0]);*/
            return false;
        }
    }

    public void CompletefinishedActions()
    {
        foreach (Region region in regions.Values)
        {
            foreach (RegionAction action in region.actions)
            {
                if (action.isActive &&
                    ((action.startMonth + action.actionDuration + action.startYear * 12) == (currentMonth + currentYear * 12)))
                {
                    region.ImplementStatisticValues(action.consequences, true);
                    action.CompleteAction();
                }
            }
        }
    }

    public void CompleteFinishedEvents()
    {
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isActive &&
                ((gameEvent.startMonth + gameEvent.eventDuration + gameEvent.startYear * 12) == (currentMonth + currentYear * 12)))
            {
                gameEvent.CompleteEvent();
            }
        }
    }

    public int getActiveEventCount()
    {
        int activeCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isActive)
                activeCount++;
        }
        return activeCount;
    }

    public void StartNewEvent()
    {
        // select event randomly from possible eventList
        // testcode
        if(events[0].isActive == false)
        {
            events[0].ActivateEvent(currentYear, currentMonth, regions["Noord Nederland"]);
        }
    }

    private void ExecuteNewYearMethods()
    {
        foreach (KeyValuePair<string, Region> region in regions)
        {
            region.Value.statistics.mutateTimeBasedStatistics();
        }

    }

    private void GenerateRegions()
    {
        regions = new Dictionary<string, Region>();

        Pollution pollution = new Pollution(10, 10, 10, 5, 5, 5);
        RegionStatistics statistics = new RegionStatistics(10000, 1000, 10, pollution, 5, 70);
        //SectorStatistics stats = new SectorStatistics();

        Region noord_Nederland = new Region("Noord Nederland", statistics);
        Region oost_Nederland = new Region("Oost Nederland", statistics);
        Region zuid_Nederland = new Region("Zuid Nederland", statistics);
        Region west_Nederland = new Region("West Nederland", statistics);

        Building building = new Building("Coal factory");
        noord_Nederland.CreateBuilding(building);

        regions.Add("Noord Nederland", noord_Nederland);
        regions.Add("Oost Nederland", oost_Nederland);
        regions.Add("Zuid Nederland", zuid_Nederland);
        regions.Add("West Nederland", west_Nederland);
    }

    /*
    public void DisplayRegion(Region currentRegion)
    {
        if (currentRegion.name == region.name)
        {
            string textDistance = "{0,-15}";
            Console.Write(textDistance, "Year/Month:");
            Console.WriteLine("{0}/{1}", currentYear, currentMonth);

            region.DisplayRegionValues(textDistance);
            break;
        }
    }

    Console.WriteLine("Active events:");
    foreach (GameEvent gameEvent in events)
    {
        if (gameEvent.isActive && gameEvent.region.name == currentRegion.name)
        {
            Console.WriteLine("{0} ({1})", gameEvent.description, gameEvent.pickedChoiceNumber);
        }

    }
     */

    private void GenerateGameEvents()
    {
        string[] choices = { "Do stuff", "Negotiation", "Do nothing" };
        RegionStatistics[] consequences1 = new RegionStatistics[choices.Length];
        RegionStatistics[] consequences2 = new RegionStatistics[choices.Length];
        RegionStatistics[] consequences3 = new RegionStatistics[choices.Length];

        string description;

        description = "dummy event 1";
        consequences1[0] = new RegionStatistics(-1000, 0, -1, new Pollution(0, 0, 0, -1, -1, -1), 0, -1);
        consequences1[1] = new RegionStatistics(0, 0, 2, new Pollution(0, 0, 0, -2, -1, 0), 0, 0);
        consequences1[2] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 1, 1, 1), 0, 0);
        //consequences1[3] = new RegionStatistics(-3000, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0); //income reduction over duration of research
        GameEvent gameEvent1 = new GameEvent(description, 2, choices, consequences1);
        events.Add(gameEvent1);

        description = "dummy event 2";
        consequences2[0] = new RegionStatistics(-2000, 250, 0, new Pollution(0, 0, 0, 0, 0, 0), 2, 0);
        consequences2[1] = new RegionStatistics(-1000, 125, 0, new Pollution(0, 0, 0, 0, 0, 0), 1, 0);
        consequences2[2] = new RegionStatistics(0, -250, -1, new Pollution(0, 0, 0, 0, 0, 0), -2, 0);
        //consequences2[3] = new RegionStatistics(-2500, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0); //income reduction over duration of research
        GameEvent gameEvent2 = new GameEvent(description, 1, choices, consequences2);
        events.Add(gameEvent2);

        description = "dummy event 3";
        consequences3[0] = new RegionStatistics(3000, 0, 2, new Pollution(0, 0, 0, -2, 0, 0), -1, 1);
        consequences3[1] = new RegionStatistics(1500, 0, 1, new Pollution(0, 0, 0, -1, 0, -1), 0, 1);
        consequences3[2] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, -2);
        //consequences3[3] = new RegionStatistics(-1000, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0); //income reduction over duration of research
        GameEvent gameEvent3 = new GameEvent(description, 3, choices, consequences3);
        events.Add(gameEvent3);
    }
}

