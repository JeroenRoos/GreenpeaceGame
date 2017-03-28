using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour {

    public GameController gameController;
    
    public void Init(GameController gameController)
    {
        this.gameController = gameController;
    }

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameController.OnRegionClick(gameObject);
        }
    }
    
}
