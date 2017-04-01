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

        // Update the main screen UI (Icons and date)
        updateUIMainScreen();

        // Update the UI in popup screen
        updateUIPopups();

        // Update values in Tooltips for Icons in Main UI
        updateUITooltips();
    }

    void updateUIMainScreen()
    {
        // Update Text and Color values in main UI
        updateUI.updateDate(game.currentMonth, game.currentYear);
        updateUI.updateMoney(game.gameStatistics.money);
        updateUI.updatePopulation(game.gameStatistics.population);
        updateUI.updateAwarness(game.gameStatistics.ecoAwareness);
        updateUI.updatePollution(game.gameStatistics.pollution);
        updateUI.updateEnergy(game.gameStatistics.energy.cleanSource);
        updateUI.updateHappiness(game.gameStatistics.happiness);

        /*
        foreach (Region region in game.regions.Values)
        {
            foreach (RegionSector sector in region.sectors.Values)
            {
                sector.statistics.
            }
        }
        */
    }
    

    void updateUITooltips()
    {
        if (updateUI.getBtnMoneyHover())
            updateUI.updateMoneyTooltip();

        if (updateUI.getBtnHappinessHover())
            updateUI.updateHappinessTooltip();

        if (updateUI.getBtnAwarenessHover())
            updateUI.updateAwarnessTooltip();

        if (updateUI.getBtnPollutionHover())
            updateUI.updatePollutionTooltip();

        if (updateUI.getBtnEnergyHover())
            updateUI.updateEnergyTooltip(game.gameStatistics.energy.cleanSource,
        game.gameStatistics.energy.fossilSource, game.gameStatistics.energy.nuclearSource);
    }

    void updateUIPopups()
    {
        if (updateUI.canvasOrganizationPopup.gameObject.activeSelf)
            updateUIOrganizationScreen();

        if (updateUI.canvasRegioPopup.gameObject.activeSelf)
            updateUIRegioScreen();

        if (updateUI.canvasTimelinePopup.gameObject.activeSelf)
            updateUITimelineScreen();
    }

    void updateUIOrganizationScreen()
    {
        int i = 0;
        foreach (Region region in game.regions.Values)
        {
            // Send the income for each region, use i to determine the region
            updateUI.updateOrganizationScreenUI(region.statistics.income, i);
            i++;
        }
    }

    void updateUIRegioScreen()
    {

    }

    void updateUITimelineScreen()
    {

    }

    void FixedUpdate()
    {
        
    }

    public void OnRegionClick(GameObject region)
    {
        Region regionModel = game.regions[region.name];
        updateUI.regionClick(regionModel);
    }
}
