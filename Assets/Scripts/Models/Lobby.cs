using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1.0");
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
        PhotonNetwork.CreateRoom(name);
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
        PhotonNetwork.ConnectUsingSettings("1");
        PhotonNetwork.JoinLobby();
    }

    public void StartGame(int index)
    {
        ApplicationModel.multiplayer = true;
        if (PhotonNetwork.isMasterClient)
        {
            SceneManager.LoadSceneAsync(index);
        }
    }
}
