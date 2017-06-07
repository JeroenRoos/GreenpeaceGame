using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//this class is currently not used due to lacking the expertise of creating a line graph

[Serializable]
public class Timeline
{
    public List<double> incomePerTurn { get; private set; }
    public List<double> happinessPerTurn { get; private set; }
    public List<double> pollutionPerTurn { get; private set; }
    public List<double> ecoAwarenessPerTurn { get; private set; }
    public List<double> prosperityPerTurn { get; private set; }
    public List<int> timeInMonths { get; private set; } 

    public Timeline()
    {
        incomePerTurn = new List<double>();
        happinessPerTurn = new List<double>();
        pollutionPerTurn = new List<double>();
        ecoAwarenessPerTurn = new List<double>();
        prosperityPerTurn = new List<double>();
        timeInMonths = new List<int>();
    }

    public void StoreTurnInTimeLine(GameStatistics s, int currentYear, int currentMonth)
    {
        incomePerTurn.Add(s.income);
        happinessPerTurn.Add(s.happiness);
        pollutionPerTurn.Add(s.pollution);
        ecoAwarenessPerTurn.Add(s.ecoAwareness);
        prosperityPerTurn.Add(s.prosperity);
        timeInMonths.Add(currentYear * 12 + currentMonth);
    }
}
