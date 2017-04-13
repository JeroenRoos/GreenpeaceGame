using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class OpenScene : MonoBehaviour
{
    public Text txtNewGameBtn;
    public Text txtLoadGame;
    public Text Quit;
    private GameController gameController;
    

    void Start()
    {
        Debug.Log("OpenScene START!");
        gameController = new GameController();
    }

    public void loadSceneByIndex(int index)
    {
        Debug.Log("NewGame Button Click!");
        GameController.loadGame = false;
        Debug.Log(index);
        SceneManager.LoadSceneAsync(index);
    }

    public void buttonExitOnClick()
    {
            Application.Quit();
    }

    public void buttonLoadGameClick()
    {
        //gameController.loadgame = true;
        GameController.loadGame = true;
        gameController.LoadGame();
    }
}
