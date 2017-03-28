using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Game game;
    private UpdateUI updateUI;
    public GameObject noordNederland;
    public GameObject oostNederland;
    public GameObject zuidNederland;
    public GameObject westNederland;

    private float time;

    public bool autoEndTurn = false;

    // Use this for initialization
    void Start()
    {
        game = new Game();
        updateUI = GetComponent<UpdateUI>();

        // setup Region Controllers
        noordNederland.GetComponent<RegionController>().Init(this);
        oostNederland.GetComponent<RegionController>().Init(this);
        zuidNederland.GetComponent<RegionController>().Init(this);
        westNederland.GetComponent<RegionController>().Init(this);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Return) || autoEndTurn)
        {
            game.UpdateTime();
        }

        // Update month and year text value in UI
        updateUI.updateDate(game.currentMonth, game.currentYear);

        // Update Money text value in UI
        updateUI.updateMoney(game.gameStatistics.money);

        // Update Population text value in UI
        updateUI.updatePopulation(game.gameStatistics.population);

        // Update Awarness color value in UI
        updateUI.updateAwarness(game.gameStatistics.ecoAwareness);

        // Update Pollution color value in UI
        updateUI.updatePollution(game.gameStatistics.pollution);

        // Update Energy color value in UI
        updateUI.updateEnergy(game.gameStatistics.energy.cleanSource);

        // Update Energy color value in UI
        updateUI.updateHappiness(game.gameStatistics.happiness);
    }

    void FixedUpdate()
    {
        
    }

    public void OnRegionClick(GameObject region)
    {
        // game.regions[region.name].statistics.ecoAwareness;
        //Debug.Log(region.name);
        //region.GetComponent<Renderer>().material.color = Color.green; 
        Region regionModel = game.regions[region.name];
        updateUI.regionClick(regionModel);
    }
}
