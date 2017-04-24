using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.Analytics;

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
    public Button MonthlyReportButon;
    public Button YearlyReportButton;
    public Button CompletedButton;
    private UpdateUI updateUI;
    public GameObject noordNederland;
    public GameObject oostNederland;
    public GameObject westNederland;
    public GameObject zuidNederland;

    public Vector3[] afterActionPosition;

    public GameObject eventObject;

    // private float time;
    public bool autoSave = true;
    public bool autoEndTurn = false;
    public bool OnQuitTrackDataSet = false;

    // Use this for initialization
    void Start()
    {
        autoSave = true;
        if (!ApplicationModel.loadGame)
        {
            game = new Game();

            LoadRegions();
            LoadRegionActions();
            LoadBuildings();
            LoadGameEvents();
            LoadQuests();
            LoadBuildings();
            LoadCards();
            game.gameStatistics.UpdateRegionalAvgs(game);

            //set reports
            game.monthlyReport.UpdateStatistics(game.regions);
            game.yearlyReport.UpdateStatistics(game.regions);

            UpdateTimeline();

            /*foreach (Region region in game.regions)
            {
                foreach (RegionSector sector in region.sectors)
                {
                    sector.TempMethod();
                    sector.statistics.pollution.CalculateAvgPollution();
                }
                region.statistics.UpdateSectorAvgs(region);
            }

            SaveRegions();
            SaveRegionActions();
            SaveBuildings();
            SaveGameEvents();
            SaveQuests();
            SaveCards();*/
        }
        else
            LoadGame();

        updateUI = GetComponent<UpdateUI>();
        //updateUI = new UpdateUI();

        eventObjectController = GetComponent<EventObjectController>();
        foreach (Region r in game.regions)
        {
            foreach (GameEvent e in r.inProgressGameEvents)
            {
                GameObject eventInstance = GameController.Instantiate(eventObject);
                eventInstance.GetComponent<EventObjectController>().PlaceEventIcons(this, r, e);
            }
        }
        updateUI.LinkGame(game);
        StartCoroutine(updateUI.showBtnQuests());
        StartCoroutine(updateUI.showBtnInvestments());

        //afterActionPosition = new Vector3[3];
        //afterActionPosition[0] = new Vector3( 5, 5, 0);
        //afterActionPosition[1] = new Vector3( 5, 115, 0);
        //afterActionPosition[2] = new Vector3( 5, 225, 0);

        //float width = Screen.width / (1920 / 45);
        //float height = Screen.height / (1080 / MonthlyReportButon.GetComponent<RectTransform>().sizeDelta.y);
        float height = Screen.height / (1080 / 55);
        //MonthlyReportButon.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        //YearlyReportButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        //CompletedButton.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        afterActionPosition = new Vector3[3];
        afterActionPosition[0] = new Vector3( 5, 5 + height * 2 * 0, 0);
        afterActionPosition[1] = new Vector3( 5, 5 + height * 2 * 1, 0);
        afterActionPosition[2] = new Vector3( 5, 5 + height * 2 * 2, 0);

        // setup Region Controllers
        noordNederland.GetComponent<RegionController>().Init(this);
        oostNederland.GetComponent<RegionController>().Init(this);
        westNederland.GetComponent<RegionController>().Init(this);
        zuidNederland.GetComponent<RegionController>().Init(this);

        EventManager.ChangeMonth += NextTurn;
        EventManager.SaveGame += SaveGame;
        EventManager.LeaveGame += SetTrackingData;
        EventManager.CallNewGame();
    }

    public void SetTrackData()
    {
    }

    private void OnApplicationQuit()
    {
        SetTrackingData();
    }

    public void SetTrackingData()
    {
        Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
        Analytics.SetUserGender(Gender.Unknown);
        Analytics.SetUserBirthYear(1996);

        Analytics.CustomEvent("GameStatisticsData", new Dictionary<string, object>
        {
            { "Year", game.currentYear.ToString() },
            { "Month", game.currentMonth.ToString() },
            { "Pollution", game.gameStatistics.pollution.ToString("0.00") },
            { "Money", game.gameStatistics.money.ToString("0") },
            { "Income", game.gameStatistics.income.ToString("0") },
            { "Happiness", game.gameStatistics.happiness.ToString("0.00") },
            { "EcoAwareness", game.gameStatistics.ecoAwareness.ToString("0.00") },
            { "Prosperity", game.gameStatistics.prosperity.ToString("0.00") }
        });
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

    public void SaveRegions()
    {
        RegionContainer regionContainer = new RegionContainer(game.regions);
        regionContainer.Save();
    }

    public void LoadRegions()
    {
        RegionContainer regionContainer = RegionContainer.Load();
        game.LoadRegions(regionContainer.regions);
    }

    public void SaveGameEvents()
    {
        GameEventContainer eventContainer = new GameEventContainer(game.events);
        eventContainer.Save();
    }

    public void LoadGameEvents()
    {
        GameEventContainer eventContainer = GameEventContainer.Load();
        game.LoadGameEvents(eventContainer.events);
    }

    public void SaveRegionActions()
    {
        RegionActionContainer regionActionContainer = new RegionActionContainer(game.regions[0].actions);
        regionActionContainer.Save();
    }

    public void LoadRegionActions()
    {
        foreach (Region region in game.regions)
        {
            RegionActionContainer regionActionContainer = RegionActionContainer.Load();
            region.LoadActions(regionActionContainer.actions);
        }
    }

    public void SaveBuildings()
    {
        BuildingContainer buildingContainer = new BuildingContainer(game.regions[0].possibleBuildings);
        buildingContainer.Save();

    }

    public void LoadBuildings()
    {
        foreach (Region region in game.regions)
        {
            BuildingContainer buildingContainer = BuildingContainer.Load();
            region.LoadBuildings(buildingContainer.buildings);
        }
    }

    public void SaveQuests()
    {
        QuestContainer questContainer = new QuestContainer(game.quests);
        questContainer.Save();
    }

    public void LoadQuests()
    {
        QuestContainer questContainer = QuestContainer.Load();
        game.LoadQuests(questContainer.quests);
    }

    public void SaveCards()
    {
        CardContainer cardContainer = new CardContainer(game.cards);
        cardContainer.Save();
    }

    public void LoadCards()
    {
        CardContainer cardContainer = CardContainer.Load();
        game.LoadCards(cardContainer.cards);
    }

    // Update is called once per frame
    void Update () {
        if (((Input.GetKeyDown(KeyCode.Return) || autoEndTurn) && game.currentYear < 31 && updateUI.tutorialStep9 && updateUI.tutorialNexTurnPossibe))
        {
            EventManager.CallChangeMonth();
        }

        // Update the main screen UI (Icons and date)
        updateUIMainScreen();

        // Update the UI in popup screen
        if (updateUI.getPopupActive())
            updateUIPopups();

        /* Update values in Tooltips for Icons in Main UI
        if (updateUI.getTooltipActive())
            updateUITooltips(); */

        UpdateRegionColor();
    }

    public void NextTurn()
    {
        if (!updateUI.popupActive)
        {
            if (!updateUI.tutorialNextTurnDone)
                updateUI.tutorialNextTurnDone = true;

            bool isNewYear = game.UpdateCurrentMonthAndYear();
            game.ExecuteNewMonthMethods();

            UpdateRegionsPollutionInfluence();
            UpdateEvents();
            game.gameStatistics.UpdateRegionalAvgs(game);
            UpdateQuests();

            if (isNewYear)
                UpdateCards();

            GenerateMonthlyUpdates(isNewYear);
            UpdateTimeline();

            game.economyAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.income);
            game.pollutionAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.pollution);

            if (autoSave)
                EventManager.CallSaveGame();

            updateUI.setNextTurnButtonNotInteractable();
        }
    }

    private void UpdateTimeline()
    {
        game.timeline.StoreTurnInTimeLine(game.gameStatistics, game.currentYear, game.currentMonth);
    }

    //yearly reward increase
    private void UpdateCards()
    {
        foreach (Card card in game.inventory.ownedCards)
        {
            if (card.currentIncrementsDone < card.maximumIncrementsDone)
                card.increaseCurrentRewards();
        }
    }

    private void GenerateMonthlyUpdates(bool isNewYear)
    {
        int index = 0;

        GenerateMonthlyReport(index);
        index++;
        if (isNewYear)
        {
            GenerateYearlyReport(index);
            index++;
            game.yearlyReport.UpdateStatistics(game.regions);
        }
        else
        {
            updateUI.btnYearlyReportStats.gameObject.SetActive(false);
        }

        //GenerateCompletedEventsAndActions(index);
        index++;

        game.monthlyReport.UpdateStatistics(game.regions);
    }

    private void GenerateMonthlyReport(int index)
    {
        updateUI.btnMonthlyReportStats.gameObject.SetActive(true);
        updateUI.InitMonthlyReport();

        updateUI.btnMonthlyReportStats.gameObject.transform.position = afterActionPosition[index];
        index++;
    }

    private bool checkNewEvents()
    {
        for (int i = 0; i < game.monthlyReport.newEvents.Length; i++)
        {
            if (game.monthlyReport.newEvents[i].Count != 0)
                return true;
        }

        return false;
    }

    private bool FindCompletedActionsAndEvents()
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

    private void GenerateYearlyReport(int index)
    {
        updateUI.btnYearlyReportStats.gameObject.SetActive(true);
        updateUI.InitYearlyReport();
        updateUI.btnYearlyReportStats.gameObject.transform.position = afterActionPosition[index];
    }

    /*private void GenerateCompletedEventsAndActions(int index)
    {
        if (FindCompletedActionsAndEvents())
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

    }*/

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
                        quest.CompleteQuest();
                        game.completedQuestsCount++;
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
                                quest.CompleteQuest();
                                game.completedQuestsCount++;
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
        int eventChance = 80;
        if (game.currentYear == 1 && game.currentMonth == 2)
            eventChance = 100;

        int eventChanceReduction = 100;

        //temp ugly code
        if (game.currentYear >= 2)
            eventChanceReduction -= 40;
        if (game.currentYear >= 5)
            eventChanceReduction -= 20;
        if (game.currentYear >= 10)
            eventChanceReduction -= 10;
        if (game.currentYear >= 20)
            eventChanceReduction -= 10;

        while (game.rnd.Next(1, 101) <= eventChance && activeCount < 4)
        {
            if (game.PossibleEventCount() > 0 && game.GetPossibleRegionsCount() > 0)
            {
                Region pickedRegion = game.PickEventRegion();
                GameEvent pickedEvent = game.GetPickedEvent(pickedRegion);
                pickedEvent.StartEvent(game.currentYear, game.currentMonth);
                pickedRegion.AddGameEvent(pickedEvent, game.gameStatistics.happiness);
                game.AddNewEventToMonthlyReport(pickedRegion, pickedEvent);

                GameObject eventInstance = GameController.Instantiate(eventObject);
                eventInstance.GetComponent<EventObjectController>().PlaceEventIcons(this, pickedRegion, pickedEvent);
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
        updateUI.updateAwarness(game.gameStatistics.ecoAwareness);
        updateUI.updatePollution(game.gameStatistics.pollution);
        updateUI.updateProsperity(game.gameStatistics.prosperity);
        updateUI.updateHappiness(game.gameStatistics.happiness);

        //updateUI.updateEnergy(game.gameStatistics.energy.cleanSource);
        //updateUI.updatePopulation(game.gameStatistics.population);
    }


    /* Tooltips worden niet meer getoont atm, bewaren voor als we van mening veranderen
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
    */

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
        //int i = 0;
        //foreach (Region region in game.regions)
        //{
            // Send the income for each region, use i to determine the region
         //   updateUI.updateOrganizationScreenUI(region.statistics.income * 12, i, game.gameStatistics.money);
          //  i++;            
       // }
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
