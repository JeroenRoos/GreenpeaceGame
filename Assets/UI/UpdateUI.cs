using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    #region UI Elements
    // Tooltip texture and GUI
    public Texture2D tooltipTexture;
    private GUIStyle tooltipStyle = new GUIStyle();
    public Texture2D buttonTexture;
    private GUIStyle buttonStyle = new GUIStyle();
    BuildingObjectController buildingObjectController;

    Region regio;
    RegionAction regioAction;
    Card card;
    double regioActionCost;
    Game game;
    public Scrollbar scrollbarAfterActionReport;

    private List<GameEvent>[] monthlyNewEvents;
    private List<GameEvent>[] monthlyCompletedEvents;
    private List<RegionAction>[] monthlyCompletedActions;

    public Text test;

    public bool playSelectSound;

    public Dropdown dropdownRegio;

    public Toggle checkboxRegionHouseholds;
    private bool checkboxHouseholds;

    public Toggle checkboxRegionAgriculture;
    private bool checkboxAgriculture;

    public Toggle checkboxRegionCompanies;
    private bool checkboxCompanies;

    // Text MonthlyAfterActionReportStats
    public Text txtAfterActionStatsName;
    public Text txtAfterActionStatsColumnLeft;
    public Text txtAfterActionStatsColumnLeftMiddle;
    public Text txtAfterActionStatsColumnRight;
    public Text txtAfterActionStatsColumnRightMiddle;
    public Text txtAfterActionStatsColumnLeftDescription;
    public Text txtAfterActionStatsColumnLeftMiddleDescription;
    public Text txtAfterActionStatsColumnRightDescription;
    public Text txtAfterActionStatsColumnRightMiddleDescription;
    public Text txtAfterActionOost;
    public Text txtAfterActionNoord;
    public Text txtAfterActionZuid;
    public Text txtAfterActionWest;

    // Text YearlyAfterActionReportStats
    public Text txtAfterActionStatsNameYearly;
    public Text txtAfterActionStatsColumnLeftYearly;
    public Text txtAfterActionStatsColumnLeftMiddleYearly;
    public Text txtAfterActionStatsColumnRightYearly;
    public Text txtAfterActionStatsColumnRightMiddleYearly;
    public Text txtAfterActionStatsColumnLeftDescriptionYearly;
    public Text txtAfterActionStatsColumnLeftMiddleDescriptionYearly;
    public Text txtAfterActionStatsColumnRightDescriptionYearly;
    public Text txtAfterActionStatsColumnRightMiddleDescriptionYearly;

    public Text txtAfterActionOostYearly;
    public Text txtAfterActionNoordYearly;
    public Text txtAfterActionZuidYearly;
    public Text txtAfterActionWestYearly;

    // Text Menu Popup
    public Text txtResume;
    public Text txtSettings;
    public Text txtSave;
    public Text txtExitMenu;
    public Text txtExitGame;

    // Menu Settings
    // Settings 
    public Button btnSettingsBack;
    public Text txtButtonSettingsBack;
    public Slider sliderMusicVolume;
    public Text txtMusicVolume;
    public Slider sliderEffectsVolume;
    public Text txtEffectsVolume;
    public Text txtLanguage;
    public Toggle toggleEnglish;
    public Toggle toggleDutch;
    public Text txtToggleEnglish;
    public Text txtToggleDutch;
    private bool toggleDutchCheck;
    private bool toggleEnglishCheck;
    public Text txtMusicVolumeSliderValue;
    public Text txtEffectsVolumeSliderValue;

    // Text Main UI
    public Text txtMoney;
    public Text txtPopulation;
    public Text txtDate;
    public Text btnNextTurnText;
    public Text txtBtnMenu;
    public Text txtBtnTimeline;
    public Button btnNextTurn;

    // Buildings Popup
    public Building activeBuilding;
    public Region buildingRegion;
    public Text txtBuildingsTitle;
    public Text txtBuildingsColumn;
    public Text txtBuildingsStats;
    public Button btnDeleteBuilding;
    public Text txtBtnDeleteBuilding;

    // Empty Building Popup
    public Text txtEmptyBuildingsTitle;
    public Text txtEmptyBuildingsColumnLeft;
    public Text txtEmptyBuildingColumRight;
    public Button btnUseBuilding;
    public Button btnEmptyBuildingPosition;
    public Text btnUseBuildingTxt;
    public Text txtEmptyBuildingInfo;
    public Text txtEmptyBuildingStats;
    public Building buildingToBeBuild;
    public Region regionToBeBuild;

    // Text Event Popup
    GameEvent gameEvent;
    Region regionEvent;
    public Text txtEventName;
    public Text txtEventDescription;
    public Toggle radioEventOption1;
    public Text radioEventOption1Text;
    private bool radioEventOption1Check;
    public Toggle radioEventOption2;
    public Text radioEventOption2Text;
    private bool radioEventOption2Check;
    public Toggle radioEventOption3;
    public Text radioEventOption3Text;
    private bool radioEventOption3Check;
    public Button btnDoEvent;
    public Text txtBtnDoEvent;
    public Text txtEventAlreadyActive;

    // Text Quests Popup
    public Text txtQuestsTitle;
    public Text txtQuestsDescription;
    public Text txtQuestsActive;

    // Text Investments Popup
    public Text txtInvestmentsTitle;
    public Text txtInvestmentsColumn;
    public Text txtInvestmentsDescription;
    public Text txtInvestmentsActionCost;
    public Text txtInvestmentsActionConsequences;
    public Text txtInvestmentsEventCost;
    public Text txtInvestmentsEventConsequences;
    public Image[] imgInvestmentActionCost;

        
    public Button btnInvestmentActionCostInvest;
    public Image imgInvestmentActionCost01;
    public Image imgInvestmentActionCost02;
    public Image imgInvestmentActionCost03;
    public Image imgInvestmentActionCost04;
    public Image imgInvestmentActionCost05;

    public Button btnInvestmentActionConsequenceInvest;
    public Image imgInvestmentActionConsequences01;
    public Image imgInvestmentActionConsequences02;
    public Image imgInvestmentActionConsequences03;
    public Image imgInvestmentActionConsequences04;
    public Image imgInvestmentActionConsequences05;

    public Button btnInvestmentEventCostInvest;
    public Image imgInvestmentEventCost01;
    public Image imgInvestmentEventCost02;
    public Image imgInvestmentEventCost03;
    public Image imgInvestmentEventCost04;
    public Image imgInvestmentEventCost05;

    public Button btnInvestmentEventConsequenceInvest;
    public Image imgInvestmentEventConsequences01;
    public Image imgInvestmentEventConsequences02;
    public Image imgInvestmentEventConsequences03;
    public Image imgInvestmentEventConsequences04;
    public Image imgInvestmentEventConsequences05;

    // Text Cards Popup
    public Text txtCardsTitle;
    public Text txtCardsColumn;
    public Text txtCardsColumnRight;
    public Text txtCardsOptionInformation;
    public RawImage imgCardsPopup;
    public Button btnUseCard;
    public Text txtBtnUseCard;
    public Toggle toggleNoordNL;
    public Toggle toggleOostNL;
    public Toggle toggleZuidNL;
    public Toggle toggleWestNL;
    private bool toggleNoordNLCheck;
    private bool toggleOostNLCheck;
    private bool toggleZuidNLCheck;
    private bool toggleWestNLCheck;
    public Text txtToggleNoord;
    public Text txtToggleOost;
    public Text txtToggleZuid;
    public Text txtToggleWest;

    // Text Organization Menu
    public Text txtColumnLeft;
    public Text txtColumnRight;
    public Text txtOrgBankDescription;
    public Text txtOrganizatonTitle;
    public Text txtOrgNoordMoneyDescription;
    public Text txtOrgOostMoneyDescription;
    public Text txtOrgZuidMoneyDescription;
    public Text txtOrgWestMoneyDescription;
    public Text txtOrgNoordMoney;
    public Text txtOrgOostMoney;
    public Text txtOrgZuidMoney;
    public Text txtOrgWestMoney;
    public Text txtOrgBank;
    public Text txtYearlyBudget;
    public Text txtDemonstration;
    public Text txtResearch;
    public Text txtEcoGuarding;
    public Text txtBigDescription;
    public Text txtAdviserEconomic;
    public Text txtAdviserPollution;

    private int taal;
    //  double totalOrgBank;

    // Text Region Menu
    public Text txtRegionName;
    public Text txtRegionMoney;
    public Text txtRegionHappiness;
    public Text txtRegionAwareness;
    public Text txtRegionProsperity;
    public Text txtRegionPollution;
    public Text txtRegionPollutionNature;
    public Text txtRegionPollutionWater;
    public Text txtRegionPollutionAir;
    public Text txtRegionTraffic;
    public Text txtRegionFarming;
    public Text txtRegionHouseholds;
    public Text txtRegionCompanies;
    public Text txtRegionActionName;
    public Text txtRegionActionDuration;
    public Text txtRegionActionCost;
    public Text txtRegionActionConsequences;
    public Text txtActiveActions;
    public Text txtActiveEvents;
    public Text txtRegionActionNoMoney;
    public Text txtRegionIncomeDescription;
    public Text txtRegionProsperityDescription;
    public Text txtRegionHappinessDescription;
    public Text txtRegionEcoAwarenessDescription;
    public Text txtRegionPollutionDescription;
    public Text txtRegionWaterDescription;
    public Text txtRegionAirDescription;
    public Text txtRegionNatureDescription;
    public Text txtRegionHouseholdsDescription;
    public Text txtRegionAgricultureDescription;
    public Text txtRegionCompainesDescription;
    public Text txtRegionColumnLeft;
    public Text txtRegionColumnCenter;
    public Text txtRegionColumnRight;
    public Text txtActiveActionDescription;
    public Text txtActiveEventsDescription;
    public Text btnDoActionText;
    public Text txtActionSectorsDescription;
    public Text txtCheckboxHouseholds;
    public Text txtCheckboxAgriculture;
    public Text txtCheckboxCompanies;
    public Text txtRegionActionSectorTotalCost;
    public Text txtRegionActionSectorTotalCostDescription;

    // Buttons 
    public Button btnMenu;
    public Button btnTimeline;
    public Button btnOrganization;
    public Button btnInvestments;
    public Button btnQuests;
    public Button btnMoney;
    public Button btnHappiness;
    public Text txtHappiness;
    public Button btnAwareness;
    public Text txtAwarness;
    public Button btnEnergy;
    public Button btnProsperity;
    public Text txtProsperity;
    public Button btnPollution;
    public Text txtPollution;
    public Button btnPopulation;
    public Button btnDoActionRegionMenu;
    public Button emptybtnHoverHouseholds;
    public Button emptybtnHoverAgriculture;
    public Button emptybtnHoverCompanies;
    public Button[] investDemonstrations;
    public Button[] investResearch;
    public Button[] investEcoGuarding;
    public Button btnMonthlyReportStats;
    public Button btnYearlyReportStats;
    public Button btnAfterActionReportCompleted;
    public RawImage imgBarBottom;
    public Button btnCards;
    public Button btnCardsPosition;

    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasSettingsPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;
    public Canvas canvasRegioPopup;
    public Canvas canvasTutorial;
    public Canvas canvasMonthlyReport;
    public Canvas canvasYearlyReport;
    //public Canvas canvasAfterActionCompletedPopup;
    public Canvas canvasQuestsPopup;
    public Canvas canvasEventPopup;
    public Canvas canvasInvestmentsPopup;
    public Canvas canvasCardsPopup;
    public Canvas canvasBuildingsPopup;
    public Canvas canvasEmptyBuildingsPopup;

    // Tooltip Variables
    private string txtTooltip;
    private string txtTooltipCompany;
    private string txtTooltipAgriculture;
    private string txtTooltipHouseholds;
    private string dropdownChoice;

    // Tutorial
    public Text txtTurorialStep1;
    public Text txtTutorialStep1BtnText;
    public Image imgTutorialStep2Highlight1;
    public Image imgTutorialStep2Highlight2;
    public Image imgTutorialStepOrgMenuHightlight;
    public Button btnTutorialNext;

    public Image imgTutorialEvents;
    public Button btnTutorialEvent;
    public Text txtTutorialEvent;
    public Text txtTutorialEventBtn;

    public Image imgTutorialQuests;
    public Button btTutorialQuests;
    public Text txtTutorialQuests;
    public Text txtTutorialQuestsBtn;

    public Image imgTutorialRegion;
    public Button btnTutorialRegion;
    public Text txtTutorialRegion;
    public Text txtTurorialReginoBtnText;

    public Image imgTutorialAfterTurn;
    public Button btnTutorialAfternTurn;
    public Text txtTutorialAfterTurn;
    public Text txtTutorialAfterTurnBtn;

    public Image imgTutorialOverworld;
    public Button btnTutorialOverworld;
    public Text txtTutorialOverworld;
    public Image imgTutorialOrganization;
    public Text txtTutorialOrganization;
    public Text txtTutorialOrganizationBtnText;
    public Button btnTutorialOrganization;

    public Image imgTutorialCards;
    public Text txtTutorialCards;
    public Text txtTutorialCardsBtn;

    public Image imgTutorialInvestements;
    public Text txtTutorialInvestements;
    public Text txtTutorialInvestementsbtn;

    public Image imgTutorialBuildings;
    public Text txtTutorialBuildings;
    public Text txtTutorialBuildingsbtn;

    public Image imgHighlightCards;
    public Image imgHighlightQuests;
    public Image imgHighlightMonthlyReport;
    public Image imgHighlightInvestements;



    private Vector3 v3Tooltip;
    //string arrays (translations
    string[] nextTurnText = { "Volgende maand", "Next month" };

    #endregion

    #region Boolean Variables
    // Booleans
    private bool btnMoneyHoverCheck;
    private bool btnHappinessHoverCheck;
    private bool btnAwarenessHoverCheck;
    private bool btnPollutionHoverCheck;
    private bool btnProsperityHoverCheck;
    private bool btnEnergyHoverCheck;
    private bool btnOrganizationCheck;
    private bool btnQuestsCheck;
    private bool btnMenuCheck;
    private bool btnTimelineCheck;
    public bool popupActive;
    private bool btnAfterActionStatsCheck;
    private bool btnMonthlyReportCheck;
    private bool btnYearlyReportCheck;
    private bool btnAfterActionCompletedCheck;
    private bool btnInvestementsHoverCheck;
    private bool btnCardsHoverCheck;
    private bool refreshCards;

    private bool tooltipActive;
    private bool regionHouseholdsCheck;
    private bool regionAgricultureCheck;
    private bool regionCompanyCheck;
    private bool dropdownChoiceMade;
    #endregion

    public bool btnOrganizationIsClicked;
    public bool btnQuestsIsClicked;
    public bool btnInvestmentsIsClicked;
    public bool btnCardsIsClicked;

    public bool organizationShakes = false;
    public bool questsShakes = false;
    public bool investmentsShakes = false;
    public bool cardsShakes = false;

    #region Start(), Update(), FixedUpdate()
    // Use this for initialization
    void Start()
    {
        taal = ApplicationModel.language;
        EventManager.CallPlayBackgroundMusic();

        initButtons();
        initButtonText();
        initCanvas();
        initOrganizationText();
        initRegionText();
        initInvestementsText();
        initCardsText();

        // GUI Styles
        tooltipStyle.normal.background = tooltipTexture;

        // GUIStyle for buttons INIT
        buttonStyle.normal.background = buttonTexture;          // Set the Texture
        buttonStyle.alignment = TextAnchor.MiddleCenter;        // Set the text in the middle of the button
        Color c = new Color();                              
        ColorUtility.TryParseHtmlString("#ccac6f", out c);      // Get the color out of the hexadecimal string
        buttonStyle.normal.textColor = c;                       // Set the color of the text to above color

        if (game.tutorial.tutorialActive)
            initTutorialActive();
        else
            initTutorialNotActive();

        btnNextTurnText.text = nextTurnText[taal];
        buildingObjectController = GetComponent<BuildingObjectController>();
    }

    void Update()
    {
        if (game.tutorial.tutorialChecks[2])//tutorialStep6)
            popupController();

        if (canvasRegioPopup.gameObject.activeSelf && dropdownChoiceMade)
        {
            
            if (checkboxAgriculture || checkboxCompanies || checkboxHouseholds)
            {
                btnDoActionRegionMenu.gameObject.SetActive(true);
                txtRegionActionNoMoney.text = "";
            }
            else
            {
                string[] error = { "Je moet een sector kiezen", "You have to chose a sector" };
                txtRegionActionNoMoney.text = error[taal];
                btnDoActionRegionMenu.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Tutorial Main Steps
    private void initTutorialActive()
    {
        canvasTutorial.gameObject.SetActive(true);
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;
        imgTutorialStepOrgMenuHightlight.enabled = false;
        imgHighlightCards.enabled = false;
        imgHighlightQuests.enabled = false;
        imgHighlightMonthlyReport.enabled = false;
        imgHighlightInvestements.enabled = false;
        canvasTutorial.gameObject.SetActive(true);
        StartCoroutine(initTutorialText());
    }

    private void initTutorialNotActive()
    {
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;
        imgTutorialStepOrgMenuHightlight.enabled = false;
        imgHighlightCards.enabled = false;
        imgHighlightQuests.enabled = false;
        imgHighlightMonthlyReport.enabled = false;
        imgHighlightInvestements.enabled = false;
        game.tutorial.doTuto = false;
        imgTutorialOverworld.gameObject.SetActive(false);
        btnTutorialOverworld.gameObject.SetActive(false);
        btnTutorialRegion.gameObject.SetActive(false);
        txtTutorialOverworld.enabled = false;
        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.regionWestActivated = true;
        game.tutorial.tutorialQuestsActive = false;
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialBuildingsClickable = true;
        game.tutorial.tutorialOnlyWestNL = true;
        game.tutorial.tutorialRegionActive = false;
        game.tutorial.tutorialEventsActive = false;
        game.tutorial.tutorialMonthlyReportActive = false;
        game.tutorial.tutorialOrganizationActive = false;
        game.tutorial.tutorialCardsActive = false;
        game.tutorial.tutorialInvestementsActive = false;
        game.tutorial.tutorialRegionsClickable = true;
        game.tutorial.tutorialBuildingsActive = false;

        game.tutorial.tutorialOrganizationDone = true;
        game.tutorial.tutorialRegionDone = true;
        game.tutorial.tutorialNextTurnDone = true;
        game.tutorial.tutorialEventsDone = true;
        game.tutorial.tutorialMonthlyReportDone = true;
        game.tutorial.tutorialCheckActionDone = true;
        game.tutorial.tutorialCardsDone = true;
        game.tutorial.tutorialQuestsDone = true;
        game.tutorial.tutorialInvestementsDone = true;
        game.tutorial.tutorialBuildingsDone = true;

        for (int i = 0; i < game.tutorial.tutorialChecks.Length; i++)
            game.tutorial.tutorialChecks[i] = true;

        StartCoroutine(ChangeScale(btnOrganization));
        StartCoroutine(ShakeOrganization());
    }

    IEnumerator initTutorialText()
    {
        Vector3 imgPosMiddle = imgTutorialOverworld.gameObject.transform.position;     // Midden in het scherm
        Vector3 imgPosRight = imgPosMiddle;
        Vector3 imgPosLeft = imgPosMiddle;
        imgPosRight.x = imgPosRight.x + Screen.width / 3;                           // Rechtsmidden in het scherm
        imgPosLeft.x = imgPosLeft.x - Screen.width / 3;                             // Linksmidden in het scherm

        btnOrganization.gameObject.SetActive(false);
        btnNextTurn.gameObject.SetActive(false);
        btnInvestments.gameObject.SetActive(false);
        btnCards.gameObject.SetActive(false);
        imgBarBottom.gameObject.SetActive(false);
        //imgBarBottom.gameObject.SetActive(false);

        game.tutorial.doTuto = true;
        string[] step1 = { "Welkom! De overheid heeft jouw organisatie de opdracht gegeven om ervoor te zorgen " + 
                "dat Nederland een milieubewust land wordt. De inwoners moeten begrijpen dat een groen land belangrijk is."
                , "Welcome! The government has given your organisation the task to make The Netherlands " + 
                "a country which is aware of the environment. The inhabitants need to understand the importance of a green country."};
        string[] btnText = { "Verder", "Next" };

        txtTurorialStep1.text = step1[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        //btnOrganization.interactable = false;
        //btnInvestments.interactable = false;
        btnNextTurn.interactable = false;
        imgTutorialOverworld.transform.position = imgPosRight;

        while (!game.tutorial.tutorialChecks[0])//tutorialStep2)
            yield return null;

        string[] step2 = { "Jouw taak is om de vervuiling naar 0% te brengen voor 2050. Het is nu 2020 dus je hebt nog 30 jaar.",
            "Your task is to reduce the pollution to 0% before 2050. It is now 2020 so you still have 30 years."};
        txtTurorialStep1.text = step2[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        imgTutorialStep2Highlight1.enabled = true;
        imgTutorialStep2Highlight2.enabled = true;

        while (!game.tutorial.tutorialChecks[1])//tutorialStep3)
            yield return null;

        //tutorialStep3 = false;
        imgTutorialOverworld.transform.position = imgPosMiddle;
        string[] step3 = { "Bovenin het scherm staan jouw resources om de vervuiling te verlagen. Welvaart, " + 
                "milieubewustheid, tevredenheid en vervuiling zijn landelijke gemiddelden." + 
                "\n\nGeld: beslissingen maken kost geld. Geld wordt per maand verhoogd door het inkomen." +
                "\n\nVervuiling: de vervuiling in het land, neemt per maand toe of af." +
                "\n\nMilieubewustheid: verlaagt de maandelijkse vervuiling." +
                "\n\nWelvaart: verhoogt het inkomen." +
                "\n\nTevredenheid: beïnvloedt consequenties van beslissingen. Boven 50% is positief, onder 50% is negatief.",
                "At the top of the screen are your recourses to reduce pollution. Prosperity, eco awareness, " + 
                "happiness and pollution are nationwide averages." +
                "\n\nMoney: decisions cost money.Money increases monthly from income." +
                "\n\nPollution: the pollution in the country, increases or decreases monthly." +
                "\n\nEco Awareness: reduces monthly pollution." +
                "\n\nProsperity: increases income." +
                "\n\nHappiness: influences consequences of decisions. Above 50% is positive, below 50% is negative." };
        txtTurorialStep1.text = step3[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        txtTurorialStep1.fontSize = 8;
        imgTutorialStep2Highlight1.gameObject.SetActive(false);
        imgTutorialStep2Highlight2.gameObject.SetActive(false);

        while (!game.tutorial.tutorialChecks[2])//tutorialStep4)
            yield return null;

        imgTutorialOverworld.gameObject.transform.position = imgPosRight;
        game.tutorial.tutorialOnlyWestNL = true;
        game.tutorial.tutorialRegionsClickable = true;
        string[] step4 = { "Er zijn 4 regio’s: Noord, Oost, West, en Zuid. Elke regio heeft een inkomen, " +
                "welvaart, vervuiling, milieubewustheid en tevredenheid." +
                "\n\nGa nu naar West Nederland door op de regio te klikken",
                "There are 4 regions: North, East, West and South. Each region has an income, prosperity, " +
                "pollution, eco-awareness and happiness." +
                "\n\nNow go to The Netherlands West by clicking on the region." };
        txtTurorialStep1.text = step4[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        txtTurorialStep1.fontSize = 9;
        btnTutorialNext.gameObject.SetActive(false);

        if (!game.tutorial.tutorialRegionDone)
        {
            while (!canvasRegioPopup.gameObject.activeSelf)
                yield return null;

            canvasTutorial.gameObject.SetActive(false);

            while (!game.tutorial.tutorialCheckActionDone)
                yield return null;

            while (canvasRegioPopup.gameObject.activeSelf)
                yield return null;
        }
        else
            canvasTutorial.gameObject.SetActive(false);

        game.tutorial.tutorialOnlyWestNL = false;
        canvasTutorial.gameObject.SetActive(true);
        imgBarBottom.gameObject.SetActive(true);
        string[] step5 = { "Onderin het scherm kun je naar het Organisatie menu gaan door op de knop te drukken. \n\nDruk nu op de Organisatie knop.",
            "At the bottom of your screen you can go to the Organization menu by pressing the button.\n\nPress the Organization Button " };
        txtTurorialStep1.text = step5[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        imgTutorialStepOrgMenuHightlight.enabled = true;
        btnOrganization.interactable = true;
        game.tutorial.tutorialOrganizationActive = true;
        imgTutorialOverworld.transform.position = imgPosMiddle;
        btnOrganization.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnOrganization));
        StartCoroutine(ShakeOrganization());
        //imgBarBottom.gameObject.SetActive(true);

        if (!game.tutorial.tutorialOrganizationDone)
        {
            while (!canvasOrganizationPopup.gameObject.activeSelf)
                yield return null;

            imgTutorialStepOrgMenuHightlight.gameObject.SetActive(false);
            canvasTutorial.gameObject.SetActive(false);

            while (!game.tutorial.tutorialOrganizationDone)
                yield return null;

            while (canvasOrganizationPopup.gameObject.activeSelf)
                yield return null;
        }
        else
        {
            imgTutorialStepOrgMenuHightlight.gameObject.SetActive(false);
            canvasTutorial.gameObject.SetActive(false);
        }

        btnNextTurn.interactable = true;
        canvasTutorial.gameObject.SetActive(true);
        btnNextTurn.gameObject.SetActive(true);
        imgTutorialOverworld.transform.position = imgPosRight;
        string[] step6 = { "Om naar de volgende maand en beurt te gaan druk je op de Volgende maand knop rechtsonderin. \n\nDruk nu op de Volgende maand knop. ",
            "You can go to the next month by pressing the Next month button in the bottom right of your screen.\n\nPress the next turn button. " };
        txtTurorialStep1.text = step6[taal];
        txtTutorialStep1BtnText.text = btnText[taal];

        game.tutorial.tutorialNexTurnPossibe = true;

        if (!game.tutorial.tutorialNextTurnDone)
        {
            while (!game.tutorial.tutorialNextTurnDone)
                yield return null;
        }

        game.tutorial.tutorialNexTurnPossibe = false;
        btnNextTurn.interactable = false;


        game.tutorial.tutorialeventsClickable = false;
        canvasTutorial.gameObject.SetActive(true);
        string[] step9 = { "Je kunt linksonder in je scherm ook een knop zien. Deze knop geeft een overzicht van de veranderingen tussen de huidige en de vorige maand. Je krijgt dit rapport elke maand.  " +
                "\n\n Druk op de knop om het maandelijkse overzicht te tonen."
                , "In the bottom left of your screen you can see a button. This button shows the changes between the current and the previous month. You will get this report every month. " +
                "\n\nClick on the button to view your monthly report."};
        txtTurorialStep1.text = step9[taal];
        imgHighlightMonthlyReport.enabled = true;
        btnMonthlyReportStats.interactable = true;
        game.tutorial.tutorialMonthlyReportActive = true;
        imgTutorialOverworld.transform.position = imgPosLeft;

        if (!game.tutorial.tutorialMonthlyReportDone)
        {
            while (!canvasMonthlyReport.gameObject.activeSelf)
                yield return null;

            canvasTutorial.gameObject.SetActive(false);
            imgHighlightMonthlyReport.gameObject.SetActive(false);

            while (!game.tutorial.tutorialMonthlyReportDone)
                yield return null;

            while (canvasMonthlyReport.gameObject.activeSelf)
                yield return null;
        }
        else
        {
            canvasTutorial.gameObject.SetActive(false);
            imgHighlightMonthlyReport.gameObject.SetActive(false);
        }

        //imgTutorialOverworld.gameObject.transform.position = imgNewPos;
        canvasTutorial.gameObject.SetActive(true);
        string[] step7 = { "er is een event bezig. Er kunnen elke nieuwe turn enkele events ontstaan. Er kan maar 1 event " +
                "tegelijk in een regio zijn. \nEr kunnen wel meerdere events tegelijk zijn in meerdere regio's. \nVoor elk event heb je een aantal beurten om te beslissen wat je met de event gaat doen. "
                , "There is an active event running at this moment. Each turn there will be new events. There can only be one event in a region at the same time. " +
                "\nThere can be multiple active events in the whole country. \nFor each event you have a few turns to decide what you are going to do. "  };
        txtTurorialStep1.text = step7[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        btnTutorialNext.gameObject.SetActive(true);

        while (!game.tutorial.tutorialChecks[6]) //tutorialstep19)
            yield return null;

        btnTutorialNext.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        string[] step8 = { "Door op het icoon van de event te klikken krijg je een pop-up. In deze pop-up kun je kiezen welke actie je bij dit event wil nemen. " +
                "\n\nKlik nu op het icoontje van de event. "
                , "By clicking on the icon of the event you get a popup. In this popup you can chose which action you want to take with this event.\n\nClick on the icon of the event to open the popup."};
        txtTurorialStep1.text = step8[taal];
        txtTutorialStep1BtnText.text = btnText[taal];
        game.tutorial.tutorialEventsActive = true;

        if (!game.tutorial.tutorialEventsDone)
        {
            while (!canvasEventPopup.gameObject.activeSelf)
                yield return null;

            canvasTutorial.gameObject.SetActive(false);

            while (!game.tutorial.tutorialEventsDone)
                yield return null;

            while (canvasEventPopup.gameObject.activeSelf)
                yield return null;
        }
        else
            canvasTutorial.gameObject.SetActive(false);


        canvasTutorial.gameObject.SetActive(true);
        btnTutorialNext.gameObject.SetActive(true);
        imgTutorialOverworld.transform.position = imgPosMiddle;
        string[] step10 = { "Je kunt nu verder spelen. "
                , "You can continue playing."};
        txtTurorialStep1.text = step10[taal];
        txtTutorialStep1BtnText.text = btnText[taal];

        while (!game.tutorial.tutorialChecks[8]) //tutorialStep21)
            yield return null;

        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialActive = false;
        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        btnNextTurn.interactable = true;
        btnAfterActionReportCompleted.interactable = true;
    }
    #endregion
    
    #region Init UI Elements
    void initButtons()
    {
        btnMenu.GetComponent<Button>();
        btnTimeline.GetComponent<Button>();
        btnOrganization.GetComponent<Button>();
        btnInvestments.GetComponent<Button>();
        btnMoney.GetComponent<Button>();
        btnHappiness.GetComponent<Button>();
        btnAwareness.GetComponent<Button>();
        btnEnergy.GetComponent<Button>();
        btnPollution.GetComponent<Button>();
        btnPopulation.GetComponent<Button>();
        btnProsperity.GetComponent<Button>();

        btnMonthlyReportStats.GetComponent<Button>();
        btnYearlyReportStats.GetComponent<Button>();
        btnMonthlyReportStats.gameObject.SetActive(false);
        btnYearlyReportStats.gameObject.SetActive(false);

        btnAfterActionReportCompleted.GetComponent<Button>();
        btnAfterActionReportCompleted.gameObject.SetActive(false);

        btnQuests.GetComponent<Button>();
        if (game.currentMonth < 6 && game.currentYear < 2)
            btnQuests.gameObject.SetActive(false);

        btnInvestments.GetComponent<Button>();
        if (game.currentYear < 6)
            btnInvestments.gameObject.SetActive(false);

        btnCards.GetComponent<Button>();
        if (game.currentYear < 4)
            btnCards.gameObject.SetActive(false);

        setBooleans();
    }

    void setBooleans()
    {
        btnMoneyHoverCheck = false;
        btnHappinessHoverCheck = false;
        btnAwarenessHoverCheck = false;
        btnPollutionHoverCheck = false;
        btnProsperityHoverCheck = false;
        btnEnergyHoverCheck = false;
        btnOrganizationCheck = false;
        btnQuestsCheck = false;
        btnTimelineCheck = false;
        btnMonthlyReportCheck = false;
        btnYearlyReportCheck = false;
        btnAfterActionCompletedCheck = false;
        btnCardsHoverCheck = false;
        btnInvestementsHoverCheck = false;
        btnMenuCheck = false;
        popupActive = false;
        regionHouseholdsCheck = false;
        tooltipActive = false;
        regionAgricultureCheck = false;
        regionCompanyCheck = false;
        checkboxHouseholds = true;
        checkboxCompanies = true;
        checkboxAgriculture = true;
        radioEventOption1Check = true;
        radioEventOption2Check = true;
        radioEventOption3Check = true;
        toggleNoordNLCheck = true;
        toggleOostNLCheck = true;
        toggleZuidNLCheck = true;
        toggleWestNLCheck = true;
        refreshCards = false;
    }

    void initCanvas()
    {
        canvasMenuPopup.GetComponent<Canvas>();
        canvasMenuPopup.gameObject.SetActive(false);

        canvasOrganizationPopup.GetComponent<Canvas>();
        canvasOrganizationPopup.gameObject.SetActive(false);

        canvasInvestmentsPopup.GetComponent<Canvas>();
        canvasInvestmentsPopup.gameObject.SetActive(false);

        canvasTimelinePopup.GetComponent<Canvas>();
        canvasTimelinePopup.gameObject.SetActive(false);

        canvasRegioPopup.GetComponent<Canvas>();
        canvasRegioPopup.gameObject.SetActive(false);

        canvasMonthlyReport.GetComponent<Canvas>();
        canvasMonthlyReport.gameObject.SetActive(false);

        canvasYearlyReport.GetComponent<Canvas>();
        canvasYearlyReport.gameObject.SetActive(false);

        //canvasAfterActionCompletedPopup.GetComponent<Canvas>();
        //canvasAfterActionCompletedPopup.gameObject.SetActive(false);

        canvasEventPopup.GetComponent<Canvas>();
        canvasEventPopup.gameObject.SetActive(false);

        canvasBuildingsPopup.GetComponent<Canvas>();
        canvasBuildingsPopup.gameObject.SetActive(false);

        canvasEmptyBuildingsPopup.GetComponent<Canvas>();
        canvasEmptyBuildingsPopup.gameObject.SetActive(false);

        canvasQuestsPopup.GetComponent<Canvas>();
        canvasQuestsPopup.gameObject.SetActive(false);

        canvasCardsPopup.GetComponent<Canvas>();
        canvasCardsPopup.gameObject.SetActive(false);

        canvasSettingsPopup.GetComponent<Canvas>();
        canvasSettingsPopup.gameObject.SetActive(false);

        if (game.tutorial.tutorialActive)
        {
            canvasTutorial.GetComponent<Canvas>();
            canvasTutorial.gameObject.SetActive(true);
        }
    }

    public void LinkGame(Game game)
    {
        this.game = game;

        // Dit kan pas als game gelinked is
        initInvestmentsImages();
    }
    #endregion

    #region Coroutines Buttons and Start Tutorial Quests/Investments/Cards
    public IEnumerator showBtnQuests()
    {
        while (game.currentMonth < 6 && game.currentYear < 2)
            yield return null;

        btnQuests.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnQuests));
        StartCoroutine(ShakeQuests());

        if (game.tutorial.doTuto)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgHighlightQuests.enabled = true;
            imgTutorialOverworld.gameObject.SetActive(true);

            game.tutorial.tutorialQuestsActive = true;
            game.tutorial.tutorialeventsClickable = false;
            game.tutorial.tutorialNexTurnPossibe = false;

            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de Organisatie menu knop gekomen. Dit is de knop voor je Missies. \n\nOpen het Missies menu door op de Missies knop te drukken. ",
            "You can see that an extra button just appeared next to the Organization menu button. This is the button for you Quests. \n\nOpen the Quests menu by pressing the Quests button " };
            string[] btnText = { "Verder", "Next" };
            txtTurorialStep1.text = step1[taal];
            btnTutorialNext.gameObject.SetActive(false);

            if (!game.tutorial.tutorialQuestsDone)
            {
                while (!canvasQuestsPopup.gameObject.activeSelf)
                    yield return null;
            }

            canvasTutorial.gameObject.SetActive(false);
            imgHighlightQuests.gameObject.SetActive(false);
        }
    }

    public IEnumerator showBtnInvestments()
    {
        while (game.currentYear < 6)
            yield return null;

        btnInvestments.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnInvestments));
        StartCoroutine(ShakeInvestments());

        if (game.tutorial.doTuto)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgTutorialOverworld.gameObject.SetActive(true);
            imgHighlightInvestements.enabled = true;

            game.tutorial.tutorialInvestementsActive = true;
            game.tutorial.tutorialNexTurnPossibe = false;

            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de Kaarten menu knop gekomen. Dit is de knop voor Investeren. \n\nOpen het Investeer menu door op de Investeer knop te drukken. ",
            "You can see that an extra button just appeared next to the Cards menu button. This is the button for you Investements. \n\nOpen the Investements menu by pressing the Investements button " };
            string[] btnText = { "Verder", "Next" };
            txtTurorialStep1.text = step1[taal];
            btnTutorialNext.gameObject.SetActive(false);

            if (!game.tutorial.tutorialInvestementsDone)
            {
                while (!canvasInvestmentsPopup.gameObject.activeSelf)
                    yield return null;
            }

            canvasTutorial.gameObject.SetActive(false);
            imgHighlightInvestements.gameObject.SetActive(false);
        }
    }

    public IEnumerator showBtnCards()
    {
        while (game.currentYear < 4)
            yield return null;

        btnCards.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnCards));
        StartCoroutine(ShakeCards());

        if (game.tutorial.doTuto)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgTutorialOverworld.gameObject.SetActive(true);
            imgHighlightCards.enabled = true;

            game.tutorial.tutorialCardsActive = true;
            game.tutorial.tutorialNexTurnPossibe = false;

            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de Missies menu knop gekomen. Dit is de knop voor je Kaarten. \n\nOpen het Kaarten menu door op de Kaarten knop te drukken. ",
            "You can see that an extra button just appeared next to the Quests menu button. This is the button for you Cards. \n\nOpen the Cards menu by pressing the Quests button " };
            string[] btnText = { "Verder", "Next" };
            txtTurorialStep1.text = step1[taal];
            btnTutorialNext.gameObject.SetActive(false);

            if (!game.tutorial.tutorialCardsDone)
            {
                while (!canvasCardsPopup.gameObject.activeSelf)
                    yield return null;
            }

            canvasTutorial.gameObject.SetActive(false);
            imgHighlightCards.gameObject.SetActive(false);
        }
    }

    public void startTutorialBuildings()
    {
        StartCoroutine(tutorialBuildings());
    }

    private IEnumerator tutorialBuildings()
    {
        Vector3 imgPosMiddle = imgTutorialOverworld.gameObject.transform.position;      // Midden in het scherm
        Vector3 imgPosLeft = imgPosMiddle;
        imgPosLeft.x = imgPosLeft.x - Screen.width / 3;                                 // Linksmidden in het scherm

        btnNextTurn.interactable = false;
        canvasTutorial.gameObject.SetActive(true);
        imgTutorialOverworld.gameObject.SetActive(true);
        imgTutorialOverworld.transform.position = imgPosLeft;

        game.tutorial.tutorialBuildingsActive = true;
        game.tutorial.tutorialNexTurnPossibe = false;

        string[] step1 = { "INSERT BUILDINGS TUTORIAL TEXT. ",
            "INSERT BUILDINGS TUTORIAL TEXT. " };
        txtTurorialStep1.text = step1[taal];
        btnTutorialNext.gameObject.SetActive(false);
        game.tutorial.tutorialBuildingsClickable = true;

        if (!game.tutorial.tutorialBuildingsDone)
        {
            while (!canvasEmptyBuildingsPopup.gameObject.activeSelf)
                yield return null;
        }

        canvasTutorial.gameObject.SetActive(false);
        imgTutorialOverworld.transform.position = imgPosMiddle;
    }

    public IEnumerator ChangeScale(Button b)
    {
        Vector3 currentScale = new Vector3(0, 0, 0);
        Vector3 endScale = b.transform.localScale;
        while (currentScale.x < endScale.x && currentScale.y < endScale.y && currentScale.z < endScale.z)
        {
            currentScale.x += endScale.x / 120;
            currentScale.y += endScale.y / 120;
            currentScale.z += endScale.z / 120;

            if (currentScale.x > endScale.x)
                currentScale.x = endScale.x;
            if (currentScale.y > endScale.y)
                currentScale.y = endScale.y;
            if (currentScale.z > endScale.z)
                currentScale.z = endScale.z;
            
            b.transform.localScale = currentScale;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator ShakeOrganization()
    {
        organizationShakes = true;
        btnOrganizationIsClicked = false;
        Quaternion standardRotation = btnOrganization.transform.rotation;
        while (!btnOrganizationIsClicked)
        {
            for (int i = 0; i < 4; i++)
            {
                btnOrganization.transform.Rotate(0, 0, -5);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < 4; i++)
            {
                btnOrganization.transform.Rotate(0, 0, 5);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2);

        }
        btnOrganization.transform.rotation = standardRotation;
        organizationShakes = false;
    }

    public IEnumerator ShakeQuests()
    {
        questsShakes = true;
        btnQuestsIsClicked = false;
        Quaternion standardRotation = btnQuests.transform.rotation;
        while (!btnQuestsIsClicked)
        {
            for (int i = 0; i < 4; i++)
            {
                btnQuests.transform.Rotate(0, 0, -5);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < 4; i++)
            {
                btnQuests.transform.Rotate(0, 0, 5);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2);

        }
        btnQuests.transform.rotation = standardRotation;
        questsShakes = false;
    }

    public IEnumerator ShakeCards()
    {
        cardsShakes = true;
        btnCardsIsClicked = false;
        Quaternion standardRotation = btnCards.transform.rotation;
        while (!btnCardsIsClicked)
        {
            for (int i = 0; i < 4; i++)
            {
                btnCards.transform.Rotate(0, 0, -5);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < 4; i++)
            {
                btnCards.transform.Rotate(0, 0, 5);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2);

        }
        btnCards.transform.rotation = standardRotation;
        cardsShakes = false;
    }

    public IEnumerator ShakeInvestments()
    {
        investmentsShakes = true;
        btnInvestmentsIsClicked = false;
        Quaternion standardRotation = btnInvestments.transform.rotation;
        while (!btnInvestmentsIsClicked)
        {
            for (int i = 0; i < 4; i++)
            {
                btnInvestments.transform.Rotate(0, 0, -5);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < 4; i++)
            {
                btnInvestments.transform.Rotate(0, 0, 5);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2);

        }
        btnInvestments.transform.rotation = standardRotation;
        investmentsShakes = false;
    }
    #endregion

    #region Code for controlling popups
    void popupController()
    {
        // Close active popup with Escape / Open Menu popup with Escape if no popup is active
        if (Input.GetKeyUp(KeyCode.Escape) && !game.tutorial.tutorialRegionActive && !game.tutorial.tutorialEventsActive &&
            !game.tutorial.tutorialQuestsActive && !game.tutorial.tutorialOrganizationActive &&
            !game.tutorial.tutorialMonthlyReportActive && !game.tutorial.tutorialCardsActive && !game.tutorial.tutorialInvestementsActive
            && !game.tutorial.tutorialBuildingsActive)
            closeWithEscape();

        // Open and close Organization popup with O
        else if (Input.GetKeyUp(KeyCode.O))
            //if (game.tutorial.tutorialStep8)
            controllerOrganizationHotkey();

        // Open and close Timeline popup with T
        else if (Input.GetKeyUp(KeyCode.T))
            if (!game.tutorial.tutorialActive)
                controllerTimelinePopup();
    }

    // Close the active popup with the Escape key (and open main menu with escape if no popup is active)
    void closeWithEscape()
    {
        EventManager.CallPlayButtonClickSFX();
        if (!popupActive)// && !tutorialActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            initButtonText();
        }
        else if (canvasOrganizationPopup.gameObject.activeSelf)
        {;
            canvasOrganizationPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasCardsPopup.gameObject.activeSelf)
        {
            canvasCardsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasInvestmentsPopup.gameObject.activeSelf)
        {
            canvasInvestmentsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasMenuPopup.gameObject.activeSelf)
        {
            canvasMenuPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasTimelinePopup.gameObject.activeSelf)
        {
            canvasTimelinePopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasRegioPopup.gameObject.activeSelf)
        {
            canvasRegioPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        if (canvasMonthlyReport.gameObject.activeSelf)
        {
            canvasMonthlyReport.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        if (canvasYearlyReport.gameObject.activeSelf)
        {
            canvasYearlyReport.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasQuestsPopup.gameObject.activeSelf)
        {
            canvasQuestsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasEventPopup.gameObject.activeSelf)
        {
            canvasEventPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasBuildingsPopup.gameObject.activeSelf)
        {
            canvasBuildingsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasEmptyBuildingsPopup.gameObject.activeSelf)
        {
            canvasEmptyBuildingsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasSettingsPopup.gameObject.activeSelf)
        {
            canvasSettingsPopup.gameObject.SetActive(false);
            canvasMenuPopup.gameObject.SetActive(true);
        }
    }

    // Open and close the Organization popup with the O key
    void controllerOrganizationHotkey()
    {
        if (!popupActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
        else if (canvasOrganizationPopup.gameObject.activeSelf)
        {
            canvasOrganizationPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
    }

    // Open and close the Timeline popup with the T key
    void controllerTimelinePopup()
    {
        if (!popupActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
        else if (canvasTimelinePopup.gameObject.activeSelf)
        {
            canvasTimelinePopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
    }
    #endregion

    #region onGUI Code
    void OnGUI()
    {
        Rect lblReqt;
        lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

        if (checkTooltip() && !popupActive)// && game.tutorial.tutorialStep3)
        {
            lblReqt.x = v3Tooltip.x + 10; lblReqt.y = v3Tooltip.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (regionHouseholdsCheck && popupActive)// && game.tutorial.tutorialStep3)
        {
            v3Tooltip = emptybtnHoverHouseholds.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 70;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipHouseholds + "</color>", tooltipStyle);
            updateRegionSectors();
            
        }
        else if (regionAgricultureCheck && popupActive)// && game.tutorial.tutorialStep3)
        {
            v3Tooltip = emptybtnHoverAgriculture.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 150;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipAgriculture + "</color>", tooltipStyle);
            updateRegionSectors();
        }
        else if (regionCompanyCheck && popupActive)// && game.tutorial.tutorialStep3)
        {
            v3Tooltip = emptybtnHoverCompanies.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 270;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipCompany + "</color>", tooltipStyle);
            updateRegionSectors();
        }
        else if (canvasCardsPopup.gameObject.activeSelf)
        {
            float yOffset = 0f;

            // Get the X position of the button
            RectTransform rectBtnCardsPosition = btnCardsPosition.GetComponent<RectTransform>();
            Vector3 btnPos = btnCardsPosition.transform.position;

            float screenHeight = Screen.height;
            float x = btnPos.x;
            float y = btnPos.z + (screenHeight / 3);

            //foreach (Card c in game.cards)
            foreach (Card c in game.inventory.ownedCards)
            {
                if (GUI.Button(new Rect(x, y + yOffset, rectBtnCardsPosition.rect.width + 50, rectBtnCardsPosition.rect.height), c.name[taal], buttonStyle))
                    setTextCardInformation(c);

                yOffset += 35;
            }
        }
        else if (canvasEmptyBuildingsPopup.gameObject.activeSelf)
        {
            float yOffset = 0f;

            RectTransform rectBtnBuildingsPosition = btnCardsPosition.GetComponent<RectTransform>();
            Vector3 btnPos = btnEmptyBuildingPosition.transform.position;
            float screenHeight = Screen.height;

            float x = btnPos.x;
            float y = btnPos.z + (screenHeight / 3);

            foreach (Building b in regionToBeBuild.possibleBuildings)
            {
                if (GUI.Button(new Rect(x, y + yOffset, rectBtnBuildingsPosition.rect.width + 50, rectBtnBuildingsPosition.rect.height), b.buildingName[taal], buttonStyle))
                    setTextBuildingInformation(b);

                yOffset += 35;
            }
        }
    }

    bool checkTooltip()
    {
        if (btnPollutionHoverCheck)
        {
            v3Tooltip = btnPollution.gameObject.transform.position;
            return true;
        }
        else if (btnAwarenessHoverCheck)
        {
            v3Tooltip = btnAwareness.gameObject.transform.position;
            return true;
        }
        else if (btnMoneyHoverCheck)
        {
            v3Tooltip = btnMoney.gameObject.transform.position;
            return true;
        }
        else if (btnEnergyHoverCheck)
        {
            v3Tooltip = btnEnergy.gameObject.transform.position;
            return true;
        }
        else if (btnProsperityHoverCheck)
        {
            v3Tooltip = btnProsperity.gameObject.transform.position;
            return true;
        }
        else if (btnHappinessHoverCheck)
        {
            v3Tooltip = btnHappiness.gameObject.transform.position;
            return true;
        }
        else
            return false;
    }
    #endregion

    #region Updating Text and Color Values of Icons
    // Update Date and Month based on value
    public void updateDate(int month, int year)
    {
        month = month - 1;
        string[,] arrMonths = new string[2, 12]
        {
            { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
            { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }
        };
        txtDate.text = arrMonths[taal, month] + " - " + (year + 2019).ToString();
    }

    // Update Money based on value
    public void updateMoney(double money)
    {
        txtMoney.text = money.ToString("0");
    }

    // Update Awareness based on value
    public void updateAwarness(double awareness)
    {
        iconController(btnAwareness, txtAwarness, awareness);
    }

    // Update Pollution based on value
    public void updatePollution(double pollution)
    {
        iconController(btnPollution, txtPollution, pollution);
    }

    public void updateProsperity(double prosperity)
    {
        iconController(btnProsperity, txtProsperity, prosperity);
    }

    // Update Happiness based on value
    public void updateHappiness(double happiness)
    {
        iconController(btnHappiness, txtHappiness, happiness);
    }

    /* Update Energy based on value
    public void updateEnergy(double energy)
    {
        iconController(btnEnergy, energy);
    } */


    /* Update Population based on value
    public void updatePopulation(double population)
    {
        int popu = Convert.ToInt32(population);
        txtPopulation.text = popu.ToString();
    }*/

    // Change color of the button based on value
    void iconController(Button btn, Text txt, double value)
    {
        ColorBlock cb;
        Color lerpColor;
        float f = (float)value / 100;
        cb = btn.colors;

        // Pollution moet laag zijn om goed te zijn, de rest hoog
        if (btn == btnPollution)
        {
            // Color based on third argument (value / 100)
            lerpColor = Color.Lerp(Color.green, Color.red, f);
        }
        else
        {
            // Color based on third argument (value / 100)
            lerpColor = Color.Lerp(Color.red, Color.green, f);
        }

        cb.normalColor = lerpColor;
        cb.highlightedColor = lerpColor;
        cb.pressedColor = lerpColor;
        btn.colors = cb;
        txt.text = " " + value.ToString("0") + "%";
        txt.color = lerpColor;
    }
    #endregion

    #region Update UI in Tooltips
    public void updateMoneyTooltip(double income)
    {
        string[] tip = { "Inkomen: " + income.ToString("0"),
            "Income: " + income.ToString("0") };
        txtTooltip = tip[taal];                 //"Donaties: " + donations + "\nInkomen: " + income;
    }

    public void updateHappinessTooltip(double happ, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip = { "Gemiddelde tevredenheid per regio:\nNoord-Nederland: "+ happ.ToString("0.00") + "\n",
                    "Average happiness per region:\nThe Netherlands North: "+ happ.ToString("0.00") + "\n" };
                txtTooltip = tip[taal];//"Gemiddelde tevredenheid per regio:\nNoord-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + happ.ToString("0.00") + "\n",
                    "The Netherlands East: " + happ.ToString("0.00") + "\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + happ.ToString("0.00") + "\n",
                    "The Netherlands West: " + happ.ToString("0.00") + "\n"};
                txtTooltip += tip3[taal];//"West-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + happ.ToString("0.00"),
                    "The Netherlands South: " + happ.ToString("0.00") };
                txtTooltip += tip4[taal];//"Zuid-Nederland: " + happ.ToString("0.00");
                break;
        }
    }

    public void updateAwarnessTooltip(double awareness, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip1 = { "Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + awareness.ToString("0.00") + "%\n",
                    "Average eco awareness per region: \nThe Netherlands North: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip = tip1[taal];
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + awareness.ToString("0.00") + "%\n",
                    "The Netherlands East: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip += tip2[taal];
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + awareness.ToString("0.00") + "\n",
                    "The Netherlands West: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip += tip3[taal];
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + awareness.ToString("0.00") + "%",
                    "The Netherlands South: " + awareness.ToString("0.00") + "%"};
                txtTooltip += tip4[taal];
                break;
        }
    }

    public void updatePollutionTooltip(double pollution, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip1 = { "Gemiddelde vervuiling per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "Average pollution per region: \nThe Netherlands North: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip = tip1[taal];
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "The Netherlands East: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip += tip2[taal];
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "The Netherlands West: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip += tip3[taal];
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + pollution.ToString("0.00") + "%",
                    "The Netherlands South: " + pollution.ToString("0.00") + "%"};
                txtTooltip += tip4[taal];
                break;
        }
    }

    public void updateProsperityTooltip(double prosperity, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip1 = { "Gemiddelde welvaart per regio:\nNoord-Nederland: " + prosperity.ToString("0.00") + "%\n",
                    "Average prosperity per region: \nThe Netherlands North: " + prosperity.ToString("0.00") + "%\n"};
                txtTooltip = tip1[taal];
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + prosperity.ToString("0.00") + "%\n",
                    "The Netherlands East: " + prosperity.ToString("0.00") + "%\n"};
                txtTooltip += tip2[taal];
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + prosperity.ToString("0.00") + "%\n",
                    "The Netherlands West: " + prosperity.ToString("0.00") + "%\n"};
                txtTooltip += tip3[taal];
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + prosperity.ToString("0.00") + "%",
                    "The Netherlands South: " + prosperity.ToString("0.00") + "%"};
                txtTooltip += tip4[taal];
                break;
        }
    }

    public void updateEnergyTooltip(double green, double fossil, double nuclear)
    {
        string[] tip = { "Groene energie: " + green.ToString() + "%\nFossiele energie: "
            + fossil + "%\nKernenergie: " + nuclear + "%",
            "Green energy " + green.ToString() + "%\nFossil energy: "
            + fossil + "%\nNuclearenergy: " + nuclear + "%"};
        txtTooltip = tip[taal];         
    }
    #endregion

    #region Code for Organization Popup
    public void updateOrganizationScreenUI()
    {
        foreach (Region region in game.regions)
        {
            if (region.name[0] == "Noord Nederland")
                txtOrgNoordMoney.text = (region.statistics.income * 12).ToString();
            else if (region.name[0] == "Oost Nederland")
                txtOrgOostMoney.text = (region.statistics.income * 12).ToString();
            else if (region.name[0] == "Zuid Nederland")
                txtOrgZuidMoney.text = (region.statistics.income * 12).ToString();
            else if (region.name[0] == "West Nederland")
                txtOrgWestMoney.text = (region.statistics.income * 12).ToString();
        }

        txtOrgBank.text = game.gameStatistics.money.ToString("0");

        imgTutorialOrganization.enabled = false;
        txtTutorialOrganization.enabled = false;
        btnTutorialOrganization.gameObject.SetActive(false);

        //initOrganizationText();
        initAdvisersText();

        if (/*tutorialStep8 && */game.tutorial.tutorialActive && game.tutorial.tutorialOrganizationActive)
        {
            imgTutorialOrganization.enabled = true;
            txtTutorialOrganization.enabled = true;
            btnTutorialOrganization.gameObject.SetActive(true);
            StartCoroutine(tutorialOrganizationPopup());
        }
    }

    IEnumerator tutorialOrganizationPopup()
    {
        string[] step1 = { "In het organisatie menu kun je het jaarlijks inkomen zien van elke regio. Handig dus om te bepalen hoeveel je kan uitgeven het komende jaar. " +
                "\n\nVerder kun je hier advies zien van je economische adviseur en je vervuilingsadviseur op basis van de status van die statistieken. \n\nJe kunt dit menu sluiten door op de ESC toets te drukken."
                , "In the organization menu you can view the yearly income of each region. This can come in handy when deciding your expanses the coming year. " +
                "\n\nYou can also view the advice from your economic advisor and your pollution advisor based on the value of these statistics. \n\nYou can close this menu by pressing the ESC key."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialOrganization.text = step1[taal];
        txtTutorialOrganizationBtnText.text = btnText[taal];

        while (!game.tutorial.tutorialChecks[4])//tutorialStep9)
            yield return null;

        imgTutorialOrganization.gameObject.SetActive(false);
        game.tutorial.tutorialOrganizationDone = true;
        game.tutorial.tutorialOrganizationActive = false;
    }

    private void initAdvisersText()
    {
        txtAdviserEconomic.text = game.economyAdvisor.name[taal] + "\n" + game.economyAdvisor.displayMessage[taal];
        txtAdviserPollution.text = game.pollutionAdvisor.name[taal] + "\n" + game.pollutionAdvisor.displayMessage[taal];
    }

    private void initOrganizationText()
    {
        string[] left = { "Budget", "Budget" };
        string[] right = { "Adviseurs", "advisor" };
        string[] title = { "Organisatie", "Organization" };
        string[] bank = { "Bank", "Storage" };
        string[] noord = { "Noord-Nederland", "The Netherlands North" };
        string[] oost = { "Oost-Nederland", "The Netherlands East" };
        string[] zuid = { "Zuid-Nederland", "The Netherlands South" };
        string[] west = { "West-Nederland", "The Netherlands West" };
        string[] yearly = { "Jaarlijks budget per regio", "Yearly budget per region" };
        string[] big = {"Zie hier het advies van je economische adviseur en je vervuilingsadviseur.",
                        "Here you can see the advice of your economic advisor and your pollution advisor. " };

        txtBigDescription.text = big[taal];
        txtColumnLeft.text = left[taal];
        txtColumnRight.text = right[taal];
        txtOrganizatonTitle.text = title[taal];
        txtOrgBankDescription.text = bank[taal];
        txtOrgNoordMoneyDescription.text = noord[taal];
        txtOrgOostMoneyDescription.text = oost[taal];
        txtOrgWestMoneyDescription.text = west[taal];
        txtOrgZuidMoneyDescription.text = zuid[taal];
        txtYearlyBudget.text = yearly[taal];

        // Oude investeringen text
        /* "Hier kun je een gedeelte van het geld op je bank investeren in de " + 
     "\norganistie. Als je meer geld in een onderdeel zet heb je en grotere" + 
     "\nkans op succes in dat onderdeel. 1 vakje is 10000", "You can invest some of your budget in your " +
     "own organization. If you invest more in one of the segments, you have a higher" + 
     "chance of success. One block equals 10000" }; */

        // string[] demonstration = { "Demonstraties", "Demonstrations" };
        // string[] research = { "Onderzoek", "Research" };
        // string[] guarding = { "Eco bescherming", "Eco guarding" };

        // txtDemonstration.text = demonstration[taal];
        // txtResearch.text = research[taal];
        // txtEcoGuarding.text = guarding[taal];
    }
    #endregion

    #region Code for the Region Popup
    public void regionClick(Region region)
    {
        // Ga naar WEST tijdens de tutorial
        if (game.tutorial.tutorialActive /*&& tutorialStep5 && tutorialRegionsClickable*/)
        {
            if (game.tutorial.tutorialOnlyWestNL && game.tutorial.tutorialRegionsClickable)
            {
                if (region.name[0] == "West Nederland")
                {
                    startRegionPopup(region);
                    game.tutorial.regionWestActivated = true;

                    btnTutorialRegion.gameObject.SetActive(true);
                    StartCoroutine(tutorialRegionPopup());
                }
            }
            else if (game.tutorial.tutorialRegionsClickable)
            {
                startRegionPopup(region);
                imgTutorialRegion.gameObject.SetActive(false);
            }
        }
        // Andere reagions clickable tijdens tutorial
        // Na de tutorial
        else if (!canvasRegioPopup.gameObject.activeSelf && !popupActive && !btnOrganizationCheck
        && !btnMenuCheck && !btnTimelineCheck && !game.tutorial.tutorialActive && !btnAfterActionStatsCheck && !btnAfterActionCompletedCheck && !btnQuestsCheck && !btnMonthlyReportCheck && !btnYearlyReportCheck
        && !btnInvestementsHoverCheck && !btnCardsHoverCheck)
        {
            startRegionPopup(region);
            imgTutorialRegion.gameObject.SetActive(false);
        }
    }

    private void startRegionPopup(Region region)
    {
        regio = region;
        canvasRegioPopup.gameObject.SetActive(true);
        popupActive = true;
        EventManager.CallPopupIsActive();
        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();
        updateRegionScreenUI();
    }

    IEnumerator tutorialRegionPopup()
    {
        game.tutorial.tutorialRegionActive = true;

        string[] step1 = { "Elke regio bestaat uit 3 sectoren. Deze sectoren zijn Huishoudens, Landbouw en Bedrijven. \n\nDe sectoren hebben statistieken voor tevredenheid, vervuiling, milieubewustheid en welvaart. "
                + "Deze sectoren statistieken maken het gemiddelde waar de regio statistieken uit bestaan. \n\nJe kunt deze sector statistieken zien door met je muis over de sector te hoveren."
                , "Each region has 3 sectors. These sectors are Households, Agriculture and Companies. \n\nThese sectors have statistics for happiness, pollution, eco awareness and prosperity. " +
                "These sector statistics create the averages which are the region statistics. It is important to keep each sector happy. \n\nYou can view these sector statistics by using your mouse to hover over the sector. "};
        string[] btnText = { "Verder", "Next" };

        txtTutorialRegion.text = step1[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        while (!game.tutorial.tutorialChecks[3])//tutorialStep6)
            yield return null;

        string[] step2 = { "Je kunt in een regio acties uitvoeren. Acties kosten echter geld en meestal ook tijd. \n\nSommige acties kunnen ook maar 1 keer of eens in de zoveel tijd gedaan worden. "
                + "Als je een actie kiest krijg je een aantal gegevens over de actie te zien. \n\nDaarnaast kun je kiezen op welke sectoren je de actie invloed uitoefent. Sommige acties kunnen in elke sector gedaan worden, andere in 1 of 2 van de sectoren. "
                + "\n\nKies nu een actie, keer vervolgens terug naar de landkaart door op de ESC toets te drukken. "
                , "You can do actions in regions. These actions cost money and most of the time also time. " +
                "\n\nSome actions you can only do once, others you can do again after some time. When you chose an action you can see a few statistics about the action. \n\nYou also have to choose in which sectors you want the action to do things. " +
                "Some actions can be done in each sectors, others only in 1 or 2 of the sectors. \n\nChoose an action, after that, return to the map by pressing the ESC key."};

        txtTutorialRegion.text = step2[taal];
        btnTutorialRegion.gameObject.SetActive(false);
        Vector3 imgOldPos = imgTutorialRegion.gameObject.transform.position;
        Vector3 imgNewPos = imgOldPos;
        imgNewPos.x = imgNewPos.x - Screen.width / 4;
        imgTutorialRegion.gameObject.transform.position = imgNewPos;

        while (!game.tutorial.tutorialCheckActionDone)
            yield return null;

        imgTutorialRegion.gameObject.SetActive(false);
        game.tutorial.tutorialRegionActive = false;
        game.tutorial.tutorialRegionDone = true;
    }

    private void updateRegionScreenUI()
    {
        // Set the text in the popup based on language
        //initRegionText();

        updateRegionTextValues();

        // Set the right actions in the dropdown
        initDropDownRegion();

        // Set toggles on not active
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);
    }

    private void initRegionText()
    {
        string[] txtHappiness = { "Tevredenheid", "Happiness" };
        string[] txtEcoAwareness = { "Milieubewustheid", "Eco awareness" };
        string[] txtIncome = { "Inkomen", "Income" };
        string[] txtPollution = { "Vervuiling", "Pollution" };
        string[] txtAir = { "Luchtvervuiling", "Air pollution" };
        string[] txtNature = { "Natuurvervuiling", "Nature pollution" };
        string[] txtWater = { "Watervervuiling", "Water pollution" };
        string[] txtHouseholds = { "Huishoudens", "Households" };
        string[] txtAgriculture = { "Landbouw", "Agriculture" };
        string[] txtCompaines = { "Bedrijven", "Companies" };
        string[] txtCenter = { "Actief", "Active" };
        string[] txtRight = { "Nieuwe actie", "New action" };
        string[] txtLeft = { "Regiostatistieken", "Region statistics" };
        string[] txtActiveEvents = { "Actieve events", "Active events" };
        string[] txtActiveActions = { "Actieve acties", "Active actions" };
        string[] btnDoAction = { "Doe actie", "Do action" };
        string[] txtProsperity = { "Welvaart", "Prosperity" };

        txtRegionHappinessDescription.text = txtHappiness[taal];
        txtRegionEcoAwarenessDescription.text = txtEcoAwareness[taal];
        txtRegionIncomeDescription.text = txtIncome[taal];
        txtRegionPollutionDescription.text = txtPollution[taal];
        txtRegionAirDescription.text = txtAir[taal];
        txtRegionProsperityDescription.text = txtProsperity[taal];
        txtRegionNatureDescription.text = txtNature[taal];
        txtRegionWaterDescription.text = txtWater[taal];
        txtRegionHouseholdsDescription.text = txtHouseholds[taal];
        txtRegionAgricultureDescription.text = txtAgriculture[taal];
        txtRegionCompainesDescription.text = txtCompaines[taal];
        txtRegionColumnLeft.text = txtLeft[taal];
        txtRegionColumnRight.text = txtRight[taal];
        txtRegionColumnCenter.text = txtCenter[taal];
        txtActiveActionDescription.text = txtActiveActions[taal];
        txtActiveEventsDescription.text = txtActiveEvents[taal];
        btnDoActionText.text = btnDoAction[taal];
        txtCheckboxHouseholds.text = txtHouseholds[taal];
        txtCheckboxAgriculture.text = txtAgriculture[taal];
        txtCheckboxCompanies.text = txtCompaines[taal];
    }

    private void updateRegionTextValues()
    {
        txtRegionName.text = regio.name[taal];
        txtRegionMoney.text = regio.statistics.income.ToString("0");
        txtRegionHappiness.text = regio.statistics.happiness.ToString("0.00");
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString("0.00") + "%";
        txtRegionProsperity.text = regio.statistics.prosperity.ToString("0.00") + "%";
        txtRegionPollution.text = regio.statistics.avgPollution.ToString("0.00") + "%";
        txtRegionPollutionAir.text = regio.statistics.avgAirPollution.ToString("0.00") + "%";
        txtRegionPollutionNature.text = regio.statistics.avgNaturePollution.ToString("0.00") + "%";
        txtRegionPollutionWater.text = regio.statistics.avgWaterPollution.ToString("0.00") + "%";

        // Set text of actions to empty
        txtRegionActionConsequences.text = "";
        txtRegionActionCost.text = "";
        txtRegionActionDuration.text = "";
        txtRegionActionName.text = "";
        txtRegionActionNoMoney.text = "";
        txtRegionActionSectorTotalCostDescription.text = "";
        txtRegionActionSectorTotalCost.text = "";

        txtRegionActionNoMoney.text = "";
        txtActionSectorsDescription.text = "";
        btnDoActionRegionMenu.gameObject.SetActive(false);
        dropdownChoiceMade = false;

        updateActiveActions();
        updateActiveEvents();

        dropdownRegio.gameObject.SetActive(true);
        initDropDownRegion();
    }

    private void updateRegionSectors()
    {
        foreach (RegionSector sector in regio.sectors)
        {
            if (sector.sectorName[taal] == "Huishoudens" || sector.sectorName[taal] == "Households")
            {
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nTevredenheid: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nWelvaart: " + sector.statistics.prosperity.ToString("0.00")  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWater pollution: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nHappiness: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nProsperity: " + sector.statistics.prosperity.ToString("0.00")  + "%"};
                txtTooltipHouseholds = tip[taal];       
    }
            else if (sector.sectorName[taal] == "Bedrijven" || sector.sectorName[taal] == "Companies")
            {
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nTevredenheid: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nWelvaart: " + sector.statistics.prosperity.ToString("0.00")  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWater pollution: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nHappiness: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nProsperity: " + sector.statistics.prosperity.ToString("0.00")  + "%"};
                txtTooltipCompany = tip[taal];
            }
            else if (sector.sectorName[taal] == "Landbouw" || sector.sectorName[taal] == "Agriculture")
            {
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nTevredenheid: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nWelvaart: " + sector.statistics.prosperity.ToString("0.00")  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWater pollution: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nHappiness: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nProsperity: " + sector.statistics.prosperity.ToString("0.00") + "%"};

                txtTooltipAgriculture = tip[taal];
            }
        }
    }

    private void updateActiveEvents()
    {
        string activeEventsRegio = "";

        foreach (GameEvent ge in regio.inProgressGameEvents)
        {
            if (ge.isActive || ge.isIdle)
                activeEventsRegio += ge.publicEventName[taal] + "\n";
        }

        txtActiveEvents.text = activeEventsRegio;
    }

    private void updateActiveActions()
    {
        string activeActionsRegio = "";
        foreach (RegionAction action in regio.actions)
        {
            if (action.isActive)
            {
                activeActionsRegio += action.name[taal] + "\n";
            }

            txtActiveActions.text = activeActionsRegio;
        }
    }

    private void initDropDownRegion()
    {
        dropdownRegio.ClearOptions();
        int currentMonth = game.currentYear * 12 + game.currentMonth;
        string[] dropdownPlaceholderText = { "Selecteer een actie", "Choose an action" };

        dropdownRegio.captionText.text = dropdownPlaceholderText[taal];

        foreach (RegionAction action in regio.actions)
        {
            if (action.isAvailable && !action.isActive &&
                (action.lastCompleted + action.actionCooldown <= currentMonth || action.lastCompleted == 0) &&
                !(action.isUnique && action.lastCompleted > 0))
            {
                dropdownRegio.options.Add(new Dropdown.OptionData() { text = action.name[taal] });
            }
        }

        //code to bypass Unity bug -> can't set .value outside the dropdown range
        dropdownRegio.options.Add(new Dropdown.OptionData() { text = " "});
        dropdownRegio.value = dropdownRegio.options.Count - 1;
        dropdownRegio.options.RemoveAt(dropdownRegio.options.Count - 1);
    }

    // Goes to this method from DropDownTrigger in Inspector
    public void getDropDownValue()
    {
        EventManager.CallPlayButtonClickSFX();
        for (int i = 0; i <= dropdownRegio.options.Count; i++)
        {
            if (dropdownRegio.value == i)
            {
                dropdownChoice = dropdownRegio.options[i].text;
            }
        }

        // Shows the right information with the chosen option in dropdown
        showInfoDropDownRegion();
    }

    private void showInfoDropDownRegion()
    {
        foreach (RegionAction action in regio.actions)
        {
            if (action.name[taal] == dropdownChoice)
            {
                regioAction = action;

                string[] actionCostText = { "Kosten per sector: " + action.afterInvestmentActionMoneyCost + " geld",
                    "Costs per sector: " + action.afterInvestmentActionMoneyCost + " geld" };
                string[] actionDurationText = { "Duur: " + regioAction.actionDuration.ToString() + " maanden",
                    "Duration: " + regioAction.actionDuration.ToString() + " months" };

                string[] txtSectorMoney = { "Totale kosten", "Total cost" };
                string[] sectorDescription = { "Mogelijke sectoren", "Possible sectors" };
                dropdownChoiceMade = true;

                txtRegionActionName.text = regioAction.description[taal];
                txtRegionActionCost.text = actionCostText[taal];
                txtRegionActionDuration.text = actionDurationText[taal];
                //txtRegionActionConsequences.text = getActionConsequences(action.consequences);
                txtActionSectorsDescription.text = sectorDescription[taal];
                txtRegionActionSectorTotalCostDescription.text = txtSectorMoney[taal];
                txtRegionActionSectorTotalCost.text = regioActionCost.ToString();

                setCheckboxes(action);
                regioActionCost = 0;
                txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
            }
        }
    }

    private void setCheckboxes(RegionAction action)
    {
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);

        playSelectSound = false;
        if (checkboxHouseholds)
            checkboxRegionHouseholds.isOn = false;
        if (checkboxAgriculture)
            checkboxRegionAgriculture.isOn = false;
        if (checkboxCompanies)
            checkboxRegionCompanies.isOn = false;
        playSelectSound = true;

        for (int i = 0; i < action.possibleSectors.Length; i++)
        {
            if (action.possibleSectors[i] == "Huishoudens")
            {
                checkboxRegionHouseholds.gameObject.SetActive(true);
            }
            if (action.possibleSectors[i] == "Bedrijven")
            {
                checkboxRegionAgriculture.gameObject.SetActive(true);
            }
            if (action.possibleSectors[i] == "Landbouw")
            {
                checkboxRegionCompanies.gameObject.SetActive(true);
            }
        }
    }

    public void btnDoActionRegionMenuClick()
    {
        regio.StartAction(regioAction, game, new bool[] { checkboxHouseholds, checkboxCompanies, checkboxAgriculture });

        btnDoActionRegionMenu.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);
        regioActionCost = 0;

        if (!checkboxHouseholds)
            checkboxRegionHouseholds.isOn = true;

        if (!checkboxAgriculture)
            checkboxRegionAgriculture.isOn = true;

        if (!checkboxCompanies)
            checkboxRegionCompanies.isOn = true;

        if (!game.tutorial.tutorialCheckActionDone)
            game.tutorial.tutorialCheckActionDone = true;

        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();
        updateRegionTextValues();
    }
    #endregion

    #region Code for Monthly/Yearly Report Popup
    IEnumerator tutorialMonthlyReport()
    {
        string[] step1 = { "In dit maandelijkse rapport kun je de veranderingen van de statistieken zien ten opzichte van vorige maand. Daarnaast kun je ook zien of er nieuwe events in een regio zijn. " +
                "Je krijgt dit rapport elke maand opnieuw. \n\nJe kunt dit menu sluiten door op de ESC toets te drukken."
                , "In this monthly report you can see the changes in the statistics with the previous month. You can also see if there is a new event in a region. " +
                "You get this report each month. \n\nYou can close this menu by pressing the ESC key."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialAfterTurn.text = step1[taal];
        txtTutorialAfterTurnBtn.text = btnText[taal];

        while (!game.tutorial.tutorialChecks[5])//tutorialStep18)
            yield return null;
        
        imgTutorialAfterTurn.gameObject.SetActive(false);
        game.tutorial.tutorialMonthlyReportDone = true;
        game.tutorial.tutorialMonthlyReportActive = false;
    }

    public void InitMonthlyReport()
    {
        monthlyNewEvents = (List<GameEvent>[])game.monthlyReport.newEvents.Clone();
        updateTextAfterActionStats(true);
        calculateDifference(game.monthlyReport.oldIncome, game.monthlyReport.oldHappiness, game.monthlyReport.oldEcoAwareness, game.monthlyReport.oldPollution, game.monthlyReport.oldProsperity, true);
    }

    public void InitYearlyReport()
    {
        updateTextAfterActionStats(false);
        calculateDifference(game.yearlyReport.oldIncome, game.yearlyReport.oldHappiness, game.yearlyReport.oldEcoAwareness, game.yearlyReport.oldPollution, game.yearlyReport.oldProsperity, false);
    }

    private void updateTextAfterActionStats(bool isMonthly)
    {

        string[] txtRight = { "West-Nederland", "The Netherlands West" };
        string[] txtRightMiddle = { "Zuid-Nederland", "The Netherlands South" };
        string[] txtLeftMiddle = { "Oost-Nederland", "The Netherlands East" };
        string[] txtLeft = { "Noord-Nederland", "The Netherlands North" };
        string[] txtIncomeDescription = { "Inkomen", "Income" };
        string[] txtHappinessDescription = { "Tevredenheid", "Happiness" };
        string[] txtEcoAwarenessDescription = { "Milieubewustheid", "Eco Awareness" };
        string[] txtPollutionDescription = { "Vervuiling", "Pollution" };
        string[] txtProsperityDescription = { "Welvaart", "Prosperity" };
        string[] txtDescription = { "<b>Veranderde waardes</b>", "<b>Changed values</b>" };
        string[] txtNewEventDescription = { "<b>Nieuwe events</b>", "<b>New events</b>" };

        if (isMonthly)
        {
            string[] txtTitleMonthly = { "Maandelijks rapport", "Monthly report" };
            txtAfterActionStatsName.text = txtTitleMonthly[taal];

            txtAfterActionStatsColumnLeft.text = txtLeft[taal];
            txtAfterActionStatsColumnLeftMiddle.text = txtLeftMiddle[taal];
            txtAfterActionStatsColumnRight.text = txtRight[taal];
            txtAfterActionStatsColumnRightMiddle.text = txtRightMiddle[taal];
            txtAfterActionStatsColumnLeftDescription.text = txtDescription[taal];
            txtAfterActionStatsColumnLeftMiddleDescription.text = txtDescription[taal];
            txtAfterActionStatsColumnRightMiddleDescription.text = txtDescription[taal];
            txtAfterActionStatsColumnRightDescription.text = txtDescription[taal];
        }
        else
        {
            string[] txtTitleYearly = { "Jaarlijks rapport", "Yearly report" };
            txtAfterActionStatsNameYearly.text = txtTitleYearly[taal];

            txtAfterActionStatsColumnLeftYearly.text = txtLeft[taal];
            txtAfterActionStatsColumnLeftMiddleYearly.text = txtLeftMiddle[taal];
            txtAfterActionStatsColumnRightYearly.text = txtRight[taal];
            txtAfterActionStatsColumnRightMiddleYearly.text = txtRightMiddle[taal];
            txtAfterActionStatsColumnLeftDescriptionYearly.text = txtDescription[taal];
            txtAfterActionStatsColumnLeftMiddleDescriptionYearly.text = txtDescription[taal];
            txtAfterActionStatsColumnRightMiddleDescriptionYearly.text = txtDescription[taal];
            txtAfterActionStatsColumnRightDescriptionYearly.text = txtDescription[taal];
        }
    }


    private void calculateDifference(double[] oldIncome, double[] oldHappiness, double[] oldEcoAwareness, double[] oldPollution, double[] oldProsperity, bool isMonthly)
    {
        double incomeDifference = 0;
        double happinessDifference = 0;
        double ecoAwarenessDifference = 0;
        double pollutionDifference = 0;
        double prosperityDifference = 0;

        for (int i = 0; i < game.monthlyReport.reportRegions.Length; i++)
        {
            incomeDifference = game.regions[i].statistics.income - oldIncome[i];
            happinessDifference = game.regions[i].statistics.happiness - oldHappiness[i];
            ecoAwarenessDifference = game.regions[i].statistics.ecoAwareness - oldEcoAwareness[i];
            pollutionDifference = game.regions[i].statistics.avgPollution - oldPollution[i];
            prosperityDifference = game.regions[i].statistics.prosperity - oldProsperity[i];

            if (isMonthly)
            {
                if (game.monthlyReport.reportRegions[i] == "Noord Nederland")
                {
                    setValuesChanged(txtAfterActionNoord, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Oost Nederland")
                {
                    setValuesChanged(txtAfterActionOost, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Zuid Nederland")
                {
                    setValuesChanged(txtAfterActionZuid, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "West Nederland")
                {
                    setValuesChanged(txtAfterActionWest, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
            }
            else
            {
                if (game.monthlyReport.reportRegions[i] == "Noord Nederland")
                {
                    setValuesChanged(txtAfterActionNoordYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Oost Nederland")
                {
                    setValuesChanged(txtAfterActionOostYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Zuid Nederland")
                {
                    setValuesChanged(txtAfterActionZuidYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "West Nederland")
                {
                    setValuesChanged(txtAfterActionWestYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
            }
        }

        initAfterActionStatsCompletedEvents();
        initAfterActionStatsCompletedActions();
    }
    

    private void setValuesChanged(Text txt, double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference)
    {
        txt.text = "";

        if (incomeDifference != 0d)
        {
            string[] difference = { "\nInkomen: ", "\nIncome: " };

            if (incomeDifference > 0d)
                difference[taal] += "+" + incomeDifference.ToString("0.00");
            else
                difference[taal] += incomeDifference.ToString("0.00");

            txt.text += difference[taal];
        }
        if (happinessDifference != 0d)
        {
            string[] difference = { "\nTevredenheid: ", "\nHappiness: " };

            if (happinessDifference > 0d)
                difference[taal] += "+" + happinessDifference.ToString("0.00") + "%";
            else
                difference[taal] += happinessDifference.ToString("0.00") + "%";

            txt.text += difference[taal];
        }
        if (ecoAwarenessDifference != 0d)
        {
            string[] difference = { "\nMilieubewustheid: ", "\nEco awareness: " };

            if (ecoAwarenessDifference > 0d)
                difference[taal] += "+" + ecoAwarenessDifference.ToString("0.00") + "%";
            else
                difference[taal] += ecoAwarenessDifference.ToString("0.00") + "%";

            txt.text += difference[taal];
        }
        if (pollutionDifference != 0d)
        {
            string[] difference = { "\nVervuiling: ", "\nPollution: " };

            if (pollutionDifference > 0d)
                difference[taal] += "+" + pollutionDifference.ToString("0.00") + "%";
            else
                difference[taal] += pollutionDifference.ToString("0.00") + "%";

            txt.text += difference[taal];
        }
        if (prosperityDifference != 0d)
        {
            string[] difference = { "\nWelvaart: ", "\nProsperity: " };

            if (prosperityDifference > 0d)
            {
                difference[taal] += "+" + prosperityDifference.ToString("0.00") + "%";
            }
            else
                difference[taal] += prosperityDifference.ToString("0.00") + "%";

            txt.text += difference[taal];
        }

    }

    public void initAfterActionStatsCompletedEvents()
    {
        monthlyCompletedEvents = (List<GameEvent>[])game.monthlyReport.completedEvents.Clone();

        if (monthlyCompletedEvents[0].Count != 0)
            setCompletedEvents(txtAfterActionNoord, monthlyCompletedEvents[0]);
        if (monthlyCompletedEvents[1].Count != 0)
            setCompletedEvents(txtAfterActionOost, monthlyCompletedEvents[1]);
        if (monthlyCompletedEvents[2].Count != 0)
            setCompletedEvents(txtAfterActionWest, monthlyCompletedEvents[2]);
        if (monthlyCompletedEvents[3].Count != 0)
            setCompletedEvents(txtAfterActionZuid, monthlyCompletedEvents[3]);
    }

    private void setCompletedEvents(Text txt, List<GameEvent> eventsList)
    {
        string[] events = { "\n\n<b>Afgeronde events:</b>\n", "\n\n<b>Completed events:</b>\n" };
        txt.text += events[taal];

        foreach (GameEvent e in eventsList)
        {
            txt.text += e.publicEventName[taal];// + " - " + e.description[taal];

            if (taal == 0)
                txt.text += "\n<b>Gekozen oplossing: </b>" + e.choicesDutch[e.pickedChoiceNumber];
            else
                txt.text += "\n<b>Chosen solution: </b>" + e.choicesEnglish[e.pickedChoiceNumber];

            string[] c = { "\n<b>Consequenties: </b>", "\n<b>Consequences: </b>" };
            txt.text += c[taal] + getSectorStatisticsConsequences(e.afterInvestmentConsequences[e.pickedChoiceNumber]);

            string[] sectorsPicked = { "\n<b>Sectoren: </b>\n", "\n<b>Sectors: </b>\n" };
            txt.text += sectorsPicked[taal];
            foreach (string s in e.possibleSectors)
            {
                foreach (RegionSector sector in game.regions[0].sectors)
                {
                    if (sector.sectorName[0] == s)
                    {
                        txt.text += sector.sectorName[taal] + " ";
                        break;
                    }
                }
            }
            txt.text += "\n\n";
        }
    }

    private void initAfterActionStatsCompletedActions()
    {
        monthlyCompletedActions = (List<RegionAction>[])game.monthlyReport.completedActions.Clone();

        if (monthlyCompletedActions[0].Count != 0)
            setCompletedActions(txtAfterActionNoord, monthlyCompletedActions[0]);
        if (monthlyCompletedActions[1].Count != 0)
            setCompletedActions(txtAfterActionOost, monthlyCompletedActions[1]);
        if (monthlyCompletedActions[2].Count != 0)
            setCompletedActions(txtAfterActionWest, monthlyCompletedActions[2]);
        if (monthlyCompletedActions[3].Count != 0)
            setCompletedActions(txtAfterActionZuid, monthlyCompletedActions[3]);
    }

    private void setCompletedActions(Text txt, List<RegionAction> actionsList)
    {
        string[] acties = { "<b>Afgeronde acties:</b>\n", "\n\n<b>Completed actions:</b>\n" };
        txt.text += acties[taal];

        for (int i = 0; i < monthlyCompletedActions.Length; i++)
        {
            foreach (RegionAction a in monthlyCompletedActions[i])
            {
                txt.text += a.name[taal] + " - " + a.description[taal];
                txt.text += getChosenSectors(a.pickedSectors);
                string[] c = { "\n<b>Consequenties: </b>", "\n<b>Consequences: </b>" };
                txt.text += c[taal] + getSectorStatisticsConsequences(a.afterInvestmentConsequences);

                if (a.actionMoneyReward != 0)
                {
                    string[] line = { "\n<b>Geld beloning: </b>", "\n<b>Money reward: </b>" };
                    txt.text += line[taal] + a.actionMoneyReward + "\n\n";
                }
            }
        }
    }

    private string getChosenSectors(bool[] sectors)
    {
        string[] sectorsPicked = { "\n<b>Sectoren: </b>\n", "\n<b>Sectors: </b>\n" };

        if (sectors[0])
        {
            string[] a = { "Huishoudens ", "Households " };
            sectorsPicked[taal] += a[taal];
        }
        if (sectors[1])
        {
            string[] a = { "Landbouw ", "Agriculture " };
            sectorsPicked[taal] += a[taal];
        }
        if (sectors[2])
        {
            string[] a = { "Bedrijven ", "Companies " };
            sectorsPicked[taal] += a[taal];
        }

        return sectorsPicked[taal];

    }
    #endregion

    #region Code for Quests Popup
    private void initQuestsPopup()
    {
        imgTutorialQuests.gameObject.SetActive(false);
        string[] title = { "Missies", "Quests" };
        string[] description = { "Actieve missies", "Active quests" };
        string[] activeQuests = { "", "" };
        string[] noActiveQuests = { "Er zijn geen actieve missies", "There are no active quests" };
        string[] beloning = { "Beloning: ", "Reward: " };

        bool activeQuest = false;

        txtQuestsTitle.text = title[taal];
        txtQuestsDescription.text = description[taal];

        if (game.tutorial.tutorialQuestsActive && game.tutorial.doTuto)
            StartCoroutine(tutorialQuestsPopup());

        foreach (Quest q in game.quests)
        {
            if (q.isActive)
            {
                activeQuests[taal] += q.name[taal] + " - " + q.description[taal] + "\n";
                activeQuests[taal] += getCompleteConditions(q.questCompleteConditions);
                activeQuests[taal] += beloning[taal] + q.questMoneyReward + "\n\n";
                txtQuestsActive.text = activeQuests[taal];
                activeQuest = true;
            }
        }
        if (!activeQuest)
            txtQuestsActive.text = noActiveQuests[taal];       
    }

    IEnumerator tutorialQuestsPopup()
    {
        Debug.Log("Coroutine Tutorial Quests!");
        imgTutorialQuests.gameObject.SetActive(true);

        string[] step2 = { "In deze pop-up kun je zien welke actieve missies je hebt. Je krijgt om de 2 jaar een nieuwe missie. \n\nAls je aan de juiste condities voldoet haal je de missie en krijg je een beloning.",
            "In this popup you can see your active quests. You get a new quest each time 2 years pass. \n\nIf you reach the quest conditions you get a reward." };
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialQuests.text = step2[taal];
        txtTutorialQuestsBtn.text = txtBtn[taal];

        while (!game.tutorial.tutorialChecks[9])//tutorialStep16)
            yield return null;


        imgTutorialQuests.gameObject.SetActive(false);
        game.tutorial.tutorialQuestsActive = false;

        while (canvasQuestsPopup.gameObject.activeSelf)
            yield return null;

        btnTutorialNext.gameObject.SetActive(true);
        canvasTutorial.gameObject.SetActive(true);
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;
        imgTutorialStepOrgMenuHightlight.enabled = false;

        while (!game.tutorial.tutorialChecks[10])//tutorialStep17)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialQuestsDone = true;
        btnNextTurn.interactable = true;
    }
    
    private string getCompleteConditions(RegionStatistics r)
    {
        string[] consequences = { "Vereisten: ", "Requirements: " };
        if (r.income != 0)
        {
            string[] a = { "\nInkomen: " + r.income + "\n", "\nIncome: " + r.income + "\n" };
            consequences[taal] += a[taal];
        }
        if (r.happiness != 0)
        {
            string[] c = { "Tevredenheid: " + r.happiness + "\n", "Happiness: " + r.happiness + "\n" };
            consequences[taal] += c[taal];
        }
        if (r.ecoAwareness != 0)
        {
            string[] d = { "Milieubewustheid: " + r.ecoAwareness + "\n", "Eco awareness: " + r.ecoAwareness + "\n" };
            consequences[taal] += d[taal];
        }
        if (r.prosperity != 0)
        {
            string[] e = { "Welvaart: " + r.prosperity + "\n", "Prosperity: " + r.prosperity + "\n" };
            consequences[taal] += e[taal];
        }
        if (r.avgAirPollution != 0)
        {
            string[] f = { "Luchtvervuiling: " + r.avgAirPollution + "\n", "Air pollution: " + r.avgAirPollution + "\n" };
            consequences[taal] += f[taal];
        }
        if (r.avgAirPollutionIncrease != 0)
        {
            string[] g = { "Watervervuiling: " + r.avgAirPollutionIncrease + "\n", "Water pollution: " + r.avgAirPollutionIncrease + "\n" };
            consequences[taal] += g[taal];
        }
        if (r.avgNaturePollution != 0)
        {
            string[] h = { "Natuurvervuiling: " + r.avgNaturePollution + "\n", "Nature pollution: " + r.avgNaturePollution + "\n" };
            consequences[taal] += h[taal];
        }
        if (r.avgNaturePollutionIncrease != 0)
        {
            string[] h = { "Natuurvervuiling: " + r.avgNaturePollutionIncrease + "\n", "Nature pollution: " + r.avgNaturePollutionIncrease + "\n" };
            consequences[taal] += h[taal];
        }
        if (r.avgPollution != 0)
        {
            string[] h = { "Natuurvervuiling: " + r.avgPollution + "\n", "Nature pollution: " + r.avgPollution + "\n" };
            consequences[taal] += h[taal];
        }
        if (r.avgWaterPollution != 0)
        {
            string[] h = { "Natuurvervuiling: " + r.avgWaterPollution + "\n", "Nature pollution: " + r.avgWaterPollution + "\n" };
            consequences[taal] += h[taal];
        }
        if (r.avgWaterPollutionIncrease != 0)
        {
            string[] h = { "Natuurvervuiling: " + r.avgWaterPollutionIncrease + "\n", "Nature pollution: " + r.avgWaterPollutionIncrease + "\n" };
            consequences[taal] += h[taal];
        }

        return consequences[taal];
    }
    #endregion

    #region Code for Event Popup
    public void initEventPopup(GameEvent e, Region r)
    {
        gameEvent = e;
        regionEvent = r;
        canvasEventPopup.gameObject.SetActive(true);
        popupActive = true;
        EventManager.CallPopupIsActive();

        initEventUI();
        initEventText(e);

        if (game.tutorial.tutorialActive && game.tutorial.tutorialEventsActive)//tutorialstep12)
        {
            imgTutorialEvents.gameObject.SetActive(true);
            StartCoroutine(eventTutorial());
        }
    }

    IEnumerator eventTutorial()
    {
        //tutorialEventsActive = true;
        string[] txtTutorial = { "Bij elk event heb je altijd 3 keuzes. Van elke keuze kun je de kosten en de duur zien. Elke keuze brengt weer andere consequenties met zich mee voor de verschillende statistieken. "
                + "Het is dus cruciaal dat je goed nadenkt over je beslissingen. \n\nLos nu dit event op door een oplossing te kiezen."
                , "You always have 3 choices for each event. You can see the cost and duration from each choice. Each choice brings other consequences for the different statistics. " +
                "This means it's crucial to think about what you want to achieve before making a choice.\n\nSolve this event by choosing  an option." };
        string[] txtBtn = { "Volgende", "Next" };
        txtTutorialEvent.text = txtTutorial[taal];
        txtTutorialEventBtn.text = txtBtn[taal];

        while (!game.tutorial.tutorialChecks[7])// tutorialStep20)
            yield return null;

        imgTutorialEvents.gameObject.SetActive(false);
    }

    private void initEventUI()
    {
        playSelectSound = false;
        if (radioEventOption1Check)
            radioEventOption1.isOn = false;

        if (radioEventOption2Check)
            radioEventOption2.isOn = false;

        if (radioEventOption3Check)
            radioEventOption3.isOn = false;

        playSelectSound = true;

        btnDoEvent.interactable = false;
    }

    private void initEventText(GameEvent e)
    {
        string[] txtBtn = { "Doe keuze", "Do choice" };
        string[] txtKosten = { "\nKosten: ", "\nCost: " };
        string[] txtMoney = { " geld", "money" };
        string[] txtDuur = { "\nDuur: ", "\nDuration: " };
        string[] txtMonths = { " maanden", " months" };
        string[] txtMonth = { " maand", " month" };

        txtEventName.text = "EVENT:" + e.publicEventName[taal];
        txtEventDescription.text = e.description[taal];
        txtBtnDoEvent.text = txtBtn[taal];

        if (ApplicationModel.language == 0)
        {
            radioEventOption1Text.text = e.choicesDutch[0];
            radioEventOption2Text.text = e.choicesDutch[1];
            radioEventOption3Text.text = e.choicesDutch[2];
        }
        else
        {
            radioEventOption1Text.text = e.choicesEnglish[0];
            radioEventOption2Text.text = e.choicesEnglish[1];
            radioEventOption3Text.text = e.choicesEnglish[2];
        }
        if (e.eventDuration[0] != 1)
            radioEventOption1Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[0] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[0] + txtMonths[taal];
        else
            radioEventOption1Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[0] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[0] + txtMonth[taal];
        if (e.eventDuration[1] != 1)
            radioEventOption2Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[1] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[1] + txtMonths[taal];
        else
            radioEventOption2Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[1] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[1] + txtMonth[taal];
        if (e.eventDuration[2] != 1)
            radioEventOption3Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[2] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[2] + txtMonths[taal];
        else
            radioEventOption3Text.text += txtKosten[taal] + e.afterInvestmentEventChoiceMoneyCost[2] + txtMoney[taal] + txtDuur[taal] + e.eventDuration[2] + txtMonth[taal];
    }

    public void valueChangedOption1()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!radioEventOption1Check)
        {
            radioEventOption1Check = true;
            radioEventOption2.isOn = false;
            radioEventOption3.isOn = false;
            if (game.gameStatistics.money >= gameEvent.afterInvestmentEventChoiceMoneyCost[0])
                btnDoEvent.interactable = true;
            else
                btnDoEvent.interactable = false;
        }
        else
            radioEventOption1Check = false;

        checkIfAllFalse();
    }

    public void valueChangedOption2()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!radioEventOption2Check)
        {
            radioEventOption2Check = true;
            radioEventOption1.isOn = false;
            radioEventOption3.isOn = false;
            if (game.gameStatistics.money >= gameEvent.afterInvestmentEventChoiceMoneyCost[1])
                btnDoEvent.interactable = true;
            else
                btnDoEvent.interactable = false;
        }
        else
            radioEventOption2Check = false;

        checkIfAllFalse();
    }

    public void valueChangedOption3()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!radioEventOption3Check)
        {
            radioEventOption3Check = true;
            radioEventOption1.isOn = false;
            radioEventOption2.isOn = false;
            if (game.gameStatistics.money >= gameEvent.afterInvestmentEventChoiceMoneyCost[2])
                btnDoEvent.interactable = true;
            else
                btnDoEvent.interactable = false;
        }
        else
            radioEventOption3Check = false;

        checkIfAllFalse();
    }

    private void checkIfAllFalse()
    {
        if (!radioEventOption1Check && !radioEventOption2Check && !radioEventOption3Check)
            btnDoEvent.interactable = false;
    }

    public void btnDoEventClick()
    {
        int option;

        if (radioEventOption1Check)
            option = 0;
        else if (radioEventOption2Check)
            option = 1;
        else 
            option = 2;

        gameEvent.SetPickedChoice(option, game, regionEvent);
        canvasEventPopup.gameObject.SetActive(false);
        popupActive = false;
        EventManager.CallPopupIsDisabled();

        if (!game.tutorial.tutorialEventsDone)
            game.tutorial.tutorialEventsDone = true;

        if (game.tutorial.tutorialEventsActive)
            game.tutorial.tutorialEventsActive = false;
    }
    #endregion

    #region Code for Empty Building Popup
    public void initEmptyBuildingPopup(Region r)
    {
        regionToBeBuild = r;
        popupActive = true;
        EventManager.CallPopupIsActive();
        canvasEmptyBuildingsPopup.gameObject.SetActive(true);

        if (game.tutorial.tutorialBuildingsActive && game.tutorial.doTuto)
            StartCoroutine(tutorialBuildingsPopup());

        initEmptyBuildingText();
    }

    private IEnumerator tutorialBuildingsPopup()
    {
        imgTutorialBuildings.gameObject.SetActive(true);

        string[] step = { "INSERT BUILDINGS TUTORIAL TEXT. ",
            "INSERT BUILDINGS TUTORIAL TEXT. "};
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialBuildings.text = step[taal];
        txtTutorialBuildingsbtn.text = txtBtn[taal];

        while (!game.tutorial.tutorialChecks[14])//tutorialStep16)
            yield return null;

        imgTutorialBuildings.gameObject.SetActive(false);
        game.tutorial.tutorialBuildingsActive = false;

        while (canvasEmptyBuildingsPopup.gameObject.activeSelf)
            yield return null;
 
        btnTutorialNext.gameObject.SetActive(true);
        canvasTutorial.gameObject.SetActive(true);

        string[] step3 = {"Je bent nu klaar om het hele spel te spelen. \n\nDenk eraan dat de vervuiling onder de 5% moet zijn voor 2050.",
            "You're now ready to play the game. \n\nThink about the fact that the pollution needs to be below 5% before 2050." };
        string[] txtButton = { "Eindig handleiding", "Finish tutorial" };

        txtTurorialStep1.text = step3[taal];
        txtTutorialStep1BtnText.text = txtButton[taal];

        while (!game.tutorial.tutorialChecks[15])//tutorialStep17)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialBuildingsDone = true;
        btnNextTurn.interactable = true;
    }

    private void initEmptyBuildingText()
    {
        string[] title = { "Gebouwen", "Buildings" };
        string[] column = { "Plaats een gebouw", "Place a building" };
        string[] info = { "Kies hieronder het gebouw dat je wilt maken", "Chose the building you want to make" };

        txtEmptyBuildingsTitle.text = title[taal]; ;
        txtEmptyBuildingsColumnLeft.text = column[taal];
        txtEmptyBuildingColumRight.text = "";
        txtEmptyBuildingStats.text = "";
        txtEmptyBuildingInfo.text = info[taal];
        btnUseBuilding.gameObject.SetActive(false);
    }

    private void setTextBuildingInformation(Building b)
    {
        buildingToBeBuild = b;

        txtEmptyBuildingColumRight.text = "";
        txtEmptyBuildingStats.text = "";
        string[] column = { "Informatie - " + b.buildingName[0], "Information - " + b.buildingName[1] };
        txtEmptyBuildingColumRight.text = column[taal];

        btnUseBuilding.gameObject.SetActive(true);
        string[] btnTxt = { "Maak gebouw", "Build building" };
        btnUseBuildingTxt.text = btnTxt[taal];

        string[] cost = { "Kosten:" + b.buildingMoneyCost + "\n", "Cost:" + b.buildingMoneyCost + "\n" };
        txtEmptyBuildingStats.text += cost[taal];

        txtEmptyBuildingStats.text += getBuildingModifiers(b);

        if (game.gameStatistics.money >= buildingToBeBuild.buildingMoneyCost)
            btnUseBuilding.interactable = true;
        else
            btnUseBuilding.interactable = false;  
    }

    // Afhandelen van BtnUseBuilding Click wordt in GameController gedaan

    #endregion

    #region Code for Active Building Popup
    public void initBuildingPopup(Building b, Region r)
    {
        activeBuilding = b;
        buildingRegion = r;
        popupActive = true;
        EventManager.CallPopupIsActive();
        canvasBuildingsPopup.gameObject.SetActive(true);

        initBuildingText();
    }

    private void initBuildingText()
    {
        string[] title = { "Gebouwen", "Buildings" };
        string[] column = { "Actief gebouw: " + activeBuilding.buildingName[0], "Active building: " + activeBuilding.buildingName[0] };
        string[] btn = { "Sloop gebouw", "Destroy building" };

        txtBuildingsStats.text = "";
        txtBuildingsTitle.text = title[taal]; 
        txtBuildingsColumn.text = column[taal];
        txtBtnDeleteBuilding.text = btn[taal];

        // string[] cost = { "Kosten:" + activeBuilding.buildingMoneyCost + "\n", "Cost:" + activeBuilding.buildingMoneyCost + "\n" };
        // txtBuildingsStats.text += cost[taal];

        string[] info = { "Effecten van " + activeBuilding.buildingName[0], "Effects caused by " + activeBuilding.buildingName[1] };
        txtBuildingsStats.text = info[taal];
        txtBuildingsStats.text += getBuildingModifiers(activeBuilding);

    }
    #endregion

    #region Code for Investments Popup
    private void initInvestementsText()
    {
        string[] title = { "Investeren", "Investments" };
        string[] column = { "Investeer in de organisatie", "Invest in the organization" };
        string[] description = { "Hier kun je geld investeren in je organisatie. Hoe meer geld je in een onderdeel investeert, hoe meer positief resultaat je zult zien.\nJe kunt 5 keer investeren in elk onderdeel. " +
                "Investeren kost 5000 per keer. Investeringen kun je niet terug draaien.",
            "You can invest money in your own organization. If you invest more in one of the segments you will see a larger positive result. You can invest 5 times in each segment. " + 
            "Investing will cost you 5000 each time. You can't rollback your investments."};
        string[] actievermindering = { "Kosten verlagen acties\nVerlaag de kosten voor elke actie.", "Cost reduction actions\nLowers the cost for all actions."};
        string[] actieconsequences = { "Betere consequencies acties\nVerbeter de consequenties bij elke actie.", "Better consequences actions\nImprove the consequencies for all actions." };
        string[] eventvermindering = { "Kosten verlagen events\nVerlaag de kosten voor elke event optie.", "Cost reduction events\nLowers the cost for all events options." };
        string[] eventconsequencies = { "Betere consequencies events\nVerbeter de consequenties bij elke event optie.", "Better consequences events\nImprove the consequencies for all event options." };

        txtInvestmentsTitle.text = title[taal];
        txtInvestmentsColumn.text = column[taal];
        txtInvestmentsDescription.text = description[taal];
        txtInvestmentsActionCost.text = actievermindering[taal];
        txtInvestmentsActionConsequences.text = actieconsequences[taal];
        txtInvestmentsEventCost.text = eventvermindering[taal];
        txtInvestmentsEventConsequences.text = eventconsequencies[taal];

        if (game.tutorial.tutorialInvestementsActive && game.tutorial.doTuto)
            StartCoroutine(tutorialInvestementsPopup());
    }

    private IEnumerator tutorialInvestementsPopup()
    {
        imgTutorialInvestements.gameObject.SetActive(true);

        string[] step = { "Je kunt investeren om de kosten en consequenties van acties en events te verlagen en te verbeteren. Je kunt 5x investeren in elk onderdeel, dus 20x in totaal.\n\n " +
                "Investeren kost 10.000 geld per keer. \n\nAls je geinvesteerd hebt kun je dit niet meer ongedaan maken.",
            "You can invest money to decrease the cost and better the consequences off actions and events. You can invest 5 times in each of these, that makes a total off 20 investements." +
            "A investment costs 10.000 money each time. When you invested your money there is no way to turn this back."};
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialInvestements.text = step[taal];
        txtTutorialInvestementsbtn.text = txtBtn[taal];

        while (!game.tutorial.tutorialChecks[13])//tutorialStep16)
            yield return null;

        imgTutorialInvestements.gameObject.SetActive(false);
        game.tutorial.tutorialInvestementsActive = false;

        while (canvasInvestmentsPopup.gameObject.activeSelf)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialInvestementsDone = true;
        btnNextTurn.interactable = true;
    }

    private void initInvestmentsImages()
    {
        setActionCostReductionInvestments();
        setActionConsequencesInvestments();
        setEventCostReductionInvestments();
        setEventConsequencesInvestments();
    }

    private void setActionCostReductionInvestments()
    {
        if (game.investments.actionCostReduction[0])
            imgInvestmentActionCost01.gameObject.SetActive(true);
        if (game.investments.actionCostReduction[1])
            imgInvestmentActionCost02.gameObject.SetActive(true);
        if (game.investments.actionCostReduction[2])
            imgInvestmentActionCost03.gameObject.SetActive(true);
        if (game.investments.actionCostReduction[3])
            imgInvestmentActionCost04.gameObject.SetActive(true);
        if (game.investments.actionCostReduction[4])
            imgInvestmentActionCost05.gameObject.SetActive(true);
    }

    private void setActionConsequencesInvestments()
    {
        if (game.investments.betterActionConsequences[0])
            imgInvestmentActionConsequences01.gameObject.SetActive(true);
        if (game.investments.betterActionConsequences[1])
            imgInvestmentActionConsequences02.gameObject.SetActive(true);
        if (game.investments.betterActionConsequences[2])
            imgInvestmentActionConsequences03.gameObject.SetActive(true);
        if (game.investments.betterActionConsequences[3])
            imgInvestmentActionConsequences04.gameObject.SetActive(true);
        if (game.investments.betterActionConsequences[4])
            imgInvestmentActionConsequences05.gameObject.SetActive(true);
    }

    private void setEventCostReductionInvestments()
    {
        if (game.investments.gameEventCostReduction[0])
            imgInvestmentEventCost01.gameObject.SetActive(true);
        if (game.investments.gameEventCostReduction[1])
            imgInvestmentEventCost02.gameObject.SetActive(true);
        if (game.investments.gameEventCostReduction[2])
            imgInvestmentEventCost03.gameObject.SetActive(true);
        if (game.investments.gameEventCostReduction[3])
            imgInvestmentEventCost04.gameObject.SetActive(true);
        if (game.investments.gameEventCostReduction[4])
            imgInvestmentEventCost05.gameObject.SetActive(true);
    }

    private void setEventConsequencesInvestments()
    {
        if (game.investments.betterGameEventConsequences[0])
            imgInvestmentEventConsequences01.gameObject.SetActive(true);
        if (game.investments.betterGameEventConsequences[1])
            imgInvestmentEventConsequences02.gameObject.SetActive(true);
        if (game.investments.betterGameEventConsequences[2])
            imgInvestmentEventConsequences03.gameObject.SetActive(true);
        if (game.investments.betterGameEventConsequences[3])
            imgInvestmentEventConsequences04.gameObject.SetActive(true);
        if (game.investments.betterGameEventConsequences[4])
            imgInvestmentEventConsequences05.gameObject.SetActive(true);
    }

    public void btnInvestActionCost()
    {
        if (game.gameStatistics.money >= game.investments.investmentCost)
        {
            game.investments.InvestInActionCostReduction(game.regions);
            setActionCostReductionInvestments();

            if (game.investments.actionCostReduction[4])
                btnInvestmentActionCostInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestActionConsequences()
    {
        if (game.gameStatistics.money >= game.investments.investmentCost)
        {
            game.investments.InvestInBetterActionConsequences(game.regions);
            setActionConsequencesInvestments();

            if (game.investments.betterActionConsequences[4])
                btnInvestmentActionConsequenceInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestEventCost()
    {
        if (game.gameStatistics.money >= game.investments.investmentCost)
        {
            game.investments.InvestInGameEventCostReduction(game.events);
            setEventCostReductionInvestments();

            if (game.investments.gameEventCostReduction[4])
                btnInvestmentEventCostInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestEventConsequences()
    {
        if (game.gameStatistics.money >= game.investments.investmentCost)
        {
            game.investments.InvestInBetterGameEventConsequences(game.events);
            setEventConsequencesInvestments();

            if (game.investments.betterGameEventConsequences[4])
                btnInvestmentEventConsequenceInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }

        updateInvestButtonsInteractable(); 
    }

    private void updateInvestButtonsInteractable()
    {
        if (game.gameStatistics.money < game.investments.investmentCost)
        {
            btnInvestmentActionCostInvest.interactable = false;
            btnInvestmentActionConsequenceInvest.interactable = false;
            btnInvestmentEventCostInvest.interactable = false;
            btnInvestmentEventConsequenceInvest.interactable = false;
        }
        else
        {
            btnInvestmentActionCostInvest.interactable = true;
            btnInvestmentActionConsequenceInvest.interactable = true;
            btnInvestmentEventCostInvest.interactable = true;
            btnInvestmentEventConsequenceInvest.interactable = true;
        }
    }
    #endregion

    #region Code for Cards Popup
    private void initCardsText()
    {
        string[] title = { "Kaarten", "Cards" };
        string[] column = { "Niet geactiveerde kaarten", "Not activated cards" };

        txtCardsTitle.text = title[taal];
        txtCardsColumn.text = column[taal];
        txtCardsColumnRight.text = "";
        txtCardsOptionInformation.text = "";
    }

    private void updateCardsUI()
    {
        txtCardsColumnRight.text = "";
        txtCardsOptionInformation.text = "";
        btnUseCard.gameObject.SetActive(false);
        toggleNoordNL.gameObject.SetActive(false);
        toggleOostNL.gameObject.SetActive(false);
        toggleZuidNL.gameObject.SetActive(false);
        toggleWestNL.gameObject.SetActive(false);

        if (game.tutorial.tutorialCardsActive && game.tutorial.doTuto)
            StartCoroutine(tutorialCardsPopup());
    }

    private IEnumerator tutorialCardsPopup()
    {
        imgTutorialCards.gameObject.SetActive(true);

        string[] step = { "Je hebt elke beurt een 2% kans om een kaart te krijgen. Je kunt kaarten bewaren om de gevolgen van de kaart groter te maken. \n\nKaarten werken op " +
                "nationaal niveau of regionaal niveau. Je kunt een kaart maar 1x inzetten.",
            "Each turn you have a 2% chance to get a card. You can increase the effects off your card by not playing it immediately.\n\nCard can be used on regional or national level. You can only play a card once." };
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialCards.text = step[taal];
        txtTutorialCardsBtn.text = txtBtn[taal];

        while (!game.tutorial.tutorialChecks[11])//tutorialStep16)
            yield return null;

        imgTutorialCards.gameObject.SetActive(false);
        game.tutorial.tutorialCardsActive = false;

        if (!game.tutorial.tutorialCardsDone)
        {
            while (canvasCardsPopup.gameObject.activeSelf)
                yield return null;
        }

        /*
        btnTutorialNext.gameObject.SetActive(true);
        canvasTutorial.gameObject.SetActive(true);

        string[] step3 = {"Je bent nu klaar om het hele spel te spelen. \n\nDenk eraan dat de vervuiling onder de 5% moet zijn voor 2050.",
            "You're now ready to play the game. \n\nThink about the fact that the pollution needs to be below 5% before 2050." };
        string[] txtButton = { "Eindig handleiding", "Finish tutorial" };

        txtTurorialStep1.text = step3[taal];
        txtTutorialStep1BtnText.text = txtButton[taal];

        while (!game.tutorial.tutorialChecks[12])//tutorialStep17)
            yield return null;
        */

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        btnNextTurn.interactable = true;
    }

    private void setTextCardInformation(Card c)
    {
        txtCardsOptionInformation.text = "";
        card = c;
        btnUseCard.gameObject.SetActive(true);
        string[] txtBtn = { "Gebuik kaart", "Use card" };
        txtBtnUseCard.text = txtBtn[taal];

        string[] columnRight = { card.name[taal] + " - Informatie", card.name[taal] + " - Information" };
        string[] national = { "Kaart wordt gebruikt op nationaal niveau", "Card will be used on national level" };
        string[] regional = { "Kaart wordt gebruikt op regionaal niveau", "Card will be used on regional level" };
        txtCardsColumnRight.text = columnRight[taal];
        txtCardsOptionInformation.text = card.description[taal] + "\n";

        if (card.isGlobal)
            txtCardsOptionInformation.text += national[taal];
        else
            txtCardsOptionInformation.text = regional[taal];

        string[] increment = { "\n\nAantal toenames: " + card.currentIncrementsDone + " - Maximaal aantal toenames: " + card.maximumIncrementsDone,
            "\n\nCurrent increment: " + card.currentIncrementsDone + " - Maximum number of increments: " + card.maximumIncrementsDone };
        txtCardsOptionInformation.text += increment[taal];
        string[] txtIncrement = { "\nToename consequencies per jaar:", "\nIncrement consequences per year:" };
        txtCardsOptionInformation.text += txtIncrement[taal];
        txtCardsOptionInformation.text += getSectorStatisticsConsequences(card.sectorConsequencesPerTurn);

        if (card.moneyRewardPerTurn != 0)
        {
            string[] moneyReward = { "\nGeld beloning: " + card.moneyRewardPerTurn, "\nMoney reward: " + card.moneyRewardPerTurn };
            txtCardsOptionInformation.text += moneyReward[taal];
        }

        string[] txtCurrentConsequences = { "\n\nHuidige consequencies:", "\n\nCurrent consequences:" };
        txtCardsOptionInformation.text += txtCurrentConsequences[taal] + getSectorStatisticsConsequences(card.currentSectorConsequences);

        if (card.currentMoneyReward != 0)
        {
            string[] moneyReward = { "\nHuidige geld beloning: " + card.currentMoneyReward, "\nCurrent money reward: " + card.currentMoneyReward };
            txtCardsOptionInformation.text += moneyReward[taal];
        }

        if (!card.isGlobal)
        {
            initCardRadioButtons();
        }
    }

    private void initCardRadioButtons()
    {
        if (toggleNoordNLCheck)
            toggleNoordNL.isOn = false;

        if (toggleOostNLCheck)
            toggleOostNL.isOn = false;

        if (toggleZuidNLCheck)
            toggleZuidNL.isOn = false;

        if (toggleWestNLCheck)
            toggleWestNL.isOn = false;

        string[] noord = { "Noord-Nederland", "The Netherland North" };
        string[] oost = { "Oost-Nederland", "The Netherland East" };
        string[] zuid = { "Zuid-Nederland", "The Netherland South" };
        string[] west = { "West-Nederland", "The Netherland West" };

        txtToggleNoord.text = noord[taal];
        txtToggleOost.text = oost[taal];
        txtToggleZuid.text = zuid[taal];
        txtToggleWest.text = west[taal];

        toggleNoordNL.gameObject.SetActive(true);
        toggleOostNL.gameObject.SetActive(true);
        toggleZuidNL.gameObject.SetActive(true);
        toggleWestNL.gameObject.SetActive(true);

        btnUseCard.interactable = false;
    }

    public void valueChangedNoordNL()
    {
        if (!toggleNoordNLCheck)
        {
            toggleNoordNLCheck = true;
            toggleOostNL.isOn = false;
            toggleZuidNL.isOn = false;
            toggleWestNL.isOn = false;
            btnUseCard.interactable = true;
        }
        else
            toggleNoordNLCheck = false;

        // Method in Code for Event Region
        checkIfAllFalseCards();
    }

    public void valueChangedOostNL()
    {
        if (!toggleOostNLCheck)
        {
            toggleOostNLCheck = true;
            toggleNoordNL.isOn = false;
            toggleZuidNL.isOn = false;
            toggleWestNL.isOn = false;
            btnUseCard.interactable = true;
        }
        else
            toggleOostNLCheck = false;

        // Method in Code for Event Region
        checkIfAllFalseCards();
    }

    public void valueChangedZuidNL()
    {
        if (!toggleZuidNLCheck)
        {
            toggleZuidNLCheck = true;
            toggleNoordNL.isOn = false;
            toggleOostNL.isOn = false;
            toggleWestNL.isOn = false;
            btnUseCard.interactable = true;
        }
        else
            toggleZuidNLCheck = false;

        // Method in Code for Event Region
        checkIfAllFalseCards();
    }

    public void valueChangedWestNL()
    {
        if (!toggleWestNLCheck)
        {
            toggleWestNLCheck = true;
            toggleNoordNL.isOn = false;
            toggleOostNL.isOn = false;
            toggleZuidNL.isOn = false;
            btnUseCard.interactable = true;
        }
        else
            toggleWestNLCheck = false;

        // Method in Code for Event Region
        checkIfAllFalseCards();
    }

    private void checkIfAllFalseCards()
    {
        if (!toggleNoordNLCheck && !toggleOostNLCheck && !toggleWestNLCheck && !toggleZuidNLCheck)
            btnUseCard.interactable = false;
    }

    public void btnUseCardClick()
    {
        if (card.isGlobal)
            card.UseCardOnCountry(game.regions, game.gameStatistics);
        else
        {
            Region cardRegion;

            if (toggleNoordNLCheck)
                cardRegion = game.regions[0];
            else if (toggleOostNLCheck)
                cardRegion = game.regions[1];
            else if (toggleWestNLCheck)
                cardRegion = game.regions[2];
            else
                cardRegion = game.regions[3];

            card.UseCardOnRegion(cardRegion, game.gameStatistics);
        }

        updateCardsUI();
    }
    #endregion

    // Game Controller
    #region Code for Button Presses for Popups
    public void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive && !game.tutorial.tutorialActive && !game.tutorial.tutorialQuestsActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive/* && tutorialStep8 */&& !game.tutorial.tutorialQuestsActive)
        {
            btnOrganizationIsClicked = true;
            EventManager.CallPlayButtonClickSFX();
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateOrganizationScreenUI();
        }
    }

    public void btnQuestsClick()
    {
        if (!canvasQuestsPopup.gameObject.activeSelf && !popupActive)//tutorialStep15)
        {
            btnQuestsIsClicked = true;
            EventManager.CallPlayButtonClickSFX();
            canvasQuestsPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            initQuestsPopup();
        }
    }

    public void btnCardsClick()
    {
        if (!canvasCardsPopup.gameObject.activeSelf && !popupActive)
        {
            btnCardsIsClicked = true;
            EventManager.CallPlayButtonClickSFX();
            canvasCardsPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateCardsUI();
        }
    }

    public void btnMenuClick()
    {
        if (!canvasMenuPopup.gameObject.activeSelf && !popupActive)
        {
            EventManager.CallPlayButtonClickSFX();
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
    }

    public void btnMonthlyReportClick()
    {
        if (!canvasMonthlyReport.gameObject.activeSelf && !popupActive)
        {
            EventManager.CallPlayButtonClickSFX();
            canvasMonthlyReport.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateTextAfterActionStats(true);

            if (game.tutorial.tutorialActive && game.tutorial.tutorialMonthlyReportActive)
            {
                imgTutorialAfterTurn.gameObject.SetActive(true);
                StartCoroutine(tutorialMonthlyReport());
            }
        }
    }

    public void btnYearlyReportClick()
    {
        if (!canvasYearlyReport.gameObject.activeSelf && !popupActive)
        {
            EventManager.CallPlayButtonClickSFX();
            canvasYearlyReport.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateTextAfterActionStats(false);
        }
    }

    public void btnInvestmentsClick()
    {
        if (!canvasInvestmentsPopup.gameObject.activeSelf && !popupActive)
        {
            btnInvestmentsIsClicked = true;
            EventManager.CallPlayButtonClickSFX();
            canvasInvestmentsPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            initInvestementsText();
            updateInvestButtonsInteractable();
        }
    }

    private void initButtonText()
    {
        string[] resume = { "Verder spelen", "Resume" };
        string[] save = { "Opslaan", "Save" };
        string[] exitgame = { "Verlaat spel", "Exit Game" };
        string[] exitmenu = { "Naar hoofdmenu", "Exit to menu" };
        string[] settings = { "Opties", "Settings" };

        txtResume.text = resume[taal];
        txtSave.text = save[taal];
        txtExitGame.text = exitgame[taal];
        txtExitMenu.text = exitmenu[taal];
        txtSettings.text = settings[taal];
    }

    public void btnPopupCloseClick()
    {
        EventManager.CallPlayButtonClickSFX();
        if (canvasOrganizationPopup.gameObject.activeSelf && !game.tutorial.tutorialOrganizationActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasCardsPopup.gameObject.activeSelf && !game.tutorial.tutorialCardsActive)
        {
            canvasCardsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasMenuPopup.gameObject.activeSelf)
        {
            canvasMenuPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasInvestmentsPopup.gameObject.activeSelf && !game.tutorial.tutorialInvestementsActive)
        {
            canvasInvestmentsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasTimelinePopup.gameObject.activeSelf)
        {
            canvasTimelinePopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasRegioPopup.gameObject.activeSelf && !game.tutorial.tutorialRegionActive)
        {
            canvasRegioPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasMonthlyReport.gameObject.activeSelf && !game.tutorial.tutorialMonthlyReportActive)
        {
            canvasMonthlyReport.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasYearlyReport.gameObject.activeSelf)
        {
            canvasYearlyReport.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasQuestsPopup.gameObject.activeSelf && !game.tutorial.tutorialQuestsActive)
        {
            canvasQuestsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasEventPopup.gameObject.activeSelf && !game.tutorial.tutorialEventsActive)
        {
            canvasEventPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasBuildingsPopup.gameObject.activeSelf)
        {
            canvasBuildingsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasEmptyBuildingsPopup.gameObject.activeSelf && !game.tutorial.tutorialBuildingsActive)
        {
            canvasEmptyBuildingsPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
    }
    #endregion

    // Game Controller
    #region Language Change Code
    public void btnNLClick()
    {
        EventManager.CallPlayButtonClickSFX();
        if (taal != 0)
        {
            game.ChangeLanguage("dutch");
            taal = ApplicationModel.language;
            btnNextTurnText.text = nextTurnText[taal];
            txtBtnTimeline.text = "Tijdlijn";
            txtBtnMenu.text = "Menu";
            initButtonText();
            initOrganizationText();
            initRegionText();
            initInvestementsText();
            initCardsText();
        }
    }

    public void btnENGClick()
    {
        EventManager.CallPlayButtonClickSFX();
        if (taal != 1)
        {
            game.ChangeLanguage("english");
            taal = ApplicationModel.language;
            btnNextTurnText.text = nextTurnText[taal];
            txtBtnTimeline.text = "Timeline";
            txtBtnMenu.text = "Menu";
            initButtonText();
            initOrganizationText();
            initRegionText();
            initInvestementsText();
            initCardsText();
        }
    }
    #endregion  // GameController

    // Game Controller
    #region Mouse Enter & Exit Code for Icons
    // OnEnter BtnMoney
    public void BtnMoneyEnter()
    {
        btnMoneyHoverCheck = true;
        tooltipActive = true;
    }

    public void btnQuestsEnter()
    {
        btnQuestsCheck = true;
    }

    public void btnQuestsExit()
    {
        btnQuestsCheck = false;
    }

    // OnExit BtnMoney
    public void BtnMoneyExit()
    {
        btnMoneyHoverCheck = false;
        tooltipActive = false;
    }

    // OnEnter BtnHappiness
    public void BtnHappinessEnter()
    {
        btnHappinessHoverCheck = true;
        tooltipActive = true;
    }

    // OnExit BtnHappiness
    public void BtnHappinessExit()
    {
        btnHappinessHoverCheck = false;
        tooltipActive = false;
    }


    // OnEnter BtnAwareness
    public void BtnAwarenessEnter()
    {
        btnAwarenessHoverCheck = true;
        tooltipActive = true;
    }

    // OnExit BtnAwareness
    public void BtnAwarenessExit()
    {
        btnAwarenessHoverCheck = false;
        tooltipActive = false;
    }

    // OnEnter BtnPollution
    public void BtnPollutionEnter()
    {
        btnPollutionHoverCheck = true;
        tooltipActive = true;
    }

    // OnExit BtnPollution
    public void BtnPollutionExit()
    {
        btnPollutionHoverCheck = false;
        tooltipActive = false;
    }

    // OnEnter BtnEnergy
    public void BtnEnergyEnter()
    {
        btnEnergyHoverCheck = true;
        tooltipActive = true;
    }

    // OnExit BtnEnergy
    public void BtnEnergyExit()
    {
        btnEnergyHoverCheck = false;
        tooltipActive = false;
    }

    public void btnProsperityEnter()
    {
        btnProsperityHoverCheck = true;
        tooltipActive = true;
    }

    public void btnProsperityExit()
    {
        btnProsperityHoverCheck = false;
        tooltipActive = false;

    }

    public void btnOrganizationEnter()
    {
        btnOrganizationCheck = true;
    }

    public void btnOrganzationExit()
    {
        btnOrganizationCheck = false;
    }

    public void btnMenuEnter()
    {
        btnMenuCheck = true;
    }

    public void btnMenuExit()
    {
        btnMenuCheck = false;
    }

    public void btnTimelineEnter()
    {
        btnTimelineCheck = true;
    }

    public void btnTimelineExit()
    {
        btnTimelineCheck = false;
    }

    public void btnMonthlyReportEnter()
    {
        btnMonthlyReportCheck = true;
    }

    public void btnMonthlyReportExit()
    {
        btnMonthlyReportCheck = false;
    }

    public void btnYearlyReportEnter()
    {
        btnYearlyReportCheck = true;
    }

    public void btnYearlyReportExit()
    {
        btnYearlyReportCheck = false;
    }

    public void btnAfterActionCompletedEnter()
    {
        btnAfterActionCompletedCheck = true;
    }

    public void btnAfterActionCompletedExit()
    {
        btnAfterActionCompletedCheck = false;
    }

    public void regionHouseholdsEnter()
    {
        regionHouseholdsCheck = true;
    }

    public void regionHouseholdsExit()
    {
        regionHouseholdsCheck = false;
    }

    public void regionAgricultureEnter()
    {
        regionAgricultureCheck = true;
    }

    public void regionAgricultureExit()
    {
        regionAgricultureCheck = false;
    }

    public void regionCompanyEnter()
    {
        regionCompanyCheck = true;
    }

    public void regionCompanyExit()
    {
        regionCompanyCheck = false;
    }

    public void btnInvestmentsEnter()
    {
        btnInvestementsHoverCheck = true;
    }

    public void btnInvestmentsExit()
    {
        btnInvestementsHoverCheck = false;
    }

    public void btnCardsEnter()
    {
        btnCardsHoverCheck = true;
    }

    public void btnCardsExit()
    {
        btnCardsHoverCheck = false;
    }
    #endregion  // 

    #region Return Boolean Values
    public bool getBtnMoneyHover()
    {
        return btnMoneyHoverCheck;
    }

    public bool getBtnHappinessHover()
    {
        return btnHappinessHoverCheck;
    }

    public bool getBtnAwarenessHover()
    {
        return btnAwarenessHoverCheck;
    }

    public bool getBtnPollutionHover()
    {
        return btnPollutionHoverCheck;
    }

    public bool getBtnProsperityHover()
    {
        return btnProsperityHoverCheck;
    }

    public bool getBtnEnergyHover()
    {
        return btnEnergyHoverCheck;
    }

    public bool getPopupActive()
    {
        return popupActive;
    }

    public bool getTooltipActive()
    {
        return tooltipActive;
    }
    #endregion

    #region Return Other Values
    public GUIStyle returnTooltipStyle()
    {
        return tooltipStyle;
    }

    public void enterEventHover()
    {
    }

    public void enterExitHover()
    {
    }
    #endregion

    // Game Controller
    #region Next Turn Button Code
    public void nextTurnOnClick()
    {
        if (game.tutorial.tutorialNexTurnPossibe && game.currentYear < 31)
        {
            EventManager.CallPlayButtonClickSFX();
            EventManager.CallChangeMonth();

            if (!game.tutorial.tutorialNextTurnDone)
                game.tutorial.tutorialNextTurnDone = true;

        }
    }

    public void nextTurnOnEnter()
    {
    }

    public void setNextTurnButtonNotInteractable()
    {
        btnNextTurn.interactable = false;
        StartCoroutine(checkCooldown());
    }

    IEnumerator checkCooldown()
    {
        float myCounter = 0.0f;
        float setTime = 3f;

        while (myCounter < setTime)
        {
            myCounter += Time.deltaTime;
            yield return null;
        }

        btnNextTurn.interactable = true;
    }
    #endregion
    
    #region Checkboxes RegionActions Code
    public void valueChangedHouseholds()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!checkboxHouseholds)
        {
            checkboxHouseholds = true;
            regioActionCost += regioAction.afterInvestmentActionMoneyCost;
        }
        else
        {
            checkboxHouseholds = false;
            regioActionCost -= regioAction.afterInvestmentActionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }

    public void valueChangedAgriculture()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!checkboxAgriculture)
        {
            checkboxAgriculture = true;
            regioActionCost += regioAction.afterInvestmentActionMoneyCost;
        }
        else
        {
            checkboxAgriculture = false;
            regioActionCost -= regioAction.afterInvestmentActionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }

    public void valueChangedCompanies()
    {
        if (playSelectSound)
            EventManager.CallPlayOptionSelectSFX();

        if (!checkboxCompanies)
        {
            checkboxCompanies = true;
            regioActionCost += regioAction.afterInvestmentActionMoneyCost;
        }
        else
        {
            checkboxCompanies = false;
            regioActionCost -= regioAction.afterInvestmentActionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }
    #endregion

    // Game Controller
    #region Menu Popup Buttons Code
    public void btnResumeMenu()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasMenuPopup.gameObject.SetActive(false);
        popupActive = false;
        EventManager.CallPopupIsDisabled();
    }

    public void buttonExitGameOnClick()
    {
        EventManager.CallPlayButtonClickSFX();
        EventManager.CallLeaveGame();
        Application.Quit();
    }

    public void buttonSaveGame()
    {
        EventManager.CallPlayButtonClickSFX();
        EventManager.CallSaveGame();
    }

    public void loadOtherScene(int index)
    {
        EventManager.CallPlayButtonClickSFX();
        EventManager.CallLeaveGame();
        SceneManager.LoadSceneAsync(index);
    }

    public void btnSettingsClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasMenuPopup.gameObject.SetActive(false);
        canvasSettingsPopup.gameObject.SetActive(true);

        initSettingsText();
        initSettingsUI();
    }

    public void btnSettingsBackClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasSettingsPopup.gameObject.SetActive(false);
        canvasMenuPopup.gameObject.SetActive(true);

        initButtonText();
    }

    private void initSettingsText()
    {
        string[] back = { "Terug", "Back" };
        string[] music = { "Muziek volume", "Music volume" };
        string[] effects = { "Geluidseffecten volume", "Sounds effects volume" };
        string[] language = { "Verander taal", "Change language" };
        string[] dutch = { "Nederlands", "Dutch" };
        string[] english = { "Engels", "English" };


        txtButtonSettingsBack.text = back[taal];
        txtMusicVolume.text = music[taal];
        txtEffectsVolume.text = effects[taal];
        txtLanguage.text = language[taal];
        txtToggleDutch.text = dutch[taal];
        txtToggleEnglish.text = english[taal];

        //ApplicationModel.valueSFX = AudioPlayer.Instance.soundEffect.volume * 100;
        sliderEffectsVolume.value = AudioPlayer.Instance.soundEffect.volume;
        txtEffectsVolumeSliderValue.text = (AudioPlayer.Instance.soundEffect.volume * 100).ToString("0");

        //ApplicationModel.valueMusic = AudioPlayer.Instance.backgroundMusic.volume * 100;
        sliderMusicVolume.value = AudioPlayer.Instance.backgroundMusic.volume;
        txtMusicVolumeSliderValue.text = (AudioPlayer.Instance.backgroundMusic.volume * 100).ToString("0");
    }

    private void initSettingsUI()
    {
        if (taal == 0)
        {
            toggleDutch.isOn = true;
            toggleDutchCheck = true;
            toggleEnglishCheck = false;
            toggleEnglish.isOn = false;
        }
        else
        {
            toggleEnglish.isOn = true;
            toggleEnglishCheck = true;
            toggleDutchCheck = false;
            toggleDutch.isOn = false;
        }
    }

    public void toggleDutchValueChanged()
    {
        if (!toggleDutchCheck)
        {
            toggleDutchCheck = true;
            toggleEnglish.isOn = false;
            ApplicationModel.language = 0;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
            initSettingsText();
        }
        else
            toggleDutchCheck = false;
    }

    public void toggleEnglishValueChanged()
    {
        if (!toggleEnglishCheck)
        {
            toggleEnglishCheck = true;
            toggleDutch.isOn = false;
            ApplicationModel.language = 1;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
            initSettingsText();
        }
        else
        {
            toggleEnglishCheck = false;
        }
    }

    public void sliderEffectsValueChanged()
    {
        float valueSFX = sliderEffectsVolume.value;
        txtEffectsVolumeSliderValue.text = (valueSFX * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeEffects(valueSFX);
        ApplicationModel.valueSFX = valueSFX;
        PlayerPrefs.SetFloat("savedSFXVolume", valueSFX);
        PlayerPrefs.Save();
    }

    public void sliderMusicValueChanged()
    {
        float valueMusic = sliderMusicVolume.value;
        txtMusicVolumeSliderValue.text = (valueMusic * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeMusic(valueMusic);
        ApplicationModel.valueMusic = valueMusic;
        PlayerPrefs.SetFloat("savedMusicVolume", valueMusic);
        PlayerPrefs.Save();
    }
    #endregion

    // Game Controller
    #region Code for controlling Tutorial buttons presses
    public void turorialButtonPress()
    {
        EventManager.CallPlayButtonClickSFX();
        for (int i = game.tutorial.tutorialIndex; i < game.tutorial.tutorialChecks.Length; i++)
        {
            if (i == 0)
            {
                game.tutorial.tutorialChecks[i] = true;
                game.tutorial.tutorialIndex++;
                break;
            }
            else if (game.tutorial.tutorialChecks[i - 1])
            {
                if (i != game.tutorial.tutorialChecks.Length - 1)
                {
                    game.tutorial.tutorialChecks[i] = true;
                    game.tutorial.tutorialIndex++;
                    break;
                }
            }
        }
    }
    
    #endregion

    #region Get SectorStatiticsConsequences Method
    private string getSectorStatisticsConsequences(SectorStatistics s)
    {
        bool noConsequences = false;

        string[] consequences = {"", "" };
        if (s.income != 0d)
        {
            string[] a = { "\nInkomen per sector: " , "\nIncome per sector: "};

            if (s.ecoAwareness > 0d)
                a[taal] += "+" + s.income + "%";
            else
                a[taal] += s.income + "%";

            consequences[taal] += a[taal];
            noConsequences = true;
        }
        if (s.happiness != 0d)
        {
            string[] c = { "\nTevredenheid per sector: ", "\nHappiness per sector: " };

            if (s.ecoAwareness > 0d)
                c[taal] += "+" + s.happiness + "%";
            else
                c[taal] += s.happiness + "%";

            consequences[taal] += c[taal];
            noConsequences = true;
        }
        if (s.ecoAwareness != 0d)
        {
            string[] d = { "\nMilieubewustheid per sector: " , "\nEco awareness per sector: " };

            if (s.ecoAwareness > 0d)
                d[taal] += "+" + s.ecoAwareness + "%";
            else
                d[taal] += s.ecoAwareness + "%";

            consequences[taal] += d[taal];
            noConsequences = true;
        }
        if (s.prosperity != 0d)
        {
            string[] e = { "\nWelvaart per sector: " , "\nProsperity per sector: "};

            if (s.prosperity > 0d)
                e[taal] += "+" + s.prosperity + "%";
            else
                e[taal] += s.prosperity + "%";

            consequences[taal] += e[taal];
            noConsequences = true;
        }
        if (s.pollution.airPollutionIncrease != 0d)
        {
            string[] f = { "\nLuchtvervuiling per sector: " , "\nAir pollution per sector: " };

            if (s.pollution.airPollutionIncrease > 0d)
                f[taal] += "+" + s.pollution.airPollutionIncrease + "%";
            else
                f[taal] += s.pollution.airPollutionIncrease + "%";

            consequences[taal] += f[taal];
            noConsequences = true;
        }
        if (s.pollution.waterPollutionIncrease != 0d)
        {
            string[] g = { "\nWatervervuiling per sector: " , "\nWater pollution per sector: "};

            if (s.pollution.waterPollutionIncrease > 0d)
                g[taal] +=  "+" + s.pollution.waterPollutionIncrease + "%";
            else
                g[taal] += s.pollution.waterPollutionIncrease + "%";

            consequences[taal] += g[taal];
            noConsequences = true;
        }
        if (s.pollution.naturePollutionIncrease != 0d)
        {
            string[] h = { "\nNatuurvervuiling per sector: ", "\nNature pollution per sector: "};

            if (s.pollution.naturePollutionIncrease > 0d)
                h[taal] += "+" + s.pollution.naturePollutionIncrease + "%";
            else
                h[taal] += s.pollution.naturePollutionIncrease + "%";

            consequences[taal] += h[taal];
            noConsequences = true;
        }

        if (!noConsequences)
        {
            string[] st = { "\nEr zijn geen consequenties", "\nThere are no consequences" };
            return st[taal];
        }

        return consequences[taal];
    }
    #endregion

    #region Code for Building Modifiers
    private string getBuildingModifiers(Building b)
    {
        bool noConsequences = true;
        string[] modifiers = { "", "" };

        if (b.happinessModifier != 0d)
        {
            noConsequences = false;
            string[] a = { "\nTevredenheid in regio: ", "\nHappiness in region: " };

            if (b.happinessModifier > 0d)
                a[taal] += "+" + b.happinessModifier + "%";
            else
                a[taal] += b.happinessModifier + "%";

            modifiers[taal] += a[taal];
        }
        if (b.incomeModifier != 0d)
        {
            noConsequences = false;
            string[] a = { "\nInkomen in regio: ", "\nIncome in region: " };

            if (b.incomeModifier > 0d)
                a[taal] += "+" + b.incomeModifier + "%";
            else
                a[taal] += b.incomeModifier + "%";

            modifiers[taal] += a[taal];
        }
        if (b.pollutionModifier != 0d)
        {
            noConsequences = false;
            string[] a = { "\nVervuilig in regio: ", "\nPollution in region: " };

            if (b.pollutionModifier > 0d)
                a[taal] += "+" + b.pollutionModifier + "%";
            else
                a[taal] += b.pollutionModifier + "%";

            modifiers[taal] += a[taal];
        }

        if (noConsequences)
        {
            string[] nc = { "Er zijn geen aanpassingen met dit gebouw.", "This building doesn't change any values" };
            modifiers[taal] += nc[taal];
        }

        return modifiers[taal];
    }
    #endregion
}