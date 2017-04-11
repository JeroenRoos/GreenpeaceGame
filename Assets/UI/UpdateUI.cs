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
    Region regio;
    RegionAction regioAction;
    double regioActionCost;
    Game game;
    public int tutorialIndex;
    public Scrollbar scrollbarAfterActionReport;

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

    public Text txtAfterActionNoordIncome;
    public Text txtAfterActionNoordHappiness;
    public Text txtAfterActionNoordEcoAwareness;
    public Text txtAfterActionNoordPollution;
    public Text txtAfterActionNoordProsperity;
    public Text txtAfterActionNoordIncomeD;
    public Text txtAfterActionNoordHappinessD;
    public Text txtAfterActionNoordEcoAwarenessD;
    public Text txtAfterActionNoordPollutionD;
    public Text txtAfterActionNoordProsperityD;
    public Text txtAfterActionNoordEventD;
    public Text txtAfterActionNoordEvent;

    public Text txtAfterActionOostIncome;
    public Text txtAfterActionOostHappiness;
    public Text txtAfterActionOostEcoAwareness;
    public Text txtAfterActionOostPollution;
    public Text txtAfterActionOostProsperity;
    public Text txtAfterActionOostIncomeD;
    public Text txtAfterActionOostHappinessD;
    public Text txtAfterActionOostEcoAwarenessD;
    public Text txtAfterActionOostPollutionD;
    public Text txtAfterActionOostProsperityD;
    public Text txtAfterActionOostEventD;
    public Text txtAfterActionOostEvent;

    public Text txtAfterActionZuidIncome;
    public Text txtAfterActionZuidHappiness;
    public Text txtAfterActionZuidEcoAwareness;
    public Text txtAfterActionZuidPollution;
    public Text txtAfterActionZuidProsperity;
    public Text txtAfterActionZuidIncomeD;
    public Text txtAfterActionZuidHappinessD;
    public Text txtAfterActionZuidEcoAwarenessD;
    public Text txtAfterActionZuidPollutionD;
    public Text txtAfterActionZuidProsperityD;
    public Text txtAfterActionZuidEventD;
    public Text txtAfterActionZuidEvent;

    public Text txtAfterActionWestIncome;
    public Text txtAfterActionWestHappiness;
    public Text txtAfterActionWestEcoAwareness;
    public Text txtAfterActionWestPollution;
    public Text txtAfterActionWestProsperity;
    public Text txtAfterActionWestIncomeD;
    public Text txtAfterActionWestHappinessD;
    public Text txtAfterActionWestEcoAwarenessD;
    public Text txtAfterActionWestPollutionD;
    public Text txtAfterActionWestProsperityD;
    public Text txtAfterActionWestEventD;
    public Text txtAfterActionWestEvent;

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

    public Text txtAfterActionNoordIncomeYearly;
    public Text txtAfterActionNoordHappinessYearly;
    public Text txtAfterActionNoordEcoAwarenessYearly;
    public Text txtAfterActionNoordPollutionYearly;
    public Text txtAfterActionNoordProsperityYearly;
    public Text txtAfterActionNoordIncomeDYearly;
    public Text txtAfterActionNoordHappinessDYearly;
    public Text txtAfterActionNoordEcoAwarenessDYearly;
    public Text txtAfterActionNoordPollutionDYearly;
    public Text txtAfterActionNoordProsperityDYearly;

    public Text txtAfterActionOostIncomeYearly;
    public Text txtAfterActionOostHappinessYearly;
    public Text txtAfterActionOostEcoAwarenessYearly;
    public Text txtAfterActionOostPollutionYearly;
    public Text txtAfterActionOostProsperityYearly;
    public Text txtAfterActionOostIncomeDYearly;
    public Text txtAfterActionOostHappinessDYearly;
    public Text txtAfterActionOostEcoAwarenessDYearly;
    public Text txtAfterActionOostPollutionDYearly;
    public Text txtAfterActionOostProsperityDYearly;

    public Text txtAfterActionZuidIncomeYearly;
    public Text txtAfterActionZuidHappinessYearly;
    public Text txtAfterActionZuidEcoAwarenessYearly;
    public Text txtAfterActionZuidPollutionYearly;
    public Text txtAfterActionZuidProsperityYearly;
    public Text txtAfterActionZuidIncomeDYearly;
    public Text txtAfterActionZuidHappinessDYearly;
    public Text txtAfterActionZuidEcoAwarenessDYearly;
    public Text txtAfterActionZuidPollutionDYearly;
    public Text txtAfterActionZuidProsperityDYearly;

    public Text txtAfterActionWestIncomeYearly;
    public Text txtAfterActionWestHappinessYearly;
    public Text txtAfterActionWestEcoAwarenessYearly;
    public Text txtAfterActionWestPollutionYearly;
    public Text txtAfterActionWestProsperityYearly;
    public Text txtAfterActionWestIncomeDYearly;
    public Text txtAfterActionWestHappinessDYearly;
    public Text txtAfterActionWestEcoAwarenessDYearly;
    public Text txtAfterActionWestPollutionDYearly;
    public Text txtAfterActionWestProsperityDYearly;

    // Text AfterActionCompleted
    public Text txtAfterActionCompletedTitle;
    public Text txtAfterActionCompletedColumnLeft;
    public Text txtAfterActionCompletedColumnRight;
    public Text txtAfterActionCompletedColumnLeftDescription;
    public Text txtAfterActionCompletedColumnRightDescription;

    // Text Menu Popup
    public Text txtResume;
    public Text txtSave;
    public Text txtExitMenu;
    public Text txtExitGame;

    // Text Main UI
    public Text txtMoney;
    public Text txtPopulation;
    public Text txtDate;
    public Text btnNextTurnText;
    public Text txtBtnMenu;
    public Text txtBtnTimeline;

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
    public Button btnMoney;
    public Button btnHappiness;
    public Button btnAwareness;
    public Button btnEnergy;
    public Button btnProsperity;
    public Button btnPollution;
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

    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;
    public Canvas canvasRegioPopup;
    public Canvas canvasTutorial;
    public Canvas canvasMonthlyReport;
    public Canvas canvasYearlyReport;
    public Canvas canvasAfterActionCompletedPopup;

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

    public Image imgTutorialRegion;
    public Button btnTutorialRegion;
    public Text txtTutorialRegion;
    public Text txtTurorialReginoBtnText;

    public Image imgTutorialOverworld;
    public Button btnTutorialOverworld;
    public Text txtTutorialOverworld;
    public Image imgTutorialOrganization;
    public Text txtTutorialOrganization;
    public Text txtTutorialOrganizationBtnText;
    public Button btnTutorialOrganization;

    private Vector3 v3Tooltip;

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
    private bool btnMenuCheck;
    private bool btnTimelineCheck;
    public bool popupActive;
    private bool btnAfterActionStatsCheck;
    private bool btnMonthlyReportCheck;
    private bool btnYearlyReportCheck;
    private bool btnAfterActionCompletedCheck;

    public bool tutorialActive;
    private bool tutorialNoTooltip;
    private bool tutorialStep2;
    private bool tutorialStep3;
    private bool tutorialStep4;
    private bool tutorialStep5;
    private bool regionWestActivated;
    private bool tutorialStep6;
    private bool tutorialStep7;
    private bool tutorialCheckActionDone;
    private bool tutorialStep8;
    public bool tutorialStep9;
    private bool tutorialStep10;
    private bool tutorialstep11;
    private bool tutorialstep12;
    private bool tutorialStep13;
    private bool tutorialOrganizationDone;
    public bool tutorialNextTurnDone;

    private bool tooltipActive;
    private bool regionHouseholdsCheck;
    private bool regionAgricultureCheck;
    private bool regionCompanyCheck;
    private bool dropdownChoiceMade;
    #endregion

    #region Start(), Update(), FixedUpdate()
    // Use this for initialization
    void Start()
    {
        initButtons();
        initCanvas();
        tooltipStyle.normal.background = tooltipTexture;
        taal = game.language;

        // Use this boolean to start the game with or without the tutorial while testing
        tutorialActive = false;

        if (tutorialActive)
            initTutorialActive();
        else
            initTutorialNotActive();
    }

    private void initTutorialActive()
    {
        canvasTutorial.gameObject.SetActive(true);
        tutorialNoTooltip = true;
        regionWestActivated = false;
        tutorialCheckActionDone = false;
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;
        imgTutorialStepOrgMenuHightlight.enabled = false;
        tutorialIndex = 1;
        canvasTutorial.gameObject.SetActive(true);
        StartCoroutine(initTutorialText());
    }

    private void initTutorialNotActive()
    {
        imgTutorialOverworld.gameObject.SetActive(false);
        btnTutorialOverworld.gameObject.SetActive(false);
        btnTutorialRegion.gameObject.SetActive(false);
        txtTutorialOverworld.enabled = false;
        canvasTutorial.gameObject.SetActive(false);
        tutorialStep2 = true;
        tutorialStep3 = true;
        tutorialStep4 = true;
        tutorialStep5 = true;
        tutorialStep6 = true;
        tutorialStep7 = true;
        tutorialStep8 = true;
        tutorialStep9 = true;
        tutorialStep10 = true;
        tutorialstep11 = true;
        tutorialstep12 = true;
        tutorialStep13 = true;
        regionWestActivated = true;
        tutorialCheckActionDone = true;
    }

    void Update()
    {
        if (tutorialStep6)
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

    void FixedUpdate()
    {

    }
    #endregion

    #region Init UI Elements
    IEnumerator initTutorialText()
    {
        string[] step1 = { "Welkom! De overheid heeft jouw organisatie de opdracht gegeven om ervoor te zorgen dat Nederland een milieubewust land wordt. " + 
                "De inwoners moeten begrijpen dat een groen land belangrijk is.", "Welcome! The government has given your organisation the task to make " +
                "The Netherlands an country aware of the environment. The inhabitants need to understand the importance of a green country. "};
        string[] btnText = { "Verder", "Next" };

        txtTurorialStep1.text = step1[taal];
        txtTutorialStep1BtnText.text = btnText[taal];

        while (!tutorialStep2)
            yield return null;


        //tutorialStep2 = false;
        string[] step2 = { "Het doel is om de vervuiling in het land onder 5% te hebben in 2050. Zoals je kunt zien zitten we nu in 2020. " +
                "Je hebt dus 30 jaar om dit doel te halen.", "The goal is to get pollution under 5% before 2050. As you can see the current year is 2020. " +
                "This means you have 30 years to reach this goal. "};
        txtTurorialStep1.text = step2[taal];
         imgTutorialStep2Highlight1.enabled = true;
         imgTutorialStep2Highlight2.enabled = true;

        //ColorBlock cb = btnPollution.colors;
        //Color color;

        while (!tutorialStep3)
        {
            yield return null;

            /*
            color = Color.red;
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.pressedColor = color;
            btnPollution.colors = cb;

            yield return new WaitForSeconds(0.1f);

            color = Color.white;
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.pressedColor = color;
            btnPollution.colors = cb;
            */
        }

        //tutorialStep3 = false;
        string[] step3 = { "Dit zijn jouw resources om het doel te behalen. Geld wordt gebruikt om jouw beslissingen te financieren. Tevredenheid bepaald of het " +
                "volk besluit om mee te werken met jouw beslissingen. Milieubewustheid zorgt ervoor dat er minder wordt vervuilt. Veruiling geeft de vervuiling in het land weer. De welvaart toont hoe het zit met de hoeveelheid geld in de verschillende regio's." + 
                "Tot slot wordt er getoont hoeveel mensen er in Nederland wonen. Al deze iconen geven het gemiddelde van de verschillende regio's weer. "+ 
                "Als je meer informatie over de statistieken wil hebben kun je met je muis eroverheen gaan. Er verschijnt dan een tooltip met de extra informatie."
                , "Here are the resources that help you achieve your goal. Money is used for financing the decisions you make. Happiness determines whether people cooperate or not" +
                "A better Eco awareness means less pollution. The pollutions shows the pollution in the country. These icons show the averages from the different regions. " + 
                "For more informations about these statistics you can hover of the icon with your mouse. You can see the extra information in the tooltip."};
        txtTurorialStep1.text = step3[taal];
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;

        while (!tutorialStep4)
            yield return null;

        Vector3 imgOldPos = imgTutorialOverworld.gameObject.transform.position;
        Vector3 imgNewPos = imgOldPos;
        imgNewPos.x = imgNewPos.x + Screen.width / 3;
        imgTutorialOverworld.gameObject.transform.position = imgNewPos;
        string[] step4 = { "Het land bestaat uit 4 regio's. Noord-Nederland, Oost-Nederland, Zuid-Nederland en West-Nederland. " +
                "Elke regio heeft een inkomen, tevredenheid, vervuiling, milieubewustheid en werlvaart. Deze statistieken verschillen weer per regio.\nGa naar West-Nederland door op de regio te klikken. "
                , "There are 4 regions, The Netherlands North, The Netherlands East, The Netherlands South and The Netherland West" +
                "Each region has a income, happiness, pollution, eco-awareness and prosperity. These statistics differ foreach region.\n Go to The Netherlands West by clicking on the region. "};
        txtTurorialStep1.text = step4[taal];

        while (!tutorialStep5) 
            yield return null;

        canvasTutorial.gameObject.SetActive(false);

        while (!tutorialCheckActionDone)
            yield return null;

        while (canvasRegioPopup.gameObject.activeSelf)
            yield return null;


        imgTutorialOverworld.gameObject.transform.position = imgOldPos;
        canvasTutorial.gameObject.SetActive(true);
        string[] step5 = { "Onderin het scherm kun je naar het Organisatie menu gaan door op de knop te drukken. ",
            "At the bottom of your screen you can go to the Organization menu by pressing the button. " };
        txtTurorialStep1.text = step5[taal];
        imgTutorialStepOrgMenuHightlight.enabled = true;

        while (!tutorialStep8)
            yield return null;

        imgTutorialStepOrgMenuHightlight.enabled = false;
        canvasTutorial.gameObject.SetActive(false);

        while (!tutorialOrganizationDone)
            yield return null;

        while (canvasOrganizationPopup.gameObject.activeSelf)
            yield return null;

        canvasTutorial.gameObject.SetActive(true);
        string[] step6 = { "Om naar de volgende maand en beurt te gaan druk je op de Volgende beurt knop rechtsonderin. Je kunt ook op de ENTER toets drukken om naar de volgende maand te gaan. ",
            "You can go to the next month / turn by pressing the Next turn button in the bottomright of your screen. You can also press the ENTER key to got to the next turn." };
        txtTurorialStep1.text = step6[taal];

        while (!tutorialStep10)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);

        while (!tutorialNextTurnDone)
            yield return null;

        canvasTutorial.gameObject.SetActive(true);
        string[] step7 = { "Er is een nieuwe maand en we hebben nog veel te doen. Er is namelijk een event bezig. Er kunnen elke nieuwe turn enkele events ontstaan. Er kan maar 1 event " +
                "tegelijk in een regio zijn. Er kunnen wel meerdere events tegelijk zijn in meerdere regio's. Voor elk event heb je een aantal beurten om te beslissen wat je met de event gaat doen. "
                , "It's a new month and there is lots to do. There is an active event running at the moment. Each turn there will be new events. There can only be one event in a region at the same time. " +
                "There can be multiple active events in the whole country. For each event you have a few turns to decide what you are going to do. "  };
        txtTurorialStep1.text = step7[taal];

        while (!tutorialstep11)
            yield return null;

        string[] step8 = { "Door met je muis over de event te hoveren krijg je extra informatie over de event te zien. Door op de event de klikken krijg je te zien welke keuzes je hebt om het op te lossen. " +
                "Bij elk event heb je altijd 3 keuzes. Elke keuze brengt weer andere consequencies met zich mee voor de verschillende statistieken. Het is dus cruciaal dat je goed nadenkt over je beslissingen. " 
                , "You can get extra information from an event by hovering over the event icon with your mouse. You can see the choices you have by clicking on the event. You always have 3 choices foreach event. " +
                "Each choice brings other consequences for the different statistics. This means it's crucial to think about what you want to achieve before making a choice. "};
        txtTurorialStep1.text = step8[taal];

        while (!tutorialstep12)
            yield return null;

        string[] step9 = { "Vergeet niet dat je voor 2050 de vervuiling onder 5% moet hebben. Houdt verder de verschillende statistieken goed in de gaten bij het doen van acties en het oplossen van events. " +
                "Je kunt nu de event oplossen op de manier die jou het beste lijkt. "
                , "Do not forget that the national pollution has to be under 5% before 2050. You also have to keep an eye on the different statistics while doing action and events. " + 
                "Now you can complete the event by making a choice you think is right."};
        txtTurorialStep1.text = step9[taal];

        string[] btnText2 = { "Handleiding voltooien", "Complete tutorial" };

        txtTutorialStep1BtnText.text = btnText2[taal];

        while (!tutorialStep13)
            yield return null;

        tutorialActive = false;
        canvasTutorial.gameObject.SetActive(false);

    }

    void initButtons()
    {
        btnMenu.GetComponent<Button>();
        btnTimeline.GetComponent<Button>();
        btnOrganization.GetComponent<Button>();
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
        btnTimelineCheck = false;
        btnMonthlyReportCheck = false;
        btnYearlyReportCheck = false;
        btnAfterActionCompletedCheck = false;
        btnMenuCheck = false;
        popupActive = false;
        regionHouseholdsCheck = false;
        tooltipActive = false;
        regionAgricultureCheck = false;
        regionCompanyCheck = false;
        checkboxHouseholds = true;
        checkboxCompanies = true;
        checkboxAgriculture = true;
    }

    void initCanvas()
    {
        canvasMenuPopup.GetComponent<Canvas>();
        canvasMenuPopup.gameObject.SetActive(false);

        canvasOrganizationPopup.GetComponent<Canvas>();
        canvasOrganizationPopup.gameObject.SetActive(false);

        canvasTimelinePopup.GetComponent<Canvas>();
        canvasTimelinePopup.gameObject.SetActive(false);

        canvasRegioPopup.GetComponent<Canvas>();
        canvasRegioPopup.gameObject.SetActive(false);

        canvasMonthlyReport.GetComponent<Canvas>();
        canvasMonthlyReport.gameObject.SetActive(false);

        canvasYearlyReport.GetComponent<Canvas>();
        canvasYearlyReport.gameObject.SetActive(false);

        canvasAfterActionCompletedPopup.GetComponent<Canvas>();
        canvasAfterActionCompletedPopup.gameObject.SetActive(false);

        if (tutorialActive)
        {
            canvasTutorial.GetComponent<Canvas>();
            canvasTutorial.gameObject.SetActive(true);
        }
    }

    public void LinkGame(Game game)
    {
        this.game = game;
    }
    #endregion

    #region Code for controlling popups
    void popupController()
    {
        // Close active popup with Escape / Open Menu popup with Escape if no popup is active
        if (Input.GetKeyUp(KeyCode.Escape))
            closeWithEscape();

        // Open and close Organization popup with O
        else if (Input.GetKeyUp(KeyCode.O))
            if (tutorialStep8)
                controllerOrganizationHotkey();

            // Open and close Timeline popup with T

            else if (Input.GetKeyUp(KeyCode.T))
                if (!tutorialActive)
                    controllerTimelinePopup();
    }

    // Close the active popup with the Escape key (and open main menu with escape if no popup is active)
    void closeWithEscape()
    {
        if (!popupActive && !tutorialActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            initButtonText();
        }
        else if (canvasOrganizationPopup.gameObject.activeSelf)
        {
            canvasOrganizationPopup.gameObject.SetActive(false);
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
        if (canvasAfterActionCompletedPopup.gameObject.activeSelf)
        {
            canvasAfterActionCompletedPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
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

        if (checkTooltip() && !popupActive && tutorialStep3)
        {
            lblReqt.x = v3Tooltip.x + 10; lblReqt.y = v3Tooltip.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (regionHouseholdsCheck && popupActive && tutorialStep3)
        {
            v3Tooltip = emptybtnHoverHouseholds.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 70;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipHouseholds + "</color>", tooltipStyle);
            updateRegionSectors();
            
        }
        else if (regionAgricultureCheck && popupActive && tutorialStep3)
        {
            v3Tooltip = emptybtnHoverAgriculture.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 150;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipAgriculture + "</color>", tooltipStyle);
            updateRegionSectors();
        }
        else if (regionCompanyCheck && popupActive && tutorialStep3)
        {
            v3Tooltip = emptybtnHoverCompanies.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 270;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipCompany + "</color>", tooltipStyle);
            updateRegionSectors();
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
        //Debug.Log(money);
        txtMoney.text = money.ToString();
    }

    // Update Population based on value
    public void updatePopulation(double population)
    {
        int popu = Convert.ToInt32(population);
        txtPopulation.text = popu.ToString();
    }

    // Update Awareness based on value
    public void updateAwarness(double awareness)
    {
        //Debug.Log("Awareness: " + awareness);
        iconController(btnAwareness, awareness);
    }

    // Update Pollution based on value
    public void updatePollution(double pollution)
    {
        //Debug.Log("Pollution: "+ pollution);
        iconController(btnPollution, pollution);
    }

    // Update Energy based on value
    public void updateEnergy(double energy)
    {
        //Debug.Log("Energy: " + energy);
        iconController(btnEnergy, energy);
    }

    public void updateProsperity(double prosperity)
    {
        iconController(btnProsperity, prosperity);
    }

    // Update Happiness based on value
    public void updateHappiness(double happiness)
    {
        //Debug.Log("Happiness: " + happiness);
        iconController(btnHappiness, happiness);
    }

    // Change color of the button based on value
    void iconController(Button btn, double value)
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

    #region Update UI in Popups
    public void updateOrganizationScreenUI(double value, int i, double money)
    {
        imgTutorialOrganization.enabled = false;
        txtTutorialOrganization.enabled = false;
        btnTutorialOrganization.gameObject.SetActive(false);
        // Region are made in following order: North > East > West > South
        switch (i)
        {
            case 0:
                txtOrgNoordMoney.text = value.ToString();
                break;
            case 1:
                txtOrgOostMoney.text = value.ToString();
                break;
            case 2:
                txtOrgWestMoney.text = value.ToString();
                break;
            case 3:
                txtOrgZuidMoney.text = value.ToString();
                break;
        }

        txtOrgBank.text = money.ToString();
        initOtherText();

        if (tutorialStep8 && tutorialActive)
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
                "De investeringen zijn nog niet uitgewerkt en werken dus nog niet. Je kunt dit menu sluiten door op de ESC toets te drukken."
                , "In the organization menu you can view the yearly income of each regio. This can come in handy when deciding your expanses the comming year. " +
                "The investments aren't working yet. You can close this menu by pressing the ESC key."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialOrganization.text = step1[taal];
        txtTutorialOrganizationBtnText.text = btnText[taal];

        while (!tutorialStep9)
            yield return null;

        imgTutorialOrganization.gameObject.SetActive(false);
        tutorialOrganizationDone = true;
    }

    private void initOtherText()
    {
        string[] left = { "Budget", "Budget" };
        string[] right = { "Investeringen", "Investments" };
        string[] title = { "Organisatie", "Organization" };
        string[] bank = { "Bank", "Storage" };
        string[] noord = { "Noord-Nederland", "The Netherlands Northern" };
        string[] oost = { "Oost-Nederland", "The Netherlands Eastern" };
        string[] zuid = { "Zuid-Nederland", "The Netherlands Southern" };
        string[] west = { "West-Nederland", "The Netherlands Western" };
        string[] yearly = { "Jaarlijks budget per regio", "Yearly budget per region" };
        string[] demonstration = { "Demonstraties", "Demonstrations" };
        string[] research = { "Onderzoek", "Research" };
        string[] guarding = { "Eco bescherming", "Eco guarding" };
        string[] big = { "Hier kun je een gedeelte van het geld op je bank investeren in de " + 
                "\norganistie. Als je meer geld in een onderdeel zet heb je en grotere" + 
                "\nkans op succes in dat onderdeel. 1 vakje is 10000", "You can invest some of your budget in your " +
                "own organization. If you invest more in one of the segments, you have a higher" + 
                "chance of success. One block equals 10000" };

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
        txtDemonstration.text = demonstration[taal];
        txtResearch.text = research[taal];
        txtEcoGuarding.text = guarding[taal];
    }
    #endregion

    #region Code for the Region Screen
    public void regionClick(Region region)
    {
        if (tutorialActive && tutorialStep5)
        {
            if (region.name[0] == "West Nederland")
            {
                startRegionPopup(region);
                regionWestActivated = true;

                btnTutorialRegion.gameObject.SetActive(true);
                StartCoroutine(tutorialRegionPopup());
            }
        }
        else if (!canvasRegioPopup.gameObject.activeSelf && !popupActive && !btnOrganizationCheck
        && !btnMenuCheck && !btnTimelineCheck && !tutorialActive && !btnMonthlyReportCheck && !btnYearlyReportCheck && !btnAfterActionCompletedCheck)
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
        string[] step1 = { "Elke regio bestaat uit 3 sectoren. Deze sectoren zijn Huishoudens, Landbouw en Bedrijven. De sectoren hebben statistieken voor " +
                "tevredenheid, vervuiling, milieubewustheid en welvaart. Deze sectoren statistieken maken het gemiddelde waar de regio statistieken uit bestaan. Je kunt deze secor statistieken zien door " +
                "met je muis over de sector te hoveren."
                , "Each region has 3 sectors. These sectors are Households, Agriculture and Companies. These sectors have statistics for happiness, pollution, eco awareness and prosperity. " +
                "These sector statistics create the averages which are the region statistics. It is important to keep each sector happy. You can view these sector statistics by using your mouse to hover over the sector. "};
        string[] btnText = { "Verder", "Next" };

        txtTutorialRegion.text = step1[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        while (!tutorialStep6)
            yield return null;

        string[] step2 = { "Je kunt in een regio acties uitvoeren. Acties kosten echter geld en meestal ook tijd. Je kunt maar 1 actie tegelijk doen in een regio. " + 
                "Sommige acties kunnen ook maar 1 keer of eens in de zoveel tijd gedaan worden. Als je een actie kiest krijg je een aantal gegevens over de actie te zien. Daarnaast kun je kiezen " +
                "op welke sectoren je de actie invloed uitoefend. Sommige acties kunnen in elke sector gedaan worden, andere in 1 of 2 van de sectoren. Kies nu een actie, keer vervolgens terug naar de landkaart door op de ESC toets te drukken."
                , "You can do actions in regions. These actions cost money and most of the time also time. You can do 1 action at the time in a region. " +
                "Soms actions you can only do once, others you can do again after some time. When you chose an action you can see a few statistics about the action. You also have to chosoe in which sectors you want the action to do things. " +
                "Some actions can be done in each sectors, others only in 1 or 2 of the sectors. Choose an action, after that, return to the map by pressing the ESC key."};

        txtTutorialRegion.text = step2[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        while (!tutorialStep7)
            yield return null;

        imgTutorialRegion.gameObject.SetActive(false);
    }

    private void updateRegionScreenUI()
    {
        // Set the text in the popup based on language
        initMainText();

        // Debug.Log("UpdateRegionScreenUI: " + regio.name);
        updateRegionTextValues();

        // Set the right actions in the dropdown
        initDropDownRegion();

        // Set toggles on not active
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);
    }

    private void initMainText()
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
        // Debug.Log("updateRegionTextValues: " + regio.name);
        txtRegionName.text = regio.name[taal];
        txtRegionMoney.text = regio.statistics.income.ToString("0.00");
        txtRegionHappiness.text = regio.statistics.happiness.ToString("0.00");
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString("0,00") + "%";
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

        // Er kan maar 1 action per regio zijn
        foreach (RegionAction a in regio.actions)
        {
            if (a.isActive)
            {
                dropdownRegio.gameObject.SetActive(false);
                break;
            }
            else
            {
                dropdownRegio.gameObject.SetActive(true);
                initDropDownRegion();
            }
        }
    }

    private void updateRegionSectors()
    {
        foreach (RegionSector sector in regio.sectors)
        {
            if (sector.sectorName[taal] == "Huishoudens" || sector.sectorName[taal] == "Households")
            {
                //Debug.Log(sector.sectorName);
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
               // Debug.Log(sector.sectorName);
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
                //Debug.Log(sector.sectorName);
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
        bool breaking = false;
        foreach (GameEvent e in game.events)
        {
            if (e.isActive || e.isIdle)
            {
                foreach (Region region in game.regions)
                {
                    foreach (GameEvent ev in region.inProgressGameEvents)
                    {
                        if (ev == e)
                        {
                            activeEventsRegio += e.publicEventName[taal] + "\n";
                            breaking = true;
                            break;
                        }
                    }

                    if (breaking)
                        break;
                }
            }
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
                activeActionsRegio += action.name[taal];
            }

            txtActiveActions.text = activeActionsRegio;
        }
    }

    private void initDropDownRegion()
    {
        dropdownRegio.ClearOptions();
        int currentMonth = game.currentYear * 12 + game.currentMonth;

        foreach (RegionAction action in regio.actions)
        {
            if (!action.isActive &&
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
        for (int i = 0; i <= dropdownRegio.options.Count; i++)
        {
            if (dropdownRegio.value == i)
            {
                dropdownChoice = dropdownRegio.options[i].text;
                Debug.Log(dropdownRegio.options[i].text);
            }
        }
        Debug.Log(dropdownChoice);

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

                string[] actionCostText = { "Kosten per sector: " + action.actionMoneyCost + " geld",
                    "Costs per sector: " + action.actionMoneyCost + " geld" };
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

        if (checkboxHouseholds)
            checkboxRegionHouseholds.isOn = false;
        if (checkboxAgriculture)
            checkboxRegionAgriculture.isOn = false;
        if (checkboxCompanies)
            checkboxRegionCompanies.isOn = false;

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

    /*private string getActionConsequences(SectorStatistics s)
    {
        string[] consequences = { "Consequenties:\n", "Consequences:\n" };

        if (s.income != 0)
        {
            string[] a = { "Inkomen: " + s.income + "\n", "Income: " + s.income + "\n" };
            consequences[taal] += a[taal];
        }
        if (s.happiness != 0)
        {
            string[] c = { "Tevredenheid: " + s.happiness + "\n", "Happiness: " + s.happiness + "\n" };
            consequences[taal] += c[taal];
        }
        if (s.ecoAwareness != 0)
        {
            string[] d = { "Milieubewustheid: " + s.ecoAwareness + "\n", "Eco awareness: " + s.ecoAwareness + "\n" };
            consequences[taal] += d[taal];
        }
        if (s.prosperity != 0)
        {
            string[] e = { "Welvaart: " + s.prosperity + "\n", "Prosperity: " + s.prosperity + "\n" };
            consequences[taal] += e[taal];
        }
        if (s.pollution.airPollutionIncrease != 0)
        {
            string[] f = { "Luchtvervuiling: " + s.pollution.airPollutionIncrease + "\n", "Air pollution: " + s.pollution.airPollutionIncrease + "\n" };
            consequences[taal] += f[taal];
        }
        if (s.pollution.waterPollutionIncrease != 0)
        {
            string[] g = { "Watervervuiling: " + s.pollution.waterPollutionIncrease + "\n", "Water pollution: " + s.pollution.waterPollutionIncrease + "\n" };
            consequences[taal] += g[taal];
        }
        if (s.pollution.naturePollutionIncrease != 0)
        {
            string[] h = { "Natuurvervuiling: " + s.pollution.naturePollutionIncrease + "\n", "Nature pollution: " + s.pollution.naturePollutionIncrease + "\n" };
            consequences[taal] += h[taal];
        }

        return consequences[taal];
    }*/

    private string getActionCost(SectorStatistics s)
    {
        //string[] tip;
        if (s.income != 0)
        {
            string[] tip = { "Kosten: " + s.income, "Cost: " + s.income };
            return tip[taal];
        }

        return "0";
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

        if (!tutorialCheckActionDone)
            tutorialCheckActionDone = true;

        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();
        updateRegionTextValues();
    }
    #endregion

    #region Code for AfterActionStats
    public void InitMonthlyReport()
    {
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
        string[] txtDescription = { "Veranderde waardes", "Changed values" };
        string[] txtNewEventDescription = { "Nieuwe events", "New events" };

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

            txtAfterActionNoordIncomeD.text = txtIncomeDescription[taal];
            txtAfterActionNoordHappinessD.text = txtHappinessDescription[taal];
            txtAfterActionNoordEcoAwarenessD.text = txtEcoAwarenessDescription[taal];
            txtAfterActionNoordPollutionD.text = txtPollutionDescription[taal];
            txtAfterActionNoordProsperityD.text = txtProsperityDescription[taal];
            txtAfterActionNoordEventD.text = txtNewEventDescription[taal];

            txtAfterActionOostIncomeD.text = txtIncomeDescription[taal];
            txtAfterActionOostHappinessD.text = txtHappinessDescription[taal];
            txtAfterActionOostEcoAwarenessD.text = txtEcoAwarenessDescription[taal];
            txtAfterActionOostPollutionD.text = txtPollutionDescription[taal];
            txtAfterActionOostProsperityD.text = txtProsperityDescription[taal];
            txtAfterActionOostEventD.text = txtNewEventDescription[taal];

            txtAfterActionZuidIncomeD.text = txtIncomeDescription[taal];
            txtAfterActionZuidHappinessD.text = txtHappinessDescription[taal];
            txtAfterActionZuidEcoAwarenessD.text = txtEcoAwarenessDescription[taal];
            txtAfterActionZuidPollutionD.text = txtPollutionDescription[taal];
            txtAfterActionZuidProsperityD.text = txtProsperityDescription[taal];
            txtAfterActionZuidEventD.text = txtNewEventDescription[taal];

            txtAfterActionWestIncomeD.text = txtIncomeDescription[taal];
            txtAfterActionWestHappinessD.text = txtHappinessDescription[taal];
            txtAfterActionWestEcoAwarenessD.text = txtEcoAwarenessDescription[taal];
            txtAfterActionWestPollutionD.text = txtPollutionDescription[taal];
            txtAfterActionWestProsperityD.text = txtProsperityDescription[taal];
            txtAfterActionWestEventD.text = txtNewEventDescription[taal];
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

            txtAfterActionNoordIncomeDYearly.text = txtIncomeDescription[taal];
            txtAfterActionNoordHappinessDYearly.text = txtHappinessDescription[taal];
            txtAfterActionNoordEcoAwarenessDYearly.text = txtEcoAwarenessDescription[taal];
            txtAfterActionNoordPollutionDYearly.text = txtPollutionDescription[taal];
            txtAfterActionNoordProsperityDYearly.text = txtProsperityDescription[taal];

            txtAfterActionOostIncomeDYearly.text = txtIncomeDescription[taal];
            txtAfterActionOostHappinessDYearly.text = txtHappinessDescription[taal];
            txtAfterActionOostEcoAwarenessDYearly.text = txtEcoAwarenessDescription[taal];
            txtAfterActionOostPollutionDYearly.text = txtPollutionDescription[taal];
            txtAfterActionOostProsperityDYearly.text = txtProsperityDescription[taal];

            txtAfterActionZuidIncomeDYearly.text = txtIncomeDescription[taal];
            txtAfterActionZuidHappinessDYearly.text = txtHappinessDescription[taal];
            txtAfterActionZuidEcoAwarenessDYearly.text = txtEcoAwarenessDescription[taal];
            txtAfterActionZuidPollutionDYearly.text = txtPollutionDescription[taal];
            txtAfterActionZuidProsperityDYearly.text = txtProsperityDescription[taal];

            txtAfterActionWestIncomeDYearly.text = txtIncomeDescription[taal];
            txtAfterActionWestHappinessDYearly.text = txtHappinessDescription[taal];
            txtAfterActionWestEcoAwarenessDYearly.text = txtEcoAwarenessDescription[taal];
            txtAfterActionWestPollutionDYearly.text = txtPollutionDescription[taal];
            txtAfterActionWestProsperityDYearly.text = txtProsperityDescription[taal];
        }
    }

    public void initAfterActionStatsNewEvents()
    {
        string[] txtNewEvent = { "Nieuwe events", "New events" };
        txtAfterActionNoordEvent.text = "";
        txtAfterActionOostEvent.text = "";
        txtAfterActionZuidEvent.text = "";
        txtAfterActionWestEvent.text = "";

        foreach (Region r in game.regions)
        {
            foreach (GameEvent ge in r.inProgressGameEvents)
            {
                for (int i = 0; i < game.monthlyReport.newEvents.Length; i++)
                {
                    foreach (GameEvent e in game.monthlyReport.newEvents[i])
                    {
                        if (e == ge)
                        {
                            if (r.name[0] == "Noord Nederland")
                                setNewEventsNoord(e);
                            else if (r.name[0] == "Oost Nederland")
                                setNewEventsOost(e);
                            else if (r.name[0] == "Zuid Nederland")
                                setNewEventsZuid(e);
                            else if (r.name[0] == "West Nederland")
                                setNewEventsWest(e);
                        }
                    }
                }
            }
        }
    }

    private void setNewEventsNoord(GameEvent e)
    {
        txtAfterActionNoordEvent.text = e.publicEventName[taal];
    }

    private void setNewEventsOost(GameEvent e)
    {
        txtAfterActionOostEvent.text = e.publicEventName[taal];
    }

    private void setNewEventsZuid(GameEvent e)
    {
        txtAfterActionZuidEvent.text = e.publicEventName[taal];
    }

    private void setNewEventsWest(GameEvent e)
    {
        txtAfterActionWestEvent.text = e.publicEventName[taal];
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
            incomeDifference = game.regions[i].statistics.income - oldIncome[i];//game.monthlyReport.oldIncome[i];
            happinessDifference = game.regions[i].statistics.happiness - oldHappiness[i];//game.monthlyReport.oldHappiness[i];
            ecoAwarenessDifference = game.regions[i].statistics.ecoAwareness - oldEcoAwareness[i];//game.monthlyReport.oldEcoAwareness[i];
            pollutionDifference = game.regions[i].statistics.avgPollution - oldPollution[i];//game.monthlyReport.oldPollution[i];
            prosperityDifference = game.regions[i].statistics.prosperity - oldProsperity[i];//game.monthlyReport.oldProsperity[i];

            if (game.monthlyReport.reportRegions[i] == "Noord Nederland")
            {
                setDifferenceTextValuesNoord(incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference, isMonthly);
            }
            else if (game.monthlyReport.reportRegions[i] == "Oost Nederland")
            {
                setDifferenceTextValuesOost(incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference, isMonthly);
            }
            else if (game.monthlyReport.reportRegions[i] == "Zuid Nederland")
            {
                setDifferenceTextValuesZuid(incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference, isMonthly);
            }
            else if (game.monthlyReport.reportRegions[i] == "West Nederland")
            {
                setDifferenceTextValuesWest(incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference, isMonthly);
            }
        }
    }

    private void setDifferenceTextValuesNoord(double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference, bool isMonthly)
    {
        if (isMonthly)
        {
            txtAfterActionNoordIncome.text = incomeDifference.ToString("0.00");
            txtAfterActionNoordHappiness.text = happinessDifference.ToString("0.00");
            txtAfterActionNoordEcoAwareness.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionNoordPollution.text = pollutionDifference.ToString("0.00");
            txtAfterActionNoordProsperity.text = prosperityDifference.ToString("0.00");
        }

        else
        {
            txtAfterActionNoordIncomeYearly.text = incomeDifference.ToString("0.00");
            txtAfterActionNoordHappinessYearly.text = happinessDifference.ToString("0.00");
            txtAfterActionNoordEcoAwarenessYearly.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionNoordPollutionYearly.text = pollutionDifference.ToString("0.00");
            txtAfterActionNoordProsperityYearly.text = prosperityDifference.ToString("0.00");

        }
    }

    private void setDifferenceTextValuesOost(double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference, bool isMonthly)
    {
        if (isMonthly)
        {
            txtAfterActionOostIncome.text = incomeDifference.ToString("0.00");
            txtAfterActionOostHappiness.text = happinessDifference.ToString("0.00");
            txtAfterActionOostEcoAwareness.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionOostPollution.text = pollutionDifference.ToString("0.00");
            txtAfterActionOostProsperity.text = prosperityDifference.ToString("0.00");
        }

        else
        {
            txtAfterActionOostIncomeYearly.text = incomeDifference.ToString("0.00");
            txtAfterActionOostHappinessYearly.text = happinessDifference.ToString("0.00");
            txtAfterActionOostEcoAwarenessYearly.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionOostPollutionYearly.text = pollutionDifference.ToString("0.00");
            txtAfterActionOostProsperityYearly.text = prosperityDifference.ToString("0.00");

        }
    }

    private void setDifferenceTextValuesZuid(double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference, bool isMonthly)
    {
        if (isMonthly)
        {
            txtAfterActionZuidIncome.text = incomeDifference.ToString("0.00");
            txtAfterActionZuidHappiness.text = happinessDifference.ToString("0.00");
            txtAfterActionZuidEcoAwareness.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionZuidPollution.text = pollutionDifference.ToString("0.00");
            txtAfterActionZuidProsperity.text = prosperityDifference.ToString("0.00");
        }

        else
        {
            txtAfterActionZuidIncomeYearly.text = incomeDifference.ToString("0.00");
            txtAfterActionZuidHappinessYearly.text = happinessDifference.ToString("0.00");
            txtAfterActionZuidEcoAwarenessYearly.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionZuidPollutionYearly.text = pollutionDifference.ToString("0.00");
            txtAfterActionZuidProsperityYearly.text = prosperityDifference.ToString("0.00");
        }
    }

    private void setDifferenceTextValuesWest(double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference, bool isMonthly)
    {
        if (isMonthly)
        {
            txtAfterActionWestIncome.text = incomeDifference.ToString("0.00");
            txtAfterActionWestHappiness.text = happinessDifference.ToString("0.00");
            txtAfterActionWestEcoAwareness.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionWestPollution.text = pollutionDifference.ToString("0.00");
            txtAfterActionWestProsperity.text = prosperityDifference.ToString("0.00");
        }

        else
        {
            txtAfterActionWestIncomeYearly.text = incomeDifference.ToString("0.00");
            txtAfterActionWestHappinessYearly.text = happinessDifference.ToString("0.00");
            txtAfterActionWestEcoAwarenessYearly.text = ecoAwarenessDifference.ToString("0.00");
            txtAfterActionWestPollutionYearly.text = pollutionDifference.ToString("0.00");
            txtAfterActionWestProsperityYearly.text = prosperityDifference.ToString("0.00");
        }
    }
    #endregion

    #region Code for AfterActionCompleted
    public void initAFterActionCompleted()
    {
        updateTextAfterActionCompleted();
        showCompletedEvents();
        showCompletedActions();
    }

    private void updateTextAfterActionCompleted()
    {
        string[] txtTitle = { "Einde beurt rapport", "End of turn view" };
        string[] txtRight = { "Afgeronde acties", "Completed Actions" };
        string[] txtLeft = { "Afgeronde events", "Completed events" };

        txtAfterActionCompletedTitle.text = txtTitle[taal];
        txtAfterActionCompletedColumnLeft.text = txtLeft[taal];
        txtAfterActionCompletedColumnRight.text = txtRight[taal];
    }

    private void showCompletedEvents()
    {
        txtAfterActionCompletedColumnLeftDescription.text = "";

        for (int i = 0; i < game.monthlyReport.completedEvents.Length; i++)
        {
            foreach (GameEvent e in game.monthlyReport.completedEvents[i])
            {
                txtAfterActionCompletedColumnLeftDescription.text += e.publicEventName[taal] + " - " + e.description[taal];
                txtAfterActionCompletedColumnLeftDescription.text += getAfterActionConsequences(e.consequences[e.pickedChoiceNumber]);
                //txtAfterActionCompletedColumnLeftDescription.text += getChosenSectors(e.pickedSectors) + "\n\n";

                string[] sectorsPicked = { "Sectoren: ", "Sectors: " };
                txtAfterActionCompletedColumnLeftDescription.text += sectorsPicked[taal];
                foreach (string s in e.possibleSectors)
                {
                    txtAfterActionCompletedColumnLeftDescription.text += s + " ";
                }
                txtAfterActionCompletedColumnLeftDescription.text += "\n\n";
            }
        }
    }

    private void showCompletedActions()
    {
        txtAfterActionCompletedColumnRightDescription.text = "";
        //scrollbarAfterActionReport.gameObject.SetActive(false);
        int index = 0;

        for (int i = 0; i < game.monthlyReport.completedActions.Length; i++)
        {
            foreach (RegionAction a in game.monthlyReport.completedActions[i])
            {
                txtAfterActionCompletedColumnRightDescription.text += a.name[taal] + " - " + a.description[taal];
                txtAfterActionCompletedColumnRightDescription.text += getChosenSectors(a.pickedSectors);
                txtAfterActionCompletedColumnRightDescription.text += getAfterActionConsequences(a.consequences);

                string[] line = { "Geld beloning: ", "Money reward: " };
                txtAfterActionCompletedColumnRightDescription.text += line[taal] + a.actionMoneyReward + "\n\n";
                index++;
            }
        }

        //if (index > 2)
        //    scrollbarAfterActionReport.gameObject.SetActive(true);
    }

    private string getChosenSectors(bool[] sectors)
    {
        string[] sectorsPicked = {"\nSectoren: ", "\nSectors: "};


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

    private string getAfterActionConsequences(SectorStatistics s)
    {
        string[] consequences = { "\nConsequenties: ", "\nConsequences: " };
        if (s.income != 0)
        {
            string[] a = { "\nInkomen: " + s.income + "\n", "\nIncome: " + s.income + "\n" };
            consequences[taal] += a[taal];
        }
        if (s.happiness != 0)
        {
            string[] c = { "Tevredenheid: " + s.happiness + "\n", "Happiness: " + s.happiness + "\n" };
            consequences[taal] += c[taal];
        }
        if (s.ecoAwareness != 0)
        {
            string[] d = { "Milieubewustheid: " + s.ecoAwareness + "\n", "Eco awareness: " + s.ecoAwareness + "\n" };
            consequences[taal] += d[taal];
        }
        if (s.prosperity != 0)
        {
            string[] e = { "Welvaart: " + s.prosperity + "\n", "Prosperity: " + s.prosperity + "\n" };
            consequences[taal] += e[taal];
        }
        if (s.pollution.airPollutionIncrease != 0)
        {
            string[] f = { "Luchtvervuiling: " + s.pollution.airPollutionIncrease + "\n", "Air pollution: " + s.pollution.airPollutionIncrease + "\n" };
            consequences[taal] += f[taal];
        }
        if (s.pollution.waterPollutionIncrease != 0)
        {
            string[] g = { "Watervervuiling: " + s.pollution.waterPollutionIncrease + "\n", "Water pollution: " + s.pollution.waterPollutionIncrease + "\n" };
            consequences[taal] += g[taal];
        }
        if (s.pollution.naturePollutionIncrease != 0)
        {
            string[] h = { "Natuurvervuiling: " + s.pollution.naturePollutionIncrease + "\n", "Nature pollution: " + s.pollution.naturePollutionIncrease + "\n" };
            consequences[taal] += h[taal];
        }

        return consequences[taal];
    }
    #endregion

    #region Code for activating popups
    public void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive && tutorialStep8)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
    }

    public void btnMenuClick()
    {
        if (!canvasMenuPopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
        }
    }

    public void btnMonthlyReportClick()
    {
        if (!canvasMonthlyReport.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasMonthlyReport.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateTextAfterActionStats(true);
        }
    }

    public void btnYearlyReportClick()
    {
        if (!canvasYearlyReport.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasYearlyReport.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            updateTextAfterActionStats(false);
        }
    }

    public void btnAfterActionCompletedClick()
    {
        if (!canvasAfterActionCompletedPopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasAfterActionCompletedPopup.gameObject.SetActive(true);
            popupActive = true;
            updateTextAfterActionCompleted();
        }
    }

    private void initButtonText()
    {
        string[] resume = { "Verder spelen", "Resume" };
        string[] save = { "Opslaan", "Save" };
        string[] exitgame = { "Verlaat spel", "Exit Game" };
        string[] exitmenu = { "Naar hoofdmenu", "Exit to menu" };

        txtResume.text = resume[taal];
        txtSave.text = save[taal];
        txtExitGame.text = exitgame[taal];
        txtExitMenu.text = exitmenu[taal];
    }
    #endregion

    #region Language Change Code
    public void btnNLClick()
    {
        if (taal != 0)
        {
            Debug.Log("BTN NL");
            game.ChangeLanguage("dutch");
            taal = game.language;
            btnNextTurnText.text = "Volgende beurt";
            txtBtnTimeline.text = "Tijdlijn";
            txtBtnMenu.text = "Menu";
            initButtonText();
        }
    }

    public void btnENGClick()
    {
        if (taal != 1)
        {
            Debug.Log("BTN ENG");
            game.ChangeLanguage("english");
            taal = game.language;
            btnNextTurnText.text = "Next turn";
            txtBtnTimeline.text = "Timeline";
            txtBtnMenu.text = "Menu";
            initButtonText();
        }
    }
    #endregion

    #region Mouse Enter & Exit Code for Icons
    // OnEnter BtnMoney
    public void BtnMoneyEnter()
    {
        btnMoneyHoverCheck = true;
        tooltipActive = true;
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
    #endregion

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


    public GUIStyle returnTooltipStyle()
    {
        return tooltipStyle;
    }

    public void enterEventHover()
    {
        Debug.Log("Event Hover ENTER!");
    }

    public void enterExitHover()
    {
        Debug.Log("Event Hover EXIT!");
    }

    public void buttonExitGameOnClick()
    {
        Application.Quit();
    }

    public void loadOtherScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void nextTurnOnClick()
    {
        if (tutorialStep9)
        {
            EventManager.CallChangeMonth();

            if (!tutorialNextTurnDone)
                tutorialNextTurnDone = true;

        }
    }

    public void valueChangedHouseholds()
    {
        if (!checkboxHouseholds)
        {
            checkboxHouseholds = true;
            regioActionCost += regioAction.actionMoneyCost;
        }
        else
        {
            checkboxHouseholds = false;
            regioActionCost -= regioAction.actionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }

    public void valueChangedAgriculture()
    {
        if (!checkboxAgriculture)
        {
            checkboxAgriculture = true;
            regioActionCost += regioAction.actionMoneyCost;
        }
        else
        {
            checkboxAgriculture = false;
            regioActionCost -= regioAction.actionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }

    public void valueChangedCompanies()
    {
        if (!checkboxCompanies)
        {
            checkboxCompanies = true;
            regioActionCost += regioAction.actionMoneyCost;
        }
        else
        {
            checkboxCompanies = false;
            regioActionCost -= regioAction.actionMoneyCost;
        }

        if (game.gameStatistics.money > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;
        
        txtRegionActionSectorTotalCost.text = regioActionCost.ToString();
    }

    public void btnResumeMenu()
    {
        canvasMenuPopup.gameObject.SetActive(false);
        popupActive = false;
        EventManager.CallPopupIsDisabled();
    }

    #region Code for controlling Tutorial buttons presses
    public void turorialButtonPress()
    {
        if (tutorialIndex == 1)
        {
            tutorialStep2 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 2)
        {
            tutorialStep3 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 3)
        {
            tutorialStep4 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 4)
        {
            tutorialStep5 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 7)
        {
            tutorialStep8 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 9)
        {
            tutorialStep10 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 10)
        {
            tutorialstep11 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 11)
        {
            tutorialstep12 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 12)
        {
            tutorialStep13 = true;
            tutorialIndex++;
        }
    }

    public void tutorialRegionButtonPress()
    {
        if (tutorialIndex == 5)
        {
            tutorialStep6 = true;
            tutorialIndex++;
        }
        else if (tutorialIndex == 6)
        {
            tutorialStep7 = true;
            tutorialIndex++;
        }
    }

    public void tutorialOrganizationButtonPress()
    {
        if (tutorialIndex == 8)
        {
            tutorialStep9 = true;
            tutorialIndex++;
        }
    }
    #endregion

}
 
