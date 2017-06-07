using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.IO;

public class OpenScene : Photon.PunBehaviour
{
    // De variabele die gebruikt worden in deze class en in de inspector
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
    public Text txtLobbyTitle;
    private string[] txtRoomButton = { " spelers", " players" };
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
    public Text txtRoomTitle;
    private RoomInfo roomInfo;
    public Text txtRoomInfo;
    public Text txtRoomBack;
    public Button btnStartGameFromRoom;
    public Text txtStartGameFromRoom;
    public Text txtPlayersInRoom;
    public Text txtReadyToStart;

    // Settings 
    public Text txtSettingsTitle;
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
        // Begin een nieuwe lobby en zorgt in constructor van Lobby ervoor dat speler automatisch een lobby joined
        lobby = new Lobby();
        Application.runInBackground = true;

        EventManager.CallPlayBackgroundMusic();

        // Haalt de playerPrefs op
        getPlayerPrefs();

        // Initialize de UI en Text
        initUI();
        initText();
        initGuiStyle();

        // Als er een save aanwezig is wordt de button "Verder spelen" op interactable gezet
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
        // Initialized alle canvassen die in deze scene gebruikt kunnen worden en zet ze op false
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
        // Initialize de tekst die in de buttons komen te staan
        string[] txtOptions = { "Instellingen", "Settings" };
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
        // Initialize de GuiStyle waar later in de lobby canvas gebruik van wordt gemaakt
        buttonStyle.normal.background = buttonTexture;
        buttonStyle.alignment = TextAnchor.MiddleCenter;
        Color c = new Color();
        ColorUtility.TryParseHtmlString("#ffffff", out c);
        buttonStyle.normal.textColor = c;
    }
    #endregion

    #region Main Menu Code

    // Toegewezen aan button "Nieuw Spel" in inspector
    public void loadSceneByIndex(int index)
    {
        ApplicationModel.multiplayer = false;
        EventManager.CallPlayButtonClickSFX();

        // Laad de volgende scene met load game op false
        ApplicationModel.loadGame = false;
        SceneManager.LoadSceneAsync(index);
    }

    // Toegewezen aan button "Spel Verlaten" in inspector
    public void buttonExitOnClick()
    {
        EventManager.CallPlayButtonClickSFX();
        Application.Quit();
    }

    // Toegewezen aan button "Spel Laden" in inspector
    public void buttonLoadGameClick()
    {
        EventManager.CallPlayButtonClickSFX();

        // Laad de volgende scene met load game op true
        ApplicationModel.loadGame = true;
        SceneManager.LoadSceneAsync(1);
    }

    // Toegewezen aan button "Instellingen" in inspector
    public void buttonSettingsClick()
    {
        EventManager.CallPlayButtonClickSFX();

        // Zet huidige canvas op false en openen nieuwe canvas van de Settings
        canvasHomeScreen.gameObject.SetActive(false);
        canvasSettings.gameObject.SetActive(true);

        // Initialize de tekst en UI uit het canvas settings
        initSettingsText();
        initSettingsUI();
    }
    #endregion

    #region Settings Code
    private void initSettingsText()
    {
        // De tekst uit het canvas worden geset
        string[] back = { "Terug", "Back" };
        string[] music = { "Muziek volume", "Music volume" };
        string[] effects = { "Geluidseffecten volume", "Sounds effects volume" };
        string[] language = { "Verander taal", "Change language" };
        string[] dutch = { "Nederlands", "Dutch" };
        string[] english = { "Engels", "English" };
        string[] title = { "Instellingen", "Settings"};

        txtSettingsTitle.text = title[taal];
        txtButtonSettingsBack.text = back[taal];
        txtMusicVolume.text = music[taal];
        txtEffectsVolume.text = effects[taal];
        txtLanguage.text = language[taal];
        txtToggleDutch.text = dutch[taal];
        txtToggleEnglish.text = english[taal];

        // De tekst waarde achter de slider voor SFX wordt geset
        sliderEffectsVolume.value = valueSFX;
        txtEffectsVolumeSliderValue.text = (valueSFX * 100).ToString("0");

        // De tekst waarde achter de slider voor Music wordt geset
        sliderMusicVolume.value = valueMusic;
        txtMusicVolumeSliderValue.text = (valueMusic * 100).ToString("0");
    }

    private void initSettingsUI()
    {
        if (taal == 0)
        {
            // Als het spel op NL staat wordt de radiobutton voor NL op true gezet en die van ENG op false
            toggleDutch.isOn = true;
            toggleDutchCheck = true;
            toggleEnglishCheck = false;
            toggleEnglish.isOn = false;
        }
        else
        {
            // Als het spel op ENG staat wordt de radiobutton voor ENG op true gezet en die van NL op false
            toggleEnglish.isOn = true;
            toggleEnglishCheck = true;
            toggleDutchCheck = false;
            toggleDutch.isOn = false;
        }
    }

    // Toegewezen aan de button "Terug" in de inspector
    public void buttonSettingsBackClick()
    {
        // Als er op de terug button wordt gedrukt wordt het juiste canvas weer actief gezet
        EventManager.CallPlayButtonClickSFX();
        canvasSettings.gameObject.SetActive(false);
        canvasHomeScreen.gameObject.SetActive(true);

        // Set de tekst van het home screen opnieuw
        initText();
    }

    // Toegewezen aan de radiobutton "Nederlands" in de inspector
    public void toggleDutchValueChanged()
    {
        if (!toggleDutchCheck)
        {
            toggleDutchCheck = true;
            toggleEnglish.isOn = false;

            // Zet de taal naar 0/NL
            ApplicationModel.language = 0;
            taal = ApplicationModel.language;

            // Sla de gekozen taal op in PlayerPrefs
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();

            // Initialize de Text van settings opnieuw omdat taal veranderd is
            initSettingsText();
        }
        else
            toggleDutchCheck = false;
    }

    // Toegewezen aan de radiobutton "Engels" in de inspector
    public void toggleEnglishValueChanged()
    {
        if (!toggleEnglishCheck)
        {
            // Zet de taal naar Engels
            toggleEnglishCheck = true;
            toggleDutch.isOn = false;

            // Zet det taal naar 1/ENG
            ApplicationModel.language = 1;
            taal = ApplicationModel.language;

            // Sla de gekozen taal op in PlayerPrefs
            PlayerPrefs.SetInt("savedLanguage", taal);
            PlayerPrefs.Save();

            // Initialize de Text van settings opnieuw omdat taal veranderd is
            initSettingsText();
        }
        else
            toggleEnglishCheck = false;

    }

    // Toegewezen aan de slider "SFX" in de inspector
    public void sliderEffectsValueChanged()
    {
        // Zet de value van de slider in een variable en gebruik deze om de text waarde achter de slider aan te passen
        valueSFX = sliderEffectsVolume.value;
        txtEffectsVolumeSliderValue.text = (valueSFX * 100).ToString("0");

        // Verander het volume van de SFX
        AudioPlayer.Instance.changeVolumeEffects(valueSFX);
        ApplicationModel.valueSFX = valueSFX;

        // Sla het volume van de SFX op in PlayerPrefs
        PlayerPrefs.SetFloat("savedSFXVolume", valueSFX);
        PlayerPrefs.Save();
    }

    // Toegewezen aan de slider "Music" in de inspector
    public void sliderMusicValueChanged()
    {
        // Zet de value van de slider in een variable en gebruik deze om de text waarde achter de slider aan te passen
        valueMusic = sliderMusicVolume.value;
        txtMusicVolumeSliderValue.text = (valueMusic * 100).ToString("0");

        // Verander het volume van de Music
        AudioPlayer.Instance.changeVolumeMusic(valueMusic);
        ApplicationModel.valueMusic = valueMusic;

        // Sla het volume van de Music op in PlayerPrefs
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
        string[] title = { "Multiplayer lobby", "Multiplayer Lobby" };

        txtLobbyTitle.text = title[taal];
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

        string[] title = { "Multiplayer kamer - " + PhotonNetwork.room.Name, "Multiplayer room - " + PhotonNetwork.room.Name };

        txtRoomTitle.text = title[taal];
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

            string[] txtplayersInroom = { "Spelers in deze kamer:", "Players in this room:" };
            txtPlayersInRoom.text = txtplayersInroom[taal];
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

