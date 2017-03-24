﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//This class stores the values of the Regions
public class Region
{
    public string name { get; private set; }
    public RegionStatistics statistics { get; private set; }
    public List<Building> buildings { get; private set; }
    public List<RegionAction> actions { get; private set; }

    public Dictionary<string, RegionSector> sectors { get; private set; }

    public Region(string name, RegionStatistics statistics, Dictionary<string, RegionSector> sectors)
    {
        this.name = name;
        this.statistics = statistics;
        this.sectors = sectors;
        ImplementSectorValues();

        buildings = new List<Building>();
        actions = new List<RegionAction>();
        GenerateActions();
    }

    public void StartAction(string description, int currentYear, int currentMonth) //methode moet van UI aangeroepen worden
    {
        foreach (RegionAction action in actions)
        {
            if (action.description == description)
            {
                ImplementStatisticValues(action.actionCosts, true);
                action.ActivateAction(currentYear, currentMonth);
                if (action.actionDuration == 0)
                    action.CompleteAction();
                break;
            }
        }
    }
    
    public void ImplementSectorValues()
    {
        foreach (RegionSector sector in sectors.Values)
        {
            statistics.changeHappiness(sector.statistics.happiness);
            statistics.ChangeEcoAwareness(sector.statistics.ecoAwareness);
            statistics.ChangeProsperity(sector.statistics.prosperity);
            statistics.pollution.ChangeAirPollutionMutation(sector.statistics.airPollutionContribution);
            statistics.pollution.ChangeNaturePollutionMutation(sector.statistics.naturePollutionContribution);
            statistics.pollution.ChangeWaterPollutionMutation(sector.statistics.waterPollutionContribution);
        }
    }

    //adds a building to the list of buildings the region has
    public void CreateBuilding(string buildingName)
    {
        Building newBuilding = new Building(buildingName);
        buildings.Add(newBuilding);
        ImplementBuildingValues(newBuilding.statistics, true);
    }

    public void DeleteBuilding(Building building)
    {
        ImplementBuildingValues(building.statistics, false);
        buildings.Remove(building);
    }

    public void ModifyBuilding(Building building, BuildingStatistics statistics)
    {
        ImplementBuildingValues(statistics, true);
        building.ModifyBuildingStatistics(statistics);
    }

    public void ImplementBuildingValues(BuildingStatistics statistics, bool isAdded) //if a building is removed for example, isAdded is false
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
    }

    public void ImplementStatisticValues(RegionStatistics statistics, bool isAdded) //if a statistic is removed for example, isAdded is false
    {
        if (isAdded)
        {
            this.statistics.ChangeIncome(statistics.income);
            this.statistics.ChangeDonations(statistics.donations);
            this.statistics.changeHappiness(statistics.happiness);
            this.statistics.ChangeEcoAwareness(statistics.ecoAwareness);
            this.statistics.ChangeProsperity(statistics.prosperity);

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
        }

        else
        {
            this.statistics.ChangeIncome(0 - statistics.income);
            this.statistics.ChangeDonations(0 - statistics.donations);
            this.statistics.changeHappiness(0 - statistics.happiness);
            this.statistics.ChangeEcoAwareness(0 - statistics.ecoAwareness);
            this.statistics.ChangeProsperity(0 - statistics.prosperity);

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
        }
    }

    private void GenerateActions()
    {
        string description;

        description = "dummy action 1";
        RegionStatistics consequence1 = new RegionStatistics(0, 0, 0, new Pollution(0, 0, 0, 0, -1, -1), 1, 0);
        RegionStatistics actionCost1 = new RegionStatistics(-2000, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        RegionAction action1 = new RegionAction(description, consequence1, actionCost1, 2, 3);
        actions.Add(action1);

        description = "dummy action 2";
        RegionStatistics consequence2 = new RegionStatistics(500, 0, 1, new Pollution(0, 0, 0, 0, 0, 0), 0, 2);
        RegionStatistics actionCost2 = new RegionStatistics(-3000, 0, 0, new Pollution(0, 0, 0, 0, 0, 0), 0, 0);
        RegionAction action2 = new RegionAction(description, consequence2, actionCost2, 2, 3);
        actions.Add(action2);
    }
}

