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
        this.pollution = pollution;
        this.income = 0;
        this.donations = 0;
        this.happiness = 0;
        this.ecoAwareness = 0;
        this.prosperity = 0;

        ChangeIncome(income);
        ChangeDonations(donations);
        ChangeHappiness(happiness);
        ChangeEcoAwareness(ecoAwareness);
        ChangeProsperity(prosperity);
    }

    public void ChangeIncome(double changeValue)
    {
        income += changeValue;
    }

    public void ChangeDonations(double changeValue)
    {
        donations += changeValue;
    }

    public void ChangeHappiness(double changeValue)
    {
        happiness +=changeValue;
    }

    public void ChangeEcoAwareness(double changeValue)
    {
        ecoAwareness += changeValue;

        double donationChangeValue = 100 * changeValue;
        ChangeDonations(donationChangeValue);

        double pollutionChangeValue = 0 - (changeValue / 10);
        pollution.ChangeAirPollutionMutation(pollutionChangeValue);
        pollution.ChangeNaturePollutionMutation(pollutionChangeValue);
        pollution.ChangeWaterPollutionMutation(pollutionChangeValue);
    }

    public void ChangeProsperity(double changeValue)
    {
        prosperity += changeValue;

        double happinessChangeValue = changeValue / 10;
        ChangeHappiness(happinessChangeValue);

        double incomeChangeValue = changeValue * 1000;
        ChangeIncome(incomeChangeValue);
    }

    public void mutateTimeBasedStatistics()
    {
        pollution.mutateTimeBasedStatistics();
    }
}

