using System;
using System.Collections.Generic;
using System.Collections;
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

    public bool isClicked = false;

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
                isClicked = true;
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


        transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[1], region.buildingPositions[2]);
        if (building != null)
        {
            // Hij komt in deze Method maar print nog steeds de empty
            // Ook opent hij nog de empty als je klikt op icon terwijl dat niet moet
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(building.buildingID);
            StartCoroutine(ChangeScale(gameObject.transform.localScale), false);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture("empty");
            transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[1], region.buildingPositions[2]);
            StartCoroutine(ChangeScale(gameObject.transform.localScale), true);
        }

    }

    public IEnumerator Shake()
    {
        Quaternion standardRotation = transform.rotation;
        while (!isClicked)
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

    IEnumerator ChangeScale(Vector3 endScale, bool shouldShake)
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
        if (shouldShake)
            StartCoroutine(Shake());
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

