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
        EventManager.ChangeMonth += CheckStatus;
        EventManager.ShowEvent  += EventAction;
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

            bool checkActive;
            System.Random rnd = new System.Random();

            foreach (Region region in gameController.game.regions.Values)
            {
                checkActive = false;
                foreach (RegionAction a in region.actions)
                {
                    if (a.isActive)
                    {
                        Debug.Log("ACTIVE ACTION : " + a.description[0] + " is active in " + region.name[0]);
                        checkActive = true;
                    }
                }
                if (!checkActive)
                {
                    if (rnd.Next(1, 101) <= 25)
                    {
                        int index = getLowestPollutionConsequenceAction(region);
                        doAction(region, index);
                    }
                }
            }
            turnCounter++;
        }
    }

    void showStatistics()
    {
        if (turnCounter % 12 == 0 || turnCounter == 359)
            getNationalStats();

        if (turnCounter % 12 == 0 || turnCounter == 359)
            getRegionalStats();
    }

    // Calculate 
    int getLowestPollutionConsequenceAction(Region region)
    {
        double tempPollutionSum = 0;
        int index = 0;
        int hightestIndex = 0;

        foreach (RegionAction a in region.actions)
        {
            double pollutionSum = 0;

            pollutionSum += a.consequences.pollution.airPollution;
            pollutionSum += a.consequences.pollution.waterPollution;
            pollutionSum += a.consequences.pollution.naturePollution;

            if (pollutionSum < tempPollutionSum)
            {
                tempPollutionSum = pollutionSum;
                hightestIndex = index;
            }

            index++;
        }

        if (hightestIndex == 0)
        {
            System.Random rnd = new System.Random();
            hightestIndex = rnd.Next(0, region.actions.Count);
        }

        return hightestIndex;
    }

    void doAction(Region region, int index)
    {
        //int action = rnd.Next(0, region.actions.Count);
        //RegionAction ra = region.actions[action];
        RegionAction ra = region.actions[index];
        Debug.Log("NEW ACTION: " + ra.description[0] + " in Regio: " + region.name[0]);
        ra.ActivateAction(gameController.game.currentYear, gameController.game.currentMonth);
    }
    #endregion

    #region Events
    // Event occured
    void EventAction()
    {
        if (isEnabled)
        {
            foreach (GameEvent gameEvent in gameController.game.events)
            {
                if (gameEvent.isIdle)
                {
                    printRegion(gameEvent);                    
                    int chosenOption = getLowestPollutionConsequenceEvent(gameEvent);//UnityEngine.Random.Range(0, gameEvent.choicesDutch.GetLength(0));
                    doChosenOption(gameEvent, chosenOption);
                }
            }
        }
    }

    void printRegion(GameEvent gameEvent)
    {
        bool breaking = false;

        foreach (Region region in gameController.game.regions.Values)
        {
            foreach (GameEvent ev in region.inProgressGameEvents)
            {
                if (ev == gameEvent)
                {
                    Debug.Log("ACTIVE EVENT: " + gameEvent.name + " is ACTIVE in Regio: " + region.name[0]);
                    breaking = true;
                    break;
                }
            }
            if (breaking)
                break;
        }
    }

    int getLowestPollutionConsequenceEvent(GameEvent gameEvent)
    {
        double tempPollutionSum = 0;
        int index = 0;
        int hightestIndex = 0;
        
        foreach (SectorStatistics stats in gameEvent.consequences)
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
            System.Random rnd = new System.Random();
            hightestIndex = rnd.Next(0, gameEvent.choicesDutch.Length);
        }

        return hightestIndex;
    }

    void doChosenOption(GameEvent gameEvent, int chosenOption)
    {
        Debug.Log("EVENT Gekozen optie: (" + chosenOption + ") - " + gameEvent.choicesDutch[chosenOption] + " bij EVENT: " + gameEvent.name);
        Debug.Log("Duur van gekozen optie: " + gameEvent.eventDuration[chosenOption]);
        gameEvent.SetPickedChoice(chosenOption, gameController.game);
    }
    #endregion

    #region National Statistics Printing
    void getNationalStats()
    {
        string[] arrMonths = new string[12]
            { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        int month = gameController.game.currentMonth;

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
    void getRegionalStats()
    {
        int i = 0;
        foreach (Region region in gameController.game.regions.Values)
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

    void getRegionalMainStats(Region region, int i)
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

    void getRegionalPollution(Region region, int i)
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

    void getSectorStats(Region region, int i)
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
