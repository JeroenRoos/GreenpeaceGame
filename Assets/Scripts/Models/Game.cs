using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;
using UnityEditor;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;

public class Game
{
    public GameController gameController;

    //All Regions and region types planned for complete game
    public int currentYear { get; private set; }
    public int currentMonth { get; private set; }
    public Dictionary<string, Region> regions { get; private set; }
    public List<GameEvent> events { get; private set; }
    public System.Random rnd { get; private set; }
    public int language { get; private set; } //0 = Dutch, 1 = English

    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy

    //public events declareren (unity)
    public GameObject eventObject;

    public Game()
    {
        language = 0;
        rnd = new System.Random();
        events = new List<GameEvent>();

        gameStatistics = new GameStatistics(20000, 17000000, new Energy());

        GenerateRegions();
        //GenerateGameEvents();
        gameStatistics.UpdateRegionalAvgs(this);

        currentYear = 1;
        currentMonth = 1;

        //SaveGameEvents();
        LoadGameEvents();
    }
    
    public void SaveGameEvents()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(GameEvent));
            foreach (GameEvent gameEvent in events)
            {
                Debug.Log("Seraliazing " + gameEvent.name);
                var path = Application.dataPath + "/GameFiles/GameEvents/" + gameEvent.name + ".xml";
                FileStream file = File.Create(path);
                writer.Serialize(file, gameEvent);
                file.Close();
            }
            Debug.Log("Serialization finished");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    public void LoadGameEvents()
    {
        try
        {
            var folderPath = Application.dataPath + "/GameFiles/GameEvents/";
            XmlSerializer reader = new XmlSerializer(typeof(GameEvent));

            foreach (string file in Directory.GetFileSystemEntries(folderPath, "*.xml"))
            {
                var stream = File.OpenRead(file);
                GameEvent gameEvent = (GameEvent)reader.Deserialize(stream);
                events.Add(gameEvent);

                Debug.Log(gameEvent.name + " loaded");
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }


    public void Init(GameObject eventObject, GameController gameController)
    {
        this.eventObject = eventObject;
        this.gameController = gameController;
    }

    public void ChangeLanguage(string language)
    {
        if (language == "english")
            this.language = 1;
        else if (language == "dutch")
            this.language = 0;
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
        CheckIdleEvents();
        CompleteFinishedEvents();

        int activeCount = getActiveEventCount();

        int eventChance = 100;
        int eventChanceReduction = 100;

        //voor demo vertical slice 1 active event max
        /*while (activeCount < events.Count && rnd.Next(1, 101) <= eventChance)
        {
            StartNewEvent();
            EventManager.CallShowEvent();

            eventChance -= eventChanceReduction;
        }*/
        //voor vertical slice
        if (activeCount < 1)
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
        double population = gameStatistics.population * 0.002;
        return population;
    }

    public void CompletefinishedActions()
    {
        foreach (Region region in regions.Values)
        {
            foreach (RegionAction action in region.actions)
            {
               // Debug.Log("Complete finished action FOR LOOP");
                if (action.isActive && ((action.startMonth + action.actionDuration + action.startYear * 12) == (currentMonth + currentYear * 12)))
                {
                    region.ImplementStatisticValues(action.consequences, true);
                    action.CompleteAction();
                    Debug.Log("Complete finished action IF STATEMENT");
                }
            }
        }
    }

    public void CheckIdleEvents()
    {
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isIdle)
            {
                gameEvent.SubtractIdleTurnsLeft();
                if (gameEvent.idleTurnsLeft == 0)
                    gameEvent.SetPickedChoice(0, this);
            }
        }
    }

    public void CompleteFinishedEvents()
    {
        foreach (GameEvent gameEvent in events)
        {

            if (gameEvent.isActive &&
                ((gameEvent.pickedChoiceStartMonth + gameEvent.eventDuration[gameEvent.pickedChoiceNumber] + gameEvent.pickedChoiceStartYear * 12) == (currentMonth + currentYear * 12)))
            {
                gameEvent.CompleteEvent();

            }

            EndTemporaryEventConsequences(gameEvent);
        }
    }

    //niet perfect (in Event class schrijven?)
    public void EndTemporaryEventConsequences(GameEvent gameEvent)
    {
        if (gameEvent.onEventStartYear == currentYear & gameEvent.onEventStartMonth == currentMonth)
            gameEvent.EndTemporaryOnEventStartConsequences(gameEvent.onEventStartConsequence);
        if (gameEvent.lastCompleted + gameEvent.temporaryConsequencesDuration[gameEvent.pickedChoiceNumber] == currentMonth + currentYear * 12)
            gameEvent.EndTemporaryConsequences(gameEvent.temporaryConsequences[gameEvent.pickedChoiceNumber]);
    }

    public int getActiveEventCount()
    {
        int activeCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isActive || gameEvent.isIdle)
                activeCount++;
        }
        return activeCount;
    }
    
    public void StartNewEvent()
    {
        List<GameEvent> possibleEvents = GetPossibleEvents();

        if (possibleEvents.Count > 0)
        {
            int pickedEvent = PickEvent(possibleEvents.Count);
            string pickedRegion = PickEventRegion();
            //events[pickedEvent].ActivateEvent(currentYear, currentMonth, regions[pickedRegion]);
            events[pickedEvent].StartEvent(regions[pickedRegion], currentYear, currentMonth);
            GameObject eventInstance = GameController.Instantiate(eventObject);
            eventInstance.GetComponent<EventObjectController>().Init(gameController, events[pickedEvent]);
        }
    }

    public List<GameEvent> GetPossibleEvents()
    {
        List<GameEvent> possibleEvents = new List<GameEvent>();
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive || !gameEvent.isIdle)
                possibleEvents.Add(gameEvent);
        }

        return possibleEvents;
    }

    public int PickEvent(int availableEventsCount)
    {
        int pickedEvent = rnd.Next(0, availableEventsCount);
        return pickedEvent;
    }

    public string PickEventRegion()
    {
        int x = rnd.Next(0, regions.Keys.Count);
        if (x == 0)
            return "NoordNederland";
        else if (x == 1)
            return "OostNederland";
        else if (x == 2)
            return "ZuidNederland";
        else
            return "WestNederland";
    }

    //Generates the 4 regions (currently hardcoded)
    private void GenerateRegions()
    {
        regions = new Dictionary<string, Region>();
        GenerateNoordNederland();
        GenerateOostNederland();
        GenerateZuidNederland();
        GenerateWestNederland();
    }

    private void GenerateNoordNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        RegionSector[] sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(10, 40, 30, 5, 20, 10);
        RegionStatistics regionStatistics = new RegionStatistics(250, 0, 5, pollution, 2, 2);

        string[] name = { "Noord Nederland", "The Netherlands North" };
        Region noord_Nederland = new Region(name, regionStatistics, sectors);

        regions.Add("NoordNederland", noord_Nederland);
    }

    private void GenerateOostNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        RegionSector[] sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(30, 20, 30, 10, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(500, 0, 5, pollution, 0, 3);

        string[] name = { "Oost Nederland", "The Netherlands East" };
        Region oost_Nederland = new Region(name, regionStatistics, sectors);
        regions.Add("OostNederland", oost_Nederland);
    }

    private void GenerateZuidNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        RegionSector[] sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(50, 10, 10, 20, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(700, 0, 5, pollution, 0, 4);

        string[] name = { "Zuid Nederland", "The Netherlands South" };
        Region zuid_Nederland = new Region(name, regionStatistics, sectors);

        regions.Add("ZuidNederland", zuid_Nederland);
    }

    private void GenerateWestNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        RegionSector[] sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(40, 20, 20, 15, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(1000, 0, 5, pollution, 3, 5);

        string[] name = { "West Nederland", "The Netherlands West" };
        Region west_Nederland = new Region(name, regionStatistics, sectors);
        regions.Add("WestNederland", west_Nederland);
    }

    private RegionSector[] GenerateRegionSectors(SectorStatistics householdStatistics, SectorStatistics companyStatistics,
                                                                   SectorStatistics agricultureStatistics)
    {
        RegionSector[] sectors = new RegionSector[3];

        string[] nameHouseholds = { "Huishoudens", "Households" };
        string[] nameCompanies = { "Bedrijven", "Companies" };
        string[] nameAgriculture = { "Landbouw", "Agriculture" };
        sectors[0] = new RegionSector(nameHouseholds, householdStatistics);
        sectors[1] = new RegionSector(nameCompanies, companyStatistics);
        sectors[2] = new RegionSector(nameAgriculture, agricultureStatistics);

        return sectors;
    }

    //Generates the game events (hardcoded)
    /*private void GenerateGameEvents()
    {
    }*/
}