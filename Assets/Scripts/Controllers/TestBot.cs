using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestBot : MonoBehaviour {

    GameController gameController;
    double currentCurrency;

    // Use this for initialization
    void Start () {
        gameController = GetComponent<GameController>();
        EventManager.ChangeMonth += CheckStatus;
        EventManager.ShowEvent  += EventAction;
    }
    
    // Month changed
    void CheckStatus()
    {
        Debug.Log("Month changed");

        currentCurrency = gameController.game.gameStatistics.money;

        foreach (Region region in gameController.game.regions.Values)
        {
            //Debug.Log(region.statistics.pollution.avgPullution);

            if (region.statistics.pollution.avgPullution < 50 && currentCurrency > 2000)
            {
                // take anti-pollution action
                //Debug.Log(region.actions[0].description);
                region.actions[0].ActivateAction(gameController.game.currentYear, gameController.game.currentMonth);
            }
        }
    }

    // Event occured
    void EventAction()
    {
        Debug.Log("Event occured");
        //int chosenOption;

        //foreach (GameEvent gameEvent in gameController.game.events)
        //{
        //    if(gameEvent.isActive == true)
        //    {
        //        chosenOption = UnityEngine.Random.Range(0, 3);
        //        //chosenOption = 1;
        //        Debug.Log("Choose option " + chosenOption);
        //        gameEvent.SetPickedChoice(chosenOption, gameController.game);
        //    }
        //}

    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.Return))
        {
            // toggle auto / manual end turn
            gameController.autoEndTurn = !gameController.autoEndTurn;
        }
	}
}
