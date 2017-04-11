using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EventObjectController : MonoBehaviour
{

    public GameController gameController;
    public GameEvent eventModel;
    public Region regionModel;
    public Texture[] allTextures;
    private UpdateUI updateUI;


    //   public bool areOptionsShown;


    //    private GameObject canvas;

    //public UpdateUI update;



    // private GameController gameController;

    /*private string txtTooltip;
    private string txtButton;
    public Texture2D tooltipTexture;
    public Texture2D buttonTexture;
    private GUIStyle tooltipStyle = new GUIStyle();
    private GUIStyle buttonStyle = new GUIStyle();
    public bool eventHoverCheck;
    public bool active;
    private int taal;
    private Vector2 clickPosition = new Vector2(0, 0);
    bool checkActivePopup;*/

    void Start()
    {
        updateUI = gameController.GetComponent<UpdateUI>();

        //eventHoverCheck = false;
        //popupActive = false;
        //tooltipStyle.normal.background = tooltipTexture;
        //buttonStyle.normal.background = buttonTexture;
        //taal = gameController.game.language;
        //areOptionsShown = false;
        //active = false;
        //clickPosition.x = 0;
        //clickPosition.y = 0;
        //checkActivePopup = false;
    }

    void Update()
    {
        // Make the options dissapear if camera chances
        //if (Camera.main.transform.hasChanged)
        //{
        //    areOptionsShown = false;
        //    Camera.main.transform.hasChanged = false;
        //}

        //checkActivePopup = gameController.getActivePopup();

        // if (checkActivePopup)
        // {
        //   areOptionsShown = false;
        // eventHoverCheck = false;
        //}

        // Event moet verwijderd worden als gekozen optie klaar is
        if (!eventModel.isIdle)
        {
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        updateUI.popupActive = false;
        updateUI.initEventPopup(eventModel, regionModel);
    }

    public void Init(GameController gameController, Region regionModel, GameEvent eventModel)
    {
        this.gameController = gameController;
        this.regionModel = regionModel;
        this.eventModel = eventModel;

        gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(eventModel.name);

        transform.position = regionModel.eventPosition;
    }

    /* public void OnMouseEnter()
     {
         eventHoverCheck = true;
     }

     public void OnMouseExit()
     {
         eventHoverCheck = false;
     }*/

    // hover over event
    /* public void OnMouseOver()
     {
         // Click on event
         if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
         {
             if (!areOptionsShown)
             {
                 ShowOptions();
             }
             else
             {
                 HideOptions();
             }
         }
     }*/

    /*private void OnGUI()
    {
        Rect btnRect;
        Rect lblReqt;
        
        lblReqt = GUILayoutUtility.GetRect(new GUIContent(txtTooltip), tooltipStyle);

        if (eventHoverCheck)
        {
            txtTooltip = eventModel.publicEventName[taal] + "\n" + eventModel.description[taal];
            Vector3 pos = Event.current.mousePosition;
            lblReqt.x = pos.x + 10;
            lblReqt.y = pos.y + 20;

            if (clickPosition.x == 0 && clickPosition.y == 0)
                clickPosition = pos;

            GUI.Label(lblReqt, "<color=#ccac6f>" + txtTooltip + "</color>", tooltipStyle);
        }

        if (areOptionsShown)
        {
            for (int i = 0; i < eventModel.choicesDutch.GetLength(0); i++)
            {
                if (taal == 0)
                    txtButton = "  " + eventModel.choicesDutch[i] + "  ";//tip[taal];
                else
                    txtButton = "  " + eventModel.choicesEnglish[i] + "  ";//tip[taal];

                btnRect = GUILayoutUtility.GetRect(new GUIContent(txtButton), buttonStyle);

                btnRect.x = clickPosition.x;

                var textWidth = GUI.skin.label.CalcSize(new GUIContent(txtButton));
                btnRect.width = textWidth.x;

                // Als er niet genoeg ruimte is om alle opties boven event te laten zien, zet ze er dan onder
                if (clickPosition.y - 150 < 0)
                {
                    if (i == 0)
                        btnRect.y = clickPosition.y + 20;//v3.y + 80

                    if (i == 1)
                        btnRect.y = clickPosition.y + 60;//v3.y + 170;

                    if (i == 2)
                        btnRect.y = clickPosition.y + 100;//v3.y + 260;
                }
                else
                {
                    if (i == 0)
                        btnRect.y = clickPosition.y - 40;//v3.y + 80

                    if (i == 1)
                        btnRect.y = clickPosition.y - 80;//v3.y + 170;

                    if (i == 2)
                        btnRect.y = clickPosition.y - 120;//v3.y + 260;
                }

                //string[] tip = { eventModel.choices[taal, i] + "\nKosten: " + eventModel.eventChoiceMoneyCost[i] +
          //          "\nDuur: " + eventModel.eventDuration[i]
          //  , eventModel.choices[taal, i] + "\nCost: " + eventModel.eventChoiceMoneyCost[i] +
          //          "\nDuration: " + eventModel.eventDuration[i]  };


                //txtButton += getConsequences(eventModel.consequences[i]);

                if (GUI.Button(btnRect, "<color=#ccac6f>" + txtButton + "</color>", buttonStyle))
                    ChooseOption(i);
            }
        }
    }*/

    /*string getConsequences(SectorStatistics s)
    {
        string[] consequences = { "\nConsequenties: ", "\nConsequences: " };

        if (s.ecoAwareness != 0)
        {
            string[] b = { " - Milieubewustheid: " + s.ecoAwareness, " - Eco awareness: " + s.ecoAwareness };
            consequences[taal] += b[taal];// " Milieubewustheid: " + s.ecoAwareness;
        }
        if (s.happiness != 0)
        {
            string[] c = { " - Tevredenheid: " + s.happiness, " - Happiness: " + s.happiness };
            consequences[taal] += c[taal];// " Tevredenheid: " + s.happiness;
        }
        if (s.income != 0)
        {
            string[] d = { " - Inkomen: " + s.income, " - Income: " + s.income };
            consequences[taal] += d[taal];// " Inkomen: " + s.income;
        }
        if (s.prosperity != 0)
        {
            string[] e = { " - Welvaart: " + s.prosperity, " - Prosperity: " + s.prosperity };
            consequences[taal] += e[taal];//" Welvaart: " + s.prosperity;
        }
        if (s.pollution.airPollutionIncrease != 0)
        {
            string[] f = { " - Luchtvervuiling: " + s.pollution.airPollutionIncrease, " - Air pollution: " + s.pollution.airPollutionIncrease };
            consequences[taal] += f[taal];// " Luchtvervuiling: " + s.pollution.airPollutionIncrease;
        }
        if (s.pollution.naturePollutionIncrease != 0)
        {
            string[] g = { " - Natuurvervuiling: " + s.pollution.naturePollutionIncrease, " - Nature pollution: " + s.pollution.naturePollutionIncrease };
            consequences[taal] += g[taal];// " Natuurvervuiling: " + s.pollution.naturePollutionIncrease;
        }
        if (s.pollution.waterPollutionIncrease != 0)
        {
            string[] h = { " - Watervervuiling: " + s.pollution.waterPollutionIncrease, " - Water pollution: " + s.pollution.waterPollutionIncrease };
            consequences[taal] += h[taal];// " Watervervuiling: " + s.pollution.waterPollutionIncrease;
        }

        return consequences[taal];
    }*/

    /*private void ShowOptions()
    {
        if (clickPosition.x == 0 && clickPosition.y == 0)
        {
            Debug.Log("CLICKPOS");
            //clickPosition = Event.current.mousePosition;
        }
        Debug.Log("Showing Options");
        areOptionsShown = true;
    }*/

    /* private void HideOptions()
     {
         clickPosition.x = 0;
         clickPosition.y = 0;
         Debug.Log("Hiding options!");
         areOptionsShown = false;
     }*/

    /*private void ChooseOption(int option)
    {
        Debug.Log("Chosen option: " + option);
        eventModel.SetPickedChoice(option, gameController.game, regionModel);
        areOptionsShown = false;
        eventHoverCheck = false;

        if (eventModel.eventDuration[option] == 0)
            Destroy(gameObject);
    } */

    private Texture SelectTexture(string description)
    {
        switch (description)
        {
            case "Earthquake":
                return allTextures[1];

            case "Flood":
                return allTextures[2];

            case "ForestFire":
                return allTextures[3];

            case "AirPollutionConcern":
                return allTextures[4];

            case "NaturePollutionConcern":
                return allTextures[4];

            case "WaterPollutionConcern":
                return allTextures[4];

            case "AccidentTransport":
                return allTextures[5];

            case "AgricultureEquipmentProject":
                return allTextures[6];

            case "AgricultureFasterWork":
                return allTextures[7];

            case "AgricultureHighPollution":
                return allTextures[8];

            case "AirPollutionNewTech":
                return allTextures[9];

            case "AntwerpenExpansion":
                return allTextures[10];

            case "BadHarvest":
                return allTextures[11];

            case "BelgiumPollution":
                return allTextures[12];

            case "CleanAirProject":
                return allTextures[13];

            case "CleanNatureProject":
                return allTextures[14];

            case "CleanWaterProject":
                return allTextures[15];

            case "ClimateDeal":
                return allTextures[16];

            case "CoalPlant":
                return allTextures[17];

            case "CompaniesEconomyUnhappy":
                return allTextures[18];

            case "CompaniesHighPollution":
                return allTextures[19];

            case "Firework":
                return allTextures[20];

            case "GasMining":
                return allTextures[21];

            case "GermanyPollution":
                return allTextures[22];

            case "HouseholdsHighPollution":
                return allTextures[23];

            case "HouseholdsPollutionDoubts":
                return allTextures[24];

            case "MoreGasExtraction":
                return allTextures[25];

            case "NaturePollutionNewTech":
                return allTextures[26];

            case "NoMoreGas":
                return allTextures[27];

            case "NuclearPlant":
                return allTextures[28];

            case "NuclearReactorAccident":
                return allTextures[29];

            case "ProductionRuhrArea":
                return allTextures[30];

            case "Recycling":
                return allTextures[31];

            case "RotterdamExpansion":
                return allTextures[32];

            case "SchipholExpansion":
                return allTextures[33];

            case "SolarPanelProject":
                return allTextures[34];

            case "TrafficJam":
                return allTextures[35];

            case "WaterPollutionNewTech":
                return allTextures[36];

            default: return allTextures[37];
        }
    }

    //public void disableTooltipAndOptions()
    // {
    //    areOptionsShown = false;
    //   eventHoverCheck = false;
    // }
}
