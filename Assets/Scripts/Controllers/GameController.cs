using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Game game;

    private EventObjectController eventObjectController;
    private UpdateUI updateUI;
    public GameObject noordNederland;
    public GameObject oostNederland;
    public GameObject zuidNederland;
    public GameObject westNederland;

    public GameObject eventObject;

   // private float time;
    public bool autoEndTurn = false;

    // Use this for initialization
    void Start()
    {
        game = new Game();
        updateUI = GetComponent<UpdateUI>();
        eventObjectController = GetComponent<EventObjectController>();
        updateUI.LinkGame(game);

        // setup Region Controllers
        noordNederland.GetComponent<RegionController>().Init(this);
        oostNederland.GetComponent<RegionController>().Init(this);
        zuidNederland.GetComponent<RegionController>().Init(this);
        westNederland.GetComponent<RegionController>().Init(this);

        game.Init(eventObject, this);
    }

    // Update is called once per frame
    void Update () {
        if ((Input.GetKeyDown(KeyCode.Return) || autoEndTurn) && game.currentYear < 31)
        {
            game.NextTurn();
        }

        // Update the main screen UI (Icons and date)
        updateUIMainScreen();

        // Update the UI in popup screen
        if (updateUI.getPopupActive())
            updateUIPopups();

        // Update values in Tooltips for Icons in Main UI
        if (updateUI.getTooltipActive())
            updateUITooltips();

        UpdateRegionColor();
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
    }
    

    void updateUITooltips()
    {
        if (updateUI.getBtnMoneyHover())
            updateUI.updateMoneyTooltip(game.gameStatistics.donations, game.gameStatistics.income);

        if (updateUI.getBtnHappinessHover())
            updateHappiness();

        if (updateUI.getBtnAwarenessHover())
            updateAwareness();

        if (updateUI.getBtnPollutionHover())
            updatePollution();

        if (updateUI.getBtnEnergyHover())
            updateUI.updateEnergyTooltip(game.gameStatistics.energy.cleanSource,
            game.gameStatistics.energy.fossilSource, game.gameStatistics.energy.nuclearSource);
    }

    void updateHappiness()
    {
        int i = 0;

        foreach (Region region in game.regions.Values)
        {
            updateUI.updateHappinessTooltip(region.statistics.happiness, i);
            i++;
        }
        
    }

    void updateAwareness()
    {
        int i = 0;

        foreach (Region region in game.regions.Values)
        {
            updateUI.updateAwarnessTooltip(region.statistics.ecoAwareness, i);
            i++;
        }
    }

    void updatePollution()
    {
        int i = 0;

        foreach (Region region in game.regions.Values)
        {
            updateUI.updatePollutionTooltip(region.statistics.pollution.avgPullution, i);
            i++;
        }
    }

    void updateUIPopups()
    {
        //eventObjectController.disableTooltipAndOptions();

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
            updateUI.updateOrganizationScreenUI(region.statistics.income * 12, i, game.gameStatistics.money);
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

    void CheckEndOfGame()
    {
        if (game.currentYear == 2050)
        {
            autoEndTurn = false;
            if(game.gameStatistics.pollution < 20)
            {
                // you did it!
            }
            else
            {
                // objective failed.
            }
        }
    }

    // update kleur van regio
    public void UpdateRegionColor()
    {
        noordNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green, 
                Color.red, 
                (float)game.regions["Noord Nederland"].statistics.pollution.avgPullution / 100
            );

        oostNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions["Oost Nederland"].statistics.pollution.avgPullution / 100
            );

        westNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions["West Nederland"].statistics.pollution.avgPullution / 100
            );

        zuidNederland.GetComponent<Renderer>().material.color = Color.Lerp(
                Color.green,
                Color.red,
                (float)game.regions["Zuid Nederland"].statistics.pollution.avgPullution / 100
            );
    }

    public bool getActivePopup()
    {
        return updateUI.getPopupActive();
    }
}
