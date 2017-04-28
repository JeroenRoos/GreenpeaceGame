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
        this.building = building;

        gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(building.buildingID);
        //transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[2], region.buildingPositions[3]);
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

            default:
                return buildingTextures[0];
        }
    }
}

