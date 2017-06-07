using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuildingObjectController : MonoBehaviour
{
    public GameController gameController;
    public Building building;
    public MapRegion region;
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

    // Open de building popup als er op het icoontje gedrukt wordt
    public void OnMouseDown()
    {
        if (!updateUI.popupActive)
        {
            if (gameController.game.tutorial.tutorialBuildingsClickable)
            {
                isClicked = true;
                EventManager.CallPlayButtonClickSFX();

                // Initialize popup in UpdateUI Class
                updateUI.initEmptyBuildingPopup(region);
            }
        }
    }

    public void placeBuildingIcon(GameController gameController, MapRegion region, Building building)
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

        // Als de building niet null is wordt het juiste icoontje geplaatst
        if (building != null)
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(building.buildingID);

            // Coroutine voor het klein > groot effect van icon
            StartCoroutine(ChangeScale(gameObject.transform.localScale, false));
        }
        // Als de buliding null is wordt het "Empty" icoontje geplaatst
        else
        {
            gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture("empty");
            transform.position = new Vector3(region.buildingPositions[0], region.buildingPositions[1], region.buildingPositions[2]);

            // Coroutine voor het klein > groot effect van icon
            StartCoroutine(ChangeScale(gameObject.transform.localScale, true));
        }

    }

    // Zorgt voor het shake effect
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

    // Het effect waardoor het icoontje klein begint en steeds groter wordt
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


    // Kiest de juiste texture om op de map te laten zien
    // Textures zijn in inspector toegevoegd aan de List
    private Texture SelectTexture(string description)
    {
        switch (description)
        {
            case "EconomyBuilding":
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

