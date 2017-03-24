using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour {
    
    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameController.OnRegionClick(gameObject);
        }
    }
    
}
