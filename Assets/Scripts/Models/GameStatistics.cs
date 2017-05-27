using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class GameStatistics
{
    // Game general statistics
    public double money { get; private set; }
    public double population { get; private set; }
    public Energy energy { get; private set; }

    // Regional averages
    public double income { get; private set; }
    public double happiness { get; private set; }
    public double pollution { get; private set; }
    public double ecoAwareness { get; private set; }
    public double prosperity { get; private set; }

    //multiplayer
    public double[] playerMoney { get; private set; }
    public int playerNumber { get; private set; }

    public GameStatistics() { }

    public GameStatistics(double money, double population, Energy energy)
    {
        this.money = money;
        this.population = population;
        this.energy = energy;
    }

    public void ModifyMoney(double changevalue, bool isAdded)
    {
        if (!ApplicationModel.multiplayer)
        {
            if (isAdded)
                money += changevalue;
            else
                money -= changevalue;
        }
        else
        {
            if (isAdded)
                playerMoney[playerNumber] += changevalue;
            else
                playerMoney[playerNumber] -= changevalue;

            MultiplayerManager.CallChangeOwnMoney(changevalue, isAdded);
        }
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
        happiness    = 0;
        pollution    = 0;
        ecoAwareness = 0;
        prosperity   = 0;

        int divisionValue = 0;

        foreach (MapRegion region in game.regions)
        {
            foreach (RegionSector sector in region.sectors)
            {
                income += sector.statistics.income;
                happiness += sector.statistics.happiness;
                pollution += sector.statistics.pollution.avgPollution;
                ecoAwareness += sector.statistics.ecoAwareness;
                prosperity += sector.statistics.prosperity;

                divisionValue++;
            }
        }

        //income /= divisionValue;
        happiness /= divisionValue;
        pollution /= divisionValue;
        ecoAwareness /= divisionValue;
        prosperity /= divisionValue;
    }

    //mulgiplayer
    public void SetMoneyMultiplayer(int playerNumber)
    {
        playerMoney = new double[2];
        playerMoney[0] = money / 2;
        playerMoney[1] = money / 2;

        this.playerNumber = playerNumber;
    }

    public void ModifyMoneyOtherPlayer(double changevalue, bool isAdded)
    {
        if (playerNumber == 0)
        {
            if (isAdded)
                playerMoney[1] += changevalue;
            else
                playerMoney[1] -= changevalue;
        }
        else
        {
            if (isAdded)
                playerMoney[0] += changevalue;
            else
                playerMoney[0] -= changevalue;
        }
    }
}

