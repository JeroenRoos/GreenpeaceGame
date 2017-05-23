using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.EventSystems;

public class TestBot : MonoBehaviour
{

    GameController gameController;
    //double currentCurrency;
    int turnCounter;
    public bool isEnabled;
    public string[] playstyles;
    public string currentPlaystyle;

    #region Double Variables
    double nationalMoney;
    double nationalHappiness;
    double nationalEcoAwareness;
    double nationalPollution;
    double nationalFossil;
    double nationalClean;
    double nationalNuclear;
    double nationalPopulation;

    double[] regionalIncome;
    double[] regionalHappiness;
    double[] regionalEcoAwareness;
    double[] regionalProsperity;

    double[] regionalAvgPollution;
    double[] regionalWaterPollution;
    double[] regionalWaterPollutionIncrease;
    double[] regionalAirPollution;
    double[] regionalAirPollutionIncrease;
    double[] regionalNaturePollution;
    double[] regionalNaturePollutionIncrease;

    double[,] regionalSectorHappiness;
    double[,] regionalSectorProsperity;
    double[,] regionalSectorEcoAwareness;
    double[,] regionalSectorAirPollutionContribution;
    double[,] regionalSectorNaturePollutionContribution;
    double[,] regionalSectorWaterPollutionContribution;
    #endregion

