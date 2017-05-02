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
    public Text txtButtonLoadGame;
    public Text txtButtonQuit;
    public Text txtButtonOptions;

    public Button btnLoad;
    private int taal;

    public Canvas canvasHomeScreen;

    // Settings 
    public Canvas canvasSettings;
    public Button btnSettingsBack;
    public Text txtButtonSettingsBack;
    public Slider sliderMusicVolume;
    public Text txtMusicVolume;
    public Slider sliderEffectsVolume;
    public Text txtEffectsVolume;
    public Text txtLanguage;
    public Toggle toggleEnglish;
    public Toggle toggleDutch;
    public Text txtToggleEnglish;
    public Text txtToggleDutch;
    private bool toggleDutchCheck;
    private bool toggleEnglishCheck;
    public Text txtMusicVolumeSliderValue;
    public Text txtEffectsVolumeSliderValue;

    public AudioSource musicSource;
    public AudioSource effectsSource;

    void Start()
    {
        EventManager.CallPlayBackgroundMusic();
        initUI();
        initText();

        var path = Application.persistentDataPath + "/Savestate.gd";
        if (File.Exists(path))
        {
            btnLoad.interactable = true;
        }

        taal = ApplicationModel.language;
    }

    private void initUI()
    {
        canvasHomeScreen.GetComponent<Canvas>();
        //canvasHomeScreen.gameObject.SetActive(true);

        canvasSettings.GetComponent<Canvas>();
        canvasSettings.gameObject.SetActive(false);
    }

    private void initText()
    {
        string[] txtOptions = { "Opties", "Options" };
        string[] txtQuit = { "Spel verlaten", "Quit" };
        string[] txtLoadGame = { "Spel laden", "Load game" };
        string[] txtNewGame = { "Nieuw spel", "New Game" };

        txtButtonLoadGame.text = txtLoadGame[taal];
        txtButtonNewGame.text = txtNewGame[taal];
        txtButtonQuit.text = txtQuit[taal];
        txtButtonOptions.text = txtOptions[taal];
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

    public void buttonSettingsClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasHomeScreen.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(true);

        initSettingsText();
        initSettingsUI();
    }

    private void initSettingsText()
    {
        string[] back = { "Terug", "Back" };
        string[] music = { "Muziek volume", "Music volume" };
        string[] effects = { "Geluidseffecten volume", "Sounds effects volume" };
        string[] language = { "Verander taal", "Change language" };
        string[] dutch = { "Nederlands", "Dutch" };
        string[] english = { "Engels", "English" };


        txtButtonSettingsBack.text = back[taal];
        txtMusicVolume.text = music[taal];
        txtEffectsVolume.text = effects[taal];
        txtLanguage.text = language[taal];
        txtToggleDutch.text = dutch[taal];
        txtToggleEnglish.text = english[taal];

        float effectsValue = AudioPlayer.Instance.soundEffect.volume * 100;
        txtEffectsVolumeSliderValue.text = effectsValue.ToString("0");

        float musicValue = AudioPlayer.Instance.backgroundMusic.volume * 100;
        txtMusicVolumeSliderValue.text = musicValue.ToString("0");
    }

    private void initSettingsUI()
    {
        if (taal == 0)
        {
            toggleDutch.isOn = true;
            toggleDutchCheck = true;
            toggleEnglishCheck = false;
            toggleEnglish.isOn = false;
        }
        else
        {
            toggleEnglish.isOn = true;
            toggleEnglishCheck = true;
            toggleDutchCheck = false;
            toggleDutch.isOn = false;
        }
    }

    public void buttonSettingsBackClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasSettings.gameObject.SetActive(false);
        canvasHomeScreen.gameObject.SetActive(true);

        initText();
    }

    public void toggleDutchValueChanged()
    {
        if (!toggleDutchCheck)
        {
            toggleDutchCheck = true;
            toggleEnglish.isOn = false;
            //game.ChangeLanguage("dutch");
            taal = ApplicationModel.language;
            initSettingsText();
        }
        else
            toggleDutchCheck = false;
    }

    public void toggleEnglishValueChanged()
    {
        if (!toggleEnglishCheck)
        {
            toggleEnglishCheck = true;
            toggleDutch.isOn = false;
            //game.ChangeLanguage("english");
            taal = ApplicationModel.language;
            initSettingsText();
        }
        else
        {
            toggleEnglishCheck = false;
        }
    }

    public void sliderEffectsValueChanged()
    {
        float value = sliderEffectsVolume.value;
        txtEffectsVolumeSliderValue.text = (value * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeEffects(value);
    }

    public void sliderMusicValueChanged()
    {
        float value = sliderMusicVolume.value;
        txtMusicVolumeSliderValue.text = (value * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeMusic(value);
    }
}
