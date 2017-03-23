using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameStatistics
{
    // Game general statistics
    public double money { get; private set; }
    public double population { get; private set; }
    public Energy energy { get; private set; }

    // Regional averages
    public double income { get; private set; }
    public double donations { get; private set; }
    public double happiness { get; private set; }
    public double pollution { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }

    public GameStatistics(double money, double population, Energy energy)
    {
        this.money = money;
        this.population = population;
        this.energy = energy;
    }

    public void UpdateRegionalAvgs(Game game)
    {
        income       = 0;
        donations    = 0;
        happiness    = 0;
        pollution    = 0;
        ecoAwareness = 0;
        prosperity   = 0;

        foreach (Region region in game.regions.Values)
        {
            income       += region.statistics.income;
            donations    += region.statistics.donations;
            happiness    += region.statistics.happiness;
            pollution    += region.statistics.pollution.avgPullution;
            ecoAwareness += region.statistics.ecoAwareness;
            prosperity   += region.statistics.prosperity;
        }
        
        income       /= 4;
        donations    /= 4;
        happiness    /= 4;
        pollution    /= 4;
        ecoAwareness /= 4;
        prosperity   /= 4;
    }
}

