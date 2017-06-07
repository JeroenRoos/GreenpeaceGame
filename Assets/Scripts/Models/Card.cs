using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
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

    public Card()
    {
        currentSectorConsequences = new SectorStatistics();
    }

    //method used for copying Card without reference
    public Card(Card card)
    {
        cardID = card.cardID;
        name = new string[2] { card.name[0], card.name[1] };
        description = new string[2] { card.description[0], card.description[1] };
        isGlobal = card.isGlobal;
        maximumIncrementsDone = card.maximumIncrementsDone;
        currentIncrementsDone = card.currentIncrementsDone;
        sectorConsequencesPerTurn = new SectorStatistics(card.sectorConsequencesPerTurn);
        moneyRewardPerTurn = card.moneyRewardPerTurn;
        currentSectorConsequences = new SectorStatistics(card.currentSectorConsequences);
        currentMoneyReward = card.currentMoneyReward;        
    }

    #region UpdateCardRewardsMethods
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
    #endregion

    #region UseCardMethods
    public void UseCardOnRegion(MapRegion r, GameStatistics gs)
    {
        foreach (RegionSector rs in r.sectors)
            rs.ImplementStatisticValues(currentSectorConsequences, true, gs.happiness);

        gs.ModifyMoney(currentMoneyReward, true);
    }

    public void UseCardOnCountry(List<MapRegion> regions, GameStatistics gs)
    {
        foreach (MapRegion r in regions)
        {
            foreach (RegionSector rs in r.sectors)
                rs.ImplementStatisticValues(currentSectorConsequences, true, gs.happiness);
        }

        gs.ModifyMoney(currentMoneyReward, true);
    }
    #endregion

    #region multiplayer
    public void SetCardReward(double[] cardValues)
    {
        currentMoneyReward = cardValues[10];
        currentSectorConsequences.SetPickedConsequencesMultiplayer(cardValues);
    }
    #endregion
}
