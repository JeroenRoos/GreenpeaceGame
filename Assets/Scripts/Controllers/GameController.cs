using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;
using UnityEngine.Analytics;
using Facebook.Unity;

/*regions order:
0 = noord
1 = oost
2 = west
3 = zuid
*/

public class GameController : MonoBehaviour
{
    public Game game;
    
    GameObject[] buildingInstances; //noord,oost,west,zuid
    GameObject eventInstance;
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
    public GameObject buildingObject;

    public double[] eventConsequenceModifiers;

    // private float time;
    public bool autoSave = true;
    public bool autoEndTurn = false;

    public bool trackingEnabled = false;
    
    float height = Screen.height / (1080 / 55);

    // Use this for initialization
    private void Awake()
    {
        FB.Init();

        if (!ApplicationModel.loadGame)
        {
            game = new Game();
            eventConsequenceModifiers = new double[5] { 0.8, 0.9, 1, 1.1, 1.2 };

            LoadRegions();
            LoadRegionActions();
            LoadBuildings();
            LoadGameEvents();
            LoadQuests();
            LoadBuildings();
            LoadCards();

            /*foreach (Region region in game.regions)
            {
                foreach (RegionSector sector in region.sectors)
                {
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

            game.gameStatistics.UpdateRegionalAvgs(game);
            UpdateTimeline();
            UpdateRegionActionAvailability();

            //set reports
            game.monthlyReport.UpdateStatistics(game.regions);
            game.yearlyReport.UpdateStatistics(game.regions);

            //set advisors
            game.economyAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.income);
            game.pollutionAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.pollution);
        }
        else
            LoadGame();

        updateUI = GetComponent<UpdateUI>();
        //setBuildingTextures();

        StartCoroutine(showBuildingIcons());

        foreach (MapRegion r in game.regions)
        {
            foreach (GameEvent e in r.inProgressGameEvents)
            {
                /*GameObject */eventInstance = GameController.Instantiate(eventObject);
                eventInstance.GetComponent<EventObjectController>().PlaceEventIcons(this, r, e);
            }
        }
        updateUI.LinkGame(game);
    }

    

    void Start()
    {
        SetPlayerTrackingData();
        autoSave = true;
        
        StartCoroutine(updateUI.showBtnQuests());

        StartCoroutine(updateUI.showBtnInvestments());
        StartCoroutine(updateUI.showBtnCards());

        afterActionPosition = new Vector3[3];
        afterActionPosition[0] = new Vector3( 5, 5 + height * 2 * 0, 0);
        afterActionPosition[1] = new Vector3( 5, 5 + height * 2 * 1, 0);
        afterActionPosition[2] = new Vector3( 5, 5 + height * 2 * 2, 0);

        // setup Region Controllers
        noordNederland.GetComponent<RegionController>().Init(this, updateUI, game.regions[0]);
        oostNederland.GetComponent<RegionController>().Init(this, updateUI, game.regions[1]);
        westNederland.GetComponent<RegionController>().Init(this, updateUI, game.regions[2]);
        zuidNederland.GetComponent<RegionController>().Init(this, updateUI, game.regions[3]);

        EventManager.ChangeMonth += NextTurn;
        EventManager.SaveGame += SaveGame;
        EventManager.LeaveGame += SetGameplayTrackingData;
        EventManager.CallNewGame();
    }

    private IEnumerator showBuildingIcons()
    {
        while (game.currentYear < 11)
            yield return null;

        buildingInstances = new GameObject[4] { Instantiate(buildingObject), Instantiate(buildingObject),
                                                Instantiate(buildingObject), Instantiate(buildingObject) };

        for (int i = 0; i < game.regions.Count; i++)
        {
            buildingInstances[i].GetComponent<BuildingObjectController>().placeBuildingIcon(this, game.regions[i], game.regions[i].activeBuilding);
        }

        if (game.tutorial.doTuto)
            updateUI.startTutorialBuildings();
    }

    public void SetPlayerTrackingData()
    {
        //Analytics.SetUserId(SystemInfo.deviceUniqueIdentifier);
        //Analytics.SetUserGender(Gender.Unknown);
        //Analytics.SetUserBirthYear(1996);
        if (trackingEnabled)
        {
            Analytics.CustomEvent("PlayerData", new Dictionary<string, object>
            {
                { "UserID", SystemInfo.deviceUniqueIdentifier },
                { "OperatingSystem", SystemInfo.operatingSystem },
                { "DeviceModel", SystemInfo.deviceModel },
                { "DeviceName", SystemInfo.deviceName },
                { "DeviceType", SystemInfo.deviceType },
            });
        }
    }

    private void OnApplicationQuit()
    {
        SetGameplayTrackingData();
    }

    public void ShareOnFacebook()
    {
        /*string url = "https://www.facebook.com/dialog/feed?";
        string appIDURL = "1821181281535576";
        string displayURL = "popup";
        string linkURL = "https://developers.facebook.com/docs/";
        string pictureURL = "http://i.imgur.com/zkYlB.jpg";
        //Application.OpenURL("https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fi.imgur.com%2FzkYlB.jpg");
        string facebookURL = url +
            "app_id=" + appIDURL +
            "&name=" + WWW.EscapeURL("Project Green Leader") +
            "&description=" + WWW.EscapeURL("Thanks Facebook for making this way too difficult and wasting my time while I have to work on my game") +
            "&display=" + WWW.EscapeURL(displayURL) +
            "&link=" + WWW.EscapeURL(pictureURL) +
            "&redirect_uri=" + WWW.EscapeURL(linkURL) +
            "&picture=" + WWW.EscapeURL(pictureURL);*/

        string appId = "145634995501895";
        string pictureUrl = "http://i.imgur.com/zkYlB.jpg";
        string linkUrl = "https://www.youtube.com/watch?v=-KqGxq5eIt0";
    string redirectUrl = "https://www.facebook.com/profile.php";

        string facebookURL = "https://www.facebook.com/dialog/feed?" +
            "app_id=" + appId + "&" +
            "display=popup&" +
            "link=" + WWW.EscapeURL(linkUrl) + " & " +
            "name=" + WWW.EscapeURL("Project Green Leader") + " & " +
            "description=" + WWW.EscapeURL("Thanks Facebook for making this way too difficult and wasting my time while I have to work on my game") + " & " +
            "picture=" + WWW.EscapeURL(pictureUrl);

    Application.OpenURL(facebookURL);

        /*FB.FeedShare(
            string.Empty,
            link: new System.Uri("http://i.imgur.com/zkYlB.jpg"),
            linkName: "Test picture",
            linkCaption: "I am sharing my amazing progress on Project Green Leader",
            linkDescription: "Click for mystery picture",
            picture: new System.Uri("https://gyazo.com/851dab54e6d082bca1aa3d876aca5493"),
            callback: LogCallback);
    }*/
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    void LogCallback(IResult response)
    {
        Debug.Log("Worked");
    }


    public void SetScoreTrackingData(double score)
    {
        if (trackingEnabled)
        {
            Analytics.CustomEvent("GameCompletionScore", new Dictionary<string, object>
            {
                { "Score", score.ToString("0") },
                { "Year", game.currentYear.ToString() },
                { "Month", game.currentMonth.ToString() },
                { "Pollution", game.gameStatistics.pollution.ToString("0.00") },
                { "Money", game.gameStatistics.money.ToString("0") },
                { "Income", game.gameStatistics.income.ToString("0") },
                { "Happiness", game.gameStatistics.happiness.ToString("0.00") },
                { "EcoAwareness", game.gameStatistics.ecoAwareness.ToString("0.00") },
                { "Prosperity", game.gameStatistics.prosperity.ToString("0.00") },
                { "TimePlayed", game.totalTimePlayed.ToString() }
            });
        }

    }

    public void SetGameplayTrackingData()
    {
        game.totalTimePlayed += Time.timeSinceLevelLoad;

        if (trackingEnabled)
        {
            int totalMonths = game.currentMonth + game.currentYear * 12;
            Analytics.CustomEvent("GameStatisticsData", new Dictionary<string, object>
            {
                { "TotalMonths", totalMonths.ToString() },
                { "Pollution", game.gameStatistics.pollution.ToString("0.00") },
                { "Money", game.gameStatistics.money.ToString("0") },
                { "Income", game.gameStatistics.income.ToString("0") },
                { "Happiness", game.gameStatistics.happiness.ToString("0.00") },
                { "EcoAwareness", game.gameStatistics.ecoAwareness.ToString("0.00") },
                { "Prosperity", game.gameStatistics.prosperity.ToString("0.00") },
                { "TimePlayed", Time.timeSinceLevelLoad.ToString("0") },
                { "TotalTimePlayed", game.totalTimePlayed.ToString() }
            });
        }

    }

    public void SetYearlyTrackingData()
    {
        if (trackingEnabled)
        {
            SetYearlyStatistics();
            SetYearlyCompletedFeatures();
        }
    }

    public void SetYearlyStatistics()
    {
        Analytics.CustomEvent("Year" + game.currentYear + "StartGameStatisticsData", new Dictionary<string, object>
        {
            { "Pollution", game.gameStatistics.pollution.ToString("0.00") },
            { "Money", game.gameStatistics.money.ToString("0") },
            { "Income", game.gameStatistics.income.ToString("0") },
            { "Happiness", game.gameStatistics.happiness.ToString("0.00") },
            { "EcoAwareness", game.gameStatistics.ecoAwareness.ToString("0.00") },
            { "Prosperity", game.gameStatistics.prosperity.ToString("0.00") },
            { "TimePlayed", game.totalTimePlayed.ToString() }
        });
    }

    public void SetYearlyCompletedFeatures()
    {
        Analytics.CustomEvent("Year" + game.currentYear + "StartCompletedFeaturesData", new Dictionary<string, object>
        {
            { "CompletedEventsCount", game.completedEventsCount.ToString() },
            { "AbandonedEventsCount", game.abandonedEventsCount.ToString() },
            { "CompletedActionsCount", game.completedActionsCount.ToString() },
            { "CompletedQuestsCount", game.completedQuestsCount.ToString() },
            { "ReceivedCardsCount", game.receivedCardsCount.ToString() },
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
        foreach (MapRegion region in game.regions)
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
        foreach (MapRegion region in game.regions)
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
        if (((Input.GetKeyDown(KeyCode.Return) || autoEndTurn) && game.currentYear < 31 && game.gameStatistics.pollution > 0 &&
            /*game.tutorial.tutorialStep9 && */game.tutorial.tutorialNexTurnPossibe))
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
    }

    public void NextTurn()
    {
        ShareOnFacebook();
        if (!updateUI.popupActive)
        {
            if (!game.tutorial.tutorialNextTurnDone)
                game.tutorial.tutorialNextTurnDone = true;

            bool isNewYear = game.UpdateCurrentMonthAndYear();
            game.ExecuteNewMonthMethods();
            UpdateRegionsPollutionInfluence();
            UpdateEvents();
            game.gameStatistics.UpdateRegionalAvgs(game);
            UpdateQuests();
            UpdateRegionActionAvailability();


            if (isNewYear)
            {
                UpdateCards();
                IncreaseYearlyPollutionChange();
                SetYearlyTrackingData();
            }

            GenerateNewCard();

            GenerateMonthlyUpdates(isNewYear);
            UpdateTimeline();

            game.economyAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.income);
            game.pollutionAdvisor.DetermineDisplayMessage(game.currentYear, game.currentMonth, game.gameStatistics.pollution);

            if (autoSave)
                EventManager.CallSaveGame();

            updateUI.setNextTurnButtonNotInteractable();

            EventManager.CallPlayNewTurnStartSFX();

            if (game.currentYear == 31 || game.gameStatistics.pollution == 0d)
                ShowGameScore();
        }
    }

    public void IncreaseYearlyPollutionChange()
    {
        double changeValue = 0.4 + 0.1 * game.currentYear;
        foreach (MapRegion r in game.regions)
        {
            foreach (RegionSector rs in r.sectors)
            {
                rs.statistics.pollution.ChangeAirPollutionMutation(changeValue);
                rs.statistics.pollution.ChangeNaturePollutionMutation(changeValue);
                rs.statistics.pollution.ChangeWaterPollutionMutation(changeValue);
            }
        }
    }

    public void ShowGameScore()
    {
        double score = CalculateScore();
        updateUI.initEndOfGameReport(score);
        SetScoreTrackingData(score);
    }

    public double CalculateScore()
    {
        double score = 0;

        score += game.gameStatistics.prosperity * 100;
        score += game.gameStatistics.ecoAwareness * 100;
        score += game.gameStatistics.happiness * 100;
        score += game.gameStatistics.income;
        score += 10000 - game.gameStatistics.pollution * 100;
        score += 36000 - ((game.currentYear - 1) * 12 + (game.currentMonth - 1)) * 100;

        return score;
    }

    private void UpdateTimeline()
    {
        game.timeline.StoreTurnInTimeLine(game.gameStatistics, game.currentYear, game.currentMonth);
    }

    public void GenerateNewCard()
    {
        if (game.currentYear == 3 && game.currentMonth == 1)
        {
            game.inventory.AddCardToInventory(new Card(game.cards[game.rnd.Next(0, game.cards.Count)]));
            game.receivedCardsCount++;
            if (!updateUI.cardsShakes)
                StartCoroutine(updateUI.ShakeCards());
        }

        else if (game.rnd.Next(1, 101) <= 2 && game.currentYear >= 3)
        {
            game.inventory.AddCardToInventory(new Card(game.cards[game.rnd.Next(0, game.cards.Count)]));
            game.receivedCardsCount++;
            if (!updateUI.cardsShakes)
                StartCoroutine(updateUI.ShakeCards());
        }
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
        updateUI.btnMonthlyReportStats.interactable = false;
        updateUI.InitMonthlyReport();

        
        Vector3 monthlyReportStartPosition = new Vector3(5, 5 + height * 2 * (2 + index), 0);
        StartCoroutine(SetMonthlyReportButtonLocation(monthlyReportStartPosition, afterActionPosition[index]));

        //updateUI.btnMonthlyReportStats.gameObject.transform.position = afterActionPosition[index];
        index++;
    }

    private void GenerateYearlyReport(int index)
    {
        updateUI.btnYearlyReportStats.gameObject.SetActive(true);
        updateUI.btnYearlyReportStats.interactable = false;
        updateUI.InitYearlyReport();

        Vector3 yearlyReportPosition = new Vector3(5, 5 + height * 2 * (2 + index), 0);
        StartCoroutine(SetYearlyReportButtonLocation(yearlyReportPosition, afterActionPosition[index]));
        //updateUI.btnYearlyReportStats.gameObject.transform.position = afterActionPosition[index];
    }

    public IEnumerator SetMonthlyReportButtonLocation(Vector3 currentPosition, Vector3 endPosition)
    {
        float positionDiff = currentPosition.y - endPosition.y;
        while (currentPosition.y > endPosition.y)
        {
            currentPosition.y -= positionDiff / 60;
            if (currentPosition.y < endPosition.y)
                currentPosition = endPosition;
            updateUI.btnMonthlyReportStats.gameObject.transform.position = currentPosition;
            yield return new WaitForFixedUpdate();
        }

        updateUI.btnMonthlyReportStats.interactable = true;
    }

    public IEnumerator SetYearlyReportButtonLocation(Vector3 currentPosition, Vector3 endPosition)
    {
        float positionDiff = currentPosition.y - endPosition.y;
        while (currentPosition.y > endPosition.y)
        {
            currentPosition.y -= positionDiff / 60;
            if (currentPosition.y < endPosition.y)
                currentPosition = endPosition;
            updateUI.btnYearlyReportStats.gameObject.transform.position = currentPosition;
            yield return new WaitForFixedUpdate();
        }

        updateUI.btnYearlyReportStats.interactable = true;
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
                    foreach (MapRegion r in game.regions)
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
            {
                quest.StartQuest();
                if (!updateUI.questsShakes)
                    StartCoroutine(updateUI.ShakeQuests());
            }
        }
    }

    private void UpdateRegionActionAvailability()
    {
        foreach (MapRegion r in game.regions)
        {
            foreach (RegionAction ra in r.actions)
                ra.GetAvailableActions(game, r.statistics);
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
                MapRegion pickedRegion = game.PickEventRegion();
                GameEvent pickedEvent = game.GetPickedEvent(pickedRegion);
                SetEventConsequences(pickedEvent);
                pickedEvent.StartEvent(game.currentYear, game.currentMonth);
                pickedRegion.AddGameEvent(pickedEvent, game.gameStatistics.happiness);
                game.AddNewEventToMonthlyReport(pickedRegion, pickedEvent);

                //Destroy(eventInstance);
                /*GameObject */eventInstance = GameController.Instantiate(eventObject);
                eventInstance.GetComponent<EventObjectController>().PlaceEventIcons(this, pickedRegion, pickedEvent);
            }

            eventChance -= eventChanceReduction;
        }

        if (activeCount < 1)
        {
        }
    }
    
    public void SetEventConsequences(GameEvent e)
    {
        e.pickedConsequences = new SectorStatistics[e.consequences.Length];
        e.pickedTemporaryConsequences = new SectorStatistics[e.consequences.Length];
        for (int i = 0; i < e.afterInvestmentConsequences.Length; i++)
        {
            e.pickedConsequences[i] = new SectorStatistics();
            e.pickedTemporaryConsequences[i] = new SectorStatistics();
            e.pickedConsequences[i].SetPickedConsequences(e.afterInvestmentConsequences[i], eventConsequenceModifiers, game.rnd);
            e.pickedTemporaryConsequences[i].SetPickedConsequences(e.afterInvestmentTemporaryConsequences[i], eventConsequenceModifiers, game.rnd);
        }
    }

    private void UpdateRegionsPollutionInfluence()
    {
        game.gameStatistics.UpdateRegionalAvgs(game);

        foreach (MapRegion region in game.regions)
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

        MapRegion regionModel = game.regions[pickedRegion];
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

    public bool getActivePopup()
    {
        return updateUI.getPopupActive();
    }


    public void btnUseBuildingPress()
    {
        MapRegion r = updateUI.regionToBeBuild;
        Building b = updateUI.buildingToBeBuild;

        r.SetBuilding(b.buildingID);
        updateUI.canvasEmptyBuildingsPopup.gameObject.SetActive(false);
        updateUI.popupActive = false;
        EventManager.CallPopupIsDisabled();

        for (int i = 0; i < game.regions.Count; i++)
        {
            if (r == game.regions[i])
            {
                Destroy(buildingInstances[i]);

                buildingInstances[i] = GameController.Instantiate(buildingObject);
                buildingInstances[i].GetComponent<BuildingObjectController>().placeBuildingIcon(this, r, b);
            }
        }

        game.gameStatistics.ModifyMoney(b.buildingMoneyCost, false);
        updateUI.initBuildingPopup(b, r);
    }

    public void btnDeleteBuildingPress()
    {
        /*Region r = updateUI.buildingRegion;
        Building b = updateUI.activeBuilding;

        //r.DeleteBuilding(b);
        updateUI.canvasBuildingsPopup.gameObject.SetActive(false);
        updateUI.popupActive = false;
        EventManager.CallPopupIsDisabled();

        GameObject buildingInstance = GameController.Instantiate(buildingObject);
        buildingInstance.GetComponent<BuildingObjectController>().placeBuildingIcon(this, r, null);*/


        MapRegion r = updateUI.buildingRegion;
        Building b = updateUI.activeBuilding;

        r.SetBuilding(null);
        updateUI.canvasBuildingsPopup.gameObject.SetActive(false);
        updateUI.popupActive = false;
        EventManager.CallPopupIsDisabled();

        for (int i = 0; i < game.regions.Count; i++)
        {
            if (r == game.regions[i])
            {
                Destroy(buildingInstances[i]);

                buildingInstances[i] = GameController.Instantiate(buildingObject);
                buildingInstances[i].GetComponent<BuildingObjectController>().placeBuildingIcon(this, r, null);
            }
        }

        updateUI.initEmptyBuildingPopup(r);
    }

    public void btnDoEventClick()
    {
        updateUI.finishEvent();

        Destroy(eventInstance);
        /*GameObject */eventInstance = Instantiate(eventObject);
        eventInstance.GetComponent<EventObjectController>().PlaceEventIcons(this, updateUI.regionEvent, updateUI.gameEvent);
    }
}

