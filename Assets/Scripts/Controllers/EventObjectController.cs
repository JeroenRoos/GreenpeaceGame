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

    public bool isClicked = false;

    void Start()
    {
        updateUI = gameController.GetComponent<UpdateUI>();
    }

    void Update()
    {
        if (!eventModel.isIdle && !eventModel.isActive)//!eventModel.isFinished)// && )//!eventModel.isIdle)
        {
            //eventModel.isFinished
            Destroy(gameObject);
        }
    }

    public void OnMouseDown()
    {
        if (!updateUI.popupActive)
        {
            // Nog geen optie gekozen
            if (gameController.game.tutorial.tutorialeventsClickable && eventModel.isIdle)
            {
                isClicked = true;
                EventManager.CallPlayButtonClickSFX();
                updateUI.popupActive = false;
                updateUI.initEventPopup(eventModel, regionModel);
            }
            // Optie gekozen
            else
            {
                isClicked = true;
                EventManager.CallPlayButtonClickSFX();
                updateUI.popupActive = false;
                updateUI.initEventPopup(eventModel, regionModel);
            }
        }
    }

    public void PlaceEventIcons(GameController gameController, Region regionModel, GameEvent eventModel)
    {
        /* Event Positions in XML file
         * Noord Nederland: 15 - 1 - 20
         * Oost Nederland:  16 - 1 - 13
         * Zuid Nederland:  13 - 1 -  8
         * West Nederland:   9 - 1 - 12
        */

        this.gameController = gameController;
        this.regionModel = regionModel;
        this.eventModel = eventModel;

        gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(eventModel.name);

        transform.position = new Vector3(regionModel.eventPositions[0], regionModel.eventPositions[1], regionModel.eventPositions[2]);
        StartCoroutine(ChangeScale(gameObject.transform.localScale));
    }

    public IEnumerator Shake()
    {
        Quaternion standardRotation = transform.rotation;
        while (eventModel.isIdle)
        {
            for (int i = 0; i < 4; i++)
            {
                transform.Rotate(0, 10, 0);
                yield return new WaitForFixedUpdate();
            }
            for (int i = 0; i < 4; i++)
            {
                transform.Rotate(0, -10, 0);
                yield return new WaitForFixedUpdate();
            }
            yield return new WaitForSeconds(2);

        }
        transform.rotation = standardRotation;
    }

    IEnumerator ChangeScale(Vector3 endScale)
    {
        Vector3 currentScale = new Vector3(0, 0, 0);
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

            transform.localScale = currentScale;

            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(Shake());
    }

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
            case "EventEmpty":
                    return allTextures[38];

            default: return allTextures[37];
        }
    }
}
