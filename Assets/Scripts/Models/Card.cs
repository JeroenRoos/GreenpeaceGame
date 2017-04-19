using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Card
{
    public string cardID { get; private set; }
    public string[] name { get; private set; }
    public string[] description { get; private set; }
    public bool isGlobal { get; private set; } //global or regional

    //limits the amount of times the card can increase the reward
    public double maximumIncrementsDone { get; private set; }
    public double currentIncrementsDone { get; private set; }

    //increment per turn
    public SectorStatistics sectorConsequencesPerTurn { get; private set; }
    public double moneyRewardPerTurn { get; private set; }

    //reward if redeemed
    public SectorStatistics currentSectorConsequences { get; private set; }
    public double currentMoneyReward { get; private set; }

    public Card() { }

    public void increaseCurrentRewards()
    {
        currentMoneyReward += moneyRewardPerTurn;

        currentSectorConsequences.ModifyIncome(sectorConsequencesPerTurn.income);
        currentSectorConsequences.ModifyHappiness(sectorConsequencesPerTurn.happiness);
        currentSectorConsequences.ModifyEcoAwareness(sectorConsequencesPerTurn.ecoAwareness);
        currentSectorConsequences.ModifyProsperity(sectorConsequencesPerTurn.prosperity);
        currentSectorConsequences.pollution.ChangeAirPollution(sectorConsequencesPerTurn.pollution.airPollution);
        currentSectorConsequences.pollution.ChangeNaturePollution(sectorConsequencesPerTurn.pollution.naturePollution);
        currentSectorConsequences.pollution.ChangeWaterPollution(sectorConsequencesPerTurn.pollution.waterPollution);
        currentSectorConsequences.pollution.ChangeAirPollutionMutation(sectorConsequencesPerTurn.pollution.airPollutionIncrease);
        currentSectorConsequences.pollution.ChangeNaturePollutionMutation(sectorConsequencesPerTurn.pollution.naturePollutionIncrease);
        currentSectorConsequences.pollution.ChangeWaterPollutionMutation(sectorConsequencesPerTurn.pollution.waterPollutionIncrease);

        currentIncrementsDone++;
    }
}
