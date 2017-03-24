using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameController : MonoBehaviour
{
    public Game game;
    private GameTimer timer;
    private UpdateUI updateUI;

    // Use this for initialization
    void Start()
    {
        game = new Game();
        timer = new GameTimer();
        timer.StartTimeflowTimer();
        timer.Elapsed += new ElapsedEventHandler(UpdateGameTime);
        updateUI = new UpdateUI();
    }

    void UpdateGameTime(object source, ElapsedEventArgs e)
    {
        if (game.currentYear > 30)
        {
            timer.Stop();
        }
        else
        {
            bool newEvent = game.UpdateTime();

            if (newEvent)
            {
                timer.Stop();
                game.StartNewEvent();
                timer.Start();
            }
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
    }
}
