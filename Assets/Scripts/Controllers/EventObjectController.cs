using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectController : MonoBehaviour {
    
    public GameController gameController;
    public GameEvent eventModel;
    public bool areOptionsShown;
    public Texture texture;

    public Texture[] allTextures;


    void Start()
    {
        gameObject.GetComponent<Renderer>().material.mainTexture =
        SelectTexture("Smog");
    }

    public void Init(GameController gameController, GameEvent eventModel)
    {
        this.gameController = gameController;
        this.eventModel = eventModel;
        //gameObject.GetComponent<Renderer>().material.mainTexture = SelectTexture(eventModel.name);
    }

    // hover over event
    public void OnMouseOver()
    {
        
        ShowTooltip();

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
    }

    void ShowTooltip()
    {
        Debug.Log("event tooltip shown");
    }

    void ShowOptions()
    {

    }

    void HideOptions()
    {

    }

    void ChooseOption(int option)
    {
        eventModel.SetPickedChoice(option);
    }

    Texture SelectTexture(string description)
    {
        switch (description) {

            case "Earthquake":
                return allTextures[1];

            case "Flood":
                return allTextures[2];

            case "ForestFire":
                return allTextures[3];

            default: return allTextures[3];
        }
    }
}
