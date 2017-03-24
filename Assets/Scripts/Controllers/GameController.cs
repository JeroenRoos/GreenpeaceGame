using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameController : MonoBehaviour
{
    public Game game;
    private UpdateUI updateUI;
    //private GameTimer timer;

    private float time;

    // Use this for initialization
    void Start()
    {
        game = new Game();
        updateUI = GetComponent<UpdateUI>();
        //timer = new GameTimer();
        //timer.StartTimeflowTimer();
        //timer.Elapsed += new ElapsedEventHandler(UpdateGameTime);
    }

    //void UpdateGameTime(object source, ElapsedEventArgs e)
    //{
    //    if (game.currentYear > 30)
    //    {
    //        timer.Stop();
    //    }
    //    else
    //    {
    //        bool newEvent = game.UpdateTime();

    //        if (newEvent)
    //        {
    //            timer.Stop();
    //            game.StartNewEvent();
    //            timer.Start();
    //        }
    //    }

    //}

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        while (time > 0.1f)
        {
            game.UpdateTime();
            /*if(game.UpdateTime())
            {
                game.StartNewEvent();
            }*/
            time -= 0.1f;
        }

    }

    // Update is called once per frame
    // Maak instance van UI en roep vanaf hier methodes aan
    // Method UpdateUI in FixedUpdate() van GameController;
    void FixedUpdate()
    {
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
}
