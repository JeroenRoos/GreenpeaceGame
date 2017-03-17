using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestBot : MonoBehaviour {

    GameController gameController;

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
    }

    // Event occured
    void EventAction()
    {
        Debug.Log("Event occured");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
