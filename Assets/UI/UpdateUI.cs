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
    public GUIStyle tooltipStyle = new GUIStyle();

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


    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;
    public Canvas canvasRegioPopup;
    #endregion

    // Tooltip Variables
    private string txtTooltip;

    #region Boolean Variables
    // Booleans
    public bool btnMoneyHoverCheck;
    public bool btnHappinessHoverCheck;
    public bool btnAwarenessHoverCheck;
    public bool btnPollutionHoverCheck;
    public bool btnEnergyHoverCheck;
    public bool popupActive;
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

        setBooleans();
    }

    void setBooleans()
    {
        btnMoneyHoverCheck = false;
        btnHappinessHoverCheck = false;
        btnAwarenessHoverCheck = false;
        btnPollutionHoverCheck = false;
        btnEnergyHoverCheck = false;
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
        if (Input.GetKeyUp(KeyCode.Escape))
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

    void checkWhichRegion(Region regio)
    {
        if (regio.name == "Noord Nederland")
        {
            txtRegionName.text = "Noord-Nederland";
            updateRegionScreenUI(regio);
        }
        if (regio.name == "Oost Nederland")
        {
            txtRegionName.text = "Oost-Nederland";
            updateRegionScreenUI(regio);
        }
        if (regio.name == "Zuid Nederland")
        {
            txtRegionName.text = "Zuid-Nederland";
            updateRegionScreenUI(regio);
        }
        if (regio.name == "West Nederland")
        {
            txtRegionName.text = "West-Nederland";
            updateRegionScreenUI(regio);
        }
    }

    void updateRegionScreenUI(Region regio)
    {
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
        
        
    }

    void updateRegionScreenUIOost(Region regio)
    {

    }

    void updateRegionScreenUIZuid(Region regio)
    {

    }

    void updateRegionScreenUIWest(Region regio)
    {

    }
    #endregion

    #region Code for activating popups
    public void regionClick(Region region)
    {
        //Debug.Log(region.name + "'s Eco-awareness = " + region.statistics.ecoAwareness);
        if (!canvasRegioPopup.gameObject.activeSelf && !popupActive)
        {
            canvasRegioPopup.gameObject.SetActive(true);
            Debug.Log("Popup Regio active!");
            popupActive = true;

            checkWhichRegion(region);
        }
    }

    public void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            Debug.Log("Popup Timeline active!");
            popupActive = true;
        }
    }

    public void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            Debug.Log("Popup Organization active!");
            popupActive = true;
        }
    }

    // Method that makes popup appear/disappear
    public void btnMenuClick()
    {
        // Temporary close of popup
        if (!canvasMenuPopup.gameObject.activeSelf && !popupActive)
        {
            canvasMenuPopup.gameObject.SetActive(true);
            Debug.Log("Popup Menu active!");
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
    #endregion






    /*        Vector3 v3;

            Color backColor = new Color();
            Color txtColor = new Color();
            string txtPopup = "";

            ColorUtility.TryParseHtmlString("#ccac6f", out backColor);
            ColorUtility.TryParseHtmlString("#05001a", out txtColor);

            Debug.Log("Arrived in Tooltip Money!");
            v3 = btnMoney.gameObject.transform.position;
            txtPopup = "This is a tooltip for Money stats";
            GUI.Box(new Rect(v3.x, (v3.y - 25), 200, 100), "Tooltip Money");
            GUI.backgroundColor = backColor;

            GUI.Label(new Rect(10, 10, 100, 100), txtPopup);
            //txtPopup.color = txtColor;*/

    /*  Use this code when OnPointerEnter works!
        Vector3 v3;
        Text txtPopup = null;
        Color backColor = new Color();
        Color txtColor = new Color();

        ColorUtility.TryParseHtmlString("#ccac6f", out backColor);
        ColorUtility.TryParseHtmlString("#05001a", out txtColor);

        GameObject obj = eventData.selectedObject;

        if (eventData.pointerEnter.gameObject == objBtnMenu)
            Debug.Log("Arrived in 1st Tooltip Menu!");

        if (eventData.pointerEnter.gameObject == btnMenu.gameObject)
            Debug.Log("Arrived in 2nd Tooltip Menu!");

        if (eventData.pointerEnter == objBtnMenu)
            Debug.Log("Arrived in 3rd Tooltip Menu!");

        if (eventData.pointerEnter == btnMenu)
            Debug.Log("Arrived in 4th Tooltip Menu!");

        if (eventData.pointerEnter == btnMenu.gameObject)
            Debug.Log("Arrived in 5th Tooltip Menu!");

        if (eventData.pointerEnter.Equals(btnMenu))
            Debug.Log("Arrived in 6th Tooltip Menu!");

        if (obj == btnMenu.gameObject)
            Debug.Log("Arrived in 7th Tooltip Menu!");

        /* Make tooltip appear
        if (eventData.pointerEnter.gameObject == objBtnMoney)//btnMoney.gameObject)
        {
            Debug.Log("Arrived in Tooltip Money!");
            v3 = btnMoney.gameObject.transform.position;
            txtPopup.text = "This is a tooltip for Money stats";
            GUI.Box(new Rect(v3.x, (v3.y - 25), 200, 100), "Tooltip Money");
            GUI.backgroundColor = backColor;

            GUI.Label(new Rect(10, 10, 100, 100), txtPopup.text);
            txtPopup.color = txtColor;
        }
        if (eventData.pointerEnter == btnHappiness)
        {

        }
        if (eventData.pointerEnter == btnAwareness)
        {

        }
        if (eventData.pointerEnter == btnEnergy)
        {

        }
        if (eventData.pointerEnter == btnPollution)
        {

        }
        if (eventData.pointerEnter == btnPopulation)
        {

        }
        */
}


