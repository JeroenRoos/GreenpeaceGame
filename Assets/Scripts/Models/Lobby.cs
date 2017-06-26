using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby 
{

    public Lobby()
    {
        // Zorgt ervoor dat de speler een lobby joined
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.autoJoinLobby = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    // Geef de detailed connection string for debugging
    /*void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }*/

    // Create een room met maximaal 2 spelers
    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 2 } , null);
        ApplicationModel.multiplayer = true;
    }

    // Join een room based on room name, je wordt hierheen gestuurd vanuit de onGUI in de OpenScene class
    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        ApplicationModel.multiplayer = true;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // Stuurt je door naar Photonnetwork leave lobby en disconnect
    public void LeaveLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveLobby();
    }

    // Stuurt door naar de join lobby in Photonnetwork
    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    // Start de game als de masterclient op start game drukt
    public void StartGame(int index)
    {
        PhotonNetwork.LoadLevel(index);
    }
}
