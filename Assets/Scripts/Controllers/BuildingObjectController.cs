using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class BuildingObjectController : MonoBehaviour
{
    public GameController gameController;
    public Building building;
    public Region region;
    public Texture[] buildingTextures;
    private UpdateUI updateUI;

    void Start()
    {
        updateUI = gameController.GetComponent<UpdateUI>();
    }

    private void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (!updateUI.popupActive)// && gameController.game.tutorial.tutorialBuildingsActive)
        {
            if (gameController.game.tutorial.tutorialBuildingsClickable)
            {
                EventManager.CallPlayButtonClickSFX();

                if (building != null)
                {
                    //gameController.activeBuildingUI(building, region);
                    updateUI.initBuildingPopup(building, region);
                }
                else
                {
                    //gameController.activeEmptyBuildingUI(region);
                    updateUI.initEmptyBuildingPopup(region);
                }
            }
        }
    }

    public void placeBuildingIcon(GameController gameController, Region region, Building building)
    {

        /* Event Positions in XML file
         * Noord Nederland: 16.5 - 1 - 20
         * Oost Nederland:  17.5 - 1 - 13
         * Zuid Nederland:  14.5 - 1 -  8
         * West Nederland:  10.5 - 1 - 12
        */

        this.gameController = gameController;
        this.region = region;
        this.building = building;


        if (building != null)
        {
            // Hij komt in deze Method maar print nog steeds de empty
            // Ook opent hij nog de empty als je klikt op icon terwijl dat niet moet
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(building.buildingID);
            transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[1], region.buildingPositions[2]);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture("empty");            
            transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[1], region.buildingPositions[2]);
        }
    }

    private Texture SelectTexture(string description)
    {
        switch (description)
        {
            case "EconomyBuiding":
                return buildingTextures[0];

            case "PollutionBuilding":
                return buildingTextures[1];

            case "HappinessBuilding":
                return buildingTextures[2];

            case "empty":
                return buildingTextures[3];

            default:
                return buildingTextures[1];
        }
    }
}

