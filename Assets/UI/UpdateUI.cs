using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour
{
    public Player playerController;

    // Hexcodes van kleuren
    // Green:       #00CC00.
    // Red:         #FF0000.

    // Variabelen die in deze class en Inspector worden gebruikt
    #region UI Elements
    // Multiplayer
    List<string> lstMessages = new List<string>();
    List<string[]> lstText = new List<string[]>();
    private PhotonPlayer[] players = new PhotonPlayer[2];
    public Text txtMultiplayerInfo;
    public Text txtMultiplayerLocalPlayer;
    public Text txtMultiplayerRemotePlayer;
    public Text txtMultiplayerRemotePlayerMoney;
    public Button btnMultiplayerRemoteMoney;
    private ChattingClient chatClient;
    public Button btnSendChatMessage;
    public Text txtChatMessages;
    public InputField inputChatMessage;
    public Text txtSendMessageButton;
    private string txtChatMessageToSend;
    public RawImage imgTextMessages;
    //public Panel panelTextMessages;
    public Text txtActivityLog;
    private RectTransform rectChatMessages;
    public Text txtReadyForNextTurn;
    public RawImage imgReadyForNextTurn;

    // Tooltip texture and GUI
    public Texture2D tooltipTexture;
    private GUIStyle tooltipStyle = new GUIStyle();
    public Texture2D buttonTexture;
    private GUIStyle buttonStyle = new GUIStyle();
    BuildingObjectController buildingObjectController;

    MapRegion regio;
    RegionAction regioAction;
    Card card;
    double regioActionCost;
    Game game;
    public Scrollbar scrollbarAfterActionReport;

    private List<GameEvent>[] monthlyNewEvents;
    private List<GameEvent>[] monthlyCompletedEvents;
    private List<RegionAction>[] monthlyCompletedActions;

    public Text test;
    public Text colorTxtTest;

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
    public Text txtNotYourBuilding;
    public Building activeBuilding;
    public MapRegion buildingRegion;
    public Text txtBuildingsTitle;
    public Text txtBuildingsColumn;
    public Text txtBuildingsStats;
    public Button btnDeleteBuilding;
    public Text txtBtnDeleteBuilding;

    // Empty Building Popup
    public Text txtNotYourBuildingDestory;
    public Text txtBuildingAlreadyActive;
    public Text txtNotYourEmptyBuilding;
    public Text txtEmptyBuildingsTitle;
    public Text txtEmptyBuildingsColumnLeft;
    public Text txtEmptyBuildingColumRight;
    public Button btnUseBuilding;
    public Button btnEmptyBuildingPosition;
    public Text btnUseBuildingTxt;
    public Text txtEmptyBuildingInfo;
    public Text txtEmptyBuildingStats;
    public Building buildingToBeBuild;
    public MapRegion regionToBeBuild;
    public Text txtNewBuildingsColumn;
    public Text txtNewBuildingsStats;
    public Button btnNewDeleteBuilding;
    public Text txtNewBtnDeleteBuilding;

    // Timeline Popup
    public Text txtTimelineTitle;
    public Text txtTimelineColumnLeft;
    public Text txtTimelineColumnRight;
    public Dropdown dropdownTimeline;
    public Text txtColumnLeftInfo;
    public Text txtColumnRightInfo;
    private string dropdownTimelinePick;

    // Text Event Popup
    public Text txtNotYourEvent;
    public Text txtEventTitle;
    public Text txtEventColumRight;
    public Text txtEventConsequencesChoice;
    public Button btnViewConsequencesEvent;
    public Text txtBtnViewConsequencesEvent;
    public Image imgEventConsequences;
    public Text txtEventConsequences;
    public Text txtEventConsequencesTitle;
    public GameEvent gameEvent;
    public MapRegion regionEvent;
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

    // Text Event Choice made
    public Text txtEventChoiceMadeTitle;
    public Text txtEventChoiceMadeInfo;
    public Text txtEventChoiceMadeHeader;

    // Text Quests Popup
    public Text txtQuestsTitle;
    public Text txtQuestsDescription;
    public Text txtQuestsActive;

    // Text End of Game Report
    public Text txtEndOfGameTitle;
    public Text txtEndOfYearInfo;
    public Text txtEndOfYearBtn;
    public Text txtShareFacebookButton;

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
    public Text txtAdviserHappiness;

    private int taal;
    //  double totalOrgBank;

    // Text Region Menu
    public Text txtRegionInfo;
    public Text txtNotYourRegion;
    public Text txtRegionAvailableMoney;
    public Text txtRegionAvailableMoneyDescription;
    public Text txtbtnAverageTab;
    public Text txtbtnHouseholdsTab;
    public Text txtbtnAgriculureTab;
    public Text txtbtnCompaniesTab;
    public Button btnAverageTab;
    public Button btnHouseholdsTab;
    public Button btnAgriculureTab;
    public Button btnCompaniesTab;
    public Button btnHistoryTab;
    public Button btnActionsTab;
    public RawImage imgIncomeRegion;
    public RawImage imgHappinessRegion;
    public RawImage imgEcoAwarenessRegion;
    public RawImage imgPollutionRegion;
    public RawImage imgPollutionWaterRegion;
    public RawImage imgPollutionAirRegion;
    public RawImage imgPollutionNatureRegion;
    public RawImage imgPollutionWaterIncreaseRegion;
    public RawImage imgPollutionAirIncreaseRegion;
    public RawImage imgPollutionNatureIncreaseRegion;
    public RawImage imgProsperityRegion;
    public Text txtRegionActionConsequences;
    public RawImage imgDropdownLine;
    public RawImage imgActions;
    public RawImage imgHistory;
    public Button btnViewConsequences;
    public Text txtBtnViewConsequences;
    public Text txtBtnTabHistory;
    public Text txtBtnTabActions;
    public Image imgSectorPopup;
    public Text txtRegionSectorTitle;
    public Text txtRegionSectorInfo;
    public Text txtRegionName;
    public Text txtRegionMoney;
    public Text txtRegionHappiness;
    public Text txtRegionAwareness;
    public Text txtRegionProsperity;
    public Text txtRegionPollution;
    public Text txtRegionPollutionIncrease;
    public Text txtRegionPollutionNature;
    public Text txtRegionPollutionNatureIncrease;
    public Text txtRegionPollutionWater;
    public Text txtRegionPollutionWaterIncrease;
    public Text txtRegionPollutionAir;
    public Text txtRegionPollutionAirIncrease;
    public Text txtRegionPollutionNatureIncreaseDescription;
    public Text txtRegionPollutionWaterIncreaseDescription;
    public Text txtRegionPollutionAirIncreaseDescription;
    public Text txtRegionTraffic;
    public Text txtRegionFarming;
    public Text txtRegionHouseholds;
    public Text txtRegionCompanies;
    public Text txtRegionActionName;
    public Text txtRegionActionDuration;
    public Text txtRegionActionCost;
    //public Text txtRegionActionConsequences;
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
    public Text txtRegionPollutionDescriptionIncrease;
    public Text txtRegionWaterDescriptionIncrease;
    public Text txtRegionAirDescriptionIncrease;
    public Text txtRegionNatureDescriptionIncrease;
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
    public Canvas canvasEndOfGame;
    public Canvas canvasEventChoiceMadePopup;

    // Tooltip Variables
    private string txtTooltip;
    private string txtTooltipCompany;
    private string txtTooltipAgriculture;
    private string txtTooltipHouseholds;
    private string dropdownChoice;

    // Tutorial
    private Vector3 imgPosMiddle;// = imgTutorialOverworld.gameObject.transform.position;     // Midden in het scherm
    private Vector3 imgPosRight;// = imgPosMiddle;
    private Vector3 imgPosLeft;
    public Image imgTutorialBig;
    public Text txtTutorialBig;
    public Text txtTutorialBigBtn;
    public Image imgTutorialSmall;
    public Text txtTutorialSmall;
    public Text txtTutorialSmallBtn;
    public Image imgTutorialStep2Highlight1;
    public Image imgTutorialStep2Highlight2;
    public Image imgTutorialStepOrgMenuHightlight;
    public Button btnTutorialBigNext;
    public Button btnTutorialSmallNext;

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
    // public Button btnTutorialOverworld;
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
    string[] availableMoney = { " geld", " money" };

    #endregion

    // Boolean variabele die in deze class worden gebruikt
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

    public bool btnOrganizationIsClicked;
    public bool btnQuestsIsClicked;
    public bool btnInvestmentsIsClicked;
    public bool btnCardsIsClicked;

    public bool organizationShakes = false;
    public bool questsShakes = false;
    public bool investmentsShakes = false;
    public bool cardsShakes = false;
    #endregion

    #region Start(), Update(), FixedUpdate()
    // Use this for initialization
    void Awake()
    {
        // Ingestelde taal wordt opgehaald
        taal = ApplicationModel.language;

        // De tekst van alle popups wordt geset aan de hand van de taal
        initButtons();
        initButtonText();
        initCanvas();
        initOrganizationText();
        initRegionText();
        initInvestementsText();
        initCardsText();
        

        // GUI Styles
        tooltipStyle.normal.background = tooltipTexture;

        // GUIStyle for buttons 
        // Set the Texture
        buttonStyle.normal.background = buttonTexture;

        // Set the text in the middle of the button    
        buttonStyle.alignment = TextAnchor.MiddleCenter;

        // Get the color out of the hexadecimal string     
        Color c = new Color();
        ColorUtility.TryParseHtmlString("#ffffff", out c);

        // Set the color of the text to above color     
        buttonStyle.normal.textColor = c;                       

        // Kijk of de tutorial aan staat en begin de tutorial wel of niet
        if (game.tutorial.tutorialActive)
            initTutorialActive();
        else
            initTutorialNotActive();

        // Tekst van next turn button wordt geset
        btnNextTurnText.text = nextTurnText[taal];
        buildingObjectController = GetComponent<BuildingObjectController>();

        if (ApplicationModel.multiplayer)
        {
            // Als het een mulitplayer potje is wordt de Multiplayer UI aan gezet
            txtMultiplayerLocalPlayer.gameObject.SetActive(false);
            txtMultiplayerRemotePlayer.gameObject.SetActive(true);
            txtMultiplayerInfo.gameObject.SetActive(true);
            btnMultiplayerRemoteMoney.gameObject.SetActive(true);
            txtMultiplayerRemotePlayerMoney.gameObject.SetActive(true);

            string[] txtInfo = { "Activiteit andere speler: ", "Activity other player: " };
            txtMultiplayerInfo.text = txtInfo[taal];

            btnSendChatMessage.gameObject.SetActive(false);
            txtChatMessages.gameObject.SetActive(true);
            inputChatMessage.gameObject.SetActive(true);
            txtActivityLog.gameObject.SetActive(true);
            txtSendMessageButton.gameObject.SetActive(true);
            imgTextMessages.gameObject.SetActive(true);

            imgReadyForNextTurn.gameObject.SetActive(false);
            txtReadyForNextTurn.gameObject.SetActive(false);

            txtActivityLog.text = "";
            txtChatMessages.text = "";
        }
        else
        {
            // Als het geen multiplayer potje is wordt de Multiplayer UI op inactive gezet
            txtMultiplayerLocalPlayer.gameObject.SetActive(false);
            txtMultiplayerRemotePlayer.gameObject.SetActive(false);
            txtMultiplayerInfo.gameObject.SetActive(false);
            btnMultiplayerRemoteMoney.gameObject.SetActive(false);
            txtMultiplayerRemotePlayerMoney.gameObject.SetActive(false);

            btnSendChatMessage.gameObject.SetActive(false);
            txtChatMessages.gameObject.SetActive(false);
            inputChatMessage.gameObject.SetActive(false);
            txtSendMessageButton.gameObject.SetActive(false);
            txtActivityLog.gameObject.SetActive(false);
            imgTextMessages.gameObject.SetActive(false);

            imgReadyForNextTurn.gameObject.SetActive(false);
            txtReadyForNextTurn.gameObject.SetActive(false);
        }
        
    }

    void Start()
    {
        // De status van wat je aan het doen bent begint met Naar Nederland Kijken en wordt gestuurd naar andere speler
        if (ApplicationModel.multiplayer)
            playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Kaart van Nederland aan het bekijken", "Looking at the map of The Netherlands");
    }

    void Update()
    {
        // Als het een multiplayer potje is
        if (ApplicationModel.multiplayer)
        {
            int playerPosition = game.GetPlayerListPosition();

            // Het geld van de andere speler wordt geupdate
            if (playerPosition == 0)
                txtMultiplayerRemotePlayerMoney.text = PhotonNetwork.playerList[0].NickName + ": " + game.gameStatistics.playerMoney[1].ToString("0");
            else
                txtMultiplayerRemotePlayerMoney.text = PhotonNetwork.playerList[0].NickName + ": " + game.gameStatistics.playerMoney[0].ToString("0");

            // Als er op ENTER wordt gedrukt (en de speler een chatbericht wil sturen)
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log(txtChatMessageToSend);
                // Controle of het chatbericht niet leeg is
                if (txtChatMessageToSend != null)
                {
                    // Chat bericht wordt gestuurd naar andere speler
                    playerController.photonView.RPC("MessageReceived", PhotonTargets.Others, txtChatMessageToSend, PhotonNetwork.player.NickName);

                    // Chat bericht wordt ook lokaal getoont
                    updateChatMessages(txtChatMessageToSend, PhotonNetwork.player.NickName);

                    // Na het sturen wordt inputfield tekst en chatbericht weer op null/empty gezet 
                    txtChatMessageToSend = null;
                    inputChatMessage.text = "";
                }
            }
        }

        // Popups sluiten met ESC toets
        if (game.tutorial.tutorialIndex > 2)
            popupController();

        // Als de regio popup actief is
        if (canvasRegioPopup.gameObject.activeSelf && dropdownChoiceMade)
        {
            if (checkboxAgriculture || checkboxCompanies || checkboxHouseholds)
            {
                // Als een van de checkboxes bij het kiezen van een actie op true staat
                btnDoActionRegionMenu.gameObject.SetActive(true);
                txtRegionActionNoMoney.gameObject.SetActive(false);
                txtRegionActionNoMoney.text = "";
                txtRegionActionConsequences.text = getSectorStatisticsConsequences(regioAction.afterInvestmentConsequences);
            }
            else
            {
                // ALs geen van de checkboxes op true staat
                txtRegionActionNoMoney.gameObject.SetActive(false);
                string[] error = { "Je moet een sector kiezen", "You have to chose a sector" };
                txtRegionActionNoMoney.text = error[taal];
                btnDoActionRegionMenu.gameObject.SetActive(false);
                txtRegionActionConsequences.text = "";
            }
        }
    }
    #endregion

    #region Tutorial Main Steps
    // Initialize van tutorial UI als tutorial active is
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

    // Alle tutorial booleans wordt geset voor als de tutorial niet active is
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
        //btnTutorialOverworld.gameObject.SetActive(false);
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

        // Groter worden effect en shake effect voor de organization button
        StartCoroutine(ChangeScale(btnOrganization));
        if (!organizationShakes)
            StartCoroutine(ShakeOrganization());
    }

    // Method waarin de tekst van de Tutorial contstant wordt geupdate
    IEnumerator initTutorialText()
    {
        imgPosMiddle = imgTutorialOverworld.gameObject.transform.position;    
        imgPosRight = imgPosMiddle;
        imgPosLeft = imgPosMiddle;

        // Positie van de tutorial image > rechts in het scherm
        imgPosRight.x = imgPosRight.x + Screen.width / 3;

        // Positie van de tutorial image > links in het scherm
        imgPosLeft.x = imgPosLeft.x - Screen.width / 3;                            

        btnOrganization.gameObject.SetActive(false);
        btnNextTurn.gameObject.SetActive(false);
        btnInvestments.gameObject.SetActive(false);
        btnCards.gameObject.SetActive(false);
        imgBarBottom.gameObject.SetActive(false);

        // Tekst voor stap 1 van de tutorial
        game.tutorial.doTuto = true;
        string[] step1 = { "Welkom! De overheid heeft jouw organisatie de opdracht gegeven om ervoor te zorgen " +
                "dat Nederland een milieubewust land wordt. De inwoners moeten begrijpen dat een groen land belangrijk is."
                , "Welcome! The government has given your organisation the task to make The Netherlands " +
                "a country which is aware of the environment. The inhabitants need to understand the importance of a green country."};
        string[] btnText = { "Verder", "Next" };

        imgTutorialBig.gameObject.SetActive(false);
        txtTutorialSmall.text = step1[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        btnNextTurn.interactable = false;
        imgTutorialSmall.transform.position = imgPosRight;

        // Zolang er niet op de verder button wordt gedrukt blijft dit doorgaan
        while (game.tutorial.tutorialIndex < 1)
            yield return null;

        // Tekst voor stap 2 van de tutorial
        string[] step2 = { "Jouw taak is om de vervuiling naar 0% te brengen voor 2050. Het is nu 2020 dus je hebt nog 30 jaar.",
            "Your task is to reduce the pollution to 0% before 2050. It is now 2020 so you still have 30 years."};
        txtTutorialSmall.text = step2[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        imgTutorialStep2Highlight1.enabled = true;
        imgTutorialStep2Highlight2.enabled = true;

        // Zolang er niet op de verder button wordt gedrukt blijft dit doorgaan
        while (game.tutorial.tutorialIndex < 2)
            yield return null;

        // Tekst voor stap 3 van de tutorial
        imgTutorialBig.transform.position = imgPosMiddle;
        string[] step3 = { "Bovenin het scherm staan jouw resources om de vervuiling te verlagen. Welvaart, " +
                "milieubewustheid, tevredenheid en vervuiling zijn landelijke gemiddelden." +
                "\n\nGeld: beslissingen maken kost geld. Geld wordt per maand verhoogd door het inkomen." +
                "\nVervuiling: de vervuiling in het land, neemt per maand toe of af." +
                "\nMilieubewustheid: verlaagt de maandelijkse vervuiling." +
                "\nWelvaart: verhoogt het inkomen." +
                "\nTevredenheid: beïnvloedt consequenties van beslissingen. Boven 50% is positief, onder 50% is negatief.",
                "At the top of the screen are your recourses to reduce pollution. Prosperity, eco awareness, " +
                "happiness and pollution are nationwide averages." +
                "\n\nMoney: decisions cost money.Money increases monthly from income." +
                "\nPollution: the pollution in the country, increases or decreases monthly." +
                "\nEco Awareness: reduces monthly pollution." +
                "\nProsperity: increases income." +
                "\nHappiness: influences consequences of decisions. Above 50% is positive, below 50% is negative." };
        imgTutorialSmall.gameObject.SetActive(false);
        imgTutorialBig.gameObject.SetActive(true);
        txtTutorialBig.text = step3[taal];
        txtTutorialBigBtn.text = btnText[taal];
        imgTutorialStep2Highlight1.gameObject.SetActive(false);
        imgTutorialStep2Highlight2.gameObject.SetActive(false);

        // Zolang er niet op de verder button wordt gedrukt blijft dit doorgaan
        while (game.tutorial.tutorialIndex < 3)
            yield return null;

        // Tekst voor stap 4 van de tutorial
        imgTutorialSmall.gameObject.transform.position = imgPosRight;
        game.tutorial.tutorialOnlyWestNL = true;
        game.tutorial.tutorialRegionsClickable = true;
        string[] step4 = { "Er zijn 4 regio’s: Noord, Oost, West, en Zuid. Elke regio heeft een inkomen, " +
                "welvaart, vervuiling, milieubewustheid en tevredenheid." +
                "\n\nGa nu naar West Nederland door op de regio te klikken",
                "There are 4 regions: North, East, West and South. Each region has an income, prosperity, " +
                "pollution, eco-awareness and happiness." +
                "\n\nNow go to The Netherlands West by clicking on the region." };
        imgTutorialBig.gameObject.SetActive(false);
        imgTutorialSmall.gameObject.SetActive(true);
        txtTutorialSmall.text = step4[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        btnTutorialSmallNext.gameObject.SetActive(false);

        // Controle of de tutorial voor de regio als af is (dit is voor als de gebruiker een save game gebruikt)
        if (!game.tutorial.tutorialRegionDone)
        {
            // Zolang de popup voor regio niet actief is blijft dit doorgaan
            while (!canvasRegioPopup.gameObject.activeSelf)
                yield return null;

            canvasTutorial.gameObject.SetActive(false);

            // Zolang de tutorial voor de regio niet klaar is blijft dit doorgaan
            while (!game.tutorial.tutorialCheckActionDone)
                yield return null;

            // Zolang de popup voor regio actief is blijft dit doorgaan
            while (canvasRegioPopup.gameObject.activeSelf)
                yield return null;
        }
        else
            canvasTutorial.gameObject.SetActive(false);

        // Tekst voor stap 5 van de Tutorial
        game.tutorial.tutorialOnlyWestNL = false;
        canvasTutorial.gameObject.SetActive(true);
        imgBarBottom.gameObject.SetActive(true);
        string[] step5 = { "Onderin het scherm kun je naar het Organisatie menu gaan door op de knop te drukken. \n\nDruk nu op de Organisatie knop.",
            "At the bottom of your screen you can go to the Organization menu by pressing the button.\n\nPress the Organization Button " };
        txtTutorialSmall.text = step5[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        imgTutorialStepOrgMenuHightlight.enabled = true;
        btnOrganization.interactable = true;
        game.tutorial.tutorialOrganizationActive = true;
        imgTutorialSmall.transform.position = imgPosMiddle;
        btnOrganization.gameObject.SetActive(true);
        if (!organizationShakes)
            StartCoroutine(ShakeOrganization());

        // Controle of de tutorial voor de regio als af is (dit is voor als de gebruiker een save game gebruikt)
        if (!game.tutorial.tutorialOrganizationDone)
        {
            // Zolang de Organisatie popup niet actief is blijft dit doorgaan
            while (!canvasOrganizationPopup.gameObject.activeSelf)
                yield return null;

            imgTutorialStepOrgMenuHightlight.gameObject.SetActive(false);
            canvasTutorial.gameObject.SetActive(false);

            // Zolang de tutorial van de organization niet klaar is blijft dit doorgaan
            while (!game.tutorial.tutorialOrganizationDone)
                yield return null;

            // Zolang de popup van de organisatie actief is blijft dit doorgaan
            while (canvasOrganizationPopup.gameObject.activeSelf)
                yield return null;
        }
        else
        {
            imgTutorialStepOrgMenuHightlight.gameObject.SetActive(false);
            canvasTutorial.gameObject.SetActive(false);
        }

        // Tekst voor stap 6 van de tutorial
        btnNextTurn.interactable = true;
        canvasTutorial.gameObject.SetActive(true);
        btnNextTurn.gameObject.SetActive(true);
        imgTutorialSmall.transform.position = imgPosRight;
        string[] step6 = { "Je kan nu ook de andere regio’s bezoeken om acties uit te voeren. Als je klaar bent " +
                "dan kan je naar de volgende maand gaan door op “volgende maand” rechtsonderin het scherm te drukken.",
            "You can now visit the other regions to execute actions. When you’re finished you can go to the next month " +
            "by pressing on the “next month” button at the bottom right of the screen." };
        txtTutorialSmall.text = step6[taal];
        txtTutorialSmallBtn.text = btnText[taal];

        // Je kunt niet next turn doen
        game.tutorial.tutorialNexTurnPossibe = true;

        // Controle of de tutorial voor de next turn al af is (dit is voor als de gebruiker een save game gebruikt)
        if (!game.tutorial.tutorialNextTurnDone)
        {
            while (!game.tutorial.tutorialNextTurnDone)
                yield return null;
        }

        game.tutorial.tutorialNexTurnPossibe = false;
        btnNextTurn.interactable = false;

        // Tekst voor tutorial stap 7
        game.tutorial.tutorialeventsClickable = false;
        canvasTutorial.gameObject.SetActive(true);
        string[] step9 = { "Je kunt linksonder in je scherm ook een knop zien. Deze knop geeft een overzicht van de veranderingen tussen de huidige en de vorige maand. Je krijgt dit rapport elke maand.  " +
                "\n\n Druk op de knop om het maandelijkse overzicht te tonen."
                , "In the bottom left of your screen you can see a button. This button shows the changes between the current and the previous month. You will get this report every month. " +
                "\n\nClick on the button to view your monthly report."};
        txtTutorialSmall.text = step9[taal];
        imgHighlightMonthlyReport.enabled = true;
        btnMonthlyReportStats.interactable = true;
        game.tutorial.tutorialMonthlyReportActive = true;
        imgTutorialSmall.transform.position = imgPosLeft;

        // Controle of de tutorial voor de monthly report al af is (dit is voor als de gebruiker een save game gebruikt)
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

        // Tekst voor tutorial stap 8
        canvasTutorial.gameObject.SetActive(true);
        string[] step7 = { "er is een event bezig. Er kunnen elke nieuwe turn enkele events ontstaan. Er kan maar 1 event " +
                "tegelijk in een regio zijn. \nEr kunnen wel meerdere events tegelijk zijn in meerdere regio's. \nVoor elk event heb je 3 beurten om te beslissen wat je met de event gaat doen. "
                , "There is an active event running at this moment. Each turn there will be new events. There can only be one event in a region at the same time. " +
                "\nThere can be multiple active events in the whole country. \nFor each event you have 3 turns to decide what you are going to do. "  };
        txtTutorialSmall.text = step7[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        btnTutorialSmallNext.gameObject.SetActive(true);

        popupActive = true;
        while (game.tutorial.tutorialIndex < 7)
            yield return null;

        popupActive = false;

        // Tekst voor tutorial stap 9
        btnTutorialSmallNext.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        string[] step8 = { "Door op het icoon van de event te klikken krijg je een pop-up. In deze pop-up kun je kiezen welke actie je bij dit event wil nemen. " +
                "\n\nKlik nu op het icoontje van de event. "
                , "By clicking on the icon of the event you get a popup. In this popup you can chose which action you want to take with this event.\n\nClick on the icon of the event to open the popup."};
        txtTutorialSmall.text = step8[taal];
        txtTutorialSmallBtn.text = btnText[taal];
        game.tutorial.tutorialEventsActive = true;

        // Controle of de tutorial voor de events al af is (dit is voor als de gebruiker een save game gebruikt)
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

        imgTutorialSmall.transform.position = imgPosMiddle;
        game.tutorial.tutorialNexTurnPossibe = true;
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

        // Bepaalde buttons worden pas zichtbaar na een paar jaar
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
        // Alle booleans worden geset
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
        // Alle canvassen worden geset en op niet active gezet 
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

        canvasEventChoiceMadePopup.GetComponent<Canvas>();
        canvasEventChoiceMadePopup.gameObject.SetActive(false);

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

        canvasEndOfGame.GetComponent<Canvas>();
        canvasEndOfGame.gameObject.SetActive(false);

        if (game.tutorial.tutorialActive)
        {
            canvasTutorial.GetComponent<Canvas>();
            canvasTutorial.gameObject.SetActive(true);
        }
    }

    // De methode die de Game class linkt aan UpdateUI
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
        // Als het een multiplayer game is zijn alle buttons meteen beschikbaar
        if (!ApplicationModel.multiplayer)
        {
            // Als het geen multiplayer game is blijft hij hier totdat het de juiste jaar/maand is
            while (game.currentMonth < 6 && game.currentYear < 2)
                yield return null;
        }

        // Zet Button op true en start shake effect
        btnQuests.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnQuests));
        if (!questsShakes)
            StartCoroutine(ShakeQuests());

        // Controle of de tutorial voor de quests als af is (dit is voor als de gebruiker een save game gebruikt)
        if (!game.tutorial.tutorialQuestsDone)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgHighlightQuests.enabled = true;
            imgTutorialSmall.gameObject.SetActive(true);

            game.tutorial.tutorialQuestsActive = true;
            game.tutorial.tutorialeventsClickable = false;
            game.tutorial.tutorialNexTurnPossibe = false;

            // Tekst voor het Quests gedeelte van de tutorial
            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de organisatie menu knop gekomen. Dit is de knop voor je missies. \n\nOpen het missies menu door op de missies knop te drukken. ",
            "You can see that an extra button just appeared next to the organization menu button. This is the button for your quests. \n\nOpen the quests menu by pressing the quests button." };
            string[] btnText = { "Verder", "Next" };
            txtTutorialSmall.text = step1[taal];
            btnTutorialSmallNext.gameObject.SetActive(false);

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
        // Als het een multiplayer game is zijn alle buttons meteen beschikbaar
        if (!ApplicationModel.multiplayer)
        {
            // Als het geen multiplayer game is blijft hij hier totdat het de juiste jaar/maand is
            while (game.currentYear < 6)
                yield return null;
        }

        // Zet de button op active en start shake effect
        btnInvestments.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnInvestments));
        if (!investmentsShakes)
            StartCoroutine(ShakeInvestments());

        // Controle of de tutorial voor de investements als af is (dit is voor als de gebruiker een save game gebruikt)
        if (!game.tutorial.tutorialInvestementsDone)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgTutorialSmall.gameObject.SetActive(true);
            imgHighlightInvestements.enabled = true;

            game.tutorial.tutorialInvestementsActive = true;
            game.tutorial.tutorialNexTurnPossibe = false;

            // Tekst voor het Investements gedeelte van de tutorial
            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de kaarten menu knop gekomen. Dit is de knop voor investeren. \n\nOpen het investeer menu door op de investeer knop te drukken.",
            "You can see that an extra button just appeared next to the cards menu button. This is the button for your investments. \n\nOpen the investments menu by pressing the investments button." };
            string[] btnText = { "Verder", "Next" };
            txtTutorialSmall.text = step1[taal];
            btnTutorialSmallNext.gameObject.SetActive(false);

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
        // Als het een multiplayer game is zijn alle buttons meteen beschikbaar
        if (!ApplicationModel.multiplayer)
        {
            // Als het geen multiplayer game is blijft hij hier totdat het de juiste jaar/maand is
            while (game.currentYear < 4)
                yield return null;
        }

        // Zet de button op active en start shake effect
        btnCards.gameObject.SetActive(true);
        StartCoroutine(ChangeScale(btnCards));
        if (!cardsShakes)
            StartCoroutine(ShakeCards());

        // Tekst voor het Cards gedeelte van de tutorial
        if (!game.tutorial.tutorialCardsDone)
        {
            btnNextTurn.interactable = false;
            canvasTutorial.gameObject.SetActive(true);
            imgTutorialSmall.gameObject.SetActive(true);
            imgHighlightCards.enabled = true;

            game.tutorial.tutorialCardsActive = true;
            game.tutorial.tutorialNexTurnPossibe = false;

            // Tekst voor het Cards gedeelte van de tutorial
            string[] step1 = { "Zoals je misschien hebt gezien is er een extra knop naast de missies menu knop gekomen. Dit is de knop voor je kaarten. \n\nOpen het kaarten menu door op de kaarten knop te drukken. ",
            "You can see that an extra button just appeared next to the quests menu button. This is the button for your cards. \n\nOpen the cards menu by pressing the cards button." };
            string[] btnText = { "Verder", "Next" };
            txtTutorialSmall.text = step1[taal];
            btnTutorialSmallNext.gameObject.SetActive(false);

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
        // Zet positie van de image naar links
        Vector3 imgPosMiddle = imgTutorialOverworld.gameObject.transform.position;      
        Vector3 imgPosLeft = imgPosMiddle;
        imgPosLeft.x = imgPosLeft.x - Screen.width / 3;                                

        btnNextTurn.interactable = false;
        canvasTutorial.gameObject.SetActive(true);
        imgTutorialSmall.transform.position = imgPosLeft;

        game.tutorial.tutorialBuildingsActive = true;
        game.tutorial.tutorialNexTurnPossibe = false;

        // Tekst voor het Building gedeelte van de tutorial
        string[] step1 = { "Er zijn 4 nieuwe icoontjes op de map verschenen! Dit komt omdat de overheid heeft jouw organisatie toestemming gegeven om 1 gebouw neer te zetten in elke regio. \\Click on one of those 4 icons.",
            "There just appeared 4 new icons on the map! This is because the government has given your organisation permission to place 1 building in each region. \n\nClick on one of those 4 icons." };
        txtTutorialSmall.text = step1[taal];
        btnTutorialSmallNext.gameObject.SetActive(false);
        game.tutorial.tutorialBuildingsClickable = true;

        if (!game.tutorial.tutorialBuildingsDone)
        {
            while (!canvasEmptyBuildingsPopup.gameObject.activeSelf)
                yield return null;
        }

        canvasTutorial.gameObject.SetActive(false);
        imgTutorialSmall.transform.position = imgPosMiddle;
    }

    // Het effect dat de button klein begint en steeds groter wordt als het voor het eerst op komt
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

    // Method die ht shake effect voor de Organisatie button maakt
    public IEnumerator ShakeOrganization()
    {
        organizationShakes = true;
        btnOrganizationIsClicked = false;
        Quaternion standardRotation = new Quaternion(0, 0, 0, 0);
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

    // Method die ht shake effect voor de Quests button maakt
    public IEnumerator ShakeQuests()
    {
        questsShakes = true;
        btnQuestsIsClicked = false;
        Quaternion standardRotation = new Quaternion(0, 0, 0, 0);
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

    // Method die ht shake effect voor de Cards button maakt
    public IEnumerator ShakeCards()
    {
        cardsShakes = true;
        btnCardsIsClicked = false;
        Quaternion standardRotation = new Quaternion(0, 0, 0, 0);
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

    // Method die ht shake effect voor de Investements button maakt
    public IEnumerator ShakeInvestments()
    {
        investmentsShakes = true;
        btnInvestmentsIsClicked = false;
        Quaternion standardRotation = new Quaternion(0, 0, 0, 0);
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
        // Het sluiten van een popup met ESC, hele hoop controlles omdat dit nog niet kan tijdens de tutorial
        // Close active popup with Escape / Open Menu popup with Escape if no popup is active
        if (Input.GetKeyUp(KeyCode.Escape) && !game.tutorial.tutorialRegionActive && !game.tutorial.tutorialEventsActive &&
            !game.tutorial.tutorialQuestsActive && !game.tutorial.tutorialOrganizationActive &&
            !game.tutorial.tutorialMonthlyReportActive && !game.tutorial.tutorialCardsActive && !game.tutorial.tutorialInvestementsActive
            && !game.tutorial.tutorialBuildingsActive)
            closeWithEscape();

        // Hotkeys die we eerst hadden
        /*
        // Open and close Organization popup with O
        else if (Input.GetKeyUp(KeyCode.O))
            //if (game.tutorial.tutorialStep8)
            controllerOrganizationHotkey();

        // Open and close Timeline popup with T
        else if (Input.GetKeyUp(KeyCode.T))
            if (!game.tutorial.tutorialActive)
                controllerTimelinePopup();
                */
    }

    // Close the active popup with the Escape key (and open main menu with escape if no popup is active)
    void closeWithEscape()
    {
        EventManager.CallPlayButtonClickSFX();
        // Als er geen popup actief is opent de ESC toest het menu
        if (!popupActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
            EventManager.CallPopupIsActive();
            initButtonText();
        }

        // Anders controleert het welke popup actief is en sluit het deze
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
            ClearActionMenu();
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
        else if (canvasEventChoiceMadePopup.gameObject.activeSelf)
        {
            canvasEventChoiceMadePopup.gameObject.SetActive(false);
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

        // Code die we hadden om tooltips te laten zien, wordt niet meer gebruikt
        if (checkTooltip() && !popupActive)// && game.tutorial.tutorialStep3)
        {
            lblReqt.x = v3Tooltip.x + 10; lblReqt.y = v3Tooltip.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        /*if (regionHouseholdsCheck && popupActive)// && game.tutorial.tutorialStep3)
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
        }*/

        // Als de popup voor cards actief is
        else if (canvasCardsPopup.gameObject.activeSelf)
        {
            float yOffset = 0f;

            // Get the X position of the button
            RectTransform rectBtnCardsPosition = btnCardsPosition.GetComponent<RectTransform>();
            Vector3 btnPos = btnCardsPosition.transform.position;

            float screenHeight = Screen.height;
            float x = btnPos.x;
            float y = btnPos.z + (screenHeight / 3);

            // Maak voor elke button in de list ownedCards een button aan
            foreach (Card c in game.inventory.ownedCards)
            {
                // Als je op de button drukt ga je naar de method setTextCardInformation()
                if (GUI.Button(new Rect(x, y + yOffset, rectBtnCardsPosition.rect.width + 50, rectBtnCardsPosition.rect.height), c.name[taal], buttonStyle))
                    setTextCardInformation(c);

                // Offset zodat de buttons oner elkaar komen te staan
                yOffset += 35;
            }
        }

        // Als de popup voor Buildings actief is
        else if (canvasEmptyBuildingsPopup.gameObject.activeSelf)
        {
            float yOffset = 0f;

            // Positie en grote van button
            RectTransform rectBtnBuildingsPosition = btnCardsPosition.GetComponent<RectTransform>();
            Vector3 btnPos = btnEmptyBuildingPosition.transform.position;
            float screenHeight = Screen.height;

            float x = btnPos.x;
            float y = btnPos.z + (screenHeight / 3);

            // Maak voor elke button in de list ownedCards een button aan
            foreach (Building b in regionToBeBuild.possibleBuildings)
            {
                // Als er al een building actief is hoeft hier geen button meer voor te zijn
                if (b != buildingRegion.activeBuilding)
                {
                    // Als je op de button drukt ga je naar de method setTextCardInformation()
                    if (GUI.Button(new Rect(x, y + yOffset, rectBtnBuildingsPosition.rect.width + 50, rectBtnBuildingsPosition.rect.height), b.buildingName[taal], buttonStyle))
                        setTextBuildingInformation(b);

                    // Offset zodat de buttons oner elkaar komen te staan
                    yOffset += 35;
                }
            }
        }
    }

    // Method die controleert of er over een statistiek gehovered wordt, als dit zo is wordt de positie waar de tooltip moet komen geset
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

        // Array met alle maanden
        string[,] arrMonths = new string[2, 12]
        {
            { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
            { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" }
        };

        // Set de tekst van de maand 
        txtDate.text = arrMonths[taal, month] + " - " + (year + 2019).ToString();
    }

    // Update Money based on value
    public void updateMoney(double money)
    {
        // Als het multiplayer is wordt niet alleen aantal geld maar ook naam van de player getoont
        if (ApplicationModel.multiplayer)
            txtMoney.text = PhotonNetwork.player.NickName + ": " + money.ToString("0");
        else
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

    // Change color of the button based on value
    void iconController(Button btn, Text txt, double value)
    {
        // Maak een colorblok aan voor de kleuren
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

        // Set de color van de tekst en icon
        cb.normalColor = lerpColor;
        cb.highlightedColor = lerpColor;
        cb.pressedColor = lerpColor;
        btn.colors = cb;
        txt.text = " " + value.ToString("0") + "%";
        txt.color = lerpColor;
    }
    #endregion

    #region Update UI in Tooltips
    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
    public void updateMoneyTooltip(double income)
    {
        string[] tip = { "Inkomen: " + income.ToString("0"),
            "Income: " + income.ToString("0") };
        txtTooltip = tip[taal];                 
    }

    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
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

    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
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

    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
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

    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
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

    // Methode waar de tekst voor de tooltip wordt gezet, txttooltip wordt dan gebruikt in de onGUI methode als tekst van de tooltip
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
    // Methode wordt aangeroepen als er op de button Organisatie wordt gedrukt
    public void updateOrganizationScreenUI()
    {
        // Haal het inkomen van elke regio op en zet de tekst value
        foreach (MapRegion region in game.regions)
        {
            if (region.name[0] == "Noord Nederland")
                txtOrgNoordMoney.text = (region.statistics.income * 12).ToString("0");
            else if (region.name[0] == "Oost Nederland")
                txtOrgOostMoney.text = (region.statistics.income * 12).ToString("0");
            else if (region.name[0] == "Zuid Nederland")
                txtOrgZuidMoney.text = (region.statistics.income * 12).ToString("0");
            else if (region.name[0] == "West Nederland")
                txtOrgWestMoney.text = (region.statistics.income * 12).ToString("0");
        }

        // Set de hoeveelheid geld van de bank
        txtOrgBank.text = game.GetMoney().ToString("0");

        imgTutorialOrganization.enabled = false;
        txtTutorialOrganization.enabled = false;
        btnTutorialOrganization.gameObject.SetActive(false);

        // Initialize de tekst die de advisers weer moeten geven
        initAdvisersText();

        // Als de tutorial actief is en dit stuk van dit tutorial nog niet gedaan is > start de tutorial
        if (game.tutorial.tutorialActive && game.tutorial.tutorialOrganizationActive)
        {
            imgTutorialOrganization.enabled = true;
            txtTutorialOrganization.enabled = true;
            btnTutorialOrganization.gameObject.SetActive(true);
            StartCoroutine(tutorialOrganizationPopup());
        }
    }

    IEnumerator tutorialOrganizationPopup()
    {
        // Tekst voor het Organization tutorial
        string[] step1 = { "Het organisatie menu toont het jaarlijks inkomen per regio. " +
                "Ook kan je hier zien wat je adviseurs van de huidige situatie vinden en wat ze zouden willen zien.",
            "The organisation menu shows the yearly income for each region. " +
            "You can also see here what your advisors are thinking about the current situation and what they would like to see."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialOrganization.text = step1[taal];
        txtTutorialOrganizationBtnText.text = btnText[taal];

        // Als er niet op ready is gedrukt 
        while (game.tutorial.tutorialIndex < 5)
            yield return null;

        imgTutorialOrganization.gameObject.SetActive(false);
        game.tutorial.tutorialOrganizationDone = true;
        game.tutorial.tutorialOrganizationActive = false;
    }

    // Set de tekst van de advisers
    private void initAdvisersText()
    {
        txtAdviserEconomic.text = game.economyAdvisor.name[taal] + "\n" + game.economyAdvisor.displayMessage[taal];
        txtAdviserPollution.text = game.pollutionAdvisor.name[taal] + "\n" + game.pollutionAdvisor.displayMessage[taal];
        txtAdviserHappiness.text = game.happinessAnalyst.name[taal] + "\n" + game.happinessAnalyst.displayMessage[taal];
    }

    // Set de overige tekst in deze popup
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
    }
    #endregion

    #region Code for the Region Popup
    // Method wordt aangeroepen als er op een van de regio wordt gedrukt
    public void regionClick(MapRegion region)
    {
        imgSectorPopup.gameObject.SetActive(false);

        // Als de tutorial actief is kan er alleen op West-Nederland gedrukt worden
        if (game.tutorial.tutorialActive && !popupActive)
        {
            // Je kunt alleen op de regio's klikken in bepaalde onderdelen van de tutorial
            if (game.tutorial.tutorialOnlyWestNL && game.tutorial.tutorialRegionsClickable)
            {
                // Als de regio waar je op hebt gedrukt West Nederland is
                if (region.name[0] == "West Nederland")
                {
                    // Start de regio popup met tutorial active
                    startRegionPopup(region);
                    game.tutorial.regionWestActivated = true;

                    btnTutorialRegion.gameObject.SetActive(true);
                    imgTutorialRegion.gameObject.SetActive(true);
                    StartCoroutine(tutorialRegionPopup());
                }
            }
            else if (game.tutorial.tutorialRegionsClickable)
            {
                startRegionPopup(region);
                imgTutorialRegion.gameObject.SetActive(false);
            }
        }
        // Je kon op de map klikken door popups en buttons heen, daarom de checks of je niet in een popup zit of op een button hovered
        else if (!canvasRegioPopup.gameObject.activeSelf && !popupActive && !btnOrganizationCheck
        && !btnMenuCheck && !btnTimelineCheck && !game.tutorial.tutorialActive && !btnAfterActionStatsCheck && !btnAfterActionCompletedCheck && !btnQuestsCheck && !btnMonthlyReportCheck && !btnYearlyReportCheck
        && !btnInvestementsHoverCheck && !btnCardsHoverCheck)
        {
            // Start de regio popup zonder tutorial
            startRegionPopup(region);
            imgTutorialRegion.gameObject.SetActive(false);
        }
    }

    private void startRegionPopup(MapRegion region)
    {
        regio = region;

        // Als het multiplayer is wordt de status geupdate 
        if (ApplicationModel.multiplayer)
            playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Regio scherm (" + regio.name[taal] + ")" + " aan het bekijken", "Looking at region screen(" + regio.name[taal] + ")");
        
        canvasRegioPopup.gameObject.SetActive(true);
        popupActive = true;
        EventManager.CallPopupIsActive();

        // Clear en refresh de dropdown menu voor acties
        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();

        // Initialize de UI uit de regio
        updateRegionScreenUI();
    }

    IEnumerator tutorialRegionPopup()
    {
        // Tutorial voor regio is actief
        game.tutorial.tutorialRegionActive = true;

        // Tekst voor Regio gedeelte van de tutorial
        string[] step1 = { "Dit is het regio menu. De statistieken van een regio worden opgemaakt door de statistieken van de sectoren in de regio: Huishoudens, Bedrijven en Landbouw." +
                "\n\nJe kan de statistieken van een sector zien door op de naam te klikken.",
            "This is the region menu. The statistics of a region consist of the statistics of the sectors in the region: Households, Companies and Agriculture." +
            "\n\nYou can see the statistics of a sector by clicking on the name."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialRegion.text = step1[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        // Zolang je niet op verder drukt
        while (game.tutorial.tutorialIndex < 4)
            yield return null;

        // Tekst voor Regio gedeelte van de tutorial
        string[] step2 = { "Je kan statistieken in sectoren aanpassen door acties te doen. Acties kosten tijd en geld " +
                "om uit te voeren. Sommige acties kan je maar 1 keer uitvoeren en anderen kan je maar eens " +
                "in de zoveel tijd doen, maar je kan acties wel in meerdere sectoren tegelijk doen." +
                "\n\nVoer een actie uit en sluit vervolgens het menu.",
            "You can manipulate statistics in a sector by executing actions. Actions cost time and money to execute. " +
            "Some action you can do only once and others can only be done once in a certain timeframe, but you can do " +
            "an action in several sections at the same time." +
            "\n\nExecute an action and then close the menu. "};

        txtTutorialRegion.text = step2[taal];
        btnTutorialRegion.gameObject.SetActive(false);

        // Positie van tutorial image wordt veranderd naar links
        imgTutorialRegion.gameObject.transform.position = imgPosLeft;

        while (!game.tutorial.tutorialCheckActionDone)
            yield return null;

        imgTutorialRegion.gameObject.SetActive(false);
        game.tutorial.tutorialRegionActive = false;
        game.tutorial.tutorialRegionDone = true;
    }

    private void updateRegionScreenUI()
    {
        // De kleur values van de statistieken in de regio worden geset
        updateRegionColorValues();

        // De tekst values van de statistieken in de regio worden geset
        updateRegionTextValues();

        // Initialize de drop down en zet er de jusite acties in
        initDropDownRegion();

        // Zet de toggles op niet actief
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);

        // Als het multiplayer is
        if (ApplicationModel.multiplayer)
        {
            // Als jij de eigenaar bent van de regio (als je niet de eigenaar bent kun je bepaalde dingen niet doen in de regio)
            if (regio.regionOwner == PhotonNetwork.player.NickName)
            {
                txtNotYourRegion.gameObject.SetActive(false);
                imgHistory.gameObject.SetActive(false);

                btnHistoryTab.gameObject.SetActive(true);
                btnActionsTab.gameObject.SetActive(true);
                btnActionsTabClick();
            }
            else
            {
                // Als jij niet de eigenaar bent kun je geen acties doen
                txtNotYourRegion.gameObject.SetActive(true);
                string[] txtInfo = { "Dit is de regio van de andere speler. Dit betekend dat je hier alleen het gemiddelde en de sector statistieken kunt zien en geen acties kunt uitvoeren. Je kunt wel de actieve acties en events tab bekijken in deze regio.",
                "This region belongs to the other player. This means you can only view the average and sector statistics and are not able to do actions. You are able to view the active events and actions tab in this region." };
                txtNotYourRegion.text = txtInfo[taal];

                imgHistory.gameObject.SetActive(false);
                imgActions.gameObject.SetActive(false);
                btnHistoryTab.gameObject.SetActive(true);
                btnActionsTab.gameObject.SetActive(true);
            }
        }
        // Als het geen multiplayer is
        else
        {
            txtNotYourRegion.gameObject.SetActive(false);
            imgHistory.gameObject.SetActive(false);
            imgActions.gameObject.SetActive(false);

            btnHistoryTab.gameObject.SetActive(true);
            btnActionsTab.gameObject.SetActive(true);
            btnActionsTab.interactable = true;
            btnHistoryTab.interactable = true;

            // De tab die als eerst meteen geopend moet worden is die van de acties
            btnActionsTabClick();
        }

        btnAverageTab.interactable = false;
        btnHouseholdsTab.interactable = true;
        btnAgriculureTab.interactable = true;
        btnCompaniesTab.interactable = true;
    }

    private void updateRegionColorValues()
    {
        // De kleur en tekst van de gemiddelde statistieken worden geset
        iconController(imgHappinessRegion, txtRegionHappinessDescription, txtRegionHappiness, regio.statistics.happiness);
        iconController(imgEcoAwarenessRegion, txtRegionEcoAwarenessDescription, txtRegionAwareness, regio.statistics.ecoAwareness);
        iconController(imgPollutionRegion, txtRegionPollutionDescription, txtRegionPollution, regio.statistics.avgPollution);
        iconController(imgPollutionAirRegion, txtRegionAirDescription, txtRegionPollutionAir, regio.statistics.avgAirPollution);
        iconController(imgPollutionWaterRegion, txtRegionWaterDescription, txtRegionPollutionWater, regio.statistics.avgWaterPollution);
        iconController(imgPollutionNatureRegion, txtRegionNatureDescription, txtRegionPollutionNature, regio.statistics.avgNaturePollution);
        iconController(imgProsperityRegion, txtRegionProsperityDescription, txtRegionProsperity, regio.statistics.prosperity);

        // De kleur en tekst van de gemiddelde pollution increase statistieken worden geset
        iconControllerPollutionIncrease(imgPollutionAirIncreaseRegion, txtRegionPollutionAirIncreaseDescription, txtRegionPollutionAirIncrease, regio.statistics.avgAirPollutionIncrease);
        iconControllerPollutionIncrease(imgPollutionNatureIncreaseRegion, txtRegionPollutionNatureIncreaseDescription, txtRegionPollutionNatureIncrease, regio.statistics.avgNaturePollutionIncrease);
        iconControllerPollutionIncrease(imgPollutionWaterIncreaseRegion, txtRegionPollutionWaterIncreaseDescription, txtRegionPollutionWaterIncrease, regio.statistics.avgWaterPollutionIncrease);
    }

    // De tekst uit de popup wordt geset
    private void initRegionText()
    {
        string[] txtHappiness = { "Tevredenheid", "Happiness" };
        string[] txtEcoAwareness = { "Milieubewustheid", "Eco awareness" };
        string[] txtIncome = { "Inkomen", "Income" };
        string[] txtAvailableIncome = { "Besteedbaar geld", "Available Money" };
        string[] txtPollution = { "Vervuiling", "Pollution" };
        string[] txtAir = { "Luchtvervuiling", "Air pollution" };
        string[] txtNature = { "Natuurvervuiling", "Nature pollution" };
        string[] txtWater = { "Watervervuiling", "Water pollution" };
        string[] txtIncrease = { "Jaarlijkse verhoging", "Yearly increase" };
        string[] txtHouseholds = { "Huishoudens", "Households" };
        string[] txtAgriculture = { "Landbouw", "Agriculture" };
        string[] txtCompaines = { "Bedrijven", "Companies" };
        string[] txtRight = { "Nieuwe actie", "New action" };
        string[] txtLeft = { "Regiostatistieken", "Region statistics" };
        string[] txtActiveEvents = { "Events", "Events" };
        string[] txtActiveActions = { "Acties", "Actions" };
        string[] btnDoAction = { "Doe actie", "Do action" };
        string[] btnViewConsequences = { "Bekijk consequencies", "View consequences" };
        string[] txtProsperity = { "Welvaart", "Prosperity" };

        string[] txtHistoryTab = { "Actief", "Active" };
        txtBtnTabHistory.text = txtHistoryTab[taal];
        string[] txtActionsTab = { "Doe actie", "Do actions" };
        txtBtnTabActions.text = txtActionsTab[taal];

        string[] txtAverageTab = { "Gemiddelde", "Average" };
        string[] txtHouseholdsTab = { "Huishoudens", "Households" };
        string[] txtCompaniesTab = { "Bedrijven", "Companies" };
        string[] txtAgricultureTab = { "Landbouw", "Agriculture" };
        txtbtnAverageTab.text = txtAverageTab[taal];
        txtbtnHouseholdsTab.text = txtHouseholdsTab[taal];
        txtbtnCompaniesTab.text = txtCompaniesTab[taal];
        txtbtnAgriculureTab.text = txtAgricultureTab[taal];

        txtRegionAvailableMoneyDescription.text = txtAvailableIncome[taal];
        txtRegionHappinessDescription.text = txtHappiness[taal];
        txtRegionEcoAwarenessDescription.text = txtEcoAwareness[taal];
        txtRegionIncomeDescription.text = txtIncome[taal];
        txtRegionPollutionDescription.text = txtPollution[taal];
        txtRegionAirDescription.text = txtAir[taal];
        txtRegionProsperityDescription.text = txtProsperity[taal];
        txtRegionNatureDescription.text = txtNature[taal];
        txtRegionWaterDescription.text = txtWater[taal];
        txtRegionPollutionAirIncreaseDescription.text = txtIncrease[taal];
        txtRegionPollutionWaterIncreaseDescription.text = txtIncrease[taal];
        txtRegionPollutionNatureIncreaseDescription.text = txtIncrease[taal];
        txtRegionHouseholdsDescription.text = txtHouseholds[taal];
        txtRegionAgricultureDescription.text = txtAgriculture[taal];
        txtRegionCompainesDescription.text = txtCompaines[taal];
        txtRegionColumnLeft.text = txtLeft[taal];
        txtRegionColumnRight.text = txtRight[taal];
        txtActiveActionDescription.text = txtActiveActions[taal];
        txtActiveEventsDescription.text = txtActiveEvents[taal];
        btnDoActionText.text = btnDoAction[taal];
        txtCheckboxHouseholds.text = txtHouseholds[taal];
        txtCheckboxAgriculture.text = txtAgriculture[taal];
        txtCheckboxCompanies.text = txtCompaines[taal];
        txtBtnViewConsequences.text = btnViewConsequences[taal];
    }

    // De tekst waarde van de statistieken wordt geset
    private void updateRegionTextValues()
    {
        txtRegionName.text = regio.name[taal];
        txtRegionAvailableMoney.text = game.GetMoney().ToString("0") + availableMoney[taal];
        txtRegionMoney.text = regio.statistics.income.ToString("0") + availableMoney[taal];
        txtRegionHappiness.text = regio.statistics.happiness.ToString("0.00");
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString("0.00") + "%";
        txtRegionProsperity.text = regio.statistics.prosperity.ToString("0.00") + "%";
        txtRegionPollution.text = regio.statistics.avgPollution.ToString("0.00") + "%";
        txtRegionPollutionAir.text = regio.statistics.avgAirPollution.ToString("0.00") + "%";
        txtRegionPollutionNature.text = regio.statistics.avgNaturePollution.ToString("0.00") + "%";
        txtRegionPollutionWater.text = regio.statistics.avgWaterPollution.ToString("0.00") + "%";

        if (regio.statistics.avgAirPollutionIncrease > 0d)
            txtRegionPollutionAirIncrease.text = "+" + regio.statistics.avgAirPollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionAirIncrease.text = regio.statistics.avgAirPollutionIncrease.ToString("0.00") + "%";

        if (regio.statistics.avgNaturePollutionIncrease > 0d)
            txtRegionPollutionNatureIncrease.text = "+" + regio.statistics.avgNaturePollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionNatureIncrease.text = regio.statistics.avgNaturePollutionIncrease.ToString("0.00") + "%";

        if (regio.statistics.avgWaterPollutionIncrease > 0d)
            txtRegionPollutionWaterIncrease.text = "+" + regio.statistics.avgWaterPollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionWaterIncrease.text = regio.statistics.avgWaterPollutionIncrease.ToString("0.00") + "%";

        // Zet alle tekst waardes van acties die leeg moeten zijn ook leeg
        txtRegionActionConsequences.text = "";
        txtRegionActionCost.text = "";
        txtRegionActionDuration.text = "";
        txtRegionActionName.text = "";
        txtRegionActionNoMoney.text = "";
        txtRegionActionSectorTotalCostDescription.text = "";
        txtRegionActionSectorTotalCost.text = "";
        txtRegionColumnCenter.text = "";
        txtRegionInfo.text = "";

        string[] left = { "Gemiddelde statistieken", "Average statistics" };
        txtRegionColumnLeft.text = left[taal];

        txtRegionActionNoMoney.text = "";
        txtActionSectorsDescription.text = "";
        btnDoActionRegionMenu.gameObject.SetActive(false);
        btnViewConsequences.gameObject.SetActive(false);
        dropdownChoiceMade = false;

        updateActiveActions();
        updateActiveEvents();

        dropdownRegio.gameObject.SetActive(true);
        initDropDownRegion();
    }

    // Update de actieve events
    private void updateActiveEvents()
    {
        string activeEventsRegio = "";

        // Voor elke event in de list met actieve events
        foreach (GameEvent ge in regio.inProgressGameEvents)
        {
            // Voeg de event naam toe
            if (ge.isActive || ge.isIdle)
                activeEventsRegio += ge.publicEventName[taal] + "\n";
        }

        txtActiveEvents.text = activeEventsRegio;
    }

    // Update de actieve acties
    private void updateActiveActions()
    {
        string activeActionsRegio = "";

        // Voor elke actieve actie in de regio
        foreach (RegionAction action in regio.actions)
        {
            if (action.isActive)
            {
                activeActionsRegio += action.name[taal] + "\n";
            }

            txtActiveActions.text = activeActionsRegio;
        }
    }

    // De dropdown voor acties wordt geinitialized
    private void initDropDownRegion()
    {
        dropdownRegio.ClearOptions();
        int currentMonth = game.currentYear * 12 + game.currentMonth;

        // Caption text van de dropdown wordt geset
        string[] dropdownPlaceholderText = { "Selecteer een actie", "Choose an action" };
        dropdownRegio.captionText.text = dropdownPlaceholderText[taal];

        // Voor elke actie in de list met acties die available is, die niet al actief is en waar de cooldown van is afgelopen
        foreach (RegionAction action in regio.actions)
        {
            if (action.isAvailable && !action.isActive &&
                (action.lastCompleted + action.actionCooldown <= currentMonth || action.lastCompleted == 0) &&
                !(action.isUnique && action.lastCompleted > 0))
            {
                // Voeg de naam van de actie toe aan de dropdown
                dropdownRegio.options.Add(new Dropdown.OptionData() { text = action.name[taal] });
            }
        }

        //code to bypass Unity bug -> can't set .value outside the dropdown range
        dropdownRegio.options.Add(new Dropdown.OptionData() { text = " " });
        dropdownRegio.value = dropdownRegio.options.Count - 1;
        dropdownRegio.options.RemoveAt(dropdownRegio.options.Count - 1);
    }

    // Wordt aangeropen op de value changed van de dropdown in de inspector
    public void getDropDownValue()
    {
        EventManager.CallPlayButtonClickSFX();
        for (int i = 0; i <= dropdownRegio.options.Count; i++)
        {
            // Pak de jusite gekozen actie uit de dropdown
            if (dropdownRegio.value == i)
            {
                // Zet deze actie in de gekozen actie variabele
                dropdownChoice = dropdownRegio.options[i].text;
            }
        }

        // Toont de juiste informatie van de gekozen optie
        showInfoDropDownRegion();
    }

    private void showInfoDropDownRegion()
    {
        // Voor alle acties in de list met acties
        foreach (RegionAction action in regio.actions)
        {
            // Pak de actie die gekozen is in de dropdown
            if (action.name[taal] == dropdownChoice)
            {
                // Zet de tekst voor de actie
                regioAction = action;

                string[] actionCostText = { "Kosten per sector: " + action.afterInvestmentActionMoneyCost + " geld",
                    "Costs per sector: " + action.afterInvestmentActionMoneyCost + " money" };
                string[] actionDurationText = { "Duur: " + regioAction.actionDuration.ToString() + " maanden",
                    "Duration: " + regioAction.actionDuration.ToString() + " months" };

                string[] txtSectorMoney = { "Totale kosten:", "Total cost:" };
                string[] sectorDescription = { "Selecteer (meerdere) sectoren om actie uit te voeren", "Select (multiple) sectors to perform action" };
                dropdownChoiceMade = true;
                string[] uniqueText = { " <b>[uniek]</b>", " <b>[unique]</b>" };

                txtRegionActionName.text = regioAction.description[taal];
                if (regioAction.isUnique)
                    txtRegionActionName.text += uniqueText[taal];

                txtRegionActionCost.text = actionCostText[taal];
                txtRegionActionDuration.text = actionDurationText[taal];
                txtActionSectorsDescription.text = sectorDescription[taal];
                txtRegionActionSectorTotalCostDescription.text = txtSectorMoney[taal];

                // Methode die de checkboxes goed zet
                setCheckboxes(action);
                regioActionCost = 0;
                txtRegionActionSectorTotalCost.text = regioActionCost.ToString() + availableMoney[taal];
            }
        }
    }

    private void setCheckboxes(RegionAction action)
    {
        // Alle checkboxes worden op niet actief gezet
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);

        // Zet de waarde van alle checkboxes op false
        playSelectSound = false;
        if (checkboxHouseholds)
            checkboxRegionHouseholds.isOn = false;
        if (checkboxAgriculture)
            checkboxRegionAgriculture.isOn = false;
        if (checkboxCompanies)
            checkboxRegionCompanies.isOn = false;
        playSelectSound = true;

        // Een actie kan niet altijd in alle sectoren gedaan worden
        // Hier de check voor welke checkboxes zichtbaar moeten worden en welke niet
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
        // Als het geen multiplayer is
        if (!ApplicationModel.multiplayer)
        {
            // Start de juiste actie, meegestuurd worden de actie, game en in welke sectoren die actie uitgevoerd moet worden
            regio.StartAction(regioAction, game, new bool[] { checkboxHouseholds, checkboxCompanies, checkboxAgriculture });
        }
        else
        {
            // Als het wel multiplayer is
            // Start de juiste actie, meegestuurd worden de actie, game en in welke sectoren die actie uitgevoerd moet worden
            regio.StartActionMultiplayer(regioAction, game, new bool[] { checkboxHouseholds, checkboxCompanies, checkboxAgriculture });

            // Stuur een update naar de log om de andere speler te laten zien dat er een actie is gestart
            playerController.photonView.RPC("ActionStarted", PhotonTargets.Others, regio.name[0], regioAction.name[0],
                new bool[] { checkboxHouseholds, checkboxCompanies, checkboxAgriculture });
        }

        // Als de tutorial voor acties nog niet done was, zet hem nu op done
        if (!game.tutorial.tutorialCheckActionDone)
            game.tutorial.tutorialCheckActionDone = true;

        // Set de caption text van de dropdown opnieuw
        string[] dropdownPlaceholderText = { "Selecteer een actie", "Choose an action" };
        dropdownRegio.captionText.text = dropdownPlaceholderText[taal];

        // Set de hoeveelheid geld opnieuw
        txtRegionAvailableMoney.text = game.GetMoney().ToString("0") + availableMoney[taal];

        // Clear en Refresh de dropdown
        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();

        updateRegionScreenUI();

        // Open de tab voor acties opnieuw zodat het scherm weer helemaal goed gereset wordt
        btnActionsTabClick();
    }

    // Clear alle tekst variable die met acties te maken hebben
    public void ClearActionMenu()
    {
        initDropDownRegion();

        btnActionsTab.interactable = true;
        btnDoActionRegionMenu.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);
        btnDoActionRegionMenu.gameObject.SetActive(false);

        regioActionCost = 0;
        txtRegionActionConsequences.text = "";
        txtRegionActionCost.text = "";
        txtRegionActionDuration.text = "";
        txtRegionActionName.text = "";
        txtRegionActionNoMoney.text = "";
        txtRegionActionSectorTotalCostDescription.text = "";
        txtRegionActionSectorTotalCost.text = "";
        txtRegionColumnCenter.text = "";
        txtActionSectorsDescription.text = "";

        playSelectSound = false;
        if (!checkboxHouseholds)
            checkboxRegionHouseholds.isOn = true;

        if (!checkboxAgriculture)
            checkboxRegionAgriculture.isOn = true;

        if (!checkboxCompanies)
            checkboxRegionCompanies.isOn = true;
        playSelectSound = true;

        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();
        imgActions.gameObject.SetActive(false);
    }

    // Toegewezen aan de button Companies in de inspector
    public void sectorCompaniesClick()
    {
        initRegionSectorPopup(regio.sectors[1]);
    }

    // Toegewezen aan de button Households in de inspector
    public void sectorHouseholdsClick()
    {
        initRegionSectorPopup(regio.sectors[0]);
    }

    // Toegewezen aan de button Agriculture in de inspector
    public void sectorAgricultureClick()
    {
        initRegionSectorPopup(regio.sectors[2]);
    }

    // Set alle tekst waarde van de stasistieken naar de juiste sector 
    private void initRegionSectorPopup(RegionSector sector)
    {
        imgSectorPopup.gameObject.SetActive(true);
        txtRegionSectorTitle.text = sector.sectorName[taal];

        string[] tip = { "\n<b>Sector statistieken: </b>\nLuchtvervuiling: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nTevredenheid: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nWelvaart: " + sector.statistics.prosperity.ToString("0.00")  + "%",

                    "<b>\nSector statistics: </b>\nAir pollution: " + sector.statistics.pollution.airPollution.ToString("0.00") + "%\nWater pollution: " + sector.statistics.pollution.waterPollution.ToString("0.00")
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution.ToString("0.00") + "%\nHappiness: " + sector.statistics.happiness.ToString("0.00")
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness.ToString("0.00") + "%\nProsperity: " + sector.statistics.prosperity.ToString("0.00")  + "%"};
        txtRegionSectorInfo.text = tip[taal];
    }

    // Toegewezen aan de button History in de inspector
    public void btnHistoryTabClick()
    {
        // Als de image voor acties actief zet, zet die of inactief
        if (imgActions.gameObject.activeSelf)
            imgActions.gameObject.SetActive(false);

        // Set de juiste buttons op wel/niet interactable
        btnActionsTab.interactable = true;
        btnHistoryTab.interactable = false;
        imgHistory.gameObject.SetActive(true);

        // Set de tekst
        string[] txtCenter = { "Actieve Acties & Events", "Active Actions & Events" };
        txtRegionColumnCenter.text = txtCenter[taal];

        if (ApplicationModel.multiplayer)
            txtNotYourRegion.text = "";
    }

    // Toegewezen aan de button Acties in de inspector
    public void btnActionsTabClick()
    {
        // Als de image voor history actief zet, zet die of inactief
        if (imgHistory.gameObject.activeSelf)
            imgHistory.gameObject.SetActive(false);

        // Set de juiste buttons op wel/niet interactable

        btnActionsTab.interactable = false;
        btnHistoryTab.interactable = true;

        // Als het multiplayer is
        if (ApplicationModel.multiplayer)
        {
            // Als jij niet de owner bent van de huidige geopende regio
            if (regio.regionOwner != PhotonNetwork.player.NickName)
            {
                // Set de tekst 
                imgActions.gameObject.SetActive(false);
                string[] txtInfo = { "Dit is de regio van de andere speler. Dit betekend dat je hier alleen het gemiddelde en de sector statistieken kunt zien en geen acties kunt uitvoeren. Je kunt wel de actieve acties en events tab bekijken in deze regio.",
                "This region belongs to the other player. This means you can only view the average and sector statistics and are not able to do actions. You are able to view the active events and actions tab in this region." };
                txtNotYourRegion.text = txtInfo[taal];
            }
            else
            {
                // Als jij wel de owner bent
                imgActions.gameObject.SetActive(true);
                string[] txtCenter = { "Doe een actie", "Do an action" };
                txtRegionColumnCenter.text = txtCenter[taal];

                string[] txt = { "Kies hier een actie", "Choose an action" };
                txtRegionInfo.text = txt[taal];
            }
        }
        else
        {
            // Als het geen multiplayer is
            imgActions.gameObject.SetActive(true);
            string[] txtCenter = { "Doe een actie", "Do an action" };
            txtRegionColumnCenter.text = txtCenter[taal];

            string[] txt = { "Kies hier een actie", "Choose an action" };
            txtRegionInfo.text = txt[taal];
        }
    }

    // Zet de kleur waardes van de sector statistieken 
    private void updateSectorColorValues(SectorStatistics s)
    {
        // De kleur en tekst van de gemiddelde statistieken worden geset
        iconController(imgHappinessRegion, txtRegionHappinessDescription ,txtRegionHappiness, s.happiness);
        iconController(imgEcoAwarenessRegion, txtRegionEcoAwarenessDescription, txtRegionAwareness, s.ecoAwareness);
        iconController(imgPollutionRegion, txtRegionPollutionDescription, txtRegionPollution, s.pollution.avgPollution);
        iconController(imgPollutionAirRegion, txtRegionAirDescription, txtRegionPollutionAir, s.pollution.airPollution);
        iconController(imgPollutionWaterRegion, txtRegionWaterDescription, txtRegionPollutionWater, s.pollution.waterPollution);
        iconController(imgPollutionNatureRegion, txtRegionNatureDescription, txtRegionPollutionNature, s.pollution.naturePollution);
        iconController(imgProsperityRegion, txtRegionProsperityDescription, txtRegionProsperity, s.prosperity);

        // De kleur en tekst van de pollution increase statistieken worden geset
        iconControllerPollutionIncrease(imgPollutionAirIncreaseRegion, txtRegionPollutionAirIncreaseDescription, txtRegionPollutionAirIncrease, s.pollution.airPollutionIncrease);
        iconControllerPollutionIncrease(imgPollutionNatureIncreaseRegion, txtRegionPollutionNatureIncreaseDescription, txtRegionPollutionNatureIncrease, s.pollution.naturePollutionIncrease);
        iconControllerPollutionIncrease(imgPollutionWaterIncreaseRegion, txtRegionPollutionWaterIncreaseDescription, txtRegionPollutionWaterIncrease, s.pollution.waterPollutionIncrease);
    }

    // De method waar alle kleur values gekozen worden
    void iconController(RawImage img, Text description, Text txt, double value)
    { 
        Color lerpColor;
        float f = (float)value / 100;

        // Pollution moet laag zijn om goed te zijn, de rest hoog
        if (img == imgPollutionAirRegion || img == imgPollutionWaterRegion || img == imgPollutionNatureRegion || img == imgPollutionRegion)
        {
            // Color based on third argument (value / 100)
            lerpColor = Color.Lerp(Color.green, Color.red, f);
        }
        else
        {
            // Color based on third argument (value / 100)
            lerpColor = Color.Lerp(Color.red, Color.green, f);
        }

        txt.text = " " + value.ToString("0.00") + "%";
        txt.color = lerpColor;
        description.color = lerpColor;
        img.color = lerpColor;
    }

    // De value is tussen -5 en 5, moet deze omrekenen naar 0 - 100
    void iconControllerPollutionIncrease(RawImage img, Text description, Text txt, double value)
    {
        Color lerpColor;

        // Als de waarde boven 1.5 > maak icon en tekst rood
        if (value > 1.5)
            lerpColor = Color.red;
        // Als de waarde onder 0 > maak icon en tekst rood
        else if (value < 0)
            lerpColor = Color.green;
        else
            // Als de waarde boven tussen 0 en 1.5 is > maak icon en tekst rood
            ColorUtility.TryParseHtmlString("#ffa500", out lerpColor);

        txt.text = " " + value.ToString("0.00") + "%";
        txt.color = lerpColor;
        description.color = lerpColor;
        img.color = lerpColor;
    }

    // Toegewezen aan de button Average in de inspector
    public void btnAverageTabClick()
    {
        btnAverageTab.interactable = false;

        btnHouseholdsTab.interactable = true;
        btnAgriculureTab.interactable = true;
        btnCompaniesTab.interactable = true;

        // Ga naar de method die de juiste gemiddelde statistieken toont
        updateRegionTextValues();
    }

    // Toegewezen aan de button Households in de inspector
    public void btnHouseholdsTabClick()
    {
        btnHouseholdsTab.interactable = false;

        btnAverageTab.interactable = true;
        btnAgriculureTab.interactable = true;
        btnCompaniesTab.interactable = true;

        // Ga naar de method die de juiste sector statistieken toont
        updateSectorTextValues(regio.sectors[0]);
    }

    // Toegewezen aan de button Agriculture in de inspector
    public void btnAgricultureTabClick()
    {
        btnAgriculureTab.interactable = false;

        btnAverageTab.interactable = true;
        btnHouseholdsTab.interactable = true;
        btnCompaniesTab.interactable = true;

        // Ga naar de method die de juiste sector statistieken toont
        updateSectorTextValues(regio.sectors[2]);
    }

    // Toegewezen aan de button Companies in de inspector
    public void btnCompaniesTabClick()
    {
        btnCompaniesTab.interactable = false;

        btnAverageTab.interactable = true;
        btnHouseholdsTab.interactable = true;
        btnAgriculureTab.interactable = true;

        // Ga naar de method die de juiste sector statistieken toont
        updateSectorTextValues(regio.sectors[1]);
    }

    // Method die de tekst waardes voor de juiste sector set
    private void updateSectorTextValues(RegionSector s)
    {
        // Set de kleur values voor de sector statistieken
        updateSectorColorValues(s.statistics);
        txtRegionColumnLeft.text = s.sectorName[taal];

        // Set alle tekst values voor de sector statistieken
        txtRegionMoney.text = s.statistics.income.ToString("0") + availableMoney[taal];
        txtRegionHappiness.text = s.statistics.happiness.ToString("0.00");
        txtRegionAwareness.text = s.statistics.ecoAwareness.ToString("0.00") + "%";
        txtRegionProsperity.text = s.statistics.prosperity.ToString("0.00") + "%";
        txtRegionPollution.text = s.statistics.pollution.avgPollution.ToString("0.00") + "%";
        txtRegionPollutionAir.text = s.statistics.pollution.airPollution.ToString("0.00") + "%";
        txtRegionPollutionNature.text = s.statistics.pollution.naturePollution.ToString("0.00") + "%";
        txtRegionPollutionWater.text = s.statistics.pollution.waterPollution.ToString("0.00") + "%";

        // If statements om te kijken of er een een + of - voor moet staan
        if (regio.statistics.avgAirPollutionIncrease > 0d)
            txtRegionPollutionAirIncrease.text = "+" + s.statistics.pollution.airPollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionAirIncrease.text = s.statistics.pollution.airPollutionIncrease.ToString("0.00") + "%";

        if (regio.statistics.avgNaturePollutionIncrease > 0d)
            txtRegionPollutionNatureIncrease.text = "+" + s.statistics.pollution.naturePollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionNatureIncrease.text = s.statistics.pollution.naturePollutionIncrease.ToString("0.00") + "%";

        if (regio.statistics.avgWaterPollutionIncrease > 0d)
            txtRegionPollutionWaterIncrease.text = "+" + s.statistics.pollution.waterPollutionIncrease.ToString("0.00") + "%";
        else
            txtRegionPollutionWaterIncrease.text = s.statistics.pollution.waterPollutionIncrease.ToString("0.00") + "%";

    }
    #endregion

    #region Code for Monthly/Yearly Report Popup
    IEnumerator tutorialMonthlyReport()
    {
        // Tekst voor Montly Report Tutorial
        string[] step1 = { "In dit maandelijkse rapport kun je de veranderingen van de statistieken zien ten opzichte van vorige maand. Daarnaast kun je ook zien of er nieuwe events in een regio zijn. " +
                "Je krijgt dit rapport elke maand opnieuw. \n\nJe kunt dit menu sluiten door op de ESC toets te drukken."
                , "In this monthly report you can see the changes in the statistics with the previous month. You can also see if there is a new event in a region. " +
                "You get this report each month. \n\nYou can close this menu by pressing the ESC key."};
        string[] btnText = { "Verder", "Next" };

        txtTutorialAfterTurn.text = step1[taal];
        txtTutorialAfterTurnBtn.text = btnText[taal];

        // Als er niet op "verder" wordt gedrukt
        while (game.tutorial.tutorialIndex < 6)
            yield return null;
        
        imgTutorialAfterTurn.gameObject.SetActive(false);
        game.tutorial.tutorialMonthlyReportDone = true;
        game.tutorial.tutorialMonthlyReportActive = false;
    }

    // Aangeroepen vanuit GameController in NextTurn() method
    public void InitMonthlyReport()
    {
        // Update de text values (true betekend dat het een maandelijks rapport is)
        updateTextAfterActionStats(true);

        // Bereken de verschillen in statistieken ten opzichte van vorige maand
        calculateDifference(game.oldMonthlyReport.oldIncome, game.oldMonthlyReport.oldHappiness, game.oldMonthlyReport.oldEcoAwareness, game.oldMonthlyReport.oldPollution, game.oldMonthlyReport.oldProsperity, true);
    }

    public void InitYearlyReport()
    {
        // Update de text values (false betekend dat het een jaarlijks rapport is)
        updateTextAfterActionStats(false);

        // Bereken de verschillen in statistieken ten opzichte van vorig jaar
        calculateDifference(game.oldYearlyReport.oldIncome, game.oldYearlyReport.oldHappiness, game.oldYearlyReport.oldEcoAwareness, game.oldYearlyReport.oldPollution, game.oldYearlyReport.oldProsperity, false);
    }

    // Method waarin de tekst wordt geset
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

    // Method waar alle verschillen ten opzichte van vorige maand/vorig jaar worden berekend
    private void calculateDifference(double[] oldIncome, double[] oldHappiness, double[] oldEcoAwareness, double[] oldPollution, double[] oldProsperity, bool isMonthly)
    {
        double incomeDifference = 0;
        double happinessDifference = 0;
        double ecoAwarenessDifference = 0;
        double pollutionDifference = 0;
        double prosperityDifference = 0;

        // Voor elke regio moeten deze verschillende berekend worden
        for (int i = 0; i < game.oldMonthlyReport.reportRegions.Length; i++)
        {
            incomeDifference = game.regions[i].statistics.income - oldIncome[i];
            happinessDifference = game.regions[i].statistics.happiness - oldHappiness[i];
            ecoAwarenessDifference = game.regions[i].statistics.ecoAwareness - oldEcoAwareness[i];
            pollutionDifference = game.regions[i].statistics.avgPollution - oldPollution[i];
            prosperityDifference = game.regions[i].statistics.prosperity - oldProsperity[i];

            // De verschillen moeten in een text variabele gezet worden die bij de maand en posistie in het canvas hoort
            // Daarom if statement voor welke regio het is
            if (isMonthly)
            {
                if (game.oldMonthlyReport.reportRegions[i] == "Noord Nederland")
                {
                    // Stuur Noord Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionNoord, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.oldMonthlyReport.reportRegions[i] == "Oost Nederland")
                {
                    // Stuur Oost Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionOost, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.oldMonthlyReport.reportRegions[i] == "Zuid Nederland")
                {
                    // Stuur Zuid Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionZuid, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.oldMonthlyReport.reportRegions[i] == "West Nederland")
                {
                    // Stuur West Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionWest, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
            }
            else
            {
                // De verschillen moeten in een text variabele gezet worden die bij de jaar en posistie in het canvas hoort
                // Daarom if statement voor welke regio het is
                if (game.monthlyReport.reportRegions[i] == "Noord Nederland")
                {
                    // Stuur Noord Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionNoordYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Oost Nederland")
                {
                    // Stuur Oost Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionOostYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "Zuid Nederland")
                {
                    // Stuur Zuid Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionZuidYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
                else if (game.monthlyReport.reportRegions[i] == "West Nederland")
                {
                    // Stuur West Nederland text variabele en de statistieken van deze regio mee
                    setValuesChanged(txtAfterActionWestYearly, incomeDifference, happinessDifference, ecoAwarenessDifference, pollutionDifference, prosperityDifference);
                }
            }
        }

        // Methods die de afgeronde acties en events tonen
        initAfterActionStatsCompletedEvents();
        initAfterActionStatsCompletedActions();
    }
    

    private void setValuesChanged(Text txt, double incomeDifference, double happinessDifference, double ecoAwarenessDifference, double pollutionDifference, double prosperityDifference)
    {
        // Kijk voor elke statistiek of er wel een verschil is tenopzichte van de vorige maand, als er geen verschil is wordt er niks getoont
        // Bij elke statistiek is er een IF statement of om te kijken of er een + of een - bij moet en in welke kleur de tekst moet staan
        txt.text = "";

        // Als het verschil niet 0 is
        if (incomeDifference != 0d)
        {
            // Als het verschil groter is dan 0
            if (incomeDifference > 0d)
            {
                // De tekst wordt nu groen en er wordt een + weergegeven
                string[] difference = { "<color=#00cc00>\nInkomen: ", "<color=#00cc00>\nIncome: " };
                difference[taal] += "+" + incomeDifference.ToString("0.00") + "</color>";
                txt.text += difference[taal];
            }
            // Als het verschil kleiner is dan 0
            else
            {
                // De tekst wordt nu rood en er wordt een - weergegeven
                string[] difference = { "<color=#FF0000>\nInkomen: ", "<color=#FF0000>\nIncome: " };
                difference[taal] +=  incomeDifference.ToString("0.00") + "</color>";
                txt.text += difference[taal];
            }

        }

        // Als het verschil niet 0 is
        if (happinessDifference != 0d)
        {
            // Als het verschil groter is dan 0
            if (happinessDifference > 0d)
            {
                // De tekst wordt nu groen en er wordt een + weergegeven
                string[] difference = { "<color=#00cc00>\nTevredenheid: ", "<color=#00cc00>\nHappiness: " };
                difference[taal] += "+" + happinessDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }
            else
            {
                // De tekst wordt nu rood en er wordt een - weergegeven
                string[] difference = { "<color=#FF0000>\nTevredenheid: ", "<color=#FF0000>\nHappiness: " };
                difference[taal] += happinessDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }

        }

        // Als het verschil niet 0 is
        if (ecoAwarenessDifference != 0d)
        {
            // Als het verschil groter is dan 0
            if (ecoAwarenessDifference > 0d)
            {
                // De tekst wordt nu groen en er wordt een + weergegeven
                string[] difference = { "<color=#00cc00>\nMilieubewustheid: ", "<color=#00cc00>\nEco awareness: " };
                difference[taal] += "+" + ecoAwarenessDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }
            else
            {
                // De tekst wordt nu rood en er wordt een - weergegeven
                string[] difference = { "<color=#FF0000>\nMilieubewustheid: ", "<color=#FF0000>\nEco awareness: " };
                difference[taal] += ecoAwarenessDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }

        }

        // Als het verschil niet 0 is
        if (pollutionDifference != 0d)
        {
            // Als het verschil groter is dan 0
            if (pollutionDifference > 0d)
            {
                // De tekst wordt nu rood en er wordt een + weergegeven
                string[] difference = { "<color=#FF0000>\nVervuiling: ", "<color=#FF0000>\nPollution: " };
                difference[taal] += "+" + pollutionDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }
            else
            {
                // De tekst wordt nu groen en er wordt een - weergegeven
                string[] difference = { "<color=#00cc00>\nVervuiling: ", "<color=#00cc00>\nPollution: " };
                difference[taal] += pollutionDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }

        }

        // Als het verschil niet 0 is
        if (prosperityDifference != 0d)
        {
            // Als het verschil groter is dan 0
            if (prosperityDifference > 0d)
            {
                // De tekst wordt nu groen en er wordt een + weergegeven
                string[] difference = { "<color=#00cc00>\nWelvaart: ", "<color=#00cc00>\nProsperity: " };
                difference[taal] += "+" + prosperityDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }
            else
            {
                // De tekst wordt nu rood en er wordt een - weergegeven
                string[] difference = { "<color=#FF0000>\nWelvaart: ", "<color=#FF0000>\nProsperity: " };
                difference[taal] += prosperityDifference.ToString("0.00") + "%</color>";
                txt.text += difference[taal];
            }

        }

    }

    // Method die voor elke regio kijkt of er event afgerond zijn
    public void initAfterActionStatsCompletedEvents()
    {
        monthlyCompletedEvents = (List<GameEvent>[])game.monthlyReport.completedEvents.Clone();

        // Kijk voor elke regio of er afgeronde events zijn, zoja > stuur ga naar de method waar de tekst voor deze events wordt geset
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
        string[] events = { "\n\n<b>Afgeronde events:</b>\n", "\n\n<b>Completed event:</b>\n" };
        txt.text += events[taal];

        // Voeg elke event in de list met events toe aan de tekst variabele
        foreach (GameEvent e in eventsList)
        {
            txt.text += e.publicEventName[taal];

            if (taal == 0)
                txt.text += "\n<b>Gekozen oplossing: </b>" + e.choicesDutch[e.pickedChoiceNumber];
            else
                txt.text += "\n<b>Chosen solution: </b>" + e.choicesEnglish[e.pickedChoiceNumber];

            string[] c = { "\n<b>Consequenties: </b>", "\n<b>Consequences: </b>" };

            // Haal de consequencies op uit de method getSectorStatisticsConsequences()
            txt.text += c[taal] + getSectorStatisticsConsequences(e.pickedConsequences[e.pickedChoiceNumber]);

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

    // Method die voor elke regio kijkt of er acties afgerond zijn
    private void initAfterActionStatsCompletedActions()
    {
        monthlyCompletedActions = (List<RegionAction>[])game.monthlyReport.completedActions.Clone();

        // Kijk voor elke regio of er afgeronde actues zijn, zoja > stuur ga naar de method waar de tekst voor deze acties wordt geset
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
        string[] acties = { "<b>Afgeronde acties:</b>\n", "\n\n<b>Completed action:</b>\n" };
        txt.text += acties[taal];
        foreach (RegionAction a in actionsList)
        {
            txt.text += a.name[taal] + " - " + a.description[taal];

            // Haal de gekozen sectoren op uit deze method
            txt.text += getChosenSectors(a.pickedSectors);

            string[] c = { "\n<b>Consequenties: </b>", "\n<b>Consequences: </b>" };

            // Haal de consequencies op uit de method getSectorStatisticsConsequences()
            txt.text += c[taal] + getSectorStatisticsConsequences(a.afterInvestmentConsequences);

            if (a.actionMoneyReward != 0)
            {
                string[] line = { "\n<b>Geld beloning: </b>", "\n<b>Money reward: </b>" };
                txt.text += line[taal] + a.actionMoneyReward + "\n\n";
            }
        }
    }

    private string getChosenSectors(bool[] sectors)
    {
        string[] sectorsPicked = { "\n<b>Sectoren: </b>\n", "\n<b>Sectors: </b>\n" };

        // Kijk welke sectoren gekozen zijn en stuur deze terug
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
    // Method wordt aangeroepen als er op de quests button wordt gedrukt
    private void initQuestsPopup()
    {
        // Set de tekst voor de Quests canvas
        imgTutorialQuests.gameObject.SetActive(false);
        string[] title = { "Missies", "Quests" };
        string[] description = { "Actieve missies", "Active quests" };
        string[] activeQuests = { "", "" };
        string[] noActiveQuests = { "Er zijn geen actieve missies", "There are no active quests" };
        string[] beloning = { "Beloning: ", "Reward: " };

        bool activeQuest = false;

        txtQuestsTitle.text = title[taal];
        txtQuestsDescription.text = description[taal];

        // Als de tutorial actief is en nog niet afgerond voor dit onderdeel > start tutorial
        if (game.tutorial.tutorialQuestsActive && game.tutorial.doTuto)
            StartCoroutine(tutorialQuestsPopup());

        // Voor elke quests in de list met quests
        foreach (Quest q in game.quests)
        {
            // Als de quests actief is, zet hem in de text variabele
            if (q.isActive)
            {
                activeQuests[taal] += q.name[taal] + " - " + q.description[taal] + "\n";

                // Haal de condities die nodig zijn om de quest te halen op uit deze method
                activeQuests[taal] += getCompleteConditions(q.questCompleteConditions);
                activeQuests[taal] += beloning[taal] + q.questMoneyReward + "\n\n";
                txtQuestsActive.text = activeQuests[taal];
                activeQuest = true;
            }
            // ALs de quests completed is, zet hem in de text variabele en maak de tekst groen
            else if (q.isCompleted)
            {
                activeQuests[taal] += "<color=#00cc00>[COMPLETED] - " + q.name[taal] + " - " + q.description[taal] + "\n";

                // Haal de condities die nodig zijn om de quest te halen op uit deze method
                activeQuests[taal] += getCompleteConditions(q.questCompleteConditions);
                activeQuests[taal] += beloning[taal] + q.questMoneyReward + "\n\n</color>";
                txtQuestsActive.text = activeQuests[taal];
            }
        }
        // Als er geen actieve of afgeronde quests zijn
        if (!activeQuest)
            txtQuestsActive.text = noActiveQuests[taal];       
    }

    IEnumerator tutorialQuestsPopup()
    {
        Debug.Log("Coroutine Tutorial Quests!");
        imgTutorialQuests.gameObject.SetActive(true);

        // Tekst voor de quests tutorial
        string[] step2 = { "In deze pop-up kun je zien welke actieve missies je hebt. Je krijgt om de 2 jaar een nieuwe missie. \n\nAls je aan de juiste condities voldoet haal je de missie en krijg je een beloning.",
            "In this popup you can see your active quests. You get a new quest each time 2 years pass. \n\nIf you reach the quest conditions you get a reward." };
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialQuests.text = step2[taal];
        txtTutorialQuestsBtn.text = txtBtn[taal];

        while (game.tutorial.tutorialIndex < 9)//tutorialStep16)
            yield return null;


        imgTutorialQuests.gameObject.SetActive(false);
        game.tutorial.tutorialQuestsActive = false;

        // Zoang er niet op verder gedrukt wordt
        while (canvasQuestsPopup.gameObject.activeSelf)
            yield return null;

        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;
        imgTutorialStepOrgMenuHightlight.enabled = false;

        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialQuestsDone = true;
        btnNextTurn.interactable = true;
    }
    
    // Method die de condities ophaalt die nodig zijn om de quest te halen
    // Kijk voor elke statistiek of hij niet 0 is, als hij 0 is wordt hij niet getoond
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

    #region Code for Event Popup (No Choice Made)
    public void initEventPopup(GameEvent e, MapRegion r)
    {
        imgEventConsequences.gameObject.SetActive(false);
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

        while (game.tutorial.tutorialIndex < 8)// tutorialStep20)
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

        //btnDoEvent.interactable = false;
        btnDoEvent.gameObject.SetActive(false);
        btnViewConsequencesEvent.interactable = false;
    }

    private void initEventText(GameEvent e)
    {
        txtNotYourEvent.gameObject.SetActive(false);
        string[] txtBtn = { "Doe keuze", "Do choice" };
        string[] txtBtn2 = { "Bekijk consequences", "View consequences" };
        string[] txtKosten = { "\nKosten: ", "\nCost: " };
        string[] txtMoney = { " geld", " money" };
        string[] txtDuur = { "\nDuur: ", "\nDuration: " };
        string[] txtMonths = { " maanden", " months" };
        string[] txtMonth = { " maand", " month" };

        txtEventTitle.text = "Event";
        txtEventName.text = "EVENT: " + e.publicEventName[taal] + " (" + regionEvent.name[taal] + ")";
        txtEventDescription.text = e.description[taal];
        txtBtnDoEvent.text = txtBtn[taal];
        txtBtnViewConsequencesEvent.text = txtBtn2[taal];
        txtEventConsequencesChoice.text = "";
        txtEventColumRight.text = "";

        if (ApplicationModel.language == 0)
        {
            radioEventOption1Text.text = "<b>" + e.choicesDutch[0] + "</b>";
            radioEventOption2Text.text = "<b>" + e.choicesDutch[1] + "</b>"; 
            radioEventOption3Text.text = "<b>" + e.choicesDutch[2] + "</b>"; 
        }
        else
        {
            radioEventOption1Text.text = "<b>" + e.choicesEnglish[0] + "</b>";
            radioEventOption2Text.text = "<b>" + e.choicesEnglish[1] + "</b>";
            radioEventOption3Text.text = "<b>" + e.choicesEnglish[2] + "</b>";
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
            if (game.GetMoney() >= gameEvent.afterInvestmentEventChoiceMoneyCost[0])
            {
                btnDoEvent.interactable = true;
                btnViewConsequencesEvent.interactable = true;
            }
            else
                btnDoEvent.interactable = false;

            //btnDoEvent.gameObject.SetActive(true);
            activateDoEventButton();
            string[] txt = { "Consequenties", "Consequences "};
            txtEventColumRight.text = txt[taal];
            txtEventConsequencesChoice.text = getSectorStatisticsConsequences(gameEvent.pickedConsequences[0]); 
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
            if (game.GetMoney() >= gameEvent.afterInvestmentEventChoiceMoneyCost[1])
            {
                btnDoEvent.interactable = true;
                btnViewConsequencesEvent.interactable = true;
            }
            else
                btnDoEvent.interactable = false;

            //btnDoEvent.gameObject.SetActive(true);
            activateDoEventButton();
            string[] txt = { "Consequenties", "Consequences " };
            txtEventColumRight.text = txt[taal];
            txtEventConsequencesChoice.text = getSectorStatisticsConsequences(gameEvent.pickedConsequences[1]); 
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
            if (game.GetMoney() >= gameEvent.afterInvestmentEventChoiceMoneyCost[2])
            {
                btnDoEvent.interactable = true;
                btnViewConsequencesEvent.interactable = true;
            }
            else
                btnDoEvent.interactable = false;

            //btnDoEvent.gameObject.SetActive(true);
            activateDoEventButton();
            string[] txt = { "Consequenties", "Consequences " };
            txtEventColumRight.text = txt[taal];
            txtEventConsequencesChoice.text = getSectorStatisticsConsequences(gameEvent.pickedConsequences[2]); 
        }
        else
            radioEventOption3Check = false;

        checkIfAllFalse();
    }

    private void checkIfAllFalse()
    {
        if (!radioEventOption1Check && !radioEventOption2Check && !radioEventOption3Check)
        {
            btnDoEvent.gameObject.SetActive(false);
            txtEventColumRight.text = "";
            //btnDoEvent.interactable = false;
            btnViewConsequencesEvent.interactable = false;
            txtEventConsequencesChoice.text = "";
        }
    }

    private void activateDoEventButton()
    {
        if (ApplicationModel.multiplayer)
        {
            if (regionEvent.regionOwner == PhotonNetwork.player.NickName)
            {
                txtNotYourEvent.gameObject.SetActive(false);
                btnDoEvent.gameObject.SetActive(true);
            }
            else
            {
                btnDoEvent.gameObject.SetActive(false);
                txtNotYourEvent.gameObject.SetActive(true);
                string[] info = { "Omdat de event in de regio van de andere speler is kun jij hem niet oplossen.",
                "You cannot finish this event because it belongs to the region of the other player." };
                txtNotYourEvent.text = info[taal];
            }
        }
        else
        {
            btnDoEvent.gameObject.SetActive(true);
        }
    }
   
    public void finishEvent()
    {
        EventManager.CallPlayButtonClickSFX();
        int option;

        if (radioEventOption1Check)
            option = 0;
        else if (radioEventOption2Check)
            option = 1;
        else 
            option = 2;

        gameEvent.SetPickedChoice(option, game, regionEvent);
        if (ApplicationModel.multiplayer)
            playerController.photonView.RPC("EventChoiceMade", PhotonTargets.Others, regionEvent.name[0], gameEvent.name, option);
        
        canvasEventPopup.gameObject.SetActive(false);
        popupActive = false;
        EventManager.CallPopupIsDisabled();

        if (!game.tutorial.tutorialEventsDone)
            game.tutorial.tutorialEventsDone = true;

        if (game.tutorial.tutorialEventsActive)
            game.tutorial.tutorialEventsActive = false;
    }

    public void btnEventConsequencesClick()
    {
        imgEventConsequences.gameObject.SetActive(true);

        EventManager.CallPlayButtonClickSFX();
        int option;

        if (radioEventOption1Check)
            option = 0;
        else if (radioEventOption2Check)
            option = 1;
        else
            option = 2;

        if (taal == 0)
            txtEventConsequencesTitle.text = gameEvent.choicesDutch[option];
        else
            txtEventConsequencesTitle.text = gameEvent.choicesEnglish[option];

        txtEventConsequences.text = getSectorStatisticsConsequences(gameEvent.pickedConsequences[option]);
    }
    #endregion

    #region Code for Event Popup (Choice Made)
    public void initEventPopupChoiceMade(GameEvent e, MapRegion r)
    {
        canvasEventChoiceMadePopup.gameObject.SetActive(true);
        popupActive = true;
        EventManager.CallPopupIsActive();

        initEventChoiceMadeText(e, r);
    }

    private void initEventChoiceMadeText(GameEvent e, MapRegion r)
    {
        string[] header = { "Opgelost event", "Finished event" };
        txtEventChoiceMadeHeader.text = header[taal];
        string[] title = { "Overzicht opgelost event: " + e.publicEventName[0] + " (" + r.name[0] + ")", "Summary solved event: " + e.publicEventName[1] + " (" + r.name[1] + ")" };
        txtEventChoiceMadeTitle.text = title[taal];
        txtEventChoiceMadeInfo.text = "";

        txtEventChoiceMadeInfo.text = e.description[taal] + "\n\n";

        if (taal == 0)
            txtEventChoiceMadeInfo.text += "\n<b>Gekozen oplossing: </b>" + e.choicesDutch[e.pickedChoiceNumber];
        else
            txtEventChoiceMadeInfo.text += "\n<b>Chosen solution: </b>" + e.choicesEnglish[e.pickedChoiceNumber];

        int turnsLeft = (e.onEventStartYear * 12 + e.onEventStartMonth + e.eventDuration[e.pickedChoiceNumber]) - (game.currentYear * 12 + game.currentMonth);
        string[] duration = { "\n<b>Resterende tijd gekozen optie:</b> " + turnsLeft + " maanden", "\n<b>Remaining duration chosen option:</b> " + turnsLeft + " months" };
        txtEventChoiceMadeInfo.text += duration[taal];

        string[] c = { "\n\n<b>Consequenties: </b>", "\n\n<b>Consequences: </b>" };
        txtEventChoiceMadeInfo.text += c[taal] + getSectorStatisticsConsequences(e.pickedConsequences[e.pickedChoiceNumber]);
    }
    #endregion

    #region Code for Empty Building Popup
    public void initEmptyBuildingPopup(MapRegion r)
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

        string[] step = { "Dit is het gebouw menu. Je mag 1 gebouw in de regio neerzetten en elk gebouw heeft zijn eigen voordelen. Je kan een gebouw echter wel weer slopen om een ander gebouw neer te zetten. ",
            "This is the building menu. You can build 1 building in each region and each building has its own benefits. You can however destroy the building to build a different one."};
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialBuildings.text = step[taal];
        txtTutorialBuildingsbtn.text = txtBtn[taal];

        while (game.tutorial.tutorialIndex < 12)//tutorialStep16)
            yield return null;

        imgTutorialBuildings.gameObject.SetActive(false);
        game.tutorial.tutorialBuildingsActive = false;

        while (canvasEmptyBuildingsPopup.gameObject.activeSelf)
            yield return null;

        btnTutorialSmallNext.gameObject.SetActive(true);
        canvasTutorial.gameObject.SetActive(true);

        string[] step3 = {"Je bent nu klaar om het hele spel te spelen. \n\nDenk eraan dat de vervuiling 0% moet zijn voor 2050.",
            "You're now ready to play the game. \n\nDon't forget that the pollution needs to be below 0% before 2050." };
        string[] txtButton = { "Eindig handleiding", "Finish tutorial" };

        txtTutorialSmall.text = step3[taal];
        txtTutorialSmallBtn.text = txtButton[taal];

        popupActive = true;

        while (game.tutorial.tutorialIndex < 13)//tutorialStep17)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialBuildingsDone = true;
        btnNextTurn.interactable = true;
        popupActive = false;
    }

    private void initEmptyBuildingText()
    {
        btnUseBuilding.gameObject.SetActive(false);
        string[] btnTxt = { "Maak gebouw", "Build building" };
        btnUseBuildingTxt.text = btnTxt[taal];
        string[] title = { "Gebouwen", "Buildings" };
        string[] column = { "Plaats een gebouw", "Place a building" };
        string[] info = { "Kies hieronder het gebouw dat je wilt maken", "Chose the building you want to make" };

        txtEmptyBuildingsTitle.text = title[taal]; ;
        txtEmptyBuildingsColumnLeft.text = column[taal];
        txtEmptyBuildingColumRight.text = "";
        txtEmptyBuildingColumRight.text = "";
        txtEmptyBuildingStats.text = "";
        txtEmptyBuildingInfo.text = info[taal];
        txtNotYourEmptyBuilding.gameObject.SetActive(false);
        txtNotYourBuildingDestory.gameObject.SetActive(false);
        txtNewBuildingsStats.text = "";
        txtNewBuildingsColumn.text = "";
        txtBuildingAlreadyActive.gameObject.SetActive(false);
        btnNewDeleteBuilding.gameObject.SetActive(false);

        if (!ApplicationModel.multiplayer)
        {
            if (regionToBeBuild.activeBuilding != null)
            {
                initBuildingPopup(regionToBeBuild.activeBuilding, regionToBeBuild);
                btnUseBuilding.gameObject.SetActive(false);
                txtBuildingAlreadyActive.gameObject.SetActive(true);

                string[] active = { "Er staat al een gebouw in deze regio. Je moet eerst dit gebouw slopen voordat je een nieuwe kunt bouwen.",
                "There already exists a building in this region. You first have to destory the other building before building a new one." };
                txtBuildingAlreadyActive.text = active[taal];

            }
        }
        else
        {
            initBuildingPopup(regionToBeBuild.activeBuilding, regionToBeBuild);
            btnUseBuilding.gameObject.SetActive(false);
            txtBuildingAlreadyActive.gameObject.SetActive(false);
        }
    }

    private void setTextBuildingInformation(Building b)
    {
        buildingToBeBuild = b;

        btnUseBuilding.gameObject.SetActive(true);
        txtEmptyBuildingColumRight.text = "";
        txtEmptyBuildingStats.text = "";
        string[] column = { "Informatie - " + b.buildingName[0], "Information - " + b.buildingName[1] };
        txtEmptyBuildingColumRight.text = column[taal];

        string[] cost = { "Kosten: " + b.buildingMoneyCost + " geld\n", "Costs: " + b.buildingMoneyCost + " geld\n" };
        txtEmptyBuildingStats.text += cost[taal];

        txtEmptyBuildingStats.text += getBuildingModifiers(b);

        if (game.GetMoney() >= buildingToBeBuild.buildingMoneyCost)
            btnUseBuilding.interactable = true;
        else
            btnUseBuilding.interactable = false;

        if (regionToBeBuild.activeBuilding != null)
        {
            btnUseBuilding.gameObject.SetActive(false);

        }

        if (ApplicationModel.multiplayer)
        {
            if (regionToBeBuild.regionOwner != PhotonNetwork.player.NickName)
            {
                txtNotYourEmptyBuilding.gameObject.SetActive(true);
                btnUseBuilding.gameObject.SetActive(false);

                string[] txt = { "Dit is niet jouw regio, daarom kun je hier ook geen gebouw maken", "This is not your region, this means you can't build here." };
                txtNotYourEmptyBuilding.text = txt[taal];
            }
            else
            {
                txtNotYourEmptyBuilding.gameObject.SetActive(false);
                btnUseBuilding.gameObject.SetActive(true);
                string[] btnTxt = { "Maak gebouw", "Build building" };
                btnUseBuildingTxt.text = btnTxt[taal];
            }
        }
        else
        {
            if (regionToBeBuild.activeBuilding != null)
            {
                txtBuildingAlreadyActive.gameObject.SetActive(true);

                string[] active = { "Er staat al een gebouw in deze regio. Je moet eerst dit gebouw slopen voordat je een nieuwe kunt bouwen.",
                "There already exists a building in this region. You first have to destory the other building before building a new one." };
                txtBuildingAlreadyActive.text = active[taal];
            }
        }
    }

    // Afhandelen van BtnUseBuilding Click wordt in GameController gedaan

    #endregion

    #region Code for Active Building Popup
    public void initBuildingPopup(Building b, MapRegion r)
    {
        //txtNotYourBuilding.gameObject.SetActive(false);
        activeBuilding = b;
        buildingRegion = r;
        popupActive = true;
        EventManager.CallPopupIsActive();
        btnNewDeleteBuilding.gameObject.SetActive(false);
        initBuildingText();

        //canvasBuildingsPopup.gameObject.SetActive(true);
        //canvasEmptyBuildingsPopup.gameObject.SetActive(true);
        //initEmptyBuildingText();
    }

    private void initBuildingText()
    {
        string[] title = { "Gebouwen", "Buildings" };
        string[] column = { "", "" };
        if (buildingRegion.activeBuilding != null)
        {
            column = new string[2]{ "Actief gebouw: " + /*activeBuilding.buildingName[0]*/buildingRegion.activeBuilding.buildingName[0], "Active building: " + buildingRegion.activeBuilding.buildingName[1] };
        }

        string[] btn = { "Sloop gebouw", "Destroy building" };

        txtEmptyBuildingColumRight.text = "";
        txtNewBuildingsStats.text = "";
        //txtNewBuildingsTitle.text = title[taal];
        txtNewBuildingsColumn.text = column[taal];
        txtNewBtnDeleteBuilding.text = btn[taal];

        txtEmptyBuildingStats.text = "";
        btnUseBuilding.gameObject.SetActive(false);

        if (ApplicationModel.multiplayer)
        {
            if (buildingRegion.regionOwner != PhotonNetwork.player.NickName)
            {
                btnNewDeleteBuilding.gameObject.SetActive(false);
                txtNotYourBuildingDestory.gameObject.SetActive(true);
                string[] txt = { "Dit gebouw is niet gebouwd in jouw regio, daarom kun je er ook niks mee doen.",
                    "This building doesn't belong to your regions, this means you can't do anything with this building." };
                txtNotYourBuildingDestory.text = txt[taal];
            }
            else
            {
                btnNewDeleteBuilding.gameObject.SetActive(true);
                txtNotYourBuildingDestory.gameObject.SetActive(false);
            }
        }
        else
            btnNewDeleteBuilding.gameObject.SetActive(true);


        // string[] cost = { "Kosten:" + activeBuilding.buildingMoneyCost + "\n", "Cost:" + activeBuilding.buildingMoneyCost + "\n" };
        // txtBuildingsStats.text += cost[taal];

        string[] info = { "Effecten van " + activeBuilding.buildingName[0], "Effects caused by " + activeBuilding.buildingName[1] };
        txtNewBuildingsStats.text = info[taal];
        txtNewBuildingsStats.text += getBuildingModifiers(activeBuilding);

    }
    #endregion

    #region Code for Investments Popup
    private void initInvestementsText()
    {
        string[] title = { "Investeren", "Investments" };
        string[] column = { "Investeer in de organisatie", "Invest in the organization" };
        string[] description = { "Hier kun je geld investeren in je organisatie. Hoe meer geld je in een onderdeel investeert, hoe meer positief resultaat je zult zien.\nJe kunt 5 keer investeren in elk onderdeel. " +
                "Investeren kost 10000 per keer. Investeringen kun je niet terug draaien.",
            "You can invest money in your own organization. If you invest more in one of the segments you will see a larger positive result. You can invest 5 times in each segment. " + 
            "Investing will cost you 10000 each time. You can't rollback your investments."};
        string[] actievermindering = { "Kosten verlagen acties\nVerlaag de kosten voor elke actie met 10%.", "Cost reduction actions\nLowers the cost for all actions with 10%."};
        string[] actieconsequences = { "Betere consequenties acties\nVerbeter de consequenties bij elke actie met 10%.", "Better consequences actions\nImprove the consequencies for all actions with 10%." };
        string[] eventvermindering = { "Kosten verlagen events\nVerlaag de kosten voor elke event optie met 10%.", "Cost reduction events\nLowers the cost for all events options with 10%." };
        string[] eventconsequencies = { "Betere consequenties events\nVerbeter de consequenties bij elke event optie met 10%.", "Better consequences events\nImprove the consequencies for all event options with 10%." };

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

        string[] step = { "Je kunt investeren om de kosten en consequenties van acties en events te verlagen en te verbeteren. Je kan 5 keer investeren in elk onderdeel.\n\n " +
                "Investeren kost 10.000 geld per keer. \n\nAls je investeert kan je dit niet meer ongedaan maken.",
            "You can invest money to decrease the cost and increase the consequences of actions and events. You can invest 5 times in each option." +
            "An investment costs 10.000 money each time and increases the impact of the option by 10%. If you invest you can't undo this."};
        string[] txtBtn = { "Volgende", "Next" };

        txtTutorialInvestements.text = step[taal];
        txtTutorialInvestementsbtn.text = txtBtn[taal];

        while (game.tutorial.tutorialIndex < 11)//tutorialStep16)
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

    public void setActionCostReductionInvestments()
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

    public void setActionConsequencesInvestments()
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

    public void setEventCostReductionInvestments()
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

    public void setEventConsequencesInvestments()
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
        if (game.GetMoney() >= game.investments.investmentCost)
        {
            game.investments.InvestInActionCostReduction(game.regions);

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("InvestmentMade", PhotonTargets.Others, "Action cost reduction");
            setActionCostReductionInvestments();

            if (game.investments.actionCostReduction[4])
                btnInvestmentActionCostInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestActionConsequences()
    {
        if (game.GetMoney() >= game.investments.investmentCost)
        {
            game.investments.InvestInBetterActionConsequences(game.regions);

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("InvestmentMade", PhotonTargets.Others, "Better action consequences");
            setActionConsequencesInvestments();

            if (game.investments.betterActionConsequences[4])
                btnInvestmentActionConsequenceInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestEventCost()
    {
        if (game.GetMoney() >= game.investments.investmentCost)
        {
            game.investments.InvestInGameEventCostReduction(game.events);

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("InvestmentMade", PhotonTargets.Others, "Event cost reduction");
            setEventCostReductionInvestments();

            if (game.investments.gameEventCostReduction[4])
                btnInvestmentEventCostInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }
        updateInvestButtonsInteractable();
    }

    public void btnInvestEventConsequences()
    {
        if (game.GetMoney() >= game.investments.investmentCost)
        {
            game.investments.InvestInBetterGameEventConsequences(game.events);

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("InvestmentMade", PhotonTargets.Others, "Better event consequences");
            setEventConsequencesInvestments();

            if (game.investments.betterGameEventConsequences[4])
                btnInvestmentEventConsequenceInvest.gameObject.SetActive(false);

            game.gameStatistics.ModifyMoney(game.investments.investmentCost, false);
        }

        updateInvestButtonsInteractable(); 
    }

    private void updateInvestButtonsInteractable()
    {
        if (game.GetMoney() < game.investments.investmentCost)
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

        while (game.tutorial.tutorialIndex < 10)//tutorialStep16)
            yield return null;

        imgTutorialCards.gameObject.SetActive(false);
        game.tutorial.tutorialCardsActive = false;

        if (!game.tutorial.tutorialCardsDone)
        {
            while (canvasCardsPopup.gameObject.activeSelf)
                yield return null;
        }

        canvasTutorial.gameObject.SetActive(false);
        game.tutorial.tutorialeventsClickable = true;
        game.tutorial.tutorialNexTurnPossibe = true;
        game.tutorial.tutorialCardsDone = true;
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
        MapRegion cardRegion = new MapRegion();

        if (card.isGlobal)
            card.UseCardOnCountry(game.regions, game.gameStatistics);
        else
        {
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

        if (ApplicationModel.multiplayer)
        {
            SectorStatistics r = card.currentSectorConsequences;
            double[] reward = new double[11] { r.income, r.happiness, r.ecoAwareness,
                    r.prosperity, r.pollution.airPollution, r.pollution.naturePollution, r.pollution.waterPollution,
                    r.pollution.airPollutionIncrease, r.pollution.naturePollutionIncrease, r.pollution.waterPollutionIncrease, card.currentMoneyReward };

            playerController.photonView.RPC("CardUsed", PhotonTargets.Others, cardRegion.name[0], reward, card.isGlobal);
        }

        game.inventory.RemoveCardFromInventory(card);
        updateCardsUI();
    }
    #endregion

    #region Code for Timeline Popup
    private void initTimelinePopup()
    {
        string[] title = { "Tijdlijn", "Timeline" };
        string[] columnLeft = { "Kies beurt", "Choose turn" };
        string[] columnRight = { "", "" };
        string[] infoRight = { "", "" };
        string[] infoLeft = { "Kies de beurt waar je de statistieken van wilt zien", "Choose a turn to view the statistics from that month" };

        txtTimelineTitle.text = title[taal];
        txtTimelineColumnLeft.text = columnLeft[taal];
        txtTimelineColumnRight.text = columnRight[taal];
        txtColumnLeftInfo.text = infoLeft[taal];
        txtColumnRightInfo.text = infoRight[taal];

        initDropdownTimeline();
    }

    private void initDropdownTimeline()
    {
        dropdownTimeline.ClearOptions();
        dropdownTimeline.RefreshShownValue();

        int currentMonth = game.currentYear * 12 + game.currentMonth;

        string[] dropdownPlaceholderText = { "Kies een beurt", "Choose a turn" };
        dropdownTimeline.captionText.text = dropdownPlaceholderText[taal];

        foreach (int month in game.timeline.timeInMonths)
            dropdownTimeline.options.Add(new Dropdown.OptionData() { text = month.ToString() });

        //code to bypass Unity bug -> can't set .value outside the dropdown range
        dropdownTimeline.options.Add(new Dropdown.OptionData() { text = " " });
        dropdownTimeline.value = dropdownTimeline.options.Count - 1;
        dropdownTimeline.options.RemoveAt(dropdownTimeline.options.Count - 1);
    }

    public void getDropdownTimelineValue()
    {
        EventManager.CallPlayButtonClickSFX();
        for (int i = 0; i <= dropdownTimeline.options.Count; i++)
        {
            if (dropdownTimeline.value == i)
            {
                dropdownTimelinePick = dropdownTimeline.options[i].text;
                //break;
            }
        }

        // Shows the right information with the chosen option in dropdown
        showInfoDropdownTimeline();
    }

    private void showInfoDropdownTimeline()
    {
        string[] column = { "Statistieken uit beurt " + dropdownTimelinePick, "Statistics from turn " + dropdownTimelinePick };
        txtTimelineColumnRight.text = column[taal];
        int pick = 0;

        if (dropdownTimelinePick != null && dropdownTimelinePick.Trim() != "")
            pick = int.Parse(dropdownTimelinePick);

        foreach (double d in game.timeline.ecoAwarenessPerTurn)
        {
            txtColumnRightInfo.text += d.ToString() + "\n";
        }
    }
    #endregion

    #region Code for End of Game Popup
    public void initEndOfGameReport(double score)
    {
        popupActive = true;
        EventManager.CallPopupIsActive();
        canvasEndOfGame.gameObject.SetActive(true);

        string[] title = { "Einde van het spel", "End of the Game" };

        if (game.gameStatistics.pollution == 0)
        {
            string[] info = { "Gefeliciteerd, je hebt het doel van 0% vervuiling gehaald!\n\nDe score die je daarbij gehaald hebt is: " + score.ToString("0") +
                    ".\n\nHet is een belangrijke dag voor Nederland, en daarom voor de wereld!",
                "Congratulations, you have reached the goal off 0% pollution!\n\nYour score this game is: " + score.ToString("0") +
                ".\n\nIt's a great day for The Netherlands, and therefore the world!"};
            txtEndOfYearInfo.text = info[taal];
        }
        else
        {
            string[] info = { "Helaas, je hebt het doel van 0% vervuiling niet gehaald. Milieuvervuiling blijft een probleem voor Nederland en de rest van de wereld.\n\nDe score " + 
                    "die je gehaald hebt is: " + score.ToString("0") + ".\n\nHet is een treurige dag voor Nederland en daarom voor de wereld.",
                "Unfortunately you didn't reach the goal of 0% pollution. Environmental pollution still is a problem for The Netherlands and the rest of the world.\n\nYou reached a score off " +
                score.ToString("0") + ".\n\nIt's a sad day for The Netherlands and therefore the world."};
            txtEndOfYearInfo.text = info[taal];
        }

        string[] btn = { "Naar hoofdmenu", "Return to main menu" };
        string[] btnShare = { "Delen op Facebook", "Share on Facebook" };
        txtShareFacebookButton.text = btnShare[taal];
        txtEndOfGameTitle.text = title[taal];
        txtEndOfYearBtn.text = btn[taal];
    }

    public void btnEndOfYearReturnClick(int index)
    {
        canvasEndOfGame.gameObject.SetActive(false);
        EventManager.CallPlayButtonClickSFX();
        EventManager.CallLeaveGame();
        SceneManager.LoadSceneAsync(index);
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
            initTimelinePopup();
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive/* && tutorialStep8 */&& !game.tutorial.tutorialQuestsActive)
        {
            //SetLocalPlayerText("Organisatie aan het bekijken", "Looking at the Organization");
            //MultiplayerManager.CallUpdateLogMessage("Organisatie aan het bekijken", "Looking at the Organization");
            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Organisatie scherm aan het bekijken", "Looking at the Organization screen");

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
            //SetLocalPlayerText("Missies aan het bekijken", "Looking at Quests");
            //MultiplayerManager.CallUpdateLogMessage("Missies aan het bekijken", "Looking at Quests");
            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Missie scherm aan het bekijken", "Looking at Quest screen");

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
            //SetLocalPlayerText("Kaarten aan het bekijken", "Looking at Cards");
            //MultiplayerManager.CallUpdateLogMessage("Kaarten aan het bekijken", "Looking at Cards");

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Kaarten scherm aan het bekijken", "Looking at Cards screen");

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
            //SetLocalPlayerText("Maandelijks Rapport aan het bekijken", "Looking at The Monthly Report");
            //MultiplayerManager.CallUpdateLogMessage("Maandelijks Rapport aan het bekijken", "Looking at The Monthly Report");

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Maandelijks Rapport scherm aan het bekijken", "Looking at The Monthly Report screen");

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

            //SetLocalPlayerText("Jaarlijks Rapport aan het bekijken", "Looking at The Yearly Report");
            // MultiplayerManager.CallUpdateLogMessage("Jaarlijks Rapport aan het bekijken", "Looking at The Yearly Report");

            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Jaarlijks Rapport scherm aan het bekijken", "Looking at The Yearly Report screen");

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
            if (ApplicationModel.multiplayer)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Investeringen scherm aan het bekijken", "Looking at Investements screen");

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
        else if (canvasRegioPopup.gameObject.activeSelf && !game.tutorial.tutorialRegionActive && !imgSectorPopup.gameObject.activeSelf)
        {
            ClearActionMenu();
            canvasRegioPopup.gameObject.SetActive(false);
            popupActive = false;
            EventManager.CallPopupIsDisabled();
        }
        else if (canvasRegioPopup.gameObject.activeSelf && imgSectorPopup.gameObject.activeSelf)
        {
            imgSectorPopup.gameObject.SetActive(false);
        }
        else if (canvasEventPopup.gameObject.activeSelf && imgEventConsequences.gameObject.activeSelf)
        {
            imgEventConsequences.gameObject.SetActive(false);
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
        else if (canvasEventChoiceMadePopup.gameObject.activeSelf)
        {
            canvasEventChoiceMadePopup.gameObject.SetActive(false);
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


        //SetLocalPlayerText("Nederland aan het bekijken", "Looking at The Netherlands");
        //MultiplayerManager.CallUpdateLogMessage("Nederland aan het bekijken", "Looking at The Netherlands");

        if (ApplicationModel.multiplayer)
        {
            //if (!game.nextTurnIsclicked)
                playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Kaart van Nederland aan het bekijken", "Looking at the map of The Netherlands");
            //else
            //    playerController.photonView.RPC("PlayerLogChanged", PhotonTargets.Others, "Klaar voor de volgende beurt", "Ready for next turn");

        }

    }
    #endregion
   
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
        btnQuests.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);
    }

    public void btnQuestsExit()
    {
        btnQuestsCheck = false;
        btnQuests.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);
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
        btnOrganization.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);
    }

    public void btnOrganzationExit()
    {
        btnOrganizationCheck = false;
        btnOrganization.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);
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
        /*btnMonthlyReportStats.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);*/
    }

    public void btnMonthlyReportExit()
    {
        btnMonthlyReportCheck = false;
        /*btnMonthlyReportStats.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);*/
    }

    public void btnYearlyReportEnter()
    {
        btnYearlyReportCheck = true;
        /*btnYearlyReportStats.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);*/
    }

    public void btnYearlyReportExit()
    {
        btnYearlyReportCheck = false;
        /*btnYearlyReportStats.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);*/
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
        btnInvestments.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);
    }

    public void btnInvestmentsExit()
    {
        btnInvestementsHoverCheck = false;
        btnInvestments.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);
    }

    public void btnCardsEnter()
    {
        btnCardsHoverCheck = true;
        btnCards.transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);
    }

    public void btnCardsExit()
    {
        btnCardsHoverCheck = false;
        btnCards.transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);
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
            if (!ApplicationModel.multiplayer)
            {
                EventManager.CallChangeMonth();

                if (!game.tutorial.tutorialNextTurnDone)
                    game.tutorial.tutorialNextTurnDone = true;
            }

            else if (!game.nextTurnIsclicked)
            {
                if (!game.OtherPlayerClickedNextTurn)
                {
                    game.isWaiting = true;
                    StartCoroutine(NextTurnWaiting());
                }

                MultiplayerManager.CallNextTurnClick();
                btnNextTurn.interactable = false;
            }
        }
    }

    IEnumerator NextTurnWaiting()
    {
        string[] txt;
        while (game.isWaiting)
        {
            txt = new string[2] { "Wachten op andere speler", "Waiting for other player" };
            btnNextTurnText.text = txt[taal];

            for (int i = 0; i < 3; i++)
            {
                if (game.isWaiting) //neccesary so it won't write more dots while still in the for loop (when next turn starts)
                {
                    btnNextTurnText.text += ".";
                    yield return new WaitForSeconds((float)0.5);
                }
            }
        }

        //txt = new string[2] { "Volgende maand", "Next month" };
        //btnNextTurnText.text = txt[ApplicationModel.language];
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

        if (game.GetMoney() > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;

        txtRegionActionSectorTotalCost.text = regioActionCost.ToString() + availableMoney[taal];
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

        if (game.GetMoney() > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;

        txtRegionActionSectorTotalCost.text = regioActionCost.ToString() + availableMoney[taal];
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

        if (game.GetMoney() > regioActionCost)
            btnDoActionRegionMenu.interactable = true;
        else
            btnDoActionRegionMenu.interactable = false;

        txtRegionActionSectorTotalCost.text = regioActionCost.ToString() + availableMoney[taal];
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

        if (ApplicationModel.multiplayer)
            PhotonNetwork.LeaveRoom();

        PhotonNetwork.Disconnect();
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

        if (ApplicationModel.multiplayer)
            PhotonNetwork.LeaveRoom();

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
            EventManager.CallPlayButtonClickSFX();
            toggleDutchCheck = true;
            toggleEnglish.isOn = false;
            ApplicationModel.language = 0;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
            initSettingsText();

            ChangeLanguage("dutch");
            btnNextTurnText.text = nextTurnText[taal];
            txtBtnTimeline.text = "Tijdlijn";
            txtBtnMenu.text = "Menu";
            initButtonText();
            initOrganizationText();
            initRegionText();
            initInvestementsText();
            initCardsText();
        }
        else
            toggleDutchCheck = false;
    }

    public void toggleEnglishValueChanged()
    {
        if (!toggleEnglishCheck)
        {
            EventManager.CallPlayButtonClickSFX();
            toggleEnglishCheck = true;
            toggleDutch.isOn = false;
            ApplicationModel.language = 1;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
            initSettingsText();

            ChangeLanguage("english");
            btnNextTurnText.text = nextTurnText[taal];
            txtBtnTimeline.text = "Timeline";
            txtBtnMenu.text = "Menu";
            initButtonText();
            initOrganizationText();
            initRegionText();
            initInvestementsText();
            initCardsText();
        }
        else
            toggleEnglishCheck = false;
    }

    public void ChangeLanguage(string language)
    {
        if (language == "english")
            ApplicationModel.language = 1;
        else if (language == "dutch")
            ApplicationModel.language = 0;
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
        game.tutorial.tutorialIndex++;
    }
    
    #endregion

    #region Get SectorStatiticsConsequences Method
    private string getSectorStatisticsConsequences(SectorStatistics s)
    {
        bool noConsequences = false;

        string[] consequences = {"", "" };
        if (s.income != 0d)
        {
            /*
            if (s.income > 0d)
            {
                string[] a = { "<color=#00cc00>\nInkomen per geselecteerde sector <b>↑</b></color>", "<color=#00cc00>\nIncome per selected sector <b>↑</b></color>" };
                //a[taal] += "+" + s.income + "</color>";
                consequences[taal] += a[taal];
            }
            else
            {
                string[] a = { "<color=#FF0000>\nInkomen per geselecteerde sector <b>↓</b></color>", "<color=#FF0000>\nIncome per selected sector <b>↓</b></color>" };
                a[taal] += s.income + "</color>";
                consequences[taal] += a[taal];
            }
            */
            if (s.income > 0d)
            {
                string[] a = { "<color=#00cc00>\nInkomen per geselecteerde sector: ", "<color=#00cc00>\nIncome per selected sector: " };
                a[taal] += "+" + s.income + "</color>";
                consequences[taal] += a[taal];
            }
            else
            {
                string[] a = { "<color=#FF0000>\nInkomen per geselecteerde sector: ", "<color=#FF0000>\nIncome per selected sector: " };
                a[taal] += s.income + "</color>";
                consequences[taal] += a[taal];
            }

            noConsequences = true;
        }
        if (s.happiness != 0d)
        {
            if (s.happiness > 0d)
            {
                string[] c = { "<color=#00cc00>\nTevredenheid per geselecteerde sector: ", "\n<color=#00cc00>Happiness per selected sector: " };
                c[taal] += "+" + s.happiness.ToString("0.00") + "% </color>";
                consequences[taal] += c[taal];
            }
            else
            {
                string[] c = { "<color=#FF0000>\nTevredenheid per geselecteerde sector: ", "\n<color=#FF0000>Happiness per selected sector: " };
                c[taal] += s.happiness.ToString("0.00") + "% </color>";
                consequences[taal] += c[taal];
            }

            noConsequences = true;
        }
        if (s.ecoAwareness != 0d)
        {
            if (s.ecoAwareness > 0d)
            {
                string[] d = { "<color=#00cc00>\nMilieubewustheid per geselecteerde sector: ", "<color=#00cc00>\nEco awareness per selected sector: " };
                d[taal] += "+" + s.ecoAwareness.ToString("0.00") + "% </color>";
                consequences[taal] += d[taal];
            }
            else
            {
                string[] d = { "<color=#FF0000>\nMilieubewustheid per geselecteerde sector: ", "<color=#FF0000>\nEco awareness per selected sector: " };
                d[taal] += s.ecoAwareness.ToString("0.00") + "% </color>";
                consequences[taal] += d[taal];
            }

            noConsequences = true;
        }
        if (s.prosperity != 0d)
        {
            if (s.prosperity > 0d)
            {
                string[] e = { "<color=#00cc00>\nWelvaart per geselecteerde sector: ", "<color=#00cc00>\nProsperity per selected sector: " };
                e[taal] += "+" + s.prosperity.ToString("0.00") + "% </color>";
                consequences[taal] += e[taal];
            }
            else
            {
                string[] e = { "<color=#FF0000>\nWelvaart per geselecteerde sector: ", "<color=#FF0000>\nProsperity per selected sector: " };
                e[taal] += s.prosperity.ToString("0.00") + "% </color>";
                consequences[taal] += e[taal];
            }

            noConsequences = true;
        }
        if (s.pollution.airPollutionIncrease != 0d)
        {
            if (s.pollution.airPollutionIncrease > 0d)
            {
                string[] f = { "<color=#FF0000>\nJaarlijkse luchtvervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#FF0000>\nYearly water pollution increase (of current) per selected sector: " };
                f[taal] += "+" + s.pollution.airPollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += f[taal];
            }
            else
            {
                string[] f = { "<color=#00cc00>\nJaarlijkse luchtvervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#00cc00>\nYearly air pollution increase (of current) per selected sector: " };
                f[taal] += s.pollution.airPollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += f[taal];
            }

            noConsequences = true;
        }
        if (s.pollution.waterPollutionIncrease != 0d)
        {
            if (s.pollution.waterPollutionIncrease > 0d)
            {
                string[] g = { "<color=#FF0000>\nJaarlijkse watervervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#FF0000>\nYearly water pollution increase (of current) per selected sector: " };
                g[taal] += "+" + s.pollution.waterPollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += g[taal];
            }
            else
            {
                string[] g = { "<color=#00cc00>\nJaarlijkse watervervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#00cc00>\nYearly water pollution increase (of current) per selected sector: " };
                g[taal] += s.pollution.waterPollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += g[taal];
            }

            noConsequences = true;
        }
        if (s.pollution.naturePollutionIncrease != 0d)
        {
            if (s.pollution.naturePollutionIncrease > 0d)
            {
                string[] h = { "<color=#FF0000>\nJaarlijkse natuurvervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#FF0000>\nYearly nature pollution increase (of current) per selected sector: " };
                h[taal] += "+" + s.pollution.naturePollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += h[taal];
            }
            else
            {
                string[] h = { "<color=#00cc00>\nJaarlijkse natuurvervuiling verhoging (van huidige) per geselecteerde sector: ", "<color=#00cc00>\nYearly nature pollution increase (of current) per selected sector: " };
                h[taal] += s.pollution.naturePollutionIncrease.ToString("0.00") + "% </color>";
                consequences[taal] += h[taal];
            }

            noConsequences = true;
        }


        if (s.pollution.airPollution != 0d)
        {
            if (s.pollution.airPollution > 0d)
            {
                string[] f = { "<color=#FF0000>\nLuchtvervuiling per geselecteerde sector: ", "<color=#FF0000>\nAir pollution per selected sector: " };
                f[taal] += "+" + s.pollution.airPollution.ToString("0.00") + "% </color>";
                consequences[taal] += f[taal];
            }
            else
            {
                string[] f = { "<color=#00cc00>\nLuchtvervuiling per geselecteerde sector: ", "<color=#00cc00>\nAir pollution per selected sector: " };
                f[taal] += s.pollution.airPollution.ToString("0.00") + "% </color>";
                consequences[taal] += f[taal];
            }

            noConsequences = true;
        }
        if (s.pollution.waterPollution != 0d)
        {
            if (s.pollution.waterPollution > 0d)
            {
                string[] g = { "<color=#FF0000>\nWatervervuiling per geselecteerde sector: ", "<color=#FF0000>\nWater pollution per selected sector: " };
                g[taal] += "+" + s.pollution.waterPollution.ToString("0.00") + "% </color>";
                consequences[taal] += g[taal];
            }
            else
            if (s.pollution.waterPollution > 0d)
            {
                string[] g = { "<color=#00cc00>\nWatervervuiling per geselecteerde sector: ", "<color=#00cc00>\nWater pollution per selected sector: " };
                g[taal] += s.pollution.waterPollution.ToString("0.00") + "% </color>";
                consequences[taal] += g[taal];
            }

            noConsequences = true;
        }
        if (s.pollution.naturePollution != 0d)
        {
            if (s.pollution.naturePollution > 0d)
            {
                string[] h = { "<color=#FF0000>\nNatuurvervuiling per geselecteerde sector: ", "<color=#FF0000>\nNature pollution per selected sector: " };
                h[taal] += "+" + s.pollution.naturePollution.ToString("0.00") + "% </color>";
                consequences[taal] += h[taal];
            }
            else
            {
                string[] h = { "<color=#00cc00>\nNatuurvervuiling per geselecteerde sector: ", "<color=#00cc00>\nNature pollution per selected sector: " };
                h[taal] += s.pollution.naturePollution.ToString("0.00") + "% </color>";
                consequences[taal] += h[taal];
            }

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

            if (b.happinessModifier > 0d)
            {
                string[] a = { "<color=#00cc00>\nTevredenheid in regio: ", "<color=#00cc00>\nHappiness in region: " };
                a[taal] += "+" + b.happinessModifier + "% </color>";
                modifiers[taal] += a[taal];
            }
            else
            {
                string[] a = { "<color=#FF0000>\nTevredenheid in regio: ", "<color=#FF0000>\nHappiness in region: " };
                a[taal] += "+" + b.happinessModifier + "% </color>";
                modifiers[taal] += a[taal];
            }

        }
        if (b.incomeModifier != 0d)
        {
            noConsequences = false;

            if (b.incomeModifier > 0d)
            {
                string[] a = { "<color=#00cc00>\nInkomen in regio: ", "<color=#00cc00>\nIncome in region: " };
                a[taal] += "+" + b.incomeModifier + "% </color>";
                modifiers[taal] += a[taal];
            }
            else
            {
                string[] a = { "<color=#FF0000>\nInkomen in regio: ", "<color=#FF0000>\nIncome in region: " };
                a[taal] += b.incomeModifier + "% </color>";
                modifiers[taal] += a[taal];
            }

        }
        if (b.pollutionModifier != 0d)
        {
            noConsequences = false;

            if (b.pollutionModifier > 0d)
            {
                string[] a = { "<color=#FF0000>\nVervuilig in regio: ", "<color=#FF0000>\nPollution in region: " };
                a[taal] += "+" + b.pollutionModifier + "% </color>";
                modifiers[taal] += a[taal];
            }
            else
            {
                string[] a = { "<color=#00cc00>\nVervuilig in regio: ", "<color=#00cc00>\nPollution in region: " };
                a[taal] += b.pollutionModifier + "% </color>";
                modifiers[taal] += a[taal];
            }

        }

        if (noConsequences)
        {
            string[] nc = { "Er zijn geen aanpassingen met dit gebouw.", "This building doesn't change any values" };
            modifiers[taal] += nc[taal];
        }

        return modifiers[taal];
    }
    #endregion

    #region Code for Multiplayer
    public void SetRemotePlayerText(string nl, string eng)
    {
        string[] txtMultiplayerPlayerInfo = { "", "" };

        string[] txt = { "\n" + PhotonNetwork.playerList[0].NickName + " - " + nl
                    , "\n" + PhotonNetwork.playerList[0].NickName + " - " + eng };

        txtMultiplayerPlayerInfo[taal] += txt[taal];
        txtMultiplayerRemotePlayer.text = txtMultiplayerPlayerInfo[taal];
    }

    public void inputChatMessagesValueChanged()
    {
        inputChatMessage.text.Trim();

        if (inputChatMessage.text != "")
        {
            txtChatMessageToSend = inputChatMessage.text;
            btnSendChatMessage.gameObject.SetActive(true);
            string[] txtBtn = { "Stuur", "Send" };
            txtSendMessageButton.text = txtBtn[taal];
        }
        else
            btnSendChatMessage.gameObject.SetActive(false);
    }

    public void btnSendChatMessageClicked()
    {
        if (ApplicationModel.multiplayer)
            playerController.photonView.RPC("MessageReceived", PhotonTargets.Others, txtChatMessageToSend, PhotonNetwork.player.NickName);

        updateChatMessages(txtChatMessageToSend, PhotonNetwork.player.NickName);
        inputChatMessage.text = "";
        btnSendChatMessage.gameObject.SetActive(false);
    }

    public void updateChatMessages(string message, string sender)
    {
        if (message != null)
        {
            txtChatMessages.text = "";
            string txt = sender + ": " + message + "\n";

            if (lstMessages.Count < 6)
                lstMessages.Add(txt);
            else
            {
                lstMessages.RemoveAt(0);
                lstMessages.Add(txt);
            }

            foreach (string text in lstMessages)
            {
                txtChatMessages.text += text;
            }

            txtChatMessageToSend = null;
            inputChatMessage.text = "";
        }
    }

    public void UpdateActivityLogText(string nl, string eng)
    {
        txtActivityLog.text = "";
        if (taal == 0)
        {
            //string[] txt = { PhotonNetwork.playerList[0].NickName + nl + "\n", PhotonNetwork.playerList[0].NickName + eng + "\n" };
            updateChatMessages(PhotonNetwork.playerList[0].NickName + nl, "LOG");

        }
        else
        {
            //string[] txt = { PhotonNetwork.playerList[0].NickName + nl + "\n", PhotonNetwork.playerList[0].NickName + eng + "\n" };
            updateChatMessages(PhotonNetwork.playerList[0].NickName + eng, "LOG");
        }

        /*
        txtActivityLog.text = "";
        string[] txt = { PhotonNetwork.playerList[0].NickName + nl + "\n", PhotonNetwork.playerList[0].NickName + eng + "\n" };

        if (lstText.Count < 4)
            lstText.Add(txt);
        else
        {
            lstText.RemoveAt(0);
            lstText.Add(txt);
        }

        foreach (string[] text in lstText)
        {
            Debug.Log("Foreach");
            txtActivityLog.text += text[taal];
        }
        */
    }
    #endregion
}