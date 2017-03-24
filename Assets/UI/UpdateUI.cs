using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // Needed for UI
using System;

public class UpdateUI : MonoBehaviour
{
    #region UI Elements
    // Text
    public Text txtMoney;
    public Text txtPopulation;
    public Text txtDate;

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
    public List<Button> lstButtons = new List<Button>();

    // Canvas 
    public Canvas canvasMenuPopup;
    public Canvas canvasOrganizationPopup;
    public Canvas canvasTimelinePopup;

    // Rectangles
    public Rect mainMenuRect;

    public Color newColor;
    #endregion

    #region Boolean Variables
    // Booleans
    public bool btnMoneyHoverCheck = false;
    public bool btnHappinessHoverCheck = false;
    public bool btnAwarenessHoverCheck = false;
    public bool btnPollutionHoverCheck = false;
    public bool btnEnergyHoverCheck = false;
    public bool popupActive = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        initButtons();
        initCanvas();
    }

    #region Init UI Elements
    public void initButtons()
    {
        btnMenu.GetComponent<Button>();
        btnMenu.onClick.AddListener(btnMenuClick);

        btnTimeline.GetComponent<Button>();
        btnTimeline.onClick.AddListener(btnTimelineClick);

        btnOrganization.GetComponent<Button>();
        btnOrganization.onClick.AddListener(btnOrganizationClick);

        btnMoney.GetComponent<Button>();
        btnHappiness.GetComponent<Button>();
        btnAwareness.GetComponent<Button>();
        btnEnergy.GetComponent<Button>();
        btnPollution.GetComponent<Button>();
        btnPopulation.GetComponent<Button>();

        addButtonToList();
    }

    void addButtonToList()
    {
        lstButtons.Add(btnHappiness);
        lstButtons.Add(btnAwareness);
        lstButtons.Add(btnEnergy);
        lstButtons.Add(btnPollution);
    }

    public void initCanvas()
    {
        canvasMenuPopup.GetComponent<Canvas>();
        canvasMenuPopup.gameObject.SetActive(false);

        canvasOrganizationPopup.GetComponent<Canvas>();
        canvasOrganizationPopup.gameObject.SetActive(false);

        canvasTimelinePopup.GetComponent<Canvas>();
        canvasTimelinePopup.gameObject.SetActive(false);
    }
    #endregion

    // Update is called once per frame
    void FixedUpdate()
    {
        popupController();
    }

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
        }
    }

    public void updateDate(int month, int year)
    {
        string[] arrMonths = new string[12]
            { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };

        txtDate.text = arrMonths[month] + " - " + year.ToString();
    }

    public void updateMoney(double money)
    {
        txtMoney.text = money.ToString();
    }

    public void updatePopulation(double population)
    {
        txtPopulation.text = population.ToString();
    }

    public void updateAwarness(double awareness)
    {
        iconController(btnAwareness, awareness);
    }

    public void updatePollution(double pollution)
    {
        iconController(btnPollution, pollution);
    }

    public void updateEnergy(double energy)
    {
        iconController(btnEnergy, energy);
    }

    public void updateHappiness(double happiness)
    {
        iconController(btnHappiness, happiness);
    }

    void iconController(Button btn, double value)
    {
        ColorBlock cb;
        float f = (float)value / 100;
        cb = btn.colors;

        // Color based on third argument (value / 100)
        Color lerpColor = Color.Lerp(Color.red, Color.green, f);
        cb.normalColor = lerpColor;
        btn.colors = cb;
    }

    void OnGUI()
    {
        Vector3 v3;

        //Color backColor = new Color();
        //Color txtColor = new Color();
        //ColorUtility.TryParseHtmlString("#05001a", out backColor);
        //ColorUtility.TryParseHtmlString("#ccac6f", out txtColor);
        string txtPopup = "";
        //Text text = txt.GetComponent<Text>();
       // txt.text = txtPopup;

        if (btnMoneyHoverCheck)
        {
            v3 = btnMoney.gameObject.transform.position;
            txtPopup = "This is a tooltip for Money stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Money");

            //GUI.Box(new Rect(v3.x, v3.z - 40, lblReqt.width, lblReqt.height), "Tooltip Money");
            lblReqt.x = v3.x + 10; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnHappinessHoverCheck)
        {
            v3 = btnHappiness.gameObject.transform.position;
            txtPopup = "This is a tooltip for Happiness stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Happiness");

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnAwarenessHoverCheck)
        {
            v3 = btnAwareness.gameObject.transform.position;
            txtPopup = "This is a tooltip for Awareness stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Awareness");

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;//lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnPollutionHoverCheck)
        {
            v3 = btnPollution.gameObject.transform.position;
            txtPopup = "This is a tooltip for Pollution stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Pollution");

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnEnergyHoverCheck)
        {
            v3 = btnEnergy.gameObject.transform.position;
            txtPopup = "This is a tooltip for Energy stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Energy");

            lblReqt.x = v3.x; lblReqt.y = v3.z + 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }
    }

    #region btnTimeline Code
    // All code for the btnTimeline
    void btnTimelineClick()
    {
        if (!canvasTimelinePopup.gameObject.activeSelf && !popupActive)
        {
            canvasTimelinePopup.gameObject.SetActive(true);
            Debug.Log("Popup Timeline active!");
            popupActive = true;
        }
    }
    #endregion

    #region btnOrganization code
    void btnOrganizationClick()
    {
        if (!canvasOrganizationPopup.gameObject.activeSelf && !popupActive)
        {
            canvasOrganizationPopup.gameObject.SetActive(true);
            Debug.Log("Popup Organization active!");
            popupActive = true;
        }
    }
    #endregion

    #region btnMenu Code
    // Method that makes popup appear/disappear
    void btnMenuClick()
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


