using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionController : MonoBehaviour {

    public GameController gameController;
    public UpdateUI ui;
    public MapRegion region;
    public bool isHovered;
    
    public void Init(GameController gameController, UpdateUI ui, MapRegion r)
    {
        this.gameController = gameController;
        this.ui = ui;
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
        gameController.OnRegionClick(gameObject);
    }

    public void OnMouseEnter()
    {
        if (!ui.popupActive)
        {
            isHovered = true;
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, (float)165 / 255, 0, 1);
        }
    }

    public void OnMouseExit()
    {
            isHovered = false;
    }
}
