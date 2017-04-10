using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

/*regions order:
0 = noord
1 = oost
2 = west
3 = zuid
*/

public class GameController : MonoBehaviour
{
    public Game game;

    private EventObjectController eventObjectController;
    private UpdateUI updateUI;
    public GameObject noordNederland;
    public GameObject oostNederland;
    public GameObject westNederland;
    public GameObject zuidNederland;

    public Vector3[] afterActionPosition;

    public GameObject eventObject;

    // private float time;
    public bool autoSave = false;
    public bool autoEndTurn = false;

    // Use this for initialization
    void Start()
    {
        game = new Game();
        updateUI = GetComponent<UpdateUI>();
        eventObjectController = GetComponent<EventObjectController>();
        updateUI.LinkGame(game);

        afterActionPosition = new Vector3[3];
        afterActionPosition[0] = new Vector3( 5, 5, 0);
        afterActionPosition[1] = new Vector3( 5, 60, 0);
        afterActionPosition[1] = new Vector3( 5, 115, 0);

        // setup Region Controllers
        noordNederland.GetComponent<RegionController>().Init(this);
        oostNederland.GetComponent<RegionController>().Init(this);
        westNederland.GetComponent<RegionController>().Init(this);
        zuidNederland.GetComponent<RegionController>().Init(this);
        
        EventManager.ChangeMonth += NextTurn;
        EventManager.CallNewGame();
    }

    public void SaveGame()
    {
        GameContainer gameContainer = new GameContainer(game);
        gameContainer.Save();
    }

    public void LoadGame()
    {
        GameContainer gameContainer = GameContainer.Load();
        game = gameContainer.game;
    }

    // Update is called once per frame
    void Update () {
        if ((Input.GetKeyDown(KeyCode.Return) || autoEndTurn) && game.currentYear < 31 && updateUI.tutorialStep9)
        {
            EventManager.CallChangeMonth();
        }

        // Update the main screen UI (Icons and date)
        updateUIMainScreen();

        // Update the UI in popup screen
        if (updateUI.getPopupActive())
            updateUIPopups();

        // Update values in Tooltips for Icons in Main UI
        if (updateUI.getTooltipActive())
            updateUITooltips();

        UpdateRegionColor();
    }

