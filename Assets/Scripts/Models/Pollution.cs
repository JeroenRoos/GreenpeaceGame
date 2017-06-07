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

    //method used for copying Pollution without reference
    public Pollution(Pollution pollution)
    {
        avgPollution = 0;
        airPollution = pollution.airPollution;
        naturePollution = pollution.naturePollution;
        waterPollution = pollution.waterPollution;
        airPollutionIncrease = pollution.airPollutionIncrease;
        naturePollutionIncrease = pollution.naturePollutionIncrease;
        waterPollutionIncrease = pollution.waterPollutionIncrease;
    }

    #region ChangeVariablesMethods
    public void ChangeAirPollution(double changeValue)
    {
        airPollution += changeValue;
        if (airPollution < 0)
            airPollution = 0;
        else if (airPollution > 100)
            airPollution = 100;
        CalculateAvgPollution();
    }

    public void ChangeNaturePollution(double changeValue)
    {
        naturePollution += changeValue;
        if (naturePollution < 0)
            naturePollution = 0;
        else if (naturePollution > 100)
            naturePollution = 100;
        CalculateAvgPollution();
    }

    public void ChangeWaterPollution(double changeValue)
    {
        waterPollution += changeValue;
        if (waterPollution < 0)
            waterPollution = 0;
        else if (waterPollution > 100)
            waterPollution = 100;
        CalculateAvgPollution();
    }

    public void CalculateAvgPollution()
    {
        avgPollution = ((airPollution + naturePollution + waterPollution) / 3);
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
        if (airPollution < 1)
            airPollution -= airPollutionIncrease / 100 / 12;
        else
            airPollution += (airPollution * airPollutionIncrease / 100 / 12);

        if (naturePollution < 1)
            naturePollution -= naturePollutionIncrease / 100 / 12;
        else
            naturePollution += (naturePollution * naturePollutionIncrease / 100 / 12);

        if (waterPollution < 1)
            waterPollution += waterPollutionIncrease / 100 / 12;
        else
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
    #endregion

    #region GameEventMethods
    public void SetPickedConsequences(SectorStatistics s, double[] modifiers, System.Random rnd)
    {
        airPollution = s.pollution.airPollution * modifiers[rnd.Next(0, modifiers.Length)];
        naturePollution = s.pollution.naturePollution * modifiers[rnd.Next(0, modifiers.Length)];
        waterPollution = s.pollution.waterPollution * modifiers[rnd.Next(0, modifiers.Length)];
        airPollutionIncrease = s.pollution.airPollutionIncrease * modifiers[rnd.Next(0, modifiers.Length)];
        naturePollutionIncrease = s.pollution.naturePollutionIncrease * modifiers[rnd.Next(0, modifiers.Length)];
        waterPollutionIncrease = s.pollution.waterPollutionIncrease * modifiers[rnd.Next(0, modifiers.Length)];
    }

    public void SetPickedConsequencesMultiplayer(double[] consequences)
    {
        airPollution = consequences[4];
        naturePollution = consequences[5];
        waterPollution = consequences[6];
        airPollutionIncrease = consequences[7];
        naturePollutionIncrease = consequences[8];
        waterPollutionIncrease = consequences[9];
    }
    #endregion
}

