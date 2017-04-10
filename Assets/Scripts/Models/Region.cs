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
    public Vector3 eventPosition;

    public List<GameEvent> inProgressGameEvents { get; private set; }

    private Region() { }

    public void LoadActions(List<RegionAction> actions)
    {
        this.actions = actions;
    }

    public void StartAction(RegionAction action, Game game, bool[] pickedSectors)
    {
        foreach (bool isTrue in pickedSectors)
        {
            Debug.Log(isTrue);
            if (isTrue)
            game.gameStatistics.ModifyMoney(action.actionMoneyCost, false);
        }
        action.ActivateAction(game.currentYear, game.currentMonth, pickedSectors);
        ImplementActionConsequences(action, action.actionCosts, false);
        ImplementActionConsequences(action, action.duringActionConsequences, true);
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
                    gameEvent.SetPickedChoice(0, game, this);
                }
            }

            if (gameEvent.isActive && ((gameEvent.pickedChoiceStartMonth + gameEvent.eventDuration[gameEvent.pickedChoiceNumber] + gameEvent.pickedChoiceStartYear * 12) == (game.currentMonth + game.currentYear * 12)))
            {
                game.AddCompletedEventToReports(this, gameEvent);
                CompleteEvent(gameEvent, game);
            }

            if (gameEvent.onEventStartYear == game.currentYear && gameEvent.onEventStartMonth == game.currentMonth)
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

    public void CompleteEvent(GameEvent gameEvent, Game game)
    {
        ImplementEventConsequences(gameEvent, gameEvent.consequences[gameEvent.pickedChoiceNumber], true);
        ImplementEventConsequences(gameEvent, gameEvent.duringEventProgressConsequences[gameEvent.pickedChoiceNumber], false);
        ImplementEventConsequences(gameEvent, gameEvent.temporaryConsequences[gameEvent.pickedChoiceNumber], true);
        gameEvent.CompleteEvent(game);
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
        for (int i = 0; i < gameEvent.possibleSectors.Length; i++)
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

    public void ImplementActionConsequences(RegionAction regionAction, SectorStatistics statistics, bool isAdded)
    {
        for (int i = 0; i < regionAction.possibleSectors.Count(); i++)
        {
            if (regionAction.pickedSectors[i])
            {
                foreach (RegionSector sector in sectors)
                {
                    if (sector.sectorName[0] == regionAction.possibleSectors[i])
                        sector.ImplementStatisticValues(statistics, isAdded);
                }
            }
        }
    }
}

