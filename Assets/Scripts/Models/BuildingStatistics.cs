using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BuildingStatistics
{
    public double income { get; private set; }
    public Pollution pollution { get; private set; }
    public double prosperity { get; private set; }

    public BuildingStatistics(double income, Pollution pollutionInfluence, double prosperityInfluence)
    {
        this.income = income;
        this.pollution = pollutionInfluence;
        this.prosperity = prosperityInfluence;
    }
    
    public void ChangeIncome(double changeValue)
    {
        income = income + changeValue;
    }

    public void ChangeProsperity(double changeValue)
    {
        prosperity = prosperity + changeValue;
    }
}

