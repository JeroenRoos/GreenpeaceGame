using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//This class stores the values of the Regions
[Serializable]
public class Region
{
    public string[] name { get; private set; }
    public RegionStatistics statistics { get; private set; }
    public List<Building> buildings { get; private set; }
    public List<RegionAction> actions { get; private set; }
    public RegionSector[] sectors { get; private set; }
    public Vector3[] eventPositions;
    
    public List<GameEvent> inProgressGameEvents { get; private set; }

    private Region() { }

    public Region(string[] name, RegionStatistics statistics, RegionSector[] sectors)
    {
        this.name = name;
        this.statistics = statistics;
        this.sectors = sectors;

        eventPositions = new Vector3[4];
        eventPositions[0] = new Vector3(9, 1, 12);
        eventPositions[1] = new Vector3(16, 1, 13);
        eventPositions[2] = new Vector3(13, 1, 8);
        eventPositions[3] = new Vector3(15, 1, 20);

        buildings = new List<Building>();
        actions = new List<RegionAction>();

        this.statistics.UpdateSectorAvgs(this);
    }

    public void LoadActions(List<RegionAction> actions)
    {
        this.actions = actions;
    }

    public void StartAction(RegionAction action, int currentYear, int currentMonth, Game game) //methode moet van UI aangeroepen worden
    {
        if (game.gameStatistics.money > action.actionMoneyCost)
        {
            game.gameStatistics.ModifyMoney(-action.actionMoneyCost);
            ImplementActionConsequences(action, action.actionCosts, true);
            action.ActivateAction(currentYear, currentMonth);

            if (action.actionDuration == 0)
                action.CompleteAction();
        }

        else
        {
            //not enough money popup message?
        }
    }

    public void AddGameEvent(GameEvent gameEvent)
    {
        inProgressGameEvents.Add(gameEvent);

        ImplementEventConsequences(gameEvent, gameEvent.onEventStartConsequence, true);
        ImplementEventConsequences(gameEvent, gameEvent.onEventStartTemporaryConsequence, true);
    }

    public void UpdateEvents(Game game)
    {
        foreach (GameEvent gameEvent in inProgressGameEvents)
        {
            if (gameEvent.isIdle)
                {
                    gameEvent.SubtractIdleTurnsLeft();
                    if (gameEvent.idleTurnsLeft == 0)
                    {
                        gameEvent.SetPickedChoice(0, game);
                    ImplementEventConsequences(gameEvent, gameEvent.duringEventProgressConsequences[gameEvent.pickedChoiceNumber], true);
                    }
                }

            if (gameEvent.isActive && ((gameEvent.pickedChoiceStartMonth + gameEvent.eventDuration[gameEvent.pickedChoiceNumber] + gameEvent.pickedChoiceStartYear * 12) == (game.currentMonth + game.currentYear * 12)))
            {
                CompleteEvent(gameEvent);
            }

            if (gameEvent.onEventStartYear == game.currentYear & gameEvent.onEventStartMonth == game.currentMonth)
            {
                ImplementEventConsequences(gameEvent, gameEvent.onEventStartTemporaryConsequence, false);
            }

            if (gameEvent.lastCompleted + gameEvent.temporaryConsequencesDuration[gameEvent.pickedChoiceNumber] == game.currentMonth + game.currentYear * 12)
            {
                ImplementEventConsequences(gameEvent, gameEvent.temporaryConsequences[gameEvent.pickedChoiceNumber], false);
                gameEvent.FinishEvent();
            }
        }
        RemoveFinishedEvents();
    }

    public void CompleteEvent(GameEvent gameEvent)
    {
        ImplementEventConsequences(gameEvent, gameEvent.consequences[gameEvent.pickedChoiceNumber], true);
        ImplementEventConsequences(gameEvent, gameEvent.duringEventProgressConsequences[gameEvent.pickedChoiceNumber], false);
        ImplementEventConsequences(gameEvent, gameEvent.temporaryConsequences[gameEvent.pickedChoiceNumber], true);
        gameEvent.CompleteEvent();
    }

    public void RemoveFinishedEvents()
    {
        for (int i = inProgressGameEvents.Count - 1; i >= 0; i--)
        {
            if (inProgressGameEvents[i].isFinished)
                inProgressGameEvents.Remove(inProgressGameEvents[i]);
        }
    }

    //adds a building to the list of buildings the region has
    /*public void CreateBuilding(string[] buildingName)
    {
        Building newBuilding = new Building(buildingName);
        buildings.Add(newBuilding);
        ImplementBuildingValues(newBuilding.statistics, true);
    }*/

    /*public void DeleteBuilding(Building building)
    {
        ImplementBuildingValues(building.statistics, false);
        buildings.Remove(building);
    }*/

    /*public void ModifyBuilding(Building building, BuildingStatistics statistics)
    {
        ImplementBuildingValues(statistics, true);
        building.ModifyBuildingStatistics(statistics);
    }*/

    /*public void ImplementBuildingValues(BuildingStatistics statistics, bool isAdded) //if a building is removed for example, isAdded is false
    {
        if (isAdded)
        {
            this.statistics.ChangeIncome(statistics.income);
            this.statistics.ChangeProsperity(statistics.prosperity); //change households and companies instead of region prosperity

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
        }

        else
        {
            this.statistics.ChangeIncome(0 - statistics.income);
            this.statistics.ChangeProsperity(0 - statistics.prosperity); //change households and companies instead of region prosperity

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
        }
    }*/

    public void ImplementEventConsequences(GameEvent gameEvent, SectorStatistics statistics, bool isAdded)
    {
        for (int i = 0; i < sectors.Count(); i++)
        {
            if (gameEvent.pickedSectors[i])
            {
                foreach (RegionSector sector in sectors)
                {
                    if (sector.sectorName[0] == gameEvent.possibleSectors[i])
                        sector.ImplementStatisticValues(statistics, isAdded);
                }
            }
        }
    }

    public void ImplementActionConsequences(RegionAction gameEvent, SectorStatistics statistics, bool isAdded)
    {
        foreach (RegionSector sector in sectors)
        {
                sector.ImplementStatisticValues(statistics, isAdded);
        }
    }
}

