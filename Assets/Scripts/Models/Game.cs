using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;
using UnityEditor;
using System.Xml.Serialization;
using System.IO;
//using System.Xml;
//using System.Xml.Linq;

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
    public List<RegionAction> actions { get; private set; }

    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy

    //public events declareren (unity)
    public GameObject eventObject;

    public Game()
    {
        language = 0;
        rnd = new System.Random();
        events = new List<GameEvent>();
        regions = new Dictionary<string, Region>();
        actions = new List<RegionAction>();

        gameStatistics = new GameStatistics(20000, 17000000, new Energy());
        
        gameStatistics.UpdateRegionalAvgs(this);

        currentYear = 1;
        currentMonth = 1;
        
        
        LoadRegions();
        LoadRegionActions();
        LoadGameEvents();
    }

    public void SaveRegions()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(Region));
            foreach (Region region in regions.Values)
            {
                Debug.Log("Serializing " + region.name[0]);
                var path = Application.dataPath + "/GameFiles/Regions/" + region.name[0] + ".xml";
                FileStream file = File.Create(path);
                writer.Serialize(file, region);
                file.Close();
            }
            Debug.Log("Serialization finished");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void LoadRegions()
    {
        try
        {
            var folderPath = Application.dataPath + "/GameFiles/Regions/";
            XmlSerializer reader = new XmlSerializer(typeof(Region));

            foreach (string file in Directory.GetFileSystemEntries(folderPath, "*.xml"))
            {
                var stream = File.OpenRead(file);
                Region region = (Region)reader.Deserialize(stream);
                regions.Add(region.name[0], region);

                Debug.Log(region.name[0] + " loaded");
            }
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void SaveGameEvents()
    {
        GameEventContainer eventContainer = new GameEventContainer(events);
        eventContainer.Save();
    }

    public void LoadGameEvents()
    {
        GameEventContainer eventContainer = GameEventContainer.Load();
        events = eventContainer.events;
        foreach (GameEvent gameEvent in events)
            Debug.Log( gameEvent.name + " event loaded");
    }

    public void SaveRegionActions()
    {
        try
        {
            XmlSerializer writer = new XmlSerializer(typeof(RegionAction));
            foreach (RegionAction action in regions["Noord Nederland"].actions)
            {
                Debug.Log("Serializing " + action.name[0]);
                var path = Application.dataPath + "/GameFiles/RegionActions/" + action.name[0] + ".xml";
                FileStream file = File.Create(path);
                writer.Serialize(file, action);
                file.Close();
                Debug.Log("Serialization update");
            }
            Debug.Log("Serialization finished");
        }

        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void LoadRegionActions()
    {
        try
        {
            var folderPath = Application.dataPath + "/GameFiles/RegionActions/";
            XmlSerializer reader = new XmlSerializer(typeof(RegionAction));

            foreach (string file in Directory.GetFileSystemEntries(folderPath, "*.xml"))
            {
                foreach (Region region in regions.Values)
                {
                    var stream = File.OpenRead(file);
                    RegionAction action = (RegionAction)reader.Deserialize(stream);
                    region.actions.Add(action);

                    Debug.Log(action + " loaded in " + region.name[0]);
                }
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
        UpdateRegionEvents();

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
        double population = gameStatistics.population * 0.0046 / 12;
        return population;
    }

    public void CompletefinishedActions()
    {
        foreach (Region region in regions.Values)
        {
            foreach (RegionAction action in region.actions)
            {
                if (action.isActive && ((action.startMonth + action.actionDuration + action.startYear * 12) == (currentMonth + currentYear * 12)))
                {
                    region.ImplementStatisticValues(action.consequences, true);
                    action.CompleteAction();
                }
            }
        }
    }

    public void CheckIdleEvents()
    {
        foreach (Region region in regions.Values)
        {
            foreach (GameEvent gameEvent in region.inProgressGameEvents)
            {
                if (gameEvent.isIdle)
                {
                    gameEvent.SubtractIdleTurnsLeft();
                    if (gameEvent.idleTurnsLeft == 0)
                    {
                        gameEvent.SetPickedChoice(0, this);
                        region.ImplementStatisticValues(gameEvent.duringEventProgressConsequences[gameEvent.pickedChoiceNumber], true);
                    }
                }
            }
        }
    }

    public void UpdateRegionEvents()
    {
        foreach (Region region in regions.Values)
        {
            region.UpdateEvents(this);
        }
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
            events[pickedEvent].StartEvent(currentYear, currentMonth);
            regions[pickedRegion].AddGameEvent(events[pickedEvent]);
            GameObject eventInstance = GameController.Instantiate(eventObject);
            eventInstance.GetComponent<EventObjectController>().Init(gameController, regions[pickedRegion], events[pickedEvent]);
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
            return "Noord Nederland";
        else if (x == 1)
            return "Oost Nederland";
        else if (x == 2)
            return "Zuid Nederland";
        else
            return "West Nederland";
    }
}