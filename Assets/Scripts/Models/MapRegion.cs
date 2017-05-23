using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


//This class stores the values of the Regions
[Serializable]
public class MapRegion
{

    public string[] name { get; private set; }
    public RegionStatistics statistics { get; private set; }
    public List<RegionAction> actions { get; private set; }
    public RegionSector[] sectors { get; private set; }
    public float[] eventPositions;
    public float[] buildingPositions;
    public float[] actionPositions;

    public List<GameEvent> inProgressGameEvents { get; private set; }

    public List<Building> possibleBuildings { get; private set; }
    public Building activeBuilding { get; private set; }

    public MapRegion() { }

    public void LoadBuildings(List<Building> possibleBuildings)
    {
        this.possibleBuildings = possibleBuildings;
    }

    public void LoadActions(List<RegionAction> actions)
    {
        this.actions = actions;
    }

    public void StartAction(RegionAction action, Game game, bool[] pickedSectors)
    {
        foreach (bool isTrue in pickedSectors)
        {
            if (isTrue)
                game.gameStatistics.ModifyMoney(action.afterInvestmentActionMoneyCost, false);
        }
        action.ActivateAction(game.currentYear, game.currentMonth, pickedSectors);
    }

    public void AddGameEvent(GameEvent gameEvent, double happiness)
    {
        inProgressGameEvents.Add(gameEvent);
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

            if (gameEvent.lastCompleted + gameEvent.temporaryConsequencesDuration[gameEvent.pickedChoiceNumber] == game.currentMonth + game.currentYear * 12)
            {
                ImplementEventConsequences(gameEvent, gameEvent.pickedTemporaryConsequences[gameEvent.pickedChoiceNumber], false, game.gameStatistics.happiness);
                gameEvent.FinishEvent();
            }
        }
        RemoveFinishedEvents();
    }

    public void CompleteEvent(GameEvent gameEvent, Game game)
    {
        ImplementEventConsequences(gameEvent, gameEvent.pickedConsequences[gameEvent.pickedChoiceNumber], true, game.gameStatistics.happiness);
        ImplementEventConsequences(gameEvent, gameEvent.pickedTemporaryConsequences[gameEvent.pickedChoiceNumber], true, game.gameStatistics.happiness);
        gameEvent.CompleteEvent(game);
        if (gameEvent.pickedChoiceNumber == 0)
            game.abandonedEventsCount++;
        else
            game.completedEventsCount++;
    }

    public void RemoveFinishedEvents()
    {
        for (int i = inProgressGameEvents.Count - 1; i >= 0; i--)
        {
            if (inProgressGameEvents[i].isFinished)
                inProgressGameEvents.Remove(inProgressGameEvents[i]);
        }
    }
    
    public void SetBuilding(string buildingID)
    {
        if (activeBuilding != null)
        {
            foreach (RegionSector rs in sectors)
            {
                rs.ImplementBuildingStatistics(activeBuilding, false);
            }
        }

        if (buildingID != null)
        {
            foreach (Building b in possibleBuildings)
            {
                if (b.buildingID == buildingID)
                {
                    activeBuilding = b;
                    foreach (RegionSector rs in sectors)
                    {
                        rs.ImplementBuildingStatistics(activeBuilding, true);
                    }
                    break;
                }
            }
        }

        else
            activeBuilding = null;
    }

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

    public void ImplementEventConsequences(GameEvent gameEvent, SectorStatistics statistics, bool isAdded, double happiness)
    {
        for (int i = 0; i < gameEvent.possibleSectors.Length; i++)
        {
            foreach (RegionSector sector in sectors)
            {
                if (sector.sectorName[0] == gameEvent.possibleSectors[i])
                    sector.ImplementStatisticValues(statistics, isAdded, happiness);
            }
        }
    }

    public void ImplementActionConsequences(RegionAction regionAction, SectorStatistics statistics, bool isAdded, double happiness)
    {
        for (int i = 0; i < regionAction.possibleSectors.Count(); i++)
        {
            if (regionAction.pickedSectors[i])
            {
                foreach (RegionSector sector in sectors)
                {
                    if (sector.sectorName[0] == regionAction.possibleSectors[i])
                        sector.ImplementStatisticValues(statistics, isAdded, happiness);
                }
            }
        }
    }
}

