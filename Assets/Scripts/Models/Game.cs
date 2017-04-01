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
    public Dictionary<string, Region> regions { get; private set; }
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
        gameStatistics.UpdateRegionalAvgs(this);

        currentYear = 1;
        currentMonth = 1;

        //DisplayRegion(regions[0]);
    }

    public void NextTurn()
    {
        bool isNewYear = UpdateCurrentMonthAndYear();

        //bool isNewEvent = ExecuteNewMonthMethods();
        ExecuteNewMonthMethods();
        EventManager.CallChangeMonth();
        //return isNewEvent;

        if (isNewYear)
            ExecuteNewYearMethods();
        
        gameStatistics.UpdateRegionalAvgs(this);
    }

    public bool UpdateCurrentMonthAndYear()
    {
        currentMonth++;
        if (currentMonth > 12)
        {
            currentMonth = currentMonth - 12;
            currentYear++;
            return true;
        }

        return false;
    }

    private void ExecuteNewMonthMethods()
    {
        double monthlyIncome = GetMonthlyIncome();
        gameStatistics.ModifyMoney(monthlyIncome);

        double monthlyPopulation = GetMonthlyPopulation();
        gameStatistics.ModifyPopulation(monthlyPopulation);

        CompletefinishedActions();
        CompleteFinishedEvents();

        int activeCount = getActiveEventCount();

        if (rnd.Next(1, 61) <= 20 && activeCount < 3)
        {
            StartNewEvent();
            EventManager.CallShowEvent();
        }
    }

    private void ExecuteNewYearMethods()
    {
        foreach (Region region in regions.Values)
        {
            region.statistics.mutateTimeBasedStatistics();
        }
    }

    public double GetMonthlyIncome()
    {
        double income = 0;

        foreach (Region region in regions.Values)
        {
            income += region.statistics.income;
        }

        return income;
    }

    public double GetMonthlyPopulation()
    {
        double population = gameStatistics.population * 0.005;
        return population;
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

    private void GenerateRegions()
    {
        regions = new Dictionary<string, Region>();
        GenerateNoordNederland();
        GenerateOostNederland();
        GenerateWestNederland();
        GenerateZuidNederland();
    }

    private void GenerateNoordNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(10, 40, 30, 5, 20, 10);
        RegionStatistics regionStatistics = new RegionStatistics(2500, 1000, 5, pollution, 10, 30);

        Region noord_Nederland = new Region("Noord Nederland", regionStatistics, sectors);

        regions.Add("NoordNederland", noord_Nederland);
    }

    private void GenerateOostNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(30, 20, 30, 10, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(5000, 500, 5, pollution, 5, 50);

        Region oost_Nederland = new Region("Oost Nederland", regionStatistics, sectors);
        regions.Add("OostNederland", oost_Nederland);
    }

    private void GenerateZuidNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(50, 10, 10, 20, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(7000, 0, 5, pollution, 0, 60);

        Region zuid_Nederland = new Region("Zuid Nederland", regionStatistics, sectors);

        regions.Add("ZuidNederland", zuid_Nederland);
    }

    private void GenerateWestNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(40, 20, 20, 15, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(10000, 1000, 5, pollution, 10, 70);

        Region west_Nederland = new Region("West Nederland", regionStatistics, sectors);
        regions.Add("WestNederland", west_Nederland);
    }

    private Dictionary<string, RegionSector> GenerateRegionSectors(SectorStatistics householdStatistics, SectorStatistics companyStatistics,
                                                                   SectorStatistics agricultureStatistics)
    {
        Dictionary<string, RegionSector> sectors = new Dictionary<string, RegionSector>();

        sectors.Add("Huishoudens", new RegionSector("Huishoudens", householdStatistics));
        sectors.Add("Bedrijven", new RegionSector("Bedrijven", companyStatistics));
        sectors.Add("Landbouw", new RegionSector("Landbouw", agricultureStatistics));

        return sectors;
    }

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

