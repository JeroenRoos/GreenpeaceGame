using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EventObjectController : MonoBehaviour
{

    public GameController gameController;
    public GameEvent eventModel;
    public MapRegion regionModel;
    public Texture[] allTextures;
    private UpdateUI updateUI;

    public bool isClicked = false;

    void Start()
    {
        updateUI = gameController.GetComponent<UpdateUI>();
        EventManager.DestroySprite += DestroyFromChoiceMade;
    }

    void Update()
    {
        // Als de event model niet meer idle (afwachtend van actie) en actief (gekozen actie is bezig) is wordt hij gedstroyed en van de map gehaald
        if (!eventModel.isIdle && !eventModel.isActive)
        {
            EventManager.DestroySprite -= DestroyFromChoiceMade;
            Destroy(gameObject);
        }
    }

    void DestroyFromChoiceMade(GameEvent e)
    {
        if (e == eventModel)
        {
            EventManager.DestroySprite -= DestroyFromChoiceMade;
            Destroy(gameObject);
        }
    }

    // Open de event popup als er op het icoontje gedrukt wordt
    public void OnMouseDown()
    {
        if (!updateUI.popupActive)
        {
            // Nog geen optie gekozen voor event
            if (gameController.game.tutorial.tutorialeventsClickable && eventModel.isIdle)
            {
                isClicked = true;
                EventManager.CallPlayButtonClickSFX();
                updateUI.popupActive = false;
                updateUI.initEventPopup(eventModel, regionModel);
            }
            // Wel een optie gekozen en dus opent er een andere popup
            else
            {
                isClicked = true;
                EventManager.CallPlayButtonClickSFX();
                updateUI.popupActive = false;
                updateUI.initEventPopupChoiceMade(eventModel, regionModel);
            }
        }
    }

    public void OnMouseEnter()
    {
        // ALs je over het icoontje hovered wordt hij groter
        transform.localScale = new Vector3((float)1.2 * transform.localScale.x, (float)1.2 * transform.localScale.y,
            (float)1.2 * transform.localScale.z);
    }

    public void OnMouseExit()
    {
        // On mouse exit wordt het icoontje weer de normale grootte
        transform.localScale = new Vector3(transform.localScale.x / (float)1.2, transform.localScale.y / (float)1.2,
            transform.localScale.z / (float)1.2);
    }

    public void PlaceEventIcons(GameController gameController, MapRegion regionModel, GameEvent eventModel)
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

        if (eventModel.isIdle)
        {
            // Set het icoontje dat bij event hoort als texture
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(eventModel.name);
        }
        // Als event model active is
        else
        {
            // Zet het "Finished" texture als icoontje
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture("finished");
        }
        
        // Start de coroutine waardoor het shake effect ontstaat
        transform.position = new Vector3(regionModel.eventPositions[0], regionModel.eventPositions[1], regionModel.eventPositions[2]);
        StartCoroutine(ChangeScale(gameObject.transform.localScale));
    }

    // Het shake effect van het icoontje
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

    // Het effect waardoor het icoontje klein begint en steeds groter wordt
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


    // Kiest de juiste texture om op de map te laten zien
    // Textures zijn in inspector toegevoegd aan de List
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

            case "TrafficJams":
                return allTextures[35];

            case "WaterPollutionNewTech":
                return allTextures[36];

            case "EventEmpty":
                    return allTextures[38];

            case "finished":
                    return allTextures[39];

            case "Acidification":
                return allTextures[40];

            case "Asbestos":
                return allTextures[41];

            case "CompaniesPersonalProfit":
                return allTextures[42];

            case "EfficencyAgriculture":
                return allTextures[43];

            case "FoodWaste":
                return allTextures[44];

            case "GovernmentMoneyReward":
                return allTextures[45];

            case "GreenCompaniesRewardProject":
                return allTextures[46];

            case "GrowthAgriculture":
                return allTextures[47];

            case "GrowthCompanies":
                return allTextures[48];

            case "GrowthHouseholds":
                return allTextures[49];

            case "IjsselmeerThrash":
                return allTextures[50];

            case "MicroDust":
                return allTextures[51];

            case "NewCleanBoatTech":
                return allTextures[52];

            case "NewCleanCarTech":
                return allTextures[53];

            case "NewCleanEnergyTech":
                return allTextures[54];

            case "NewCleanPlaneTech":
                return allTextures[55];

            case "NewEnergySavingTech":
                return allTextures[56];

            case "NewFoodCarefulTech":
                return allTextures[57];

            case "NewRecyclingTech":
                return allTextures[58];

            case "NewSolarPanelTech":
                return allTextures[59];

            case "NoMoreFossil":
                return allTextures[60];

            case "PollutingManure":
                return allTextures[61];

            case "ToxicDump":
                return allTextures[62];

            case "WaterAlgae":
                return allTextures[63];

            case "WaterDuckWeed":
                return allTextures[64];

            case "LessCycling":
                return allTextures[65];

            case "NewFoodEfficiencyTech":
                return allTextures[66];

            default: return allTextures[37];
        }
    }
}
