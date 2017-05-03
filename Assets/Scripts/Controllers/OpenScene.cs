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
    private float valueMusic;
    private float valueSFX;

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
        getPlayerPrefs();
        initUI();
        initText();

        var path = Application.persistentDataPath + "/Savestate.gd";
        if (File.Exists(path))
        {
            btnLoad.interactable = true;
        }
    }

    private void getPlayerPrefs()
    {
        // PlayerPrefs Language
        if (PlayerPrefs.HasKey("savedLanguage"))
        {
            taal = PlayerPrefs.GetInt("savedLanguage");
            ApplicationModel.language = taal;
        }
        else
            taal = ApplicationModel.language;

        // PlayerPrefs Music Volume
        if (PlayerPrefs.HasKey("savedMusicVolume"))
        {
            valueMusic = PlayerPrefs.GetFloat("savedMusicVolume");
            ApplicationModel.valueMusic = valueMusic;
            AudioPlayer.Instance.backgroundMusic.volume = valueMusic;
            Debug.Log("PlayerPrefs Music: " + valueMusic);
        }
        else
            valueMusic = ApplicationModel.valueMusic;

        // PlayerPrefs SFX Volume
        if (PlayerPrefs.HasKey("savedSFXVolume"))
        {
            valueSFX = PlayerPrefs.GetFloat("savedSFXVolume");
            ApplicationModel.valueSFX = valueSFX;
            AudioPlayer.Instance.soundEffect.volume = valueSFX;
            Debug.Log("PlayerPrefs SFX: " + valueSFX);
        }
        else
            valueSFX = ApplicationModel.valueSFX;
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

        //ApplicationModel.valueSFX = AudioPlayer.Instance.soundEffect.volume * 100;
        sliderEffectsVolume.value = valueSFX;
        txtEffectsVolumeSliderValue.text = (valueSFX * 100).ToString("0");

        //ApplicationModel.valueMusic = AudioPlayer.Instance.backgroundMusic.volume * 100;
        sliderMusicVolume.value = valueMusic;
        txtMusicVolumeSliderValue.text = (valueMusic * 100).ToString("0");
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
            ApplicationModel.language = 0;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
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
            ApplicationModel.language = 1;
            taal = ApplicationModel.language;
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();
            initSettingsText();
        }
        else
        {
            toggleEnglishCheck = false;
        }
    }

    public void sliderEffectsValueChanged()
    {
        valueSFX = sliderEffectsVolume.value;
        txtEffectsVolumeSliderValue.text = (valueSFX * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeEffects(valueSFX);
        ApplicationModel.valueSFX = valueSFX;
        PlayerPrefs.SetFloat("savedSFXVolume", valueSFX);
        PlayerPrefs.Save();
    }

    public void sliderMusicValueChanged()
    {
        valueMusic = sliderMusicVolume.value;
        txtMusicVolumeSliderValue.text = (valueMusic * 100).ToString("0");
        AudioPlayer.Instance.changeVolumeMusic(valueMusic);
        ApplicationModel.valueMusic = valueMusic;
        PlayerPrefs.SetFloat("savedMusicVolume", valueMusic);
        PlayerPrefs.Save();
    }
}
