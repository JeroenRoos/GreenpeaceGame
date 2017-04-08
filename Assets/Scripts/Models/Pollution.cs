using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Pollution
{
    public double avgPollution { get; private set; }
    
    public double airPollution { get; private set; }
    public double naturePollution { get; private set; }
    public double waterPollution { get; private set; }

    //the increase of pollution during a certain timeframe
    public double airPollutionIncrease { get; private set; }
    public double naturePollutionIncrease { get; private set; }
    public double waterPollutionIncrease { get; private set; }

    public Pollution() { }
    
    public void ChangeAirPollution(double changeValue)
    {
        airPollution += changeValue;
        if (airPollution < 0)
            airPollution = 0;
    }

    public void ChangeNaturePollution(double changeValue)
    {
        naturePollution += changeValue;
        if (naturePollution < 0)
            naturePollution = 0;
    }

    public void ChangeWaterPollution(double changeValue)
    {
        waterPollution += changeValue;
        if (waterPollution < 0)
            waterPollution = 0;
    }

    public void ChangeAirPollutionMutation(double changeValue)
    {
        airPollutionIncrease += changeValue;
    }

    public void ChangeNaturePollutionMutation(double changeValue)
    {
        naturePollutionIncrease += changeValue;
    }
    public void ChangeWaterPollutionMutation(double changeValue)
    {
        waterPollutionIncrease += changeValue;
    }

    public void mutateTimeBasedStatistics()
    {
        airPollution += (airPollution * airPollutionIncrease / 100 / 12);
        naturePollution += (naturePollution * naturePollutionIncrease / 100 / 12);
        waterPollution += (waterPollution * waterPollutionIncrease / 100 / 12);

        KeepPollutionValuesWithinBoundaries();
        CalculateAvgPollution();
    }

    public void KeepPollutionValuesWithinBoundaries()
    {
        if (airPollution > 100)
            airPollution = 100;
        else if (airPollution < 0)
            airPollution = 0;

        if (naturePollution > 100)
            naturePollution = 100;
        else if (airPollution < 0)
            naturePollution = 0;

        if (waterPollution > 100)
            waterPollution = 100;
        else if (airPollution < 0)
            waterPollution = 0;
    }

    public void CalculateAvgPollution()
    {
        avgPollution = ((airPollution + naturePollution + waterPollution) / 3);
    }
}

