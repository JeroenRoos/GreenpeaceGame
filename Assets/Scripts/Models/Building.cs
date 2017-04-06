using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//this class stores the values of the buildings in a region
[Serializable]
public class Building
{
    public string[] buildingName { get; private set; }

    public BuildingStatistics statistics { get; private set; }

    public Building(string[] buildingName)
    {
        this.buildingName = buildingName;
    }

    private Building() { }

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

