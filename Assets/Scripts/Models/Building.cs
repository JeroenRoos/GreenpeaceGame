using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//this class stores the values of the buildings in a region
public class Building
{
    public string[] buildingName { get; private set; }

    public BuildingStatistics statistics { get; private set; }

    public Building(string[] buildingName)
    {
        this.buildingName = buildingName;
        Pollution pollution;
        switch (buildingName[0])
        {
            case "kolenfabriek":
                pollution = new Pollution(0, 0, 0, 5, 5, 5);
                statistics = new BuildingStatistics(2000, pollution, 5);
                break;
            default:
                pollution = new Pollution(0, 0, 0, 0, 0, 0);
                statistics = new BuildingStatistics(0, pollution, 0);
                break;
        }
    }

    public void ModifyBuildingStatistics(BuildingStatistics statistics)
    {
        this.statistics.ChangeIncome(statistics.income);
        this.statistics.ChangeProsperity(statistics.prosperity);

        //temporary methods (incomplete)
        this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
        this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
        this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
    }
}

