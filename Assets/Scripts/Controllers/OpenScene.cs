using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;

public class OpenScene : MonoBehaviour
{
    public Text txtNewGameBtn;
    public Text txtLoadGame;
    public Text Quit;
    public Button btnLoad;


    void Start()
    {
        EventManager.CallPlayBackgroundMusic();

        var path = Application.persistentDataPath + "/Savestate.gd";
        if (File.Exists(path))
        {
            btnLoad.interactable = true;
        }
    }

    public void loadSceneByIndex(int index)
    {
        EventManager.CallPlayButtonClickSFX();
        ApplicationModel.loadGame = false;
        Debug.Log(index);
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
