using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class BuildingStatistics
{
    public double income { get; private set; }
    public Pollution pollution { get; private set; }
    public double prosperity { get; private set; }

    private BuildingStatistics() { }

    public BuildingStatistics(double income, Pollution pollutionInfluence, double prosperityInfluence)
    {
        this.income = income;
        this.pollution = pollutionInfluence;
        this.prosperity = prosperityInfluence;
    }
    
    public void ChangeIncome(double changeValue)
    {
        income += changeValue;
    }

    public void ChangeProsperity(double changeValue)
    {
        prosperity += changeValue;
    }
}

