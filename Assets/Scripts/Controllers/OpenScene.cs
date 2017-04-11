using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OpenScene : MonoBehaviour
{
    void Start()
    {

    }

    public void loadSceneByIndex(int index)
    {
        Debug.Log(index);
        SceneManager.LoadSceneAsync(index);

        // New GameController to start game, not sure about this one
        GameController c = new GameController();
    }

    public void buttonExitOnClick()
    {
        //if (UnityEditor.EditorApplication.isPlaying)
        //    UnityEditor.EditorApplication.isPlaying = false;
        //else
            Application.Quit();

    }
}
