using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;

public class OpenScene : Photon.PunBehaviour
{
    #region Variables
    private RoomInfo[] rooms;
    float yOffset = 0f;

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
    public Canvas canvasLobby;
    public Canvas canvasRoom;

    // Lobby
    private string[] txtRoomButton = { "spelers", "players" };
    public Button btnRefreshLobby;
    public Text txtRefreshLobby;
    public Text txtLobby;
    public Text txtRoom;
    private string roomName;
    private string nickName;
    public Text txtMultiplayerBack;
    public Text txtMultiplayerCreateRoom;
    public Text txtAanrading;

    public RawImage imgNickname;
    public Text txtNicknameInfo;
    public InputField inputNickname;
    public Button btnChooseNickname;
    public Text txtChooseNickname;
    public Text txtCancelChooseNickname;

    public RawImage imgCreateRoom;
    public Text txtCreateRoomInfo;
    public InputField inputRoomName;
    public Button btnMultiplayerBack;
    public Button btnCreateRoom;
    public Text txtCancelCreateRoom;
    public Button btnPopupCreate;
    public Text txtPopupCreate;

    // Room
    private RoomInfo roomInfo;
    public Text txtRoomInfo;
    public Text txtRoomBack;
    public Button btnStartGameFromRoom;
    public Text txtStartGameFromRoom;
    public Text txtPlayersInRoom;
    public Text txtReadyToStart;

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
    #endregion

    #region Start(), PlayerPrefs, Init's
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

        canvasLobby.GetComponent<Canvas>();
        canvasLobby.gameObject.SetActive(false);

        canvasRoom.GetComponent<Canvas>();
        canvasRoom.gameObject.SetActive(false);
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
    #endregion

    #region Main Menu Code
    public void loadSceneByIndex(int index)
    {
        ApplicationModel.multiplayer = false;
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
    #endregion

    #region Settings Code
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
    #endregion

    #region Code for Multiplayer Lobby
    public void buttonMultiplayerOnClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasLobby.gameObject.SetActive(true);

        initChooseNickname();
        getRoomList();
    }

    private void initChooseNickname()
    {
        imgNickname.gameObject.SetActive(true);
        btnCreateRoom.interactable = false;
        btnMultiplayerBack.interactable = false;
        btnRefreshLobby.interactable = false;

        string[] txtCancel = { "Annuleer", "Cancel" };
        txtCancelChooseNickname.text = txtCancel[taal];
        string[] txtInfo = { "Voer een naam in:", "Choose a nickname:" };
        txtNicknameInfo.text = txtInfo[taal];

        string[] placeholder = { "Geef je naam...", "Enter nickname..." };
        inputNickname.placeholder.GetComponent < Text >().text = placeholder[taal];

        btnChooseNickname.gameObject.SetActive(false);

    }
    public void inputNicknameValueChanged()
    {
        inputNickname.text.Trim();

        if (inputNickname.text != "")
        {
            nickName = inputNickname.text;
            btnChooseNickname.gameObject.SetActive(true);
            string[] txtBtn = { "Kies naam", "Choose name" };
            txtChooseNickname.text = txtBtn[taal];
        }
        else
        {
            btnChooseNickname.gameObject.SetActive(false);
        }
    }


