using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObjectController : MonoBehaviour {
    
    public GameController gameController;
    public GameEvent eventModel;
    public bool areOptionsShown;

    public void Init(GameController gameController, GameEvent eventModel)
    {
        this.gameController = gameController;
        this.eventModel = eventModel;
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
}
