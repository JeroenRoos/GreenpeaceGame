using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;

public class OpenScene : MonoBehaviour
{
    Lobby lobby;
    public Texture2D buttonTexture;
    private GUIStyle buttonStyle = new GUIStyle();
    public Button btnPosition;
    public Text txtNoRooms;

    public Text txtButtonNewGame;
    public Text txtButtonLoadGame;
    public Text txtButtonQuit;
    public Text txtButtonOptions;
    public Text txtButtonMultiplayer;

    public Button btnLoad;
    private int taal;
    private float valueMusic;
    private float valueSFX;

    public Canvas canvasHomeScreen;
    public Canvas canvasMultiplayerScreen;

    // Multiplayer
    public Text txtMultiplayerBack;
    public Text txtMultiplayerCreateRoom;

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
        lobby = new Lobby();
        Application.runInBackground = true;

        EventManager.CallPlayBackgroundMusic();
        getPlayerPrefs();
        initUI();
        initText();
        initGuiStyle();

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
        }
        else
            valueMusic = ApplicationModel.valueMusic;

        // PlayerPrefs SFX Volume
        if (PlayerPrefs.HasKey("savedSFXVolume"))
        {
            valueSFX = PlayerPrefs.GetFloat("savedSFXVolume");
            ApplicationModel.valueSFX = valueSFX;
            AudioPlayer.Instance.soundEffect.volume = valueSFX;
        }
        else
            valueSFX = ApplicationModel.valueSFX;
    }

    private void initUI()
    {
        canvasHomeScreen.GetComponent<Canvas>();

        canvasSettings.GetComponent<Canvas>();
        canvasSettings.gameObject.SetActive(false);

        canvasMultiplayerScreen.GetComponent<Canvas>();
        canvasMultiplayerScreen.gameObject.SetActive(false);
    }

    private void initText()
    {
        string[] txtOptions = { "Opties", "Options" };
        string[] txtQuit = { "Spel verlaten", "Quit" };
        string[] txtLoadGame = { "Spel laden", "Load game" };
        string[] txtNewGame = { "Nieuw spel", "New Game" };
        string txtMultiplayer = "Multiplayer";

        txtButtonLoadGame.text = txtLoadGame[taal];
        txtButtonNewGame.text = txtNewGame[taal];
        txtButtonQuit.text = txtQuit[taal];
        txtButtonOptions.text = txtOptions[taal];
        txtButtonMultiplayer.text = txtMultiplayer;
    }

    private void initGuiStyle()
    {
        buttonStyle.normal.background = buttonTexture;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        Color c = new Color();
        ColorUtility.TryParseHtmlString("#ccac6f", out c);
        buttonStyle.normal.textColor = c;
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

    public void buttonMultiplayerOnClick()
    {
        EventManager.CallPlayButtonClickSFX();
        //canvasHomeScreen.gameObject.SetActive(false);
        canvasMultiplayerScreen.gameObject.SetActive(true);
        lobby.JoinLobby();
        initMultiplayerText();
    }

    private void initMultiplayerText()
    {
        string[] txtBack = { "Terug", "Back" };
        string[] txtCreate = { "Maak een room", "Create room" };

        txtMultiplayerBack.text = txtBack[taal];
        txtMultiplayerCreateRoom.text = txtCreate[taal];
    }

    public void buttonMultiplayerBackClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasMultiplayerScreen.gameObject.SetActive(false);
        lobby.LeaveLobby();
    }

    public void buttonCreateRoomClick()
    {
        Debug.Log("CREATE");
        lobby.CreateRoom("TestRoom");
    }

    void OnGUI()
    {
        if (canvasMultiplayerScreen.gameObject.activeSelf)
        {
            Debug.Log("Multiplayer Canvas Active");

            RoomInfo[] lstRooms = PhotonNetwork.GetRoomList();

            if (lstRooms.Length != 0)
            {
                txtNoRooms.gameObject.SetActive(false);
                Debug.Log("Rooms Available");

                foreach (RoomInfo game in lstRooms)
                {
                    float yOffset = 0f;

                    RectTransform rectPosition = btnPosition.GetComponent<RectTransform>();
                    Vector3 btnPos = btnPosition.transform.position;
                    float screenHeight = Screen.height;

                    float x = btnPos.x;
                    float y = btnPos.z + (screenHeight / 3);

                    if (GUI.Button(new Rect(0, 0 + yOffset, rectPosition.rect.width + 50, rectPosition.rect.height), game.Name + " " + game.PlayerCount + "/" + game.MaxPlayers, buttonStyle))
                    {
                        PhotonNetwork.JoinRoom(game.Name);
                        yOffset += 35;
                    }
                }
            }
            else
            {
                Debug.Log("No Rooms Available");
                txtNoRooms.gameObject.SetActive(true);
                string[] txt = { "Er zijn op het moment geen rooms beschikbaar", "There are no rooms available at this moment" };
                txtNoRooms.text = txt[taal];
            }
        }
    }
}

