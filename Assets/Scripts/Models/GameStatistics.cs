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
<<<<<<< HEAD

    public void ModifyMoney(double changevalue)
    {
        money += changevalue;
    }

    public void ModifyPopulation(double changevalue)
    {
        population += changevalue;
    }

    //moet voor Energyclass aansluiten
    public void ModifyEnergy(double changevalue)
    {
        //energy += changevalue;
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
            //pollution    += region.statistics.pollution.avgPullution;
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
=======
>>>>>>> 1b894b7f528e47001249c96fb9448c6ed9da6861
}
