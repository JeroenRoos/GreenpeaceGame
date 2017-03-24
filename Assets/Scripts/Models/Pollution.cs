using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Pollution
{
    public double airPollution { get; private set; }
    public double naturePollution { get; private set; }
    public double waterPollution { get; private set; }

    //the increase of pollution during a certain timeframe
    public double airPollutionIncrease { get; private set; }
    public double naturePollutionIncrease { get; private set; }
    public double waterPollutionIncrease { get; private set; }

    public Pollution(double airPollution, double naturePollution, double waterPollution,
                     double airPollutionIncrease, double naturePollutionIncrease, double waterPollutionIncrease)
    {
        this.airPollution = airPollution;
        this.naturePollution = naturePollution;
        this.waterPollution = waterPollution;

        this.airPollutionIncrease = airPollutionIncrease;
        this.naturePollutionIncrease = naturePollutionIncrease;
        this.waterPollutionIncrease = waterPollutionIncrease;
    }

    public Pollution()
    {
        // UI Constructor
    }

    public void ChangeAirPollutionMutation(double changeValue)
    {
        airPollutionIncrease = airPollutionIncrease + changeValue;
    }

    public void ChangeNaturePollutionMutation(double changeValue)
    {
        naturePollutionIncrease = naturePollutionIncrease + changeValue;
    }
    public void ChangeWaterPollutionMutation(double changeValue)
    {
        waterPollutionIncrease = waterPollutionIncrease + changeValue;
    }


    public void mutateTimeBasedStatistics()
    {
        if (airPollution > 0)
            airPollution = airPollution + (airPollution / 100 * airPollutionIncrease);
        else
            airPollution = airPollution + ((airPollution + 20) / 100 * airPollution);

        if (naturePollution > 0)
            naturePollution = naturePollution + (naturePollution / 100 * naturePollutionIncrease);
        else
            naturePollution = naturePollution + ((naturePollution + 20) / 100 * naturePollution);

        if (waterPollution > 0)
            waterPollution = waterPollution + (waterPollution / 100 * waterPollutionIncrease);
        else
            waterPollution = waterPollution + ((waterPollution + 20) / 100 * waterPollution);
    }
}

