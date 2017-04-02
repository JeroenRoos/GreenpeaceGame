using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                      
using System;

public class UpdateUI : MonoBehaviour
{
    #region UI Elements
    // Tooltip texture and GUI
    public Texture2D tooltipTexture;
    private GUIStyle tooltipStyle = new GUIStyle();
    Region regio;
    RegionAction regioAction;
    Game game;

    public Dropdown dropdownRegio;

    // Text Main UI
    public Text txtMoney;
    public Text txtPopulation;
    public Text txtDate;

    // Text Organization Menu
    public Text txtOrgNoordMoney;
    public Text txtOrgOostMoney;
    public Text txtOrgZuidMoney;
    public Text txtOrgWestMoney;
    public Text txtOrgBank;

    private int taal;
    //  double totalOrgBank;

    // Text Region Menu
    public Text txtRegionName;
    public Text txtRegionHappiness;
    public Text txtRegionAwareness;
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


    // Buttons 
    public Button btnMenu;
    public Button btnTimeline;
    public Button btnOrganization;
    public Button btnMoney;
    public Button btnHappiness;
    public Button btnAwareness;
    public Button btnEnergy;
    public Button btnPollution;
    public Button btnPopulation;
    public Button btnDoActionRegionMenu;
    public Button emptybtnHoverHouseholds;
    public Button emptybtnHoverAgriculture;
    public Button emptybtnHoverCompanies;
    public Button[] investDemonstrations;
    public Button[] investResearch;
    public Button[] investEcoGuarding;

    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;
    public Canvas canvasRegioPopup;

    // Tooltip Variables
    private string txtTooltip;
    private string txtTooltipCompany;
    private string txtTooltipAgriculture;
    private string txtTooltipHouseholds;
    private string dropdownChoice;
    // string txttooltipPollution;

    private Vector3 v3Tooltip;

    #endregion

    #region Boolean Variables
    // Booleans
    private bool btnMoneyHoverCheck;
    private bool btnHappinessHoverCheck;
    private bool btnAwarenessHoverCheck;
    private bool btnPollutionHoverCheck;
    private bool btnEnergyHoverCheck;
    private bool btnOrganizationCheck;
    private bool btnMenuCheck;
    private bool btnTimelineCheck;
    private bool popupActive;
    private bool tooltipActive;
    private bool regionHouseholdsCheck;
    private bool regionAgricultureCheck;
    private bool regionCompanyCheck;
    #endregion

    #region Start(), Update(), FixedUpdate()
    // Use this for initialization
    void Start()
    {
        initButtons();
        initCanvas();
        tooltipStyle.normal.background = tooltipTexture;
        taal = game.language;
    }

    void Update()
    {
        popupController();
        //Debug.Log(taal);
    }

    void FixedUpdate()
    {

    }
    #endregion

    #region Init UI Elements
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
        //btnDoActionRegionMenu.gameObject.SetActive(false);

