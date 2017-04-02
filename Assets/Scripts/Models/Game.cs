using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;

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
        GenerateGameEvents();
        gameStatistics.UpdateRegionalAvgs(this);

        currentYear = 1;
        currentMonth = 1;

        //DisplayRegion(regions[0]);
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

        int eventChance = 35;
        int eventChanceReduction = 10;

        while (rnd.Next(1, 101) <= eventChance && activeCount < events.Count)
        {
            StartNewEvent();
            EventManager.CallShowEvent();

            eventChance -= eventChanceReduction;
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

    public void CheckIdleEvents()
    {
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isIdle)
                gameEvent.SubtractIdleTurnsLeft();
            if (gameEvent.idleTurnsLeft == 0)
                gameEvent.SetPickedChoice(0, this);
        }
    }

    public void CompleteFinishedEvents()
    {
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isActive &&
                ((gameEvent.startMonth + gameEvent.eventDuration[(int)gameEvent.pickedChoiceNumber] + gameEvent.startYear * 12) == (currentMonth + currentYear * 12)))
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
        List<GameEvent> possibleEvents = GetPossibleEvents();

        if (possibleEvents.Count > 0)
        {
            int pickedEvent = PickEvent(possibleEvents.Count);
            string pickedRegion = PickEventRegion();
            //events[pickedEvent].ActivateEvent(currentYear, currentMonth, regions[pickedRegion]);
            events[pickedEvent].StartEvent(regions[pickedRegion]);
            GameObject eventInstance = GameController.Instantiate(eventObject);
            eventInstance.GetComponent<EventObjectController>().Init(gameController, events[pickedEvent]);
        }
    }

    public List<GameEvent> GetPossibleEvents()
    {
        List<GameEvent> possibleEvents = new List<GameEvent>();
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive)
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
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(10, 40, 30, 5, 20, 10);
        RegionStatistics regionStatistics = new RegionStatistics(250, 1000, 5, pollution, 10, 30);

        string[] name = { "Noord Nederland", "The Netherlands North" };
        Region noord_Nederland = new Region(name, regionStatistics, sectors);

        regions.Add("NoordNederland", noord_Nederland);
    }

    private void GenerateOostNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(30, 20, 30, 10, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(500, 500, 5, pollution, 5, 50);

        string[] name = { "Oost Nederland", "The Netherlands East" };
        Region oost_Nederland = new Region(name, regionStatistics, sectors);
        regions.Add("OostNederland", oost_Nederland);
    }

    private void GenerateZuidNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(50, 10, 10, 20, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(700, 0, 5, pollution, 0, 60);

        string[] name = { "Zuid Nederland", "The Netherlands South" };
        Region zuid_Nederland = new Region(name, regionStatistics, sectors);

        regions.Add("ZuidNederland", zuid_Nederland);
    }

    private void GenerateWestNederland()
    {
        SectorStatistics householdStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics companyStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        SectorStatistics agricultureStatistics = new SectorStatistics(0, 0, 0, 0, 0, 0);
        Dictionary<string, RegionSector> sectors = GenerateRegionSectors(householdStatistics, companyStatistics, agricultureStatistics);

        Pollution pollution = new Pollution(40, 20, 20, 15, 10, 10);
        RegionStatistics regionStatistics = new RegionStatistics(1000, 1000, 5, pollution, 10, 70);

        string[] name = { "West Nederland", "The Netherlands West" };
        Region west_Nederland = new Region(name, regionStatistics, sectors);
        regions.Add("WestNederland", west_Nederland);
    }

    private Dictionary<string, RegionSector> GenerateRegionSectors(SectorStatistics householdStatistics, SectorStatistics companyStatistics,
                                                                   SectorStatistics agricultureStatistics)
    {
        Dictionary<string, RegionSector> sectors = new Dictionary<string, RegionSector>();

        string[] nameHouseholds = { "Huishoudens", "Households" };
        string[] nameCompanies = { "Bedrijven", "Companies" };
        string[] nameAgriculture = { "Landbouw", "Agriculture" };
        sectors.Add("Huishoudens", new RegionSector(nameHouseholds, householdStatistics));
        sectors.Add("Bedrijven", new RegionSector(nameCompanies, companyStatistics));
        sectors.Add("Landbouw", new RegionSector(nameAgriculture, agricultureStatistics));

        return sectors;
    }









    //Generates the game events (currently hardcoded)
    private void GenerateGameEvents()
    {
        //Event 01 AirPollutionConcern
        string name01 = "AirPollutionConcern";
        string[,] choices01 = new string[2, 3]
        {
            {"niets doen", "Campagne voeren over luchtvervuiling", "campagne voeren voor organisatie" },
            {"Do nothing", "Campaign over air pollution", "Campaign for organisation" }
        };

        int[] eventDuration01 = new int[3]
        {
            0, 2, 2
        };

        string[] description01 = { "Uit een onderzoek is de organisatie erachter gekomen dat veel mensen zich zorgen maker over de luchtvervuiling.",
            "A research from the organisation concludes that people are worried about the air pollution." };
        RegionStatistics[] consequences01 = new RegionStatistics[3];
        consequences01[0] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences01[1] = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, -2, 0, 0), 1, 0);
        consequences01[2] = new RegionStatistics(0, 250, 0, new Pollution(0, 0, 0, 0, 0, 0), 1, 0);
        
        RegionStatistics onStartConsequences01 = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost01 = { 0, 2000, 2000 };
        int eventCooldown01 = 3;

        GameEvent gameEvent01 = new GameEvent(name01, description01, eventDuration01, choices01, consequences01, onStartConsequences01, choiceMoneyCost01, eventCooldown01, false);
        events.Add(gameEvent01);


        //Event 02 Earthquake
        string name02 = "Earthquake";
        string[,] choices02 = new string[2, 3]
        {
            {"Niets doen", "Inwoners vergoeden", "Toekomstige aardbevingen tegengaan (kansverlaging)" },
            {"Do nothing", "Refund citizens", "Prevent future earthquakes (chance reduction)" }
        };

        int[] eventDuration02 = new int[3]
        {
            0, 0, 5
        };

        string[] description02 = { "Er is een aardbeving geweest dat schade heeft veroorzaakt.",
            "There has been an earthquake that caused damage." };
        RegionStatistics[] consequences02 = new RegionStatistics[3];
        consequences02[0] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences02[1] = new RegionStatistics(0, 0, 2, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences02[2] = new RegionStatistics(-200, 0, 4, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        
        RegionStatistics onStartConsequences02 = new RegionStatistics(0, 0, -2, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost02 = { 0, 1000, 4000 };
        int eventCooldown02 = 9;

        GameEvent gameEvent02 = new GameEvent(name02, description02, eventDuration02, choices02, consequences02, onStartConsequences02, choiceMoneyCost02, eventCooldown02, false);
        events.Add(gameEvent02);


        //Event 03 Flood
        string name03 = "Flood";
        string[,] choices03 = new string[2, 3]
        {
            {"Niets doen", "Inwoners vergoeden", "Toekomstige overstromingen tegengaan (kansverlaging)" },
            {"Do nothing", "Refund citizens", "Prevent future floods (chance reduction)" }
        };

        int[] eventDuration03 = new int[3]
        {
            0, 0, 5
        };

        string[] description03 = { "Er is een overstroming geweest dat schade heeft veroorzaakt.",
            "There has been a flood that caused damage." };
        RegionStatistics[] consequences03 = new RegionStatistics[3];
        consequences03[0] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences03[1] = new RegionStatistics(0, 0, 2, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences03[2] = new RegionStatistics(-200, 0, 4, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        RegionStatistics onStartConsequences03 = new RegionStatistics(0, 0, -2, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost03 = { 0, 1000, 4000 };
        int eventCooldown03 = 12;

        GameEvent gameEvent03 = new GameEvent(name03, description03, eventDuration03, choices03, consequences03, onStartConsequences03, choiceMoneyCost03, eventCooldown03, false);
        events.Add(gameEvent03);


        //Event 04 ForestFire
        string name04 = "ForestFire";
        string[,] choices04 = new string[2, 3]
        {
            {"Niets doen", "Bomen herplanten", "Toekomstige bosbranden tegengaan (kansverlaging)" },
            {"Do nothing", "Replant trees", "Prevent future forest fires (chance reduction)" }
        };

        int[] eventDuration04 = new int[3]
        {
            0, 2, 5
        };

        string[] description04 = { "Er is een bosbrand geweest dat schade heeft veroorzaakt.",
            "There has been a forest fire that caused damage." };
        RegionStatistics[] consequences04 = new RegionStatistics[3];
        consequences04[0] = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 2, 0), 0, 0);
        consequences04[1] = new RegionStatistics(0, 0, 1, new Pollution(0, 0, 0, -1, -1, 0), 0, 0);
        consequences04[2] = new RegionStatistics(-200, 0, 4, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        RegionStatistics onStartConsequences04 = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost04 = { 0, 1000, 4000 };
        int eventCooldown04 = 9;

        GameEvent gameEvent04 = new GameEvent(name04, description04, eventDuration04, choices04, consequences04, onStartConsequences04, choiceMoneyCost04, eventCooldown04, false);
        events.Add(gameEvent04);


        //Event 05 NaturePollutionConcern
        string name05 = "NaturePollutionConcern";
        string[,] choices05 = new string[2, 3]
        {
            {"niets doen", "Campagne voeren over natuurvervuiling", "campagne voeren voor organisatie" },
            {"Do nothing", "Campaign over nature pollution", "Campaign for organisation" }
        };

        int[] eventDuration05 = new int[3]
        {
            0, 2, 2
        };

        string[] description05 = { "Uit een onderzoek is de organisatie erachter gekomen dat veel mensen zich zorgen maker over de natuurvervuiling.",
            "A research from the organisation concludes that people are worried about the nature pollution." };
        RegionStatistics[] consequences05 = new RegionStatistics[3];
        consequences05[0] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences05[1] = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, -2, 0), 1, 0);
        consequences05[2] = new RegionStatistics(0, 250, 0, new Pollution(0, 0, 0, 0, 0, 0), 1, 0);

        RegionStatistics onStartConsequences05 = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost05 = { 0, 2000, 2000 };
        int eventCooldown05 = 3;

        GameEvent gameEvent05 = new GameEvent(name05, description05, eventDuration05, choices05, consequences05, onStartConsequences05, choiceMoneyCost05, eventCooldown05, false);
        events.Add(gameEvent05);


        //Event 06 WaterPollutionConcern
        string name06 = "WaterPollutionConcern";
        string[,] choices06 = new string[2, 3]
        {
            {"niets doen", "Campagne voeren over watervervuiling", "campagne voeren voor organisatie" },
            {"Do nothing", "Campaign over water pollution", "Campaign for organisation" }
        };

        int[] eventDuration06 = new int[3]
        {
            0, 2, 2
        };

        string[] description06 = { "Uit een onderzoek is de organisatie erachter gekomen dat veel mensen zich zorgen maker over de watervervuiling.",
            "A research from the organisation concludes that people are worried about the water pollution." };
        RegionStatistics[] consequences06 = new RegionStatistics[3];
        consequences06[0] = new RegionStatistics(0, 0, -1, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        consequences06[1] = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, -2), 1, 0);
        consequences06[2] = new RegionStatistics(0, 250, 0, new Pollution(0, 0, 0, 0, 0, 0), 1, 0);

        RegionStatistics onStartConsequences06 = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);

        double[] choiceMoneyCost06 = { 0, 2000, 2000 };
        int eventCooldown06 = 3;

        GameEvent gameEvent06 = new GameEvent(name06, description06, eventDuration06, choices06, consequences06, onStartConsequences06, choiceMoneyCost06, eventCooldown06, false);
        events.Add(gameEvent06);
    }
}

