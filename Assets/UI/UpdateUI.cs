using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // Needed for UI
using UnityEngine.EventSystems;             // Needed for Tooltips 
using System;

public class UpdateUI : MonoBehaviour
{
    #region UI Elements
    // Text
    public Text txtMoney;
    public Text txtPopulation;
    public Text popupTxt;

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

    // Rectangles
    public Rect mainMenuRect;
    #endregion

    #region Boolean Variables
    // Booleans
    public bool btnMenuCheck = false;
    public bool btnTimelineCheck = false;
    public bool hoverMenu = false;
    public bool btnOrganizationCheck = false;
    public bool btnMoneyHoverCheck = false;
    public bool btnHappinessHoverCheck = false;
    public bool btnAwarenessHoverCheck = false;
    public bool btnPollutionHoverCheck = false;
    public bool btnEnergyHoverCheck = false;
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
        /*Button btn = */btnMenu.GetComponent<Button>();
        /*btn*/btnMenu.onClick.AddListener(btnMenuClick);

        /*btn = */btnTimeline.GetComponent<Button>();
        /*btn*/btnTimeline.onClick.AddListener(btnTimelineClick);

        /*btn = */btnOrganization.GetComponent<Button>();
        /*btn*/btnOrganization.onClick.AddListener(btnOrganizationClick);

        /*btn = */btnMoney.GetComponent<Button>();
        /*btn = */btnHappiness.GetComponent<Button>();
        /*btn = */btnAwareness.GetComponent<Button>();
        /*btn = */btnEnergy.GetComponent<Button>();
        /*btn = */btnPollution.GetComponent<Button>();
        /*btn = */btnPopulation.GetComponent<Button>();
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
        UpdateTextUI();
    }

    void UpdateTextUI()
    {
        txtMoney.text = "20.000";
        txtPopulation.text = "17.123.987";
    }

    void OnGUI()
    {
        Vector3 v3;

        //Color backColor = new Color();
        //Color txtColor = new Color();
        //ColorUtility.TryParseHtmlString("#05001a", out backColor);
        //ColorUtility.TryParseHtmlString("#ccac6f", out txtColor);
        string txtPopup = "";

        if (btnMoneyHoverCheck)
        {
            Debug.Log("Arrived in Tooltip Money!");
            v3 = btnMoney.gameObject.transform.position;
            txtPopup = "This is a tooltip for Money stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Money");
            
            lblReqt.x = v3.x; lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnHappinessHoverCheck)
        {
            Debug.Log("Arrived in Tooltip Happiness!");
            v3 = btnHappiness.gameObject.transform.position;
            txtPopup = "This is a tooltip for Happiness stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Happiness");

            lblReqt.x = v3.x; lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnAwarenessHoverCheck)
        {
            Debug.Log("Arrived in Tooltip Awareness!");
            v3 = btnAwareness.gameObject.transform.position;
            txtPopup = "This is a tooltip for Awareness stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Awareness");

            lblReqt.x = v3.x; lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnPollutionHoverCheck)
        {
            Debug.Log("Arrived in Tooltip Pollution!");
            v3 = btnPollution.gameObject.transform.position;
            txtPopup = "This is a tooltip for Pollution stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Pollution");

            lblReqt.x = v3.x; lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }

        if (btnEnergyHoverCheck)
        {
            Debug.Log("Arrived in Tooltip Energy!");
            v3 = btnEnergy.gameObject.transform.position;
            txtPopup = "This is a tooltip for Energy stats\n";
            Rect lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtPopup), "Tooltip Energy");

            lblReqt.x = v3.x; lblReqt.y = 40;
            GUI.Label(lblReqt, "<color=#05001a>" + txtPopup + "</color>");
            //GUI.backgroundColor = backColor;
        }
    }

    #region btnTimeline Code
    // All code for the btnTimeline
    void btnTimelineClick()
    {
        if (!btnTimelineCheck)
        {
            btnTimelineCheck = true;
            canvasTimelinePopup.gameObject.SetActive(true);
            Debug.Log("Popup Timeline active!");
        }
        else
        {
            btnTimelineCheck = false;
            canvasTimelinePopup.gameObject.SetActive(false);
            Debug.Log("Popup Timeline not active!");
        }
    }
    #endregion

    #region btnOrganization code
    void btnOrganizationClick()
    {
        if (!btnOrganizationCheck)
        {
            btnOrganizationCheck = true;
            canvasOrganizationPopup.gameObject.SetActive(true);
            Debug.Log("Popup Organization active!");
        }
        else
        {
            btnOrganizationCheck = false;
            canvasOrganizationPopup.gameObject.SetActive(false);
            Debug.Log("Popup Organization not active!");
        }
    }
    #endregion

    #region btnMenu Code
    // Method that makes popup appear/disappear
    void btnMenuClick()
    {
        // Temporary close of popup
        if (!btnMenuCheck)
        {
            btnMenuCheck = true;
            canvasMenuPopup.gameObject.SetActive(true);
            Debug.Log("Popup Menu active!");
        }
        else
        {
            btnMenuCheck = false;
            canvasMenuPopup.gameObject.SetActive(false);
            Debug.Log("Popup Menu not active!");
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


