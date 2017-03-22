using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class GameController : MonoBehaviour {

    public Game game;
    //private GameTimer timer;

    private float time;

    // Use this for initialization
    void Start()
    {
        game = new Game();
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
            if(game.UpdateTime())
            {
                game.StartNewEvent();
            }
            time -= 0.1f;
        }
    }
}
