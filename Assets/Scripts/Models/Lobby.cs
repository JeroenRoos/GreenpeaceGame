using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby 
{
    public List<RoomInfo> lstRooms;

    public Lobby()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.autoJoinLobby = true;
        lstRooms = new List<RoomInfo>();
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
        ApplicationModel.multiplayer = true;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
        ApplicationModel.multiplayer = true;
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

    /*
    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        base.OnJoinedLobby();
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
        //PhotonNetwork.ConnectUsingSettings("1");
        PhotonNetwork.JoinLobby();
    }
    */

    public void StartGame(int index)
    {
        PhotonNetwork.LoadLevel(index);
        //ChattingClient c = new ChattingClient();
    }
}
