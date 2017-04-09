using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//monthly report class
[Serializable]
public class ProgressReport
{
    public string[] reportRegions { get; private set; }
    public double[] oldIncome { get; private set; }
    public double[] oldHappiness { get; private set; }
    public double[] oldEcoAwareness { get; private set; }
    public double[] oldProsperity { get; private set; }
    public double[] oldPollution { get; private set; }

    public List<RegionAction>[] completedActions { get; private set; }
    public List<GameEvent>[] completedEvents { get; private set; }

    public ProgressReport()
    {
        reportRegions = new string[4] { "Noord Nederland", "Oost Nederland", "Zuid Nederland", "West Nederland" };
        oldIncome = new double[4] { 0, 0, 0, 0 };
        oldHappiness = new double[4] { 0, 0, 0, 0 };
        oldEcoAwareness = new double[4] { 0, 0, 0, 0 };
        oldProsperity = new double[4] { 0, 0, 0, 0 };
        oldPollution = new double[4] { 0, 0, 0, 0 };
        completedActions = new List<RegionAction>[] { new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>(), new List<RegionAction>()};
        completedEvents = new List<GameEvent>[] { new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>(), new List<GameEvent>() };
    }

    public void UpdateStatistics(List<Region> regions)
    {
        foreach (Region region in regions)
        {
            for (int i = 0; i < reportRegions.Length; i++)
            {
                if (region.name[0] == reportRegions[i])
                {
                    oldIncome[i] = region.statistics.income;
                    oldHappiness[i] = region.statistics.happiness;
                    oldEcoAwareness[i] = region.statistics.ecoAwareness;
                    oldProsperity[i] = region.statistics.prosperity;
                    oldPollution[i] = region.statistics.avgPollution;
                    completedActions[i] = new List<RegionAction>();
                    completedEvents[i] = new List<GameEvent>();
                    break;
                }
            }
        }
    }

    public void AddCompletedAction(Region region, RegionAction action)
    {
        for (int i = 0; i < reportRegions.Length; i++)
        {
            if (region.name[0] == reportRegions[i])
            {
                completedActions[i].Add(action);
                break;
            }
        }
    }

    public void AddCompletedGameEvent(Region region, GameEvent gameEvent)
    {
        for (int i = 0; i < reportRegions.Length; i++)
        {
            if (region.name[0] == reportRegions[i])
            {
                completedEvents[i].Add(gameEvent);
                break;
            }
        }
    }
}