    public void NextTurn()
    {
        bool isNewYear = game.UpdateCurrentMonthAndYear();

        game.ExecuteNewMonthMethods();

        UpdateRegionsPollutionInfluence();
        UpdateEvents();
        game.gameStatistics.UpdateRegionalAvgs(game);
        UpdateQuests();

        game.economyAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.income);
        game.pollutionAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.pollution);

        GenerateMonthlyReport();
        if (isNewYear)
            GenerateYearlyReport();

        if (!updateUI.tutorialNextTurnDone)
            updateUI.tutorialNextTurnDone = true;

        if (autoSave)
            SaveGame();
    }

    private void GenerateMonthlyReport()
    {
        /* montly report generating
         * updateui.StatisticsChangeButton(game.monthlyReport.regions, g.m.oldincome. g.m.oldhappines, etc., etc.)
         * updateui.completedEventsButton(game.monthlyReport.regions, game.monthlyReport.completedEvents)
         * updateui.newEventsButton(game.monthlyReport.regions, game.monthlyReport.newEvents)
         * updateui.completedActionsButton(game.monthlyReport.regions, game.monthlyReport.completedActions)
         */

        /*
         * EVENTS + ACTIES
         * - Laat zien dat hij completed is
         * - Laat tijdelijke consequenties zijn voor het aantal maanden
         */ 

        updateUI.btnAfterActionReportStats.gameObject.SetActive(true);
        updateUI.InitAfterActionStats();
        updateUI.btnAfterActionReportStats.gameObject.transform.position = afterActionPosition[0];

        int index = 1;
        if (checkActionAndEventEmpty())
        {
            updateUI.btnAfterActionReportCompleted.gameObject.SetActive(true);
            updateUI.initAFterActionCompleted();
            updateUI.btnAfterActionReportCompleted.gameObject.transform.position = afterActionPosition[index];
            index++;
        }
        else
        {
            updateUI.btnAfterActionReportCompleted.gameObject.SetActive(false);
        }

        game.monthlyReport.UpdateStatistics(game.regions);
    }

    private bool checkActionAndEventEmpty()
    {
        for (int i = 0; i < game.monthlyReport.completedActions.Length; i++)
        {
            if (game.monthlyReport.completedActions[i].Count != 0)
                return true;
        }

        for (int j = 0; j < game.monthlyReport.completedEvents.Length; j++)
        {
            if (game.monthlyReport.completedEvents[j].Count != 0)
                return true;
        }

        return false;
    }

    private void GenerateYearlyReport()
    {
        /* yearly report generating
         * updateui.StatisticsChangeButton(game.yearlyReport.regions, g.y.oldincome. g.y.oldhappines, etc., etc.)
         * updateui.completedEventsButton(game.yearlyReport.regions, game.yearlyReport.completedEvents) 
         * updateui.newEventsButton(game.yearlyReport.regions, game.yearlyReport.newEvents)
         * completed/newEvents in 1 button?
         * updateui.completedActionsButton(game.yearlyReport.regions, game.yearlyReport.completedActions)
         */
        game.yearlyReport.UpdateStatistics(game.regions);

    }

    private void UpdateQuests()
    {
        StartNewQuests();
        CompleteActiveQuests();
    }

    private void CompleteActiveQuests()
    {
        foreach (Quest quest in game.quests)
        {
            //only check active quests
            if (quest.isActive)
            {
                //National or regional
                if (quest.questLocation == "National")
                {
                    //checks if conditions are met, (needs seperate "if" statement)
                    if (quest.NationalCompleteConditionsMet(game.gameStatistics))
                    {
                        game.gameStatistics.ModifyMoney(quest.questMoneyReward, true);
                        Debug.Log("Quest completed:" + quest.description[0]);
                        quest.CompleteQuest();
                    }
                }
                else
                {
                    foreach (Region r in game.regions)
                    {
                        //find quest region
                        if (r.name[0] == quest.questLocation)
                        {
                            //checks if conditions are met, (needs seperate "if" statement)
                            if (quest.RegionalCompleteConditionsMet(r.statistics))
                            {
                                game.gameStatistics.ModifyMoney(quest.questMoneyReward, true);
                                Debug.Log("Quest completed:" + quest.description[0]);
                                quest.CompleteQuest();
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    private void StartNewQuests()
    {
        foreach (Quest quest in game.quests)
        {
            if (quest.startYear == game.currentYear && quest.startMonth == game.currentMonth)
                quest.StartQuest();
        }
    }

    private void UpdateEvents()
    {
        int activeCount = game.getActiveEventCount();
        int eventChance = 100;
        int eventChanceReduction = 100;

        //temp ugly code
        if (game.currentYear >= 2)
            eventChanceReduction -= 30;
        if (game.currentYear >= 5)
            eventChanceReduction -= 20;
        if (game.currentYear >= 10)
            eventChanceReduction -= 15;
        if (game.currentYear >= 20)
            eventChanceReduction -= 10;

        while (game.rnd.Next(1, 101) <= eventChance && activeCount < 4)
        {
            if (game.PossibleEventCount() > 0 && game.GetPossibleRegionsCount() > 0)
            {
                Region pickedRegion = game.PickEventRegion();
                GameEvent pickedEvent = game.GetPickedEvent(pickedRegion);
                pickedEvent.pickEventSector(game.rnd);
                pickedEvent.StartEvent(game.currentYear, game.currentMonth);
                pickedRegion.AddGameEvent(pickedEvent);
                game.AddNewEventToMonthlyReport(pickedRegion, pickedEvent);

                GameObject eventInstance = GameController.Instantiate(eventObject);
                eventInstance.GetComponent<EventObjectController>().Init(this, pickedRegion, pickedEvent);
            }

            eventChance -= eventChanceReduction;
        }

        if (activeCount < 1)
        {
        }
    }
    
    private void UpdateRegionsPollutionInfluence()
    {
        game.gameStatistics.UpdateRegionalAvgs(game);

        foreach (Region region in game.regions)
        {
            double pollutionDifference = game.gameStatistics.pollution - region.statistics.avgPollution;
            double pollutionChangeValue = pollutionDifference * 0.3 / 12;
            //Debug.Log("Region: " + region + " diff with avg: " + pollutionDifference + " changevalue: " + pollutionChangeValue);
            foreach (RegionSector regionSector in region.sectors)
            {
                regionSector.statistics.pollution.ChangeAirPollution(pollutionChangeValue);
                regionSector.statistics.pollution.ChangeNaturePollution(pollutionChangeValue);
                regionSector.statistics.pollution.ChangeWaterPollution(pollutionChangeValue);
            }
            region.statistics.UpdateSectorAvgs(region);
        }

        game.gameStatistics.UpdateRegionalAvgs(game);
    }

    private void updateUIMainScreen()
    {
        // Update Text and Color values in main UI
        updateUI.updateDate(game.currentMonth, game.currentYear);
        updateUI.updateMoney(game.gameStatistics.money);
        updateUI.updatePopulation(game.gameStatistics.population);
        updateUI.updateAwarness(game.gameStatistics.ecoAwareness);
        updateUI.updatePollution(game.gameStatistics.pollution);
        updateUI.updateEnergy(game.gameStatistics.energy.cleanSource);
        updateUI.updateProsperity(game.gameStatistics.prosperity);
        updateUI.updateHappiness(game.gameStatistics.happiness);       
    }


    private void updateUITooltips()
    {
        if (updateUI.getBtnMoneyHover())
            updateUI.updateMoneyTooltip(game.gameStatistics.income);

        if (updateUI.getBtnHappinessHover())
            updateHappiness();

        if (updateUI.getBtnAwarenessHover())
            updateAwareness();

        if (updateUI.getBtnPollutionHover())
            updatePollution();

        if (updateUI.getBtnProsperityHover())
            updateProsperity();

        if (updateUI.getBtnEnergyHover())
            updateUI.updateEnergyTooltip(game.gameStatistics.energy.cleanSource,
            game.gameStatistics.energy.fossilSource, game.gameStatistics.energy.nuclearSource);
    }

    private void updateHappiness()
    {
        for (int j = 0; j < game.regions.Count; j++)
        {
            updateUI.updateHappinessTooltip(game.regions[j].statistics.happiness, j);
        }
    }

    private void updateAwareness()
    {
        for (int j = 0; j < game.regions.Count; j++)
        {
            updateUI.updateAwarnessTooltip(game.regions[j].statistics.ecoAwareness, j);
        }
    }

    private void updatePollution()
    {
        for (int j = 0; j < game.regions.Count; j++)
        {
            updateUI.updatePollutionTooltip(game.regions[j].statistics.avgPollution, j);
        }
    }

    private void updateProsperity()
    {
        for (int j = 0; j < game.regions.Count; j++)
        {
            updateUI.updateProsperityTooltip(game.regions[j].statistics.prosperity, j);
        }
    }

    private void updateUIPopups()
    {
        //eventObjectController.disableTooltipAndOptions();

        if (updateUI.canvasOrganizationPopup.gameObject.activeSelf)
            updateUIOrganizationScreen();

        if (updateUI.canvasRegioPopup.gameObject.activeSelf)
            updateUIRegioScreen();

        if (updateUI.canvasTimelinePopup.gameObject.activeSelf)
            updateUITimelineScreen();
    }

    private void updateUIOrganizationScreen()
    {
        int i = 0;
        foreach (Region region in game.regions)
        {
            // Send the income for each region, use i to determine the region
            updateUI.updateOrganizationScreenUI(region.statistics.income * 12, i, game.gameStatistics.money);
            i++;            
        }
    }

    private void updateUIRegioScreen()
    {

    }

    private void updateUITimelineScreen()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void OnRegionClick(GameObject region)
    {
        int pickedRegion = 0;
        switch (region.name)
        {
            case "Noord Nederland":
                pickedRegion = 0;
                break;
            case "Oost Nederland":
                pickedRegion = 1;
                break;
            case "West Nederland":
                pickedRegion = 2;
                break;
            case "Zuid Nederland":
                pickedRegion = 3;
                break;
        }

        Region regionModel = game.regions[pickedRegion];
        updateUI.regionClick(regionModel);
    }

    void CheckEndOfGame()
    {
        if (game.currentYear == 2050)
        {
            autoEndTurn = false;
            if(game.gameStatistics.pollution < 20)
            {
                // you did it!
            }
            else
            {
                // objective failed.
            }
        }
    }

    // update kleur van regio
    public void UpdateRegionColor()
    {
        noordNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green, 
                Color.red, 
                (float)game.regions[0].statistics.avgPollution / 100
            );

        oostNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions[1].statistics.avgPollution / 100
            );

        westNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions[2].statistics.avgPollution / 100
            );

        zuidNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions[3].statistics.avgPollution / 100
            );
    }

    public bool getActivePopup()
    {
        return updateUI.getPopupActive();
    }
}
