using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.EventSystems;
using System.IO;

public class TestBot : MonoBehaviour
{

    GameController gameController;
    double currentCurrency;
    int turnCounter;
    public bool isEnabled;

    // Use this for initialization
    void Start ()
    {
        isEnabled = false;

        turnCounter = 0;
        gameController = GetComponent<GameController>();
        EventManager.ChangeMonth += CheckStatus;
        EventManager.ShowEvent  += EventAction;
    }

    // Month changed
    void CheckStatus()
    {
        if (isEnabled)
        {
            Debug.Log("BOT: Month changed");

            if (turnCounter % 10 == 0)
                getNationalStats();

            if (turnCounter % 25 == 0)
                getRegionalStats();


            currentCurrency = gameController.game.gameStatistics.money;
            bool checkActive;
            System.Random rnd = new System.Random();

            foreach (Region region in gameController.game.regions.Values)
            {
                checkActive = false;
                foreach (RegionAction a in region.actions)
                {
                    if (a.isActive)
                    {
                        Debug.Log("BOT ACTION : " + a.description[0] + "is active in " + region.name[0]);
                        checkActive = true;
                    }
                }

                if (!checkActive)
                {
                    Debug.Log("BOT: No action active in " + region.name[0]);

                    if (rnd.Next(1, 101) <= 25)
                    {
                        int action = rnd.Next(0, region.actions.Count);
                        RegionAction ra = region.actions[action];
                        Debug.Log("BOT: Start ACTION: " + ra.description[0] + " in Regio: " + region.name[0]);
                        ra.ActivateAction(gameController.game.currentYear, gameController.game.currentMonth);
                    }
                }
            }

            turnCounter++;
        }
    }

    // Event occured
    void EventAction()
    {
        if (isEnabled)
        {
            int chosenOption;

            foreach (GameEvent gameEvent in gameController.game.events)
            {
                if (gameEvent.isIdle)//isActive)
                {
                    Debug.Log("BOT: EVENT: " + gameEvent.name + " is ACTIVE in Regio: " + gameEvent.region.name[0]);
                    chosenOption = UnityEngine.Random.Range(0, gameEvent.choices.GetLength(1));
                    Debug.Log("BOT: Gekozen optie: (" + chosenOption + ") - " + gameEvent.choices[0, chosenOption] + " bij EVENT: " + gameEvent.name[0]);
                    gameEvent.SetPickedChoice(chosenOption, gameController.game);
                }
            }
        }

    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Return))
        {
            // toggle auto / manual end turn
            gameController.autoEndTurn = !gameController.autoEndTurn;
        }
	}

    void getNationalStats()
    {
        string[] arrMonths = new string[12]
            { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        int month = gameController.game.currentMonth;

        Debug.Log("\nNational statistics - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");
        Debug.Log("\nMoney: " + gameController.game.gameStatistics.money);
        Debug.Log("\nHappiness: " + gameController.game.gameStatistics.happiness);
        Debug.Log("\nEco-awareness: " + gameController.game.gameStatistics.ecoAwareness);
        Debug.Log("\nPollution: " + gameController.game.gameStatistics.pollution);
        Debug.Log("\nFossil Energy: " + gameController.game.gameStatistics.energy.fossilSource);
        Debug.Log("\nNuclear Energy: " + gameController.game.gameStatistics.energy.nuclearSource);
        Debug.Log("\nClean Energy: " + gameController.game.gameStatistics.energy.cleanSource);
        Debug.Log("\nPopulation: " + gameController.game.gameStatistics.population);
    }

    void getRegionalStats()
    {
        foreach (Region region in gameController.game.regions.Values)
        {
            string[] arrMonths = new string[12]
                { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int month = gameController.game.currentMonth;

            Debug.Log("\nRegional statistics " + region.name[0] + " - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");
            Debug.Log("\nIncome: " + region.statistics.income);
            Debug.Log("\nHappiness: " + region.statistics.happiness);
            Debug.Log("\nDonaties: " + region.statistics.donations);
            Debug.Log("\nEco-awareness: " + region.statistics.ecoAwareness);
            Debug.Log("\nProsperity: " + region.statistics.prosperity);
            Debug.Log("\nAverage Pollution: " + region.statistics.pollution.avgPullution);
            Debug.Log("\nWater Pollution: " + region.statistics.pollution.waterPollution);
            Debug.Log("\nWater Pollution Increase: " + region.statistics.pollution.waterPollutionIncrease);
            Debug.Log("\nAir Pollution: " + region.statistics.pollution.airPollution);
            Debug.Log("\nAir Pollution Increase: " + region.statistics.pollution.airPollutionIncrease);
            Debug.Log("\nNature Pollution: " + region.statistics.pollution.naturePollution);
            Debug.Log("\nNature Pollution Increase: " + region.statistics.pollution.naturePollutionIncrease);

            getSectorStats(region);
        }
    }

    void getSectorStats(Region region)
    {
        foreach (RegionSector sector in region.sectors.Values)
        {
            Debug.Log("\n" + sector.sectorName[0] + " from Region " + region.name[0]);
            Debug.Log("\nHappiness: " + sector.statistics.happiness);
            Debug.Log("\nProsperity: " + sector.statistics.prosperity);
            Debug.Log("\nEco-awareness: " + sector.statistics.ecoAwareness);
            Debug.Log("\nAir Pollution Contribution: " + sector.statistics.airPollutionContribution);
            Debug.Log("\nNature Pollution Contribution: " + sector.statistics.naturePollutionContribution);
            Debug.Log("\nWater Pollution Contribution: " + sector.statistics.waterPollutionContribution);
        }
    }
}
