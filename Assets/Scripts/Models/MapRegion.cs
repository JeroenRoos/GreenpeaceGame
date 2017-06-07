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

    //muliplayer
    public string regionOwner { get; private set; }

    public MapRegion() { }

    #region LoadData
    public void LoadBuildings(List<Building> possibleBuildings)
    {
        this.possibleBuildings = possibleBuildings;
    }

    public void LoadActions(List<RegionAction> actions)
    {
        this.actions = actions;
    }
    #endregion

    #region RegionActionMethods
    public void StartAction(RegionAction action, Game game, bool[] pickedSectors)
    {
        foreach (bool isTrue in pickedSectors)
        {
            if (isTrue)
                game.gameStatistics.ModifyMoney(action.afterInvestmentActionMoneyCost, false);
        }
        action.ActivateAction(game.currentYear, game.currentMonth, pickedSectors);
    }

    /*updates the sector statistics with the consequences of the action, method received both the action and statistics because
    /the statistics can be either normal consequences or temporary consequences*/
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
    #endregion

    #region GameEventMethods
    public void AddGameEvent(GameEvent gameEvent, double happiness)
    {
        inProgressGameEvents.Add(gameEvent);
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

    //cleans the list "inProgressGameEvents" list, removes events that are completely finished from this list
    public void RemoveFinishedEvents()
    {
        for (int i = inProgressGameEvents.Count - 1; i >= 0; i--)
        {
            if (inProgressGameEvents[i].isFinished)
                inProgressGameEvents.Remove(inProgressGameEvents[i]);
        }
    }

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
    #endregion

    #region BuildingMethods
    public void SetBuilding(string buildingID)
    {
        if (activeBuilding != null)
        {
            RemoveBuildingStatistics();
        }

        //add new building statistics to the regionsectors
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

    //remove old building statistics from the regionsectors
    public void RemoveBuildingStatistics()
    {
        foreach (RegionSector rs in sectors)
        {
            rs.ImplementBuildingStatistics(activeBuilding, false);
        }
    }
    #endregion

    #region Multiplayer
    public void SetRegionOwner(string playerID)
    {
        regionOwner = playerID;
    }

    public void StartActionMultiplayer(RegionAction action, Game game, bool[] pickedSectors)
    {
        foreach (bool isTrue in pickedSectors)
        {
            if (isTrue)
                game.gameStatistics.ModifyMoney(action.afterInvestmentActionMoneyCost, false);
        }
        action.ActivateAction(game.currentYear, game.currentMonth, pickedSectors);
        action.isOwnAction = true;
    }

    public void StartOtherPlayerAction(RegionAction action, Game game, bool[] pickedSectors)
    {
        action.ActivateAction(game.currentYear, game.currentMonth, pickedSectors);
        action.isOwnAction = false;
    }
    #endregion
}

