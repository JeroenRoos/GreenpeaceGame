﻿using System.Collections;
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
    Game game;
    public int tutorialIndex;

    public Dropdown dropdownRegio;

    public Toggle checkboxRegionHouseholds;
    private bool checkboxHouseholds;

    public Toggle checkboxRegionAgriculture;
    private bool checkboxAgriculture;

    public Toggle checkboxRegionCompanies;
    private bool checkboxCompanies;

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
    public Canvas canvasTutorial;

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
    public Image imgTutorialRegion;
    public Text txtTutorialRegion;
    public Text txtTurorialReginoBtnText;
    public Button btnTutorialRegion;

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
    public bool popupActive;

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

        if (tooltipActive)
        {
            tutorialNoTooltip = true;
            regionWestActivated = false;
            imgTutorialStep2Highlight1.enabled = false;
            imgTutorialStep2Highlight2.enabled = false;
            tutorialIndex = 1;
            StartCoroutine(initTutorialText());
        }
        else
        {
            tutorialStep2 = true;
            tutorialStep3 = true;
            tutorialStep4 = true;
            tutorialStep5 = true;
            tutorialStep6 = true;
            tutorialStep7 = true;
            regionWestActivated = true;
            tutorialCheckActionDone = true;
        }
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
        string[] step2 = { "Het doel is om de vervuiling in het land onder 5% te hebben in 2050. Zoals je kunt zien zitten we nu in 2030. " +
                "Je hebt dus 30 jaar om dit doel te halen.", "The goal is to get pollution under 5% before 2050. As you can see the current year is 2030. " +
                "This means you have 30 years to reach this goal. "};
        txtTurorialStep1.text = step2[taal];
        imgTutorialStep2Highlight1.enabled = true;
        imgTutorialStep2Highlight2.enabled = true;

        while (!tutorialStep3)
            yield return null;

        //tutorialStep3 = false;
        string[] step3 = { "Dit zijn jouw resources om het doel te behalen. Geld wordt gebruikt om jouw beslissingen te financieren. Tevredenheid bepaald of het " +
                "volk besluit om mee te werken met jouw beslissingen. Milieubewustheid zorgt ervoor dat er minder wordt vervuilt. Veruiling geeft de vervuiling in het land weer. " + 
                "Tot slot wordt er getoont hoeveel mensen er in Nederland wonen. Al deze iconen geven het gemiddelde van de verschillende regio's weer. "+ 
                "Als je meer informatie over de statistieken wil hebben kun je met je muis eroverheen gaan. Er verschijnt dan een tooltip met de extra informatie."
                , "Here are the resources that help you achieve your goal. Money is used for financing the decisions you make. Happiness determines whether people cooperate or not" +
                "A better Eco awareness means less pollution. The pollutions hows the pollution in the country. These icons show the averages from the different regions. " + 
                "For more informations about these statistics you can hover of the icon with your mouse. You can see the extra information in the tooltip."};
        txtTurorialStep1.text = step3[taal];
        imgTutorialStep2Highlight1.enabled = false;
        imgTutorialStep2Highlight2.enabled = false;

        while (!tutorialStep4)
            yield return null;

        //tutorialStep4 = false;
        string[] step4 = { "Het land bestaat uit 4 regio's. Noord-Nederland, Oost-Nederland, Zuid-Nederland en West-Nederland. " +
                "Elke regio heeft een inkomen, tevredenheid, vervuiling, milieubewustheid en werlvaart. Deze statistieken verschillen weer per regio.\nGa naar West-Nederland door op de regio te klikken. "
                , "There are 4 regions, The Netherlands North, The Netherlands East, The Netherlands South and The Netherland West" +
                "Each region has a income, happiness, pollution, eco-awareness and prosperity. These statistics differ foreach region.\n Go to The Netherlands West by clicking on the region. "};
        txtTurorialStep1.text = step4[taal];

        while (!tutorialStep5) // && !regionWestActivated)
            yield return null;

        canvasTutorial.gameObject.SetActive(false);

        while (!tutorialCheckActionDone && canvasRegioPopup.gameObject.activeSelf)
            yield return null;

        
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
            initButtonText();
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
                string[] tip1 = { "Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + awareness.ToString("0.00") + "%\n",
                    "Average eco awareness per region: \nThe Netherlands Northern: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip = tip1[taal];//"Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + awareness.ToString("0.00") + "%\n",
                    "The Netherlands Eastern: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + awareness.ToString("0.00") + "\n",
                    "The Netherlands Western: " + awareness.ToString("0.00") + "%\n"};
                txtTooltip += tip3[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + awareness.ToString("0.00") + "%",
                    "The Netherlands Southern: " + awareness.ToString("0.00") + "%"};
                txtTooltip += tip4[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
        }
    }

    public void updatePollutionTooltip(double pollution, int i)
    {
        switch (i)
        {
            case 0:
                string[] tip1 = { "Gemiddelde vervuiling per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "Average pollution per region: \nThe Netherlands Northern: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip = tip1[taal];//"Gemiddelde milieubewustheid per regio:\nNoord-Nederland: " + pollution.ToString("0.00") + "\n";
                break;
            case 1:
                string[] tip2 = { "Oost-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "The Netherlands Eastern: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip += tip2[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 2:
                string[] tip3 = { "West-Nederland: " + pollution.ToString("0.00") + "%\n",
                    "The Netherlands Western: " + pollution.ToString("0.00") + "%\n"};
                txtTooltip += tip3[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
            case 3:
                string[] tip4 = { "Zuid-Nederland: " + pollution.ToString("0.00") + "%",
                    "The Netherlands Southern: " + pollution.ToString("0.00") + "%"};
                txtTooltip += tip4[taal];//"Oost-Nederland: " + awareness.ToString("0.00") + "\n";
                break;
        }
    }

    public void updateEnergyTooltip(double green, double fossil, double nuclear)
    {
        string[] tip = { "Groene energie: " + green.ToString() + "%\nFossiele energie: "
            + fossil + "%\nKernenergie: " + nuclear + "%",
            "Green energy " + green.ToString() + "%\nFossil energy: "
            + fossil + "%\nNuclearenergy: " + nuclear + "%"};
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
        initOtherText();
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
                StartCoroutine(tutorialRegionPopup());
            }
        }
        else if (!canvasRegioPopup.gameObject.activeSelf && !popupActive && !btnOrganizationCheck
        && !btnMenuCheck && !btnTimelineCheck && !tutorialActive)
        {
            startRegionPopup(region);
            imgTutorialRegion.enabled = false;
            txtTutorialRegion.enabled = false;
            btnTutorialRegion.gameObject.SetActive(false);
        }
    }

    private void startRegionPopup(Region region)
    {
        regio = null;
        regio = region;
        canvasRegioPopup.gameObject.SetActive(true);
        popupActive = true;
        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();
        updateRegionScreenUI();
    }

    IEnumerator tutorialRegionPopup()
    {
        string[] step1 = { "Elke regio bestaat uit 3 sectoren. Deze sectoren zijn Huishoudens, Landbouw en Bedrijven. De sectoren hebben statistieken voor " +
                "tevredenheid, vervuiling, milieubewustheid en welvaart. Deze sectoren statistieken maken het gemiddelde waar de regio statistieken uit bestaan. Je kunt deze secor statistieken zien door ." +
                "met je muis over de sector te hoveren."
                , "Each region has 3 sectors. These sectors are Households, Agriculture and Companies. These sectors have statistics for happiness, pollution, eco awareness and prosperity. " +
                "These sector statistics create the averages which are the region statistics. It is important to keep each sector happy. You can view these sector statistics by using your mouse to hover over the sector. "};
        string[] btnText = { "Verder", "Next" };

        txtTutorialRegion.text = step1[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        while (!tutorialStep6)
            yield return null;

        string[] step2 = { "Je kunt in een regio acties uitvoeren. Acties kosten echter geld en meestal ook tijd. Je kunt maar 1 actie teglijk doen in een regio. " + 
                "Sommige acties kunnen ook maar 1 keer of eens in de zoveel tijd gedaan worden. Als je een actie kiest krijg je een aantal gegevens over de actie te zien. Daarnaast kun je kiezen " +
                "op welke sectoren je de actie invloed uitoefend. Sommige acties kunnen in elke sector gedaan worden, andere in 1 of 2 van de sectoren. Kies nu een actie, keer vervolgens terug naar de landkaart door op de ESC toets te drukken."
                , "You can do actions in regions. These actions cost money and most of the time also time. You can do 1 action at the time in a region. " +
                "Soms actions you can only do once, others you can do again after some time. When you chose an action you can see a few statistics about the action. You also have to chosoe in which sectors you want the action to do things. " +
                "Some actions can be done in each sectors, others only in 1 or 2 of the sectors. Choose an action, after that, return to the map by pressing the ESC key."};

        txtTutorialRegion.text = step2[taal];
        txtTurorialReginoBtnText.text = btnText[taal];

        while (!tutorialStep7)
            yield return null;

        imgTutorialRegion.enabled = false;
        txtTutorialRegion.enabled = false;
        btnTutorialRegion.gameObject.SetActive(false);
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

        txtRegionHappinessDescription.text = txtHappiness[taal];
        txtRegionEcoAwarenessDescription.text = txtEcoAwareness[taal];
        txtRegionIncomeDescription.text = txtIncome[taal];
        txtRegionPollutionDescription.text = txtPollution[taal];
        txtRegionAirDescription.text = txtAir[taal];
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
        dropdownRegio.ClearOptions();
        dropdownRegio.RefreshShownValue();

        // Debug.Log("updateRegionTextValues: " + regio.name);
        txtRegionName.text = regio.name[taal];
        txtRegionMoney.text = regio.statistics.income.ToString();
        txtRegionHappiness.text = regio.statistics.happiness.ToString();
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString() + "%";
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
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution + "%\nTevredenheid: " + sector.statistics.happiness
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness + "%\nWelvaart: " + sector.statistics.prosperity  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution + "%\nWater pollution: " + sector.statistics.pollution.waterPollution
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution + "%\nHappiness: " + sector.statistics.happiness
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness + "%\nProsperity: " + sector.statistics.prosperity  + "%"};
                txtTooltipHouseholds = tip[taal];       /*"Luchtvervuiling: " + sector.statistics.airPollutionContribution + "\nWatervervuiling: " + sector.statistics.waterPollutionContribution
                                                        + "\nNatuurvervuiling: " + sector.statistics.naturePollutionContribution + "\nTevredenheid: " + sector.statistics.happiness
                                                        + "\nMilieubewustheid: " + sector.statistics.ecoAwareness + "\nWelvaart: " + sector.statistics.prosperity;*/

    }
            else if (sector.sectorName[taal] == "Bedrijven" || sector.sectorName[taal] == "Companies")
            {
               // Debug.Log(sector.sectorName);
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution + "%\nTevredenheid: " + sector.statistics.happiness
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness + "%\nWelvaart: " + sector.statistics.prosperity  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution + "%\nWater pollution: " + sector.statistics.pollution.waterPollution
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution + "%\nHappiness: " + sector.statistics.happiness
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness + "%\nProsperity: " + sector.statistics.prosperity  + "%"};
                txtTooltipCompany = tip[taal];
            }
            else if (sector.sectorName[taal] == "Landbouw" || sector.sectorName[taal] == "Agriculture")
            {
                //Debug.Log(sector.sectorName);
                string[] tip = { "Luchtvervuiling: " + sector.statistics.pollution.airPollution + "%\nWatervervuiling: " + sector.statistics.pollution.waterPollution
                    + "%\nNatuurvervuiling: " + sector.statistics.pollution.naturePollution + "%\nTevredenheid: " + sector.statistics.happiness
                    + "%\nMilieubewustheid: " + sector.statistics.ecoAwareness + "%\nWelvaart: " + sector.statistics.prosperity  + "%",

                    "Air pollution: " + sector.statistics.pollution.airPollution + "%\nWater pollution: " + sector.statistics.pollution.waterPollution
                    + "%\nNature pollution: " + sector.statistics.pollution.naturePollution + "%\nHappiness: " + sector.statistics.happiness
                    + "%\nEco-awareness: " + sector.statistics.ecoAwareness + "%\nProsperity: " + sector.statistics.prosperity + "%"};

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
                foreach (Region region in game.regions.Values)
                {
                    foreach (GameEvent ev in region.inProgressGameEvents)
                    {
                        if (ev == e)
                        {
                            activeEventsRegio += e.name + "\n";
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

        foreach (RegionAction action in regio.actions)
        {
            dropdownRegio.options.Add(new Dropdown.OptionData() { text = action.name[taal] });
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

        // Shows the right information with the chosen option in dropdown
        showInfoDropDownRegion();
    }

    private void showInfoDropDownRegion()
    {
        foreach (RegionAction action in regio.actions)
        {
            if (action.name[taal] == dropdownChoice)
            {
                dropdownChoiceMade = true;
                regioAction = action;
                txtRegionActionName.text = regioAction.description[taal];
                txtRegionActionCost.text = getActionCost(action.actionCosts); 
                txtRegionActionDuration.text = "Duur: " + regioAction.actionDuration.ToString() + " maanden";
                txtRegionActionConsequences.text = getActionConsequences(action.consequences);
                string[] SectorDescription = { "Mogelijke sectoren", "Possible sectors" };
                txtActionSectorsDescription.text = SectorDescription[taal];

                setCheckboxes(action);

                if (game.gameStatistics.money > action.actionMoneyCost)
                {
                    btnDoActionRegionMenu.interactable = true;
                    txtRegionActionNoMoney.text = "";
                }
                else
                {
                    btnDoActionRegionMenu.interactable = false;
                    string[] error2 = { "Niet genoeg geld om de actie te doen", "You don't have enough money for this action" };
                    txtRegionActionNoMoney.text = "Niet genoeg geld om de actie te doen...";
                }
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

    private string getActionConsequences(SectorStatistics s)
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
    }

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
        regio.StartAction(regioAction, game, new bool[] { checkboxHouseholds, checkboxRegionCompanies, checkboxRegionAgriculture });

        updateRegionTextValues();
        btnDoActionRegionMenu.gameObject.SetActive(false);
        checkboxRegionAgriculture.gameObject.SetActive(false);
        checkboxRegionHouseholds.gameObject.SetActive(false);
        checkboxRegionCompanies.gameObject.SetActive(false);

        if (!checkboxHouseholds)
            checkboxRegionHouseholds.isOn = true;

        if (!checkboxAgriculture)
            checkboxRegionAgriculture.isOn = true;

        if (!checkboxCompanies)
            checkboxRegionCompanies.isOn = true;

        if (!tutorialCheckActionDone)
            tutorialCheckActionDone = true;

    }
    #endregion

    #region Code for activating popups
    public void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            popupActive = true;
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            popupActive = true;
        }
    }

    public void btnMenuClick()
    {
        if (!canvasMenuPopup.gameObject.activeSelf && !popupActive && !tutorialActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            popupActive = true;
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

    public void buttonExitGameOnClick()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }

    public void loadOtherScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void nextTurnOnClick()
    {
        game.NextTurn();
    }

    public void valueChangedHouseholds()
    {
        Debug.Log("Value of Households has changed!");

        if (!checkboxHouseholds)
            checkboxHouseholds = true;
        else
            checkboxHouseholds = false;

        Debug.Log("Households: " + checkboxHouseholds);
    }

    public void valueChangedAgriculture()
    {
        Debug.Log("Value of Agriculture has changed!");

        if (!checkboxAgriculture)
            checkboxAgriculture = true;
        else
            checkboxAgriculture = false;

        Debug.Log("Agriculture: " + checkboxAgriculture);
    }

    public void valueChangedCompanies()
    {
        Debug.Log("Value of Companies has changed!");

        if (!checkboxCompanies)
            checkboxCompanies = true;
        else
            checkboxCompanies = false;

        Debug.Log("Companies: " + checkboxCompanies);
    }

    public void btnResumeMenu()
    {
        canvasMenuPopup.gameObject.SetActive(false);
        popupActive = false;
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
    #endregion

}