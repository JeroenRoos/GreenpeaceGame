using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour {

    public GameController gameController;
    public Region region;
    public bool isHovered;
    
    public void Init(GameController gameController, Region r)
    {
        this.gameController = gameController;
        this.region = r;
        isHovered = false;
    }

    public void Update()
    {
        if (!isHovered)
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)region.statistics.avgPollution / 100
            );
    }

    public void OnMouseDown()
    {
        /*if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            gameController.OnRegionClick(gameObject);
        }*/
        gameController.OnRegionClick(gameObject);
    }

    public void OnMouseEnter()
    {
        isHovered = true;
        gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, (float)165/255, 0, 1);
    }

    public void OnMouseExit()
    {
        isHovered = false;
        //gameObject.GetComponent<MeshRenderer>().material.SetColor("_OutlineColor", Color.black);
    }

}