        setBooleans();
    }

    void setBooleans()
    {
        btnMoneyHoverCheck = false;
        btnHappinessHoverCheck = false;
        btnAwarenessHoverCheck = false;
        btnPollutionHoverCheck = false;
        btnEnergyHoverCheck = false;
        btnOrganizationCheck = false;
        btnTimelineCheck = false;
        btnMenuCheck = false;
        popupActive = false;
        regionHouseholdsCheck = false;
        tooltipActive = false;
        regionAgricultureCheck = false;
        regionCompanyCheck = false;
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
            controllerOrganizationHotkey();

        // Open and close Timeline popup with T
        else if (Input.GetKeyUp(KeyCode.T))
            controllerTimelinePopup();
    }

    // Close the active popup with the Escape key (and open main menu with escape if no popup is active)
    void closeWithEscape()
    {
        if (!popupActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
        }
        else if (canvasOrganizationPopup.gameObject.activeSelf)
        {
            canvasOrganizationPopup.gameObject.SetActive(false);
            popupActive = false;
        }
        else if (canvasMenuPopup.gameObject.activeSelf)
        {
            canvasMenuPopup.gameObject.SetActive(false);
            popupActive = false;
        }
        else if (canvasTimelinePopup.gameObject.activeSelf)
        {
            canvasTimelinePopup.gameObject.SetActive(false);
            popupActive = false;
        }
        else if (canvasRegioPopup.gameObject.activeSelf)
        {
            canvasRegioPopup.gameObject.SetActive(false);
            popupActive = false;
        }
    }


    // Open and close the Organization popup with the O key
    void controllerOrganizationHotkey()
    {
        if (!popupActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
        }
        else if (canvasOrganizationPopup.gameObject.activeSelf)
        {
            canvasOrganizationPopup.gameObject.SetActive(false);
            popupActive = false;
        }
    }

    // Open and close the Timeline popup with the T key
    void controllerTimelinePopup()
    {
        if (!popupActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
        }
        else if (canvasTimelinePopup.gameObject.activeSelf)
        {
            canvasTimelinePopup.gameObject.SetActive(false);
            popupActive = false;
        }
    }
    #endregion

    #region onGUI Code
    void OnGUI()
    {
        Rect lblReqt;

        lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

        if (checkTooltip())
        {
            lblReqt.x = v3Tooltip.x + 10; lblReqt.y = v3Tooltip.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (regionHouseholdsCheck)
        {
            v3Tooltip = emptybtnHoverHouseholds.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + -20;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipHouseholds + "</color>", tooltipStyle);
            updateRegionSectors();
            
        }
        else if (regionAgricultureCheck)
        {
            v3Tooltip = emptybtnHoverAgriculture.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 100;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltipAgriculture + "</color>", tooltipStyle);
            updateRegionSectors();
        }
        else if (regionCompanyCheck)
        {
            v3Tooltip = emptybtnHoverCompanies.gameObject.transform.position;
            lblReqt.x = v3Tooltip.x + 50; lblReqt.y = v3Tooltip.y + 220;
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
        iconController(btnAwareness, awareness);
    }

    // Update Pollution based on value
    public void updatePollution(double pollution)
    {
        iconController(btnPollution, pollution);
    }

    // Update Energy based on value
    public void updateEnergy(double energy)
    {
        iconController(btnEnergy, energy);
    }

    // Update Happiness based on value
    public void updateHappiness(double happiness)
    {
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
    public void updateMoneyTooltip(double donations, double income)
    {
        string[] tip = { "Donaties: " + donations + "\nInkomen: " + income,
            "Donations: " + donations + "\nIncome: " + income };
        txtTooltip = tip[taal];                 //"Donaties: " + donations + "\nInkomen: " + income;
    }

    public void updateHappinessTooltip(double happ, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip = { "Gemiddelde tevredenheid per regio:\nNoord-Nederland: "+ happ.ToString("0.00") + "\n",
                    "Average happiness per region:\nThe Netherlands Northen: "+ happ.ToString("0.00") + "\n" };
                txtTooltip = tip[taal];//"Gemiddelde tevredenheid per regio:\nNoord-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + happ.ToString("0.00") + "\n",
                    "The Netherlands Eastern: " + happ.ToString("0.00") + "\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + happ.ToString("0.00") + "\n",
                    "The Netherlands Western: " + happ.ToString("0.00") + "\n"};
                txtTooltip += tip3[taal];//"West-Nederland: " + happ.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + happ.ToString("0.00"),
                    "The Netherlands Southern: " + happ.ToString("0.00") };
                txtTooltip += tip4[taal];//"Zuid-Nederland: " + happ.ToString("0.00");
                break;
        }
    }

    public void updateAwarnessTooltip(double awareness, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip1 = { "Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + awareness.ToString("0.00") + "\n",
                    "Average eco awareness per region: \nThe Netherlands Northern: " + awareness.ToString("0.00") + "\n"};
                txtTooltip = tip1[taal];//"Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + awareness.ToString("0.00") + "\n",
                    "The Netherlands Eastern: " + awareness.ToString("0.00") + "\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + awareness.ToString("0.00") + "\n",
                    "The Netherlands Western: " + awareness.ToString("0.00") + "\n"};
                txtTooltip += tip3[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + awareness.ToString("0.00"),
                    "The Netherlands Southern: " + awareness.ToString("0.00")};
                txtTooltip += tip4[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
        }
    }

    public void updatePollutionTooltip(double pollution, int i)
    {
        switch (i)
        {
            /*
            case 0:
                txtTooltip = "Gemiddelde vervuiling per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "\n";
                break;
            case 1:
                txtTooltip += "Oost-Nederland: " + pollution.ToString("0.00") + "\n";
                break;
            case 2:
                txtTooltip += "West-Nederland: " + pollution.ToString("0.00") + "\n";
                break;
            case 3:
                txtTooltip += "Zuid-Nederland: " + pollution.ToString("0.00");
                break;*/
            case 0:
                string[] tip1 = { "Gemiddelde vervuiling per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "\n",
                    "Average pollution per region: \nThe Netherlands Northern: " + pollution.ToString("0.00") + "\n"};
                txtTooltip = tip1[taal];//"Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + pollution.ToString("0.00") + "\n",
                    "The Netherlands Eastern: " + pollution.ToString("0.00") + "\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + pollution.ToString("0.00") + "\n",
                    "The Netherlands Western: " + pollution.ToString("0.00") + "\n"};
                txtTooltip += tip3[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + pollution.ToString("0.00"),
                    "The Netherlands Southern: " + pollution.ToString("0.00")};
                txtTooltip += tip4[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
        }
    }

    public void updateEnergyTooltip(double green, double fossil, double nuclear)
    {
        string[] tip = { "Groene energie: " + green.ToString() + "\nFossiele energie: "
            + fossil + "\nKernenergie: " + nuclear ,
            "Green energy " + green.ToString() + "\nFossil energy: "
            + fossil + "\nNuclearenergy: " + nuclear};
        txtTooltip = tip[taal];         //"Groene energie: " + green.ToString() + "\nFossiele energie: "
                                        // + fossil + "\nKernenergie: " + nuclear;
    }
    #endregion

    #region Update UI in Popups
    public void updateOrganizationScreenUI(double value, int i, double money)
    {
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
    }
    #endregion

    #region Code for the Region Screen
    public void regionClick(Region region)
    {
        if (!canvasRegioPopup.gameObject.activeSelf && !popupActive && !btnOrganizationCheck
            && !btnMenuCheck && !btnTimelineCheck)
        {
            regio = null;
            regio = region;

            canvasRegioPopup.gameObject.SetActive(true);
            popupActive = true;

            updateRegionScreenUI();
        }
    }

    void updateRegionScreenUI()
    {
        // Debug.Log("UpdateRegionScreenUI: " + regio.name);
        updateRegionTextValues();

        // Set the right actions in the dropdown
        initDropDownRegion();
    }

    void updateRegionTextValues()
    {
        // Debug.Log("updateRegionTextValues: " + regio.name);
        txtRegionName.text = regio.name[taal];
        txtRegionHappiness.text = regio.statistics.happiness.ToString();
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString();
        txtRegionPollution.text = regio.statistics.pollution.avgPullution.ToString("0.00");
        txtRegionPollutionAir.text = regio.statistics.pollution.airPollution.ToString();
        txtRegionPollutionNature.text = regio.statistics.pollution.naturePollution.ToString();
        txtRegionPollutionWater.text = regio.statistics.pollution.waterPollution.ToString();

        // Set text of actions to empty
        txtRegionActionConsequences.text = "";
        txtRegionActionCost.text = "";
        txtRegionActionDuration.text = "";
        txtRegionActionName.text = "";

        updateActiveActions();
    }

    void updateRegionSectors()
    {
        foreach (RegionSector sector in regio.sectors.Values)
        {
            if (sector.sectorName[taal] == "Huishoudens" || sector.sectorName[taal] == "Households")
            {
                Debug.Log(sector.sectorName);
                string[] tip = { "Luchtvervuiling: " + sector.statistics.airPollutionContribution + "\nWatervervuiling: " + sector.statistics.waterPollutionContribution
                    + "\nNatuurvervuiling: " + sector.statistics.naturePollutionContribution + "\nTevredenheid: " + sector.statistics.happiness
                    + "\nMilieubewustheid: " + sector.statistics.ecoAwareness + "\nWelvaart: " + sector.statistics.prosperity ,

                    "Air pollution: " + sector.statistics.airPollutionContribution + "\nWater pollution: " + sector.statistics.waterPollutionContribution
                    + "\nNature pollution: " + sector.statistics.naturePollutionContribution + "\nHappiness: " + sector.statistics.happiness
                    + "\nEco-awareness: " + sector.statistics.ecoAwareness + "\nProsperity: " + sector.statistics.prosperity };
                txtTooltipHouseholds = tip[taal];       /*"Luchtvervuiling: " + sector.statistics.airPollutionContribution + "\nWatervervuiling: " + sector.statistics.waterPollutionContribution
                                                        + "\nNatuurvervuiling: " + sector.statistics.naturePollutionContribution + "\nTevredenheid: " + sector.statistics.happiness
                                                        + "\nMilieubewustheid: " + sector.statistics.ecoAwareness + "\nWelvaart: " + sector.statistics.prosperity;*/

            }
            else if (sector.sectorName[taal] == "Bedrijven" || sector.sectorName[taal] == "Companies")
            {
                Debug.Log(sector.sectorName);
                string[] tip = { "Luchtvervuiling: " + sector.statistics.airPollutionContribution + "\nWatervervuiling: " + sector.statistics.waterPollutionContribution
                    + "\nNatuurvervuiling: " + sector.statistics.naturePollutionContribution + "\nTevredenheid: " + sector.statistics.happiness
                    + "\nMilieubewustheid: " + sector.statistics.ecoAwareness + "\nWelvaart: " + sector.statistics.prosperity ,

                    "Air pollution: " + sector.statistics.airPollutionContribution + "\nWater pollution: " + sector.statistics.waterPollutionContribution
                    + "\nNature pollution: " + sector.statistics.naturePollutionContribution + "\nHappiness: " + sector.statistics.happiness
                    + "\nEco-awareness: " + sector.statistics.ecoAwareness + "\nProsperity: " + sector.statistics.prosperity };
                txtTooltipCompany = tip[taal];
            }
            else if (sector.sectorName[taal] == "Landbouw" || sector.sectorName[taal] == "Agriculture")
            {
                Debug.Log(sector.sectorName);
                string[] tip = { "Luchtvervuiling: " + sector.statistics.airPollutionContribution + "\nWatervervuiling: " + sector.statistics.waterPollutionContribution
                    + "\nNatuurvervuiling: " + sector.statistics.naturePollutionContribution + "\nTevredenheid: " + sector.statistics.happiness
                    + "\nMilieubewustheid: " + sector.statistics.ecoAwareness + "\nWelvaart: " + sector.statistics.prosperity ,

                    "Air pollution: " + sector.statistics.airPollutionContribution + "\nWater pollution: " + sector.statistics.waterPollutionContribution
                    + "\nNature pollution: " + sector.statistics.naturePollutionContribution + "\nHappiness: " + sector.statistics.happiness
                    + "\nEco-awareness: " + sector.statistics.ecoAwareness + "\nProsperity: " + sector.statistics.prosperity };

                txtTooltipAgriculture = tip[taal];
            }
        }
    }

    void updateActiveActions()
    {
        string activeActionsRegio = "";
        foreach (RegionAction action in regio.actions)
        {
            if (action.isActive)
            {
                Debug.Log(action.description + " is active!");
                activeActionsRegio += action.description + "\n";
            }

            txtActiveActions.text = activeActionsRegio;
        }
    }

    void initDropDownRegion()
    {
        // Debug.Log("initDropDownRegion: " + regio.name);
        dropdownRegio.ClearOptions();

        foreach (RegionAction action in regio.actions)
        {
            dropdownRegio.options.Add(new Dropdown.OptionData() { text = action.description[taal] });
        }
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

        // Debug.Log("getDropDownValue: " + regio.name);

        // Shows the right information with the chosen option in dropdown
        showInfoDropDownRegion();
    }

    void showInfoDropDownRegion()
    {
        // Debug.Log("showInfoDropDownRegion: " + regio.name);
        foreach (RegionAction action in regio.actions)
        {
            if (action.description[taal] == dropdownChoice)
            {
                regioAction = action;
                txtRegionActionName.text = regioAction.description[0];
                txtRegionActionCost.text = regioAction.actionCosts.ToString();
                txtRegionActionDuration.text = regioAction.actionDuration.ToString();
                txtRegionActionConsequences.text = regioAction.consequences.ToString();

                btnDoActionRegionMenu.gameObject.SetActive(true);
            }
        }
    }

    public void btnDoActionRegionMenuClick()
    {
        // Debug.Log("btnDoActionRegionMenuClick: " + regio.name);
        //Debug.Log("Year: " + regioAction.startYear.GetValueOrDefault() + " Month: " + regioAction.startMonth.GetValueOrDefault());

        regio.StartAction(regioAction, game.currentYear, game.currentMonth, this.game);

        updateRegionTextValues();
        btnDoActionRegionMenu.gameObject.SetActive(false);
    }
    #endregion

    #region Code for activating popups
    public void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
        }
    }

    public void btnMenuClick()
    {
        if (!canvasMenuPopup.gameObject.activeSelf && !popupActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
        }
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
        }
    }

    public void btnENGClick()
    {
        if (taal != 1)
        {
            Debug.Log("BTN ENG");
            game.ChangeLanguage("english");
            taal = game.language;
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
}

