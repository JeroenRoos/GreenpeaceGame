using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Game
{
    //All Regions and region types planned for complete game
    private string[] regionNames = { "Noord Nederland", "West Nederland", "Oost Nederland", "Zuid Nederland"};
    //public Dictionary<string, Region> regions;

    public int currentYear { get; private set; }
    public int currentMonth { get; private set; }
    public List<Region> regions { get; private set; }
    public List<GameEvent> events { get; private set; }
    public Random rnd { get; private set; }
        
    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy

    //public events declareren (unity)


    public Game()
    {
        rnd = new Random();
        events = new List<GameEvent>();

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
        return isNewEvent;
    }

    private bool ExecuteNewMonthMethods()
    {
        int activeCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isActive)
            {
                if ((gameEvent.startMonth + gameEvent.eventDuration + gameEvent.startYear * 12) == (currentMonth + currentYear * 12))
                {
                    gameEvent.CompleteEvent();
                }

                else
                {
                    activeCount++;
                }
            }
        }

        if (rnd.Next(1, 61) <= 20 && activeCount < 3)
            return true;

        else
        {
            /*Console.Clear();
            DisplayRegion(regions[0]);*/
            return false;
        }

    }

    public void StartNewEvent()
    {
        bool eventFound = false;
        while (!eventFound)
        {
            int i = rnd.Next(0, events.Count());

            if (!events[i].isActive)
            {
                eventFound = true;
                events[i].ActivateEvent(currentYear, currentMonth, regions[0]);
                events[i].SetPickedChoice(rnd.Next(0, events[i].choices.Length));
            }
        }

        //DisplayRegion(regions[0]);
    }

    private void ExecuteNewYearMethods()
    {
        foreach (Region region in regions)
        {
            region.statistics.mutateTimeBasedStatistics();
        }
        /*Console.Clear();
        DisplayRegion(regions[0]);*/
    }

    private void GenerateRegions()
    {
        regions = new List<Region>();
        GenerateNoordNederland();
    }

    private void GenerateNoordNederland()
    {
        Pollution pollution = new Pollution(10, 10, 10, 5, 5, 5);
        RegionStatistics statistics = new RegionStatistics(10000, 1000, 10, pollution, 5, 70);

        Region noord_Nederland = new Region(regionNames[0], statistics);

        Building building = new Building("Coal factory");
        noord_Nederland.CreateBuilding(building);

        regions.Add(noord_Nederland);
    }

    /*
    public void DisplayRegion(Region currentRegion)
    {
        Console.Clear();
        foreach (Region region in regions)
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
