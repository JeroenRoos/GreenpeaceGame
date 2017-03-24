using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameStatistics
{
    public double money { get; private set; }
    public double population { get; private set; }
    public string energy { get; private set; } //Energy class

    public GameStatistics(double money, double population, string energy)
    {
        this.money = money;
        this.population = population;
        this.energy = energy;
    }
}

