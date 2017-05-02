using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;

public class OpenScene : MonoBehaviour
{
    public Text txtButtonNewGame;
    private string[] txtNewGame;

    public Text txtButtonLoadGame;
    private string[] txtLoadGame;

    public Text txtButtonQuit;
    private string[] txtQuit;

    public Text txtButtonOptions;
    private string[] txtOptions;
        
    public Button btnLoad;
    private int taal;


    void Start()
    {
        EventManager.CallPlayBackgroundMusic();

        var path = Application.persistentDataPath + "/Savestate.gd";
        if (File.Exists(path))
        {
            btnLoad.interactable = true;
        }

        taal = ApplicationModel.language;
        
        if (taal == 0)
        {
            txtButtonLoadGame.text = "Spel laden";
            txtButtonNewGame.text = "Nieuw spel";
            txtButtonQuit.text = "Spel verlaten";
            txtButtonOptions.text = "Opties";
        }
        else
        {
            txtButtonLoadGame.text = "Load game";
            txtButtonNewGame.text = "New Game";
            txtButtonQuit.text = "Quit";
            txtButtonOptions.text = "Options";
        }
    }

    public void loadSceneByIndex(int index)
    {
        EventManager.CallPlayButtonClickSFX();
        ApplicationModel.loadGame = false;
        SceneManager.LoadSceneAsync(index);
    }

    public void buttonExitOnClick()
    {
        EventManager.CallPlayButtonClickSFX();
        Application.Quit();
    }

    public void buttonLoadGameClick()
    {
        EventManager.CallPlayButtonClickSFX();
        ApplicationModel.loadGame = true;
        SceneManager.LoadSceneAsync(1);
    }
}
