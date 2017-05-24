using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : Photon.PunBehaviour
{
    public Lobby()
    {
        Debug.Log("Start Lobby");
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.automaticallySyncScene = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 2 } , null);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void LeaveLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LeaveLobby();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        base.OnConnectedToMaster();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
    }
}
