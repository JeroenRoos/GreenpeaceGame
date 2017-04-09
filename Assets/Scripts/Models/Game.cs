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

    //All Regions and region types planned for complete game
    public int currentYear { get; private set; }
    public int currentMonth { get; private set; }
    //public Dictionary<string, Region> regions { get; private set; }

    //0,1,2,3: Noord,Oost,West,Zuid
    public List<Region> regions { get; private set; }
    public List<GameEvent> events { get; private set; }
    public System.Random rnd { get; private set; }
    public int language { get; private set; } //0 = Dutch, 1 = English
    public List<RegionAction> actions { get; private set; }

    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy

    public Game()
    {
        language = 0;
        rnd = new System.Random();
        events = new List<GameEvent>();
        regions = new List<Region>();
        actions = new List<RegionAction>();

        gameStatistics = new GameStatistics(20000, 17000000, new Energy());
        
        gameStatistics.UpdateRegionalAvgs(this);

        currentYear = 1;
        currentMonth = 1;
        
        LoadRegions();
        LoadRegionActions();
        LoadGameEvents();
        gameStatistics.UpdateRegionalAvgs(this);

        /*foreach (Region region in regions)
        {
            foreach (RegionSector sector in region.sectors)
            {
                sector.statistics.pollution.CalculateAvgPollution();
            }
            region.statistics.UpdateSectorAvgs(region);
        }

        SaveRegions();
        SaveRegionActions();
        SaveGameEvents();*/
    }

    public void SaveRegions()
    {
        List<Region> templist = new List<Region>();
        foreach (Region region in regions)
            templist.Add(region);

        RegionContainer regionContainer = new RegionContainer(templist);
        regionContainer.Save();
    }

    public void LoadRegions()
    {
        RegionContainer regionContainer = RegionContainer.Load();
        regions = regionContainer.regions;
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
    }

    public void SaveRegionActions()
    {
        RegionActionContainer regionActionContainer = new RegionActionContainer(regions[0].actions);
        regionActionContainer.Save();
    }

    public void LoadRegionActions()
    {
        foreach (Region region in regions)
        {
            RegionActionContainer regionActionContainer = RegionActionContainer.Load();
            region.LoadActions(regionActionContainer.actions);
        }
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
        
        ExecuteNewMonthMethods();

        if (isNewYear)
            ExecuteNewYearMethods();
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
        CompletefinishedActions();
        UpdateRegionEvents();
        MutateMonthlyStatistics();
    }

    public void ExecuteNewYearMethods() { }

    public void MutateMonthlyStatistics()
    {
        double monthlyIncome = GetMonthlyIncome();
        gameStatistics.ModifyMoney(monthlyIncome, true);

        double monthlyPopulation = GetMonthlyPopulation();
        gameStatistics.ModifyPopulation(monthlyPopulation);

        foreach (Region region in regions)
        {
            foreach (RegionSector sector in region.sectors)
            {
                sector.statistics.mutateTimeBasedStatistics();
            }
            region.statistics.UpdateSectorAvgs(region);
        }
    }

    public double GetMonthlyIncome()
    {
        double income = 0;

        foreach (Region region in regions)
        {
            foreach (RegionSector sector in region.sectors)
            {
                income += region.statistics.income;
            }
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
        foreach (Region region in regions)
        {
            foreach (RegionAction action in region.actions)
            {
                if (action.isActive && ((action.startMonth + action.actionDuration + action.startYear * 12) == (currentMonth + currentYear * 12)))
                {
                    region.ImplementActionConsequences(action, action.duringActionConsequences, false);
                    region.ImplementActionConsequences(action, action.consequences, true);
                    region.ImplementActionConsequences(action, action.temporaryConsequences, true);
                    gameStatistics.ModifyMoney(action.actionMoneyReward, true);
                    action.CompleteAction();
                }

                if (action.endTemporaryConsequencesMonth == currentYear * 12 + currentMonth)
                    region.ImplementActionConsequences(action, action.temporaryConsequences, false);
            }
        }
    }

    public void UpdateRegionEvents()
    {
        foreach (Region region in regions)
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

    public int PossibleEventCount()
    {
        int possibleEventCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive || !gameEvent.isIdle)
                possibleEventCount++;
        }

        return possibleEventCount;
    }

    public GameEvent GetPickedEvent(Region region)
    {
        List<GameEvent> possibleEvents = new List<GameEvent>();
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive || !gameEvent.isIdle)
            {
                foreach (string possibleRegion in gameEvent.possibleRegions)
                {
                    if (possibleRegion == region.name[0])
                    {
                        possibleEvents.Add(gameEvent);
                        break;
                    }
                }
            }
        }

        return possibleEvents[rnd.Next(0, possibleEvents.Count)];
    }

    public int GetPossibleRegionsCount()
    {
        int possibleRegionsCount = 0;

        foreach (Region region in regions)
        {
            bool isPossible = true;
            foreach (GameEvent gameEvent in region.inProgressGameEvents)
            {
                if (gameEvent.isActive || gameEvent.isIdle)
                {
                    isPossible = false;
                    break;
                }
            }
            if (isPossible)
                possibleRegionsCount++;
        }
        return possibleRegionsCount;
    }

    public Region PickEventRegion()
    {
        List<Region> possibleRegions = new List<Region>();
        foreach (Region region in regions)
        {
            bool isPossible = true;
            foreach (GameEvent gameEvent in region.inProgressGameEvents)
            {
                if (gameEvent.isActive || gameEvent.isIdle)
                {
                    isPossible = false;
                    break;
                }
            }
            if (isPossible)
                possibleRegions.Add(region);
        }

        int value = rnd.Next(0, possibleRegions.Count);

        return possibleRegions[value];
    }
}