    public void buttonCancelChooseNickname()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasLobby.gameObject.SetActive(false);
    }

    public void buttonChooseNickname()
    {
        PhotonNetwork.player.NickName = nickName;
        imgNickname.gameObject.SetActive(false);
        initLobbyText();
    }

    private void initLobbyText()
    {
        string[] txtBack = { "Terug", "Back" };
        string[] txtCreate = { "Maak een kamer", "Create room" };
        string[] txtRefresh = { "Vernieuwen", "Refresh" };
        string[] txt = { "Wij raden aan om het spel al een keer gespeeld te hebben voordat je aan multiplayer begint.", "We recommend to first play singleplayer before starting a multiplayer game." };

        txtAanrading.text = txt[taal];
        txtMultiplayerBack.text = txtBack[taal];
        txtMultiplayerCreateRoom.text = txtCreate[taal];
        txtRefreshLobby.text = txtRefresh[taal];
        imgCreateRoom.gameObject.SetActive(false);
        btnCreateRoom.interactable = true;
        btnMultiplayerBack.interactable = true;
        btnRefreshLobby.interactable = true;
    }

    public void buttonMultiplayerBackClick()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasLobby.gameObject.SetActive(false);
    }

    public void buttonCreateRoomClick()
    {
        imgCreateRoom.gameObject.SetActive(true);
        btnCreateRoom.interactable = false;
        btnMultiplayerBack.interactable = false;
        btnRefreshLobby.interactable = false;

        string[] txtCancel = { "Annuleer", "Cancel" };
        txtCancelCreateRoom.text = txtCancel[taal];
        string[] txtInfo = { "Geef een kamer naam:", "Enter a room name:" };
        txtCreateRoomInfo.text = txtInfo[taal];

        string[] placeholder = { "Voer naam in...", "Enter roomname..." };
        inputRoomName.placeholder.GetComponent<Text>().text = placeholder[taal];

        btnPopupCreate.gameObject.SetActive(false);
    }

    public void inputRoomNameValueChanged()
    {
        inputRoomName.text.Trim();

        if (inputRoomName.text != "")
        {
            roomName = inputRoomName.text;
            btnPopupCreate.gameObject.SetActive(true);
            string[] txtBtn = { "Maak", "Create" };
            txtPopupCreate.text = txtBtn[taal];
        }
        else
        {
            btnPopupCreate.gameObject.SetActive(false);
        }
    }

    public void buttonCreateClick()
    {
        EventManager.CallPlayButtonClickSFX();

        lobby.CreateRoom(roomName);
        imgCreateRoom.gameObject.SetActive(false);
        btnCreateRoom.interactable = true;
        btnMultiplayerBack.interactable = true;
        btnRefreshLobby.interactable = true;
    }

    public void buttonCancelCreateRoom()
    {
        EventManager.CallPlayButtonClickSFX();
        imgCreateRoom.gameObject.SetActive(false);
        btnCreateRoom.interactable = true;
        btnMultiplayerBack.interactable = true;
        btnRefreshLobby.interactable = true;
    }

    private void getRoomList()
    {
        rooms = PhotonNetwork.GetRoomList();
    }

    public void buttonRefreshLobbyClick()
    {
        EventManager.CallPlayButtonClickSFX();
        getRoomList();
    }
    #endregion

    #region Code for Multiplayer Room
    private void initRoomText()
    {
        canvasLobby.gameObject.SetActive(false);
        canvasRoom.gameObject.SetActive(true);
        getRoomList();
        string[] txtBack = { "Terug", "Back" };
        txtRoomBack.text = txtBack[taal];
    }

    public void buttonRoomBack()
    {
        EventManager.CallPlayButtonClickSFX();
        canvasLobby.gameObject.SetActive(true);
        canvasRoom.gameObject.SetActive(false);
        lobby.LeaveRoom();
        lobby.JoinLobby();
        getRoomList();
    }

    public void buttonRoomStartGame()
    {
        EventManager.CallPlayButtonClickSFX();
        lobby.StartGame(1);
    }
    #endregion

    #region OnGUI Code
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (canvasRoom.gameObject.activeSelf)
        {
            if (PhotonNetwork.inRoom)
                txtRoom.text = "You are in a room (" + PhotonNetwork.room.Name + ")";
            else
                txtRoom.text = "You are NOT in a room!";

            string[] txtInfoRoom = {"Je hebt 2 spelers nodig om het spel te starten: \n\n" +
                PhotonNetwork.room.Name + " \nAantal spelers: " + PhotonNetwork.room.PlayerCount + "\nAantal spelers nodig om te starten: " + PhotonNetwork.room.MaxPlayers

                , "You need 2 players to start the game: \n\n" +
                PhotonNetwork.room.Name + " \nNumber of players: " + PhotonNetwork.room.PlayerCount + "\nNeeded amount of players: " + PhotonNetwork.room.MaxPlayers };
            txtRoomInfo.text = txtInfoRoom[taal];

            txtPlayersInRoom.text = "Players in this room:";
            foreach (PhotonPlayer p in PhotonNetwork.playerList)
                txtPlayersInRoom.text += "\n" + p.NickName;


            if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
            {
                string[] txtBtn = { "Start spel", "Start game" };
                txtStartGameFromRoom.text = txtBtn[taal];

                if (PhotonNetwork.isMasterClient)
                {
                    string[] txtInfo = { "Je kunt het spel starten!", "You are ready to start the game!" };
                    txtReadyToStart.text = txtInfo[taal];
                    btnStartGameFromRoom.interactable = true;
                }
                else
                {
                    string[] txtInfo = { "Alleen de host kan het spel starten...", "Only the host can start the game..." };
                    txtReadyToStart.text = txtInfo[taal];
                    btnStartGameFromRoom.interactable = false;
                }
            }
            else
            {
                string[] txtInfo = { "Er zijn niet genoeg spelers om het spel te starten...", "You need more players to start the game..." };
                txtReadyToStart.text = txtInfo[taal];
                btnStartGameFromRoom.interactable = false;
            }
        }

        if (canvasLobby.gameObject.activeSelf)
        {
            if (PhotonNetwork.insideLobby)
                txtLobby.text = "You are in a lobby!";
            else
                txtLobby.text = "You are NOT in a lobby!";

            if (!imgNickname.gameObject.activeSelf)
            {
                if (rooms.Length != 0)
                {
                    txtNoRooms.gameObject.SetActive(false);
                    yOffset = 0;

                    foreach (RoomInfo game in rooms)
                    {
                        RectTransform rectPosition = btnPosition.GetComponent<RectTransform>();
                        Vector3 btnPos = btnPosition.transform.position;
                        float screenHeight = Screen.height;

                        float x = btnPos.x;// - rectPosition.rect.width;
                        float y = btnPos.z + (screenHeight / 3);

                        if (game.MaxPlayers != game.PlayerCount)
                        {
                            if (GUI.Button(new Rect(x, y + yOffset, rectPosition.rect.width + 50, rectPosition.rect.height), game.Name + " " + game.PlayerCount + " / " + game.MaxPlayers + txtRoomButton[taal], buttonStyle))
                            {
                                lobby.JoinRoom(game.Name);
                                roomName = game.Name;
                            }
                        }
                        yOffset += 35;
                    }
                }
                else
                {
                    txtNoRooms.gameObject.SetActive(true);
                    string[] txt = { "Er zijn op het moment geen kamers beschikbaar", "There are no rooms available at this moment" };
                    txtNoRooms.text = txt[taal];
                }
            }
        }
    }
    #endregion

    #region PUNBehaviour
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
        initLobbyText();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        base.OnJoinedRoom();
        initRoomText();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        //initLobbyText();
        //PhotonNetwork.ConnectUsingSettings("1");
        //PhotonNetwork.JoinLobby();
    }
    #endregion
}

