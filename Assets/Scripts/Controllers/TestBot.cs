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

        Debug.Log("TURN: " + turnCounter);

        if (isEnabled)
        {
            if (turnCounter % 12 == 0)
                getNationalStats();

            if (turnCounter % 12 == 0)
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
                        Debug.Log("ACTIVE ACTION : " + a.description[0] + " is active in " + region.name[0]);
                        checkActive = true;
                    }
                }
                if (!checkActive)
                {
                    if (rnd.Next(1, 101) <= 25)
                    {
                        int action = rnd.Next(0, region.actions.Count);
                        RegionAction ra = region.actions[action];
                        Debug.Log("NEW ACTION: " + ra.description[0] + " in Regio: " + region.name[0]);
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
                    Debug.Log("ACTIVE EVENT: " + gameEvent.name + " is ACTIVE in Regio: " + gameEvent.region.name[0]);
                    chosenOption = UnityEngine.Random.Range(0, gameEvent.choicesDutch.GetLength(0));
                    Debug.Log("EVENT Gekozen optie: (" + chosenOption + ") - " + gameEvent.choicesDutch[chosenOption] + " bij EVENT: " + gameEvent.name);
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

        Debug.Log("\nNational statistics CHANGE - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");
        Debug.Log("Money: " + gameController.game.gameStatistics.money);
        Debug.Log("Happiness: " + gameController.game.gameStatistics.happiness);
        Debug.Log("Eco-awareness: " + gameController.game.gameStatistics.ecoAwareness);
        Debug.Log("Pollution: " + gameController.game.gameStatistics.pollution);
        Debug.Log("Fossil Energy: " + gameController.game.gameStatistics.energy.fossilSource);
        Debug.Log("Nuclear Energy: " + gameController.game.gameStatistics.energy.nuclearSource);
        Debug.Log("Clean Energy: " + gameController.game.gameStatistics.energy.cleanSource);
        Debug.Log("Population: " + gameController.game.gameStatistics.population);
    }

    void getRegionalStats()
    {
        int i = 0;
        foreach (Region region in gameController.game.regions.Values)
        {
            string[] arrMonths = new string[12]
                { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            int month = gameController.game.currentMonth;

            Debug.Log("\nRegional statistics CHANGE " + region.name[0] + " - turn: " + turnCounter + " (" + arrMonths[month] + " - " + (gameController.game.currentYear + 2019) + ")");
            Debug.Log("Income: " + region.statistics.income);
            Debug.Log("Happiness: " + region.statistics.happiness);
            Debug.Log("Donaties: " + region.statistics.donations);
            Debug.Log("Eco-awareness: " + region.statistics.ecoAwareness);
            Debug.Log("Prosperity: " + region.statistics.prosperity);
            Debug.Log("Average Pollution: " + region.statistics.pollution.avgPullution);
            Debug.Log("Water Pollution: " + region.statistics.pollution.waterPollution);
            Debug.Log("Water Pollution Increase: " + region.statistics.pollution.waterPollutionIncrease);
            Debug.Log("Air Pollution: " + region.statistics.pollution.airPollution);
            Debug.Log("Air Pollution Increase: " + region.statistics.pollution.airPollutionIncrease);
            Debug.Log("Nature Pollution: " + region.statistics.pollution.naturePollution);
            Debug.Log("Nature Pollution Increase: " + region.statistics.pollution.naturePollutionIncrease);

            getSectorStats(region);

        }
    }

    void getSectorStats(Region region)
    {
        foreach (RegionSector sector in region.sectors)
        {
            Debug.Log("\nCHANGE in " + sector.sectorName[0] + " from Region " + region.name[0]);
            Debug.Log("Happiness: " + sector.statistics.happiness);
            Debug.Log("Prosperity: " + sector.statistics.prosperity);
            Debug.Log("Eco-awareness: " + sector.statistics.ecoAwareness);
            Debug.Log("Air Pollution Contribution: " + sector.statistics.airPollutionContribution);
            Debug.Log("Nature Pollution Contribution: " + sector.statistics.naturePollutionContribution);
            Debug.Log("Water Pollution Contribution: " + sector.statistics.waterPollutionContribution);
        }
    }
}
