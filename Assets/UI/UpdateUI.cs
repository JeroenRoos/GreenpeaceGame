﻿using System.Collections;
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
    double totalOrgBank;

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


    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;
    public Canvas canvasRegioPopup;

    // Tooltip Variables
    private string txtTooltip;
    private string dropdownChoice;
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
    #endregion

    #region Start(), Update(), FixedUpdate()
    // Use this for initialization
    void Start()
    {
        initButtons();
        initCanvas();
        tooltipStyle.normal.background = tooltipTexture;
    }
    
    void Update()
    {
        popupController();
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

        btnDoActionRegionMenu.gameObject.SetActive(false);

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
        Vector3 v3;
        Rect lblReqt;

        if (btnMoneyHoverCheck)
        {
            v3 = btnMoney.gameObject.transform.position;
            lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

            lblReqt.x = v3.x + 10; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (btnHappinessHoverCheck)
        {
            v3 = btnHappiness.gameObject.transform.position;
            lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (btnAwarenessHoverCheck)
        {
            v3 = btnAwareness.gameObject.transform.position;
            lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (btnPollutionHoverCheck)
        {
            v3 = btnPollution.gameObject.transform.position;
            lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (btnEnergyHoverCheck)
        {
            v3 = btnEnergy.gameObject.transform.position;
            lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }
    }
    #endregion

    #region Updating Text and Color Values of Icons
    // Update Date and Month based on value
    public void updateDate(int month, int year)
    {
        month = month - 1;
        string[] arrMonths = new string[12]
            { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

        txtDate.text = arrMonths[month] + " - " + year.ToString();
    }

    // Update Money based on value
    public void updateMoney(double money)
    {
        txtMoney.text = money.ToString();
    }

    // Update Population based on value
    public void updatePopulation(double population)
    {
        txtPopulation.text = population.ToString();
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
        float f = (float)value / 100;
        cb = btn.colors;

        // Pollution moet laag zijn om goed te zijn, de rest hoog
        if (btn == btnPollution)
        {
            // Color based on third argument (value / 100)
            Color lerpColor = Color.Lerp(Color.green, Color.red, f);
            cb.normalColor = lerpColor;
            btn.colors = cb;
        }
        else
        {
            // Color based on third argument (value / 100)
            Color lerpColor = Color.Lerp(Color.red, Color.green, f);
            cb.normalColor = lerpColor;
            btn.colors = cb;
        }
    }
    #endregion

    #region Update UI in Tooltips
    public void updateMoneyTooltip()
    {
        txtTooltip = "Juiste waardes moeten nog gemaakt worden!";
    }

    public void updateHappinessTooltip()
    {
        txtTooltip = "Juiste waardes moeten nog gemaakt worden!";
    }

    public void updateAwarnessTooltip()
    {
        txtTooltip = "Juiste waardes moeten nog gemaakt worden!";
    }

    public void updatePollutionTooltip()
    {
        txtTooltip = "Juiste waardes moeten nog gemaakt worden!";
    }

    public void updateEnergyTooltip(double green, double fossil, double nuclear)
    {
        txtTooltip = "Groene energie: " + green.ToString() + "\nFossiele energie: "
            + fossil + "\nKernenergie: " + nuclear;
    }
    #endregion

    #region Update UI in Popups
    public void updateOrganizationScreenUI(double value, int i)
    {
        // Region are made in following order: North > East > West > South
        switch (i)
        {
            case 0:
                txtOrgNoordMoney.text = value.ToString();
                totalOrgBank += value;
                break;
            case 1:
                txtOrgOostMoney.text = value.ToString();
                totalOrgBank += value;
                break;
            case 2:
                txtOrgWestMoney.text = value.ToString();
                totalOrgBank += value;
                break;
            case 3:
                txtOrgZuidMoney.text = value.ToString();
                totalOrgBank += value;
                break;
        }

        txtOrgBank.text = totalOrgBank.ToString();

        // Reset to 0 to recalculate the next time popups opens
        if (i == 3)
            totalOrgBank = 0;
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
        Debug.Log("UpdateRegionScreenUI: " + regio.name);
        updateRegionTextValues();

        foreach (RegionAction action in regio.actions)
        {
            // ... 
        }

        // Set the right actions in the dropdown
        initDropDownRegion();
    }

    void updateRegionTextValues()
    {
        Debug.Log("updateRegionTextValues: " + regio.name);
        txtRegionName.text = regio.name;
        txtRegionHappiness.text = regio.statistics.happiness.ToString();
        txtRegionAwareness.text = regio.statistics.ecoAwareness.ToString();
        txtRegionPollution.text = regio.statistics.pollution.avgPullution.ToString("0.0");
        txtRegionPollutionAir.text = regio.statistics.pollution.airPollution.ToString();
        txtRegionPollutionNature.text = regio.statistics.pollution.naturePollution.ToString();
        txtRegionPollutionWater.text = regio.statistics.pollution.waterPollution.ToString();
        txtRegionTraffic.text = regio.statistics.publicTransport.ToString();
        txtRegionFarming.text = "Comming soon!";
        txtRegionCompanies.text = "Comming soon!";
        // Ik ga ervanuit dat cityEviroment huishoudens is
        txtRegionHouseholds.text = regio.statistics.cityEnvironment.ToString();

        // Set text of actions to empty
        txtRegionActionConsequences.text = "";
        txtRegionActionCost.text = "";
        txtRegionActionDuration.text = "";
        txtRegionActionName.text = "";
    }

    void initDropDownRegion()
    {
        Debug.Log("initDropDownRegion: " + regio.name);
        dropdownRegio.ClearOptions();

        foreach (RegionAction action in regio.actions)
        {
            dropdownRegio.options.Add(new Dropdown.OptionData() { text = action.description });
        }
    }

    // Goes to this method from DropDownTrigger in Inspector
    public void getDropDownValue()
    {
        for (int i = 0; i <= dropdownRegio.options.Count; i++)
        {
            if (dropdownRegio.value == i)
                dropdownChoice = dropdownRegio.options[i].text;
        }

        Debug.Log("getDropDownValue: " + regio.name);

        // Shows the right information with the chosen option in dropdown
        showInfoDropDownRegion();
    }

    void showInfoDropDownRegion()
    {
        Debug.Log("showInfoDropDownRegion: " + regio.name);
        foreach (RegionAction action in regio.actions)
        {
            if (action.description == dropdownChoice)
            {
                regioAction = action;
                txtRegionActionName.text = action.description;
                txtRegionActionCost.text = action.actionCosts.ToString();
                txtRegionActionDuration.text = action.actionDuration.ToString();
                txtRegionActionConsequences.text = action.consequences.ToString();

                btnDoActionRegionMenu.gameObject.SetActive(true);
            }
        }
    }

    public void btnDoActionRegionMenuClick()
    {
        Debug.Log("btnDoActionRegionMenuClick: " + regio.name);
        //Debug.Log("Year: " + regioAction.startYear.GetValueOrDefault() + " Month: " + regioAction.startMonth.GetValueOrDefault());

        regio.StartAction(regioAction.description, regioAction.startYear.GetValueOrDefault(), regioAction.startMonth.GetValueOrDefault());

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

    #region Mouse Enter & Exit Code for Icons
    // OnEnter BtnMoney
    public void BtnMoneyEnter()
    {
        btnMoneyHoverCheck = true;
    }

    // OnExit BtnMoney
    public void BtnMoneyExit()
    {
        btnMoneyHoverCheck = false;
    }


    // OnEnter BtnHappiness
    public void BtnHappinessEnter()
    {
        btnHappinessHoverCheck = true;
    }

    // OnExit BtnHappiness
    public void BtnHappinessExit()
    {
        btnHappinessHoverCheck = false;
    }


    // OnEnter BtnAwareness
    public void BtnAwarenessEnter()
    {
        btnAwarenessHoverCheck = true;
    }

    // OnExit BtnAwareness
    public void BtnAwarenessExit()
    {
        btnAwarenessHoverCheck = false;
    }


    // OnEnter BtnPollution
    public void BtnPollutionEnter()
    {
        btnPollutionHoverCheck = true;
    }

    // OnExit BtnPollution
    public void BtnPollutionExit()
    {
        btnPollutionHoverCheck = false;
    }


    // OnEnter BtnEnergy
    public void BtnEnergyEnter()
    {
        btnEnergyHoverCheck = true;
    }

    // OnExit BtnEnergy
    public void BtnEnergyExit()
    {
        btnEnergyHoverCheck = false;
    }

    public void btnOrganizationEnter()
    {
        btnOrganizationCheck = true;
    }

    public void btnOrganzationExit()
    {
        btnMenuCheck = false;
    }

    public void btnMenuEnter()
    {
        btnMenuCheck = true;
    }

    public void btnMenuExit()
    {
        btnOrganizationCheck = false;
    }

    public void btnTimelineEnter()
    {
        btnTimelineCheck = true;
    }

    public void btnTimelineExit()
    {
        btnTimelineCheck = false;
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
    #endregion
}


