using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RegionStatistics
{
    public double income { get; private set; }
    public double donations { get; private set; }
    public double happiness { get; private set; }
    public Pollution pollution { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }

    public double publicTransport { get; private set; } //public transport class
    public double cityEnvironment { get; private set; } //city environment class


    public RegionStatistics(double income, double donations, double happiness, Pollution pollution, double ecoAwareness, double prosperity)
    {
        this.income = income;
        this.donations = donations;
        this.happiness = happiness;
        this.pollution = pollution;
        this.ecoAwareness = ecoAwareness;
        this.prosperity = prosperity;
    }

    public void ChangeIncome(double changeValue)
    {
        income = income + changeValue;
    }

    public void ChangeDonations(double changeValue)
    {
        donations = donations + changeValue;
    }

    public void changeHappiness(double changeValue)
    {
        happiness = happiness + changeValue;
    }

    public void ChangeEcoAwareness(double changeValue)
    {
        ecoAwareness = ecoAwareness + changeValue;
    }

    public void ChangeProsperity(double changeValue)
    {
        prosperity = prosperity + changeValue;
    }

    public void mutateTimeBasedStatistics()
    {
        pollution.mutateTimeBasedStatistics();
    }
}

