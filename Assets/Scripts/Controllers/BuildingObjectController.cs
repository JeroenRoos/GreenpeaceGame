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
        EventManager.CallPlayButtonClickSFX();
        updateUI.initBuildingPopup(building, region);
    }

    public void placeBuildingIcon(GameController gameController, Region region, Building building)
    {
        this.gameController = gameController;
        this.region = region;

        if (building != null)
        {
            this.building = building;

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
                return buildingTextures[0];
        }
    }
}

