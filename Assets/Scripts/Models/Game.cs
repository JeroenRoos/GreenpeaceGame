using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
//using System.Xml;
//using System.Xml.Linq;

[Serializable]
public class Game
{
    //Game statistics
    public GameStatistics gameStatistics { get; private set; } //money, population, energy
    public int currentYear { get; private set; }
    public int currentMonth { get; private set; }

    //0,1,2,3: Noord,Oost,West,Zuid
    public List<MapRegion> regions { get; private set; }

    //game elements
    public List<GameEvent> events { get; private set; }
    public List<RegionAction> actions { get; private set; }
    public List<Quest> quests { get; private set; }
    public Investments investments { get; private set; }
    public List<Card> cards { get; private set; }

    //inventory
    public Inventory inventory { get; private set; }

    //new turn reports
    public ProgressReport monthlyReport { get; private set; }
    public ProgressReport yearlyReport { get; private set; }
    public Timeline timeline { get; private set; }
    
    //advisors
    public EconomyAdvisor economyAdvisor { get; private set; }
    public PollutionAdvisor pollutionAdvisor { get; private set; }
    
    //public int language { get; private set; } //0 = Dutch, 1 = English
    public System.Random rnd { get; private set; }

    //trackdata
    public int completedEventsCount;
    public int abandonedEventsCount;
    public int completedActionsCount;
    public int completedQuestsCount;
    public int receivedCardsCount;
    public float totalTimePlayed;

    public Tutorial tutorial;

    //multiplayer
    public string[] players { get; private set; }

    public bool nextTurnIsclicked;
    public bool OtherPlayerClickedNextTurn;

    public Game()
    {
        //language = 0;
        rnd = new System.Random();
        events = new List<GameEvent>();
        regions = new List<MapRegion>();
        actions = new List<RegionAction>();
        quests = new List<Quest>();
        monthlyReport = new ProgressReport();
        yearlyReport = new ProgressReport();
        economyAdvisor = new EconomyAdvisor();
        pollutionAdvisor = new PollutionAdvisor();
        investments = new Investments();
        cards = new List<Card>();
        inventory = new Inventory();
        timeline = new Timeline();
        tutorial = new Tutorial();


        completedEventsCount = 0;
        abandonedEventsCount = 0;
        completedActionsCount = 0;
        completedQuestsCount = 0;
        receivedCardsCount = 0;

        gameStatistics = new GameStatistics(10000, 17000000, new Energy());

        currentYear = 1;
        currentMonth = 1;

        totalTimePlayed = 0;
    }

    public void LoadRegions(List<MapRegion> regions)
    {
        this.regions = regions;
    }

    public void LoadGameEvents(List<GameEvent> events)
    {
        this.events = events;
    }

    public void LoadQuests(List<Quest> quests)
    {
        this.quests = quests;
    }

    public void LoadCards(List<Card> cards)
    {
        this.cards = cards;
    }

    public void ChangeLanguage(string language)
    {
        if (language == "english")
            ApplicationModel.language = 1;
        else if (language == "dutch")
            ApplicationModel.language = 0;
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

    public void ExecuteNewMonthMethods()
    {
        CompletefinishedActions();
        UpdateRegionEvents();
        MutateMonthlyStatistics();
    }

    public void MutateMonthlyStatistics()
    {
        GetRegionIncome();

        double monthlyPopulation = GetMonthlyPopulation();
        gameStatistics.ModifyPopulation(monthlyPopulation);

        foreach (MapRegion region in regions)
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

        foreach (MapRegion region in regions)
        {
            foreach (RegionSector sector in region.sectors)
            {
                income += sector.statistics.income;
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
        foreach (MapRegion region in regions)
        {
            foreach (RegionAction action in region.actions)
            {
                if (action.isActive && ((action.startMonth + action.actionDuration + action.startYear * 12) == (currentMonth + currentYear * 12)))
                {
                    region.ImplementActionConsequences(action, action.afterInvestmentConsequences, true, gameStatistics.happiness);
                    region.ImplementActionConsequences(action, action.afterInvestmentConsequences, true, gameStatistics.happiness);
                    foreach (bool ps in action.pickedSectors)
                    {
                        if (ps)
                            gameStatistics.ModifyMoney(action.actionMoneyReward, true);
                    }
                    AddCompletedActionToReports(region, action);
                    action.CompleteAction();
                    completedActionsCount++;
                }

                if (action.endTemporaryConsequencesMonth == currentYear * 12 + currentMonth)
                    region.ImplementActionConsequences(action, action.afterInvestmentTemporaryConsequences, false, gameStatistics.happiness);
            }
        }
    }

    public void AddCompletedActionToReports(MapRegion region, RegionAction action)
    {
        monthlyReport.AddCompletedAction(region, action);
        yearlyReport.AddCompletedAction(region, action);
    }

    public void AddNewEventToMonthlyReport(MapRegion region, GameEvent gameEvent)
    {
        monthlyReport.AddNewGameEvent(region, gameEvent);
    }

    public void AddCompletedEventToReports(MapRegion region, GameEvent gameEvent)
    {
        monthlyReport.AddCompletedGameEvent(region, gameEvent);
        yearlyReport.AddCompletedGameEvent(region, gameEvent);

    }

    public void UpdateRegionEvents()
    {
        foreach (MapRegion region in regions)
        {
            region.UpdateEvents(this);
        }
    }

    public int getActiveEventCount()
    {
        int activeCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (gameEvent.isIdle)
                activeCount++;
        }
        return activeCount;
    }

    public int PossibleEventCount()
    {
        int possibleEventCount = 0;
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive && !gameEvent.isIdle)
                possibleEventCount++;
        }

        return possibleEventCount;
    }

    public GameEvent GetPickedEvent(MapRegion region)
    {
        List<GameEvent> possibleEvents = new List<GameEvent>();
        foreach (GameEvent gameEvent in events)
        {
            if (!gameEvent.isActive && !gameEvent.isIdle)
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

        foreach (MapRegion region in regions)
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

    public MapRegion PickEventRegion()
    {
        List<MapRegion> possibleRegions = new List<MapRegion>();
        foreach (MapRegion region in regions)
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

    //multiplayer
    public void ChangeGameForMultiplayer()
    {
        players = new string[2] { PhotonNetwork.playerList[0].UserId, PhotonNetwork.playerList[1].UserId };

        regions[0].SetRegionOwner(players[0]);
        regions[1].SetRegionOwner(players[0]);
        regions[2].SetRegionOwner(players[1]);
        regions[3].SetRegionOwner(players[1]);

        gameStatistics.SetMoneyMultiplayer(GetPlayerListPosition());
    }

    public int GetPlayerListPosition()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == PhotonNetwork.player.UserId)
                return i;
        }

        return 0;
    }

    public double GetMoney()
    {
        if (!ApplicationModel.multiplayer)
            return gameStatistics.money;
        else
            return gameStatistics.playerMoney[gameStatistics.playerNumber];
    }

    public void GetRegionIncome()
    {
        if (!ApplicationModel.multiplayer)
        {
            gameStatistics.ModifyMoney(GetMonthlyIncome(), true);
        }
        else
        {
            foreach (MapRegion r in regions)
            {
                double income = 0;
                foreach (RegionSector rs in r.sectors)
                    income += rs.statistics.income;

                gameStatistics.ModifyMoney(income, true);
            }
        }
    }
}