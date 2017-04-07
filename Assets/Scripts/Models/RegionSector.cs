using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class RegionSector
{
    public string[] sectorName { get; protected set; }
    public SectorStatistics statistics { get; protected set; }

    public RegionSector() { }

    public RegionSector(string[] sectorName, SectorStatistics statistics)
    {
        this.sectorName = sectorName;
        this.statistics = statistics;
    }

    public void ImplementStatisticValues(SectorStatistics statistics, bool isAdded) //if a statistic is removed for example, isAdded is false
    {
        if (isAdded)
        {
            this.statistics.ModifyIncome(statistics.income);
            this.statistics.ModifyHappiness(statistics.happiness);
            this.statistics.ModifyEcoAwareness(statistics.ecoAwareness);
            this.statistics.ModifyProsperity(statistics.prosperity);

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(statistics.pollution.waterPollutionIncrease);
        }

        else
        {
            this.statistics.ModifyIncome(0 - statistics.income);
            this.statistics.ModifyHappiness(0 - statistics.happiness);
            this.statistics.ModifyEcoAwareness(0 - statistics.ecoAwareness);
            this.statistics.ModifyProsperity(0 - statistics.prosperity);

            //temporary methods (incomplete)
            this.statistics.pollution.ChangeAirPollutionMutation(0 - statistics.pollution.airPollutionIncrease);
            this.statistics.pollution.ChangeNaturePollutionMutation(0 - statistics.pollution.naturePollutionIncrease);
            this.statistics.pollution.ChangeWaterPollutionMutation(0 - statistics.pollution.waterPollutionIncrease);
        }
    }
}