    // Use this for initialization
    void Start ()
    {
        #region Init Double Variables
        regionalSectorHappiness = new double[4, 3];
        regionalSectorProsperity = new double[4, 3];
        regionalSectorEcoAwareness = new double[4, 3];
        regionalSectorAirPollutionContribution = new double[4, 3];
        regionalSectorNaturePollutionContribution = new double[4, 3];
        regionalSectorWaterPollutionContribution = new double[4, 3];

        regionalAvgPollution = new double[4];
        regionalWaterPollution = new double[4];
        regionalWaterPollutionIncrease = new double[4];
        regionalAirPollution = new double[4];
        regionalAirPollutionIncrease = new double[4];
        regionalNaturePollution = new double[4];
        regionalNaturePollutionIncrease = new double[4];

        regionalIncome = new double[4];
        regionalHappiness = new double[4];
        regionalEcoAwareness = new double[4];
        regionalProsperity = new double[4];

        nationalMoney = 0;
        nationalHappiness = 0;
        nationalEcoAwareness = 0;
        nationalPollution = 0;
        nationalFossil = 0;
        nationalNuclear = 0;
        nationalClean = 0;
        nationalPopulation = 0;
        #endregion

        isEnabled = false; 

        Debug.Log(System.DateTime.Now);
        turnCounter = 0;
        gameController = GetComponent<GameController>();
        EventManager.NewGame += CheckStatus;
        EventManager.ChangeMonth += CheckStatus;

        playstyles = new string[] { "Random", "IncomeFocused", "PollutionFocused" };
        currentPlaystyle = playstyles[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Return))
        {
            // toggle auto / manual end turn
            gameController.autoEndTurn = !gameController.autoEndTurn;
        }
    }

    #region Actions 
    // Month changed
    void CheckStatus()
    {
        if (isEnabled)
        {
            Debug.Log("TURN: " + turnCounter);

            showStatistics();

            foreach (MapRegion region in gameController.game.regions)
            {
                DoEvents(region);

                bool isAvailable = CheckIfActionAvailable(region);
                if (isAvailable)
                {
                    if(currentPlaystyle == playstyles[0])
                    {
                        RandomPlaystyle(region);
                    }

                    else if (currentPlaystyle == playstyles[1])
                    {
                        IncomePlaystyle(region);
                    }

                    else if (currentPlaystyle == playstyles[2])
                    {
                        PollutionPlaystyle(region);
                    }
                }
            }
            turnCounter++;
        }
    }

    //do events based on most expensive choice
    private void DoEvents(MapRegion region)
    {
        foreach (GameEvent gameEvent in region.inProgressGameEvents)
        {
            if (gameEvent.isIdle)
            {
                int pickedOption = 0;

                for (int i = 1; i < gameEvent.choicesDutch.Length; i++)
                {
                    if (gameEvent.afterInvestmentEventChoiceMoneyCost[i] < gameController.game.gameStatistics.money &&
                        gameEvent.afterInvestmentEventChoiceMoneyCost[i] > gameEvent.afterInvestmentEventChoiceMoneyCost[pickedOption])
                        pickedOption = i;
                }

                Debug.Log("check");
                doChosenOption(region, gameEvent, pickedOption);
            }
        }
    }

    private bool CheckIfActionAvailable(MapRegion region)
    {
        foreach (RegionAction a in region.actions)
        {
            if (a.isActive)
            {
                Debug.Log("ACTIVE ACTION : " + a.description[0] + " is active in " + region.name[0]);
                return false;
            }
        }

        return true;
    }

    private void showStatistics()
    {
        if (turnCounter % 12 == 0 || turnCounter == 359)
            getNationalStats();
    }

    private void RandomPlaystyle(MapRegion region)
    {
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;
        List<RegionAction> tempList = new List<RegionAction>();
        bool actionFound = false;
        foreach (RegionAction ra in region.actions)
        {
            if ((ra.afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (ra.lastCompleted + ra.actionCooldown <= currentMonth || ra.lastCompleted == 0) &&
                !(ra.isUnique && ra.lastCompleted > 0))
            {
                tempList.Add(ra);
                actionFound = true;
            }
        }
        if (actionFound)
            doAction(region, tempList[gameController.game.rnd.Next(0, tempList.Count)]);
    }

    private void IncomePlaystyle(MapRegion region)
    {
        bool actionStarted = false;

        //Regions that fall behind in pollution
        if ((nationalPollution > 60 || nationalPollution < region.statistics.avgPollution * 0.8) && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if ((nationalPollution > 60 || nationalPollution < region.statistics.avgPollution * 0.8) && !actionStarted)
            actionStarted = getHighestEcoAwarenessConsequenceAction(region);

        if (region.statistics.income < 1500 && !actionStarted)
            actionStarted = getHighestIncomeConsequenceAction(region);

        if (region.statistics.prosperity < 20 && !actionStarted)
            actionStarted = getHighestProsperityConsequenceAction(region);

        if (region.statistics.ecoAwareness < 50 && !actionStarted)
            actionStarted = getHighestEcoAwarenessConsequenceAction(region);

        if (region.statistics.avgPollution > 0 && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if (region.statistics.happiness > 0 && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if (!actionStarted)
            actionStarted = getHighestMoneyAction(region);
    }

    private void PollutionPlaystyle(MapRegion region)
    {
        bool actionStarted = false;

        if ((nationalPollution > 60 || nationalPollution < region.statistics.avgPollution * 0.8) && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if ((nationalPollution > 60 || nationalPollution < region.statistics.avgPollution * 0.8) && !actionStarted)
            actionStarted = getHighestEcoAwarenessConsequenceAction(region);
        
        if ((region.statistics.income < 400 && !actionStarted || nationalPollution > region.statistics.avgPollution * 0.8) && !actionStarted)
            actionStarted = getHighestIncomeConsequenceAction(region);

        if (region.statistics.avgPollution > 0 && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if (region.statistics.ecoAwareness < 50 && !actionStarted)
            actionStarted = getHighestEcoAwarenessConsequenceAction(region);

        if (region.statistics.prosperity < 20 && !actionStarted)
            actionStarted = getHighestProsperityConsequenceAction(region);

        if (region.statistics.income < 1500 && !actionStarted)
            actionStarted = getHighestIncomeConsequenceAction(region);

        if (region.statistics.happiness > 0 && !actionStarted)
            actionStarted = getLowestPollutionConsequenceAction(region);

        if (!actionStarted)
            actionStarted = getHighestMoneyAction(region);
    }

    private bool getHighestMoneyAction(MapRegion region)
    {
        int highestMoneyIndex = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if ((region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0) &&
                (region.actions[i].actionMoneyReward > highestMoneyIndex))
            {
                highestMoneyIndex = i;
                actionFound = true;
            }
        }

        if (actionFound)
            doAction(region, region.actions[highestMoneyIndex]);

        return actionFound;
    }

    private bool getHighestIncomeConsequenceAction(MapRegion region)
    {
        int highestIncomeIndex = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if ((region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0) &&
                (region.actions[i].afterInvestmentConsequences.income > highestIncomeIndex))
            {
                highestIncomeIndex = i;
                actionFound = true;
            }
        }

        if (actionFound)
            doAction(region, region.actions[highestIncomeIndex]);

        return actionFound;
    }

    private bool getHighestProsperityConsequenceAction(MapRegion region)
    {
        int highestProsperityIndex = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if ((region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0) &&
                (region.actions[i].afterInvestmentConsequences.prosperity > highestProsperityIndex))
            {
                highestProsperityIndex = i;
                actionFound = true;
            }
        }

        if (actionFound)
            doAction(region, region.actions[highestProsperityIndex]);

        return actionFound;
    }
    
    private bool getHighestEcoAwarenessConsequenceAction(MapRegion region)
    {
        int highestEcoAwarenessIndex = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if ((region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0) &&
                (region.actions[i].afterInvestmentConsequences.ecoAwareness > highestEcoAwarenessIndex))
            {
                highestEcoAwarenessIndex = i;
                actionFound = true;
            }
        }

        if (actionFound)
            doAction(region, region.actions[highestEcoAwarenessIndex]);

        return actionFound;
    }

    private bool getHighestHappinessConsequencesAction(MapRegion region)
    {
        int highestHappinessIndex = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if ((region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money) &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0) &&
                (region.actions[i].afterInvestmentConsequences.happiness > highestHappinessIndex))
            {
                highestHappinessIndex = i;
                actionFound = true;
            }
        }

        if (actionFound)
            doAction(region, region.actions[highestHappinessIndex]);

        return actionFound;
    }

    // Calculate 
    private bool getLowestPollutionConsequenceAction(MapRegion region)
    {
        int hightestIndex = 0;
        double tempPollutionSum = 0;
        bool actionFound = false;
        int currentMonth = gameController.game.currentYear * 12 + gameController.game.currentMonth;

        for (int i = 0; i < region.actions.Count; i++)
        {
            if (region.actions[i].afterInvestmentActionMoneyCost * 3 < gameController.game.gameStatistics.money &&
                (region.actions[i].lastCompleted + region.actions[i].actionCooldown <= currentMonth || region.actions[i].lastCompleted == 0) &&
                !(region.actions[i].isUnique && region.actions[i].lastCompleted > 0))
            {
                double pollutionSum = 0;

                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.airPollution;
                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.waterPollution;
                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.naturePollution;

                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.airPollutionIncrease;
                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.naturePollutionIncrease;
                pollutionSum += region.actions[i].afterInvestmentConsequences.pollution.waterPollutionIncrease;

                if (pollutionSum < tempPollutionSum)
                {
                    tempPollutionSum = pollutionSum;
                    hightestIndex = i;
                    actionFound = true;
                }
                else if (pollutionSum == tempPollutionSum && gameController.game.rnd.Next(0,2) == 0)
                {
                    tempPollutionSum = pollutionSum;
                    hightestIndex = i;
                    actionFound = true;
                }
            }
        }

        if (actionFound)
            doAction(region, region.actions[hightestIndex]);

        return actionFound;
    }

    private void doAction(MapRegion region, RegionAction ra)
    {
        Debug.Log("NEW ACTION: " + ra.description[0] + " in Regio: " + region.name[0]);
        region.StartAction(ra, gameController.game, new bool[] { true, true, true });
    }
    #endregion
    
    private int getLowestPollutionConsequenceEvent(GameEvent gameEvent)
    {
        double tempPollutionSum = 0;
        int index = 0;
        int hightestIndex = 0;
        
        foreach (SectorStatistics stats in gameEvent.afterInvestmentConsequences)
        {
            double pollutionSum = 0;

            pollutionSum += stats.pollution.airPollution;
            pollutionSum += stats.pollution.waterPollution;
            pollutionSum += stats.pollution.naturePollution;

            if (pollutionSum < tempPollutionSum)
            {
                tempPollutionSum = pollutionSum;
                hightestIndex = index;
            }

            index++;
        }

        if (hightestIndex == 0)
        {
            hightestIndex = gameController.game.rnd.Next(0, gameEvent.choicesDutch.Length);
        }

        return hightestIndex;
    }


    void doChosenOption(MapRegion region, GameEvent gameEvent, int chosenOption)
    {
        Debug.Log("EVENT Gekozen optie: (" + chosenOption + ") - " + gameEvent.choicesDutch[chosenOption] + " bij EVENT: " + gameEvent.name);
        Debug.Log("Duur van gekozen optie: " + gameEvent.eventDuration[chosenOption]);
        gameEvent.SetPickedChoice(chosenOption, gameController.game, region);
    }

    #region National Statistics Printing
    private void getNationalStats()
    {
        string[] arrMonths = new string[12]
            { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        int month = gameController.game.currentMonth - 1;

        Debug.Log("\nNational statistics CHANGE - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");

        if (nationalMoney != gameController.game.gameStatistics.money)
        {
            Debug.Log("Money: " + gameController.game.gameStatistics.money);
            nationalMoney = gameController.game.gameStatistics.money;
        }
        if (nationalHappiness != gameController.game.gameStatistics.happiness)
        {
            Debug.Log("Happiness: " + gameController.game.gameStatistics.happiness);
            nationalHappiness = gameController.game.gameStatistics.happiness;
        }
        if (nationalEcoAwareness != gameController.game.gameStatistics.ecoAwareness)
        {
            Debug.Log("Eco-awareness: " + gameController.game.gameStatistics.ecoAwareness);
            nationalEcoAwareness = gameController.game.gameStatistics.ecoAwareness;
        }
        if (nationalPollution != gameController.game.gameStatistics.pollution)
        {
            Debug.Log("Pollution: " + gameController.game.gameStatistics.pollution);
            nationalPollution = gameController.game.gameStatistics.pollution;
        }
        if (nationalFossil != gameController.game.gameStatistics.energy.fossilSource)
        {
            Debug.Log("Fossil Energy: " + gameController.game.gameStatistics.energy.fossilSource);
            nationalFossil = gameController.game.gameStatistics.energy.fossilSource;
        }
        if (nationalNuclear != gameController.game.gameStatistics.energy.nuclearSource)
        {
            Debug.Log("Nuclear Energy: " + gameController.game.gameStatistics.energy.nuclearSource);
            nationalNuclear = gameController.game.gameStatistics.energy.nuclearSource;
        }
        if (nationalClean != gameController.game.gameStatistics.energy.cleanSource)
        {
            Debug.Log("Clean Energy: " + gameController.game.gameStatistics.energy.cleanSource);
            nationalClean = gameController.game.gameStatistics.energy.cleanSource;
        }
        if (nationalPopulation != gameController.game.gameStatistics.population)
        {
            Debug.Log("Population: " + gameController.game.gameStatistics.population);
            nationalPopulation = gameController.game.gameStatistics.population;
        }
    }
    #endregion

    #region Regional Statistics Printing
    private void getRegionalStats()
    {
        int i = 0;
        foreach (MapRegion region in gameController.game.regions)
        {
            string[] arrMonths = new string[12]
                { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int month = gameController.game.currentMonth;

            Debug.Log("\nRegional statistics CHANGE " + region.name[0] + " - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");

            getRegionalMainStats(region, i);
            getRegionalPollution(region, i);
            getSectorStats(region, i);

            i++;
        }
    }

    private void getRegionalMainStats(MapRegion region, int i)
    {
        if (regionalIncome[i] != region.statistics.income)
        {
            Debug.Log("Income: " + region.statistics.income);
            regionalIncome[i] = region.statistics.income;
        }
        if (regionalHappiness[i] != region.statistics.happiness)
        {
            Debug.Log("Happiness: " + region.statistics.happiness);
            regionalHappiness[i] = region.statistics.happiness;
        }
        if (regionalEcoAwareness[i] != region.statistics.ecoAwareness)
        {
            Debug.Log("Eco-awareness: " + region.statistics.ecoAwareness);
            regionalEcoAwareness[i] = region.statistics.ecoAwareness;
        }
        if (regionalProsperity[i] != region.statistics.prosperity)
        {
            Debug.Log("Prosperity: " + region.statistics.prosperity);
            regionalProsperity[i] = region.statistics.prosperity;
        }
    }

    private void getRegionalPollution(MapRegion region, int i)
    {
        if (regionalAvgPollution[i] != region.statistics.avgPollution)
        {
            Debug.Log("Average Pollution: " + region.statistics.avgPollution);
            regionalAvgPollution[i] = region.statistics.avgPollution;
        }
        if (regionalWaterPollution[i] != region.statistics.avgWaterPollution)
        {
            Debug.Log("Water Pollution: " + region.statistics.avgWaterPollution);
            regionalWaterPollution[i] = region.statistics.avgWaterPollution;
        }
        if (regionalWaterPollutionIncrease[i] != region.statistics.avgWaterPollutionIncrease)
        {
            Debug.Log("Water Pollution Increase: " + region.statistics.avgWaterPollutionIncrease);
            regionalWaterPollutionIncrease[i] = region.statistics.avgWaterPollutionIncrease;
        }
        if (regionalAirPollution[i] != region.statistics.avgAirPollution)
        {
            Debug.Log("Air Pollution: " + region.statistics.avgAirPollution);
            regionalAirPollution[i] = region.statistics.avgAirPollution;
        }
        if (regionalAirPollutionIncrease[i] != region.statistics.avgAirPollutionIncrease)
        {
            Debug.Log("Air Pollution Increase: " + region.statistics.avgAirPollutionIncrease);
            regionalAirPollutionIncrease[i] = region.statistics.avgAirPollutionIncrease;
        }
        if (regionalNaturePollution[i] != region.statistics.avgNaturePollution)
        {
            Debug.Log("Nature Pollution: " + region.statistics.avgNaturePollution);
            regionalNaturePollution[i] = region.statistics.avgNaturePollution;
        }
        if (regionalNaturePollutionIncrease[i] != region.statistics.avgNaturePollutionIncrease)
        {
            Debug.Log("Nature Pollution Increase: " + region.statistics.avgNaturePollutionIncrease);
            regionalNaturePollutionIncrease[i] = region.statistics.avgNaturePollutionIncrease;
        }
    }

    private void getSectorStats(MapRegion region, int i)
    {
        int j = 0;
        foreach (RegionSector sector in region.sectors)
        {
            Debug.Log("\nCHANGE in " + sector.sectorName[0] + " from Region " + region.name[0]);

            if (regionalSectorHappiness[i, j] != sector.statistics.happiness)
            {
                Debug.Log("Happiness: " + sector.statistics.happiness);
                regionalSectorHappiness[i, j] = sector.statistics.happiness;
            }
            if (regionalSectorProsperity[i, j] != sector.statistics.prosperity)
            {
                Debug.Log("Prosperity: " + sector.statistics.prosperity);
                regionalSectorProsperity[i, j] = sector.statistics.prosperity;
            }
            if (regionalSectorEcoAwareness[i, j] != sector.statistics.ecoAwareness)
            {
                Debug.Log("Eco-awareness: " + sector.statistics.ecoAwareness);
                regionalSectorEcoAwareness[i, j] = sector.statistics.ecoAwareness;
            }
            if (regionalSectorAirPollutionContribution[i, j] != sector.statistics.pollution.airPollution)
            {
                Debug.Log("Air Pollution Contribution: " + sector.statistics.pollution.airPollution);
                regionalSectorAirPollutionContribution[i, j] = sector.statistics.pollution.airPollution;
            }
            if (regionalSectorNaturePollutionContribution[i, j] != sector.statistics.pollution.naturePollution)
            {
                Debug.Log("Nature Pollution Contribution: " + sector.statistics.pollution.naturePollution);
                regionalSectorNaturePollutionContribution[i, j] = sector.statistics.pollution.naturePollution;
            }
            if (regionalSectorWaterPollutionContribution[i, j] != sector.statistics.pollution.waterPollution)
            {
                Debug.Log("Water Pollution Contribution: " + sector.statistics.pollution.waterPollution);
                regionalSectorWaterPollutionContribution[i, j] = sector.statistics.pollution.waterPollution;
            }
            
            j++;
        }
    }

    #endregion
}