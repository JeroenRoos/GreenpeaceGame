using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.Chat;
using UnityEngine;

class ChattingClient : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    private UpdateUI ui;

    public ChattingClient(UpdateUI ui)
    {
        Debug.Log("Start ChattingClient");
        this.ui = ui;
        ExitGames.Client.Photon.ConnectionProtocol connectProtocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
        chatClient = new ChatClient(this, connectProtocol);
        chatClient.ChatRegion = "EU";

        ExitGames.Client.Photon.Chat.AuthenticationValues authValues = new ExitGames.Client.Photon.Chat.AuthenticationValues();
        authValues.UserId = "uniqueUserName";
        authValues.AuthType = ExitGames.Client.Photon.Chat.CustomAuthenticationType.None;

        chatClient.Connect(/*"d12a70e0-dafa-4716-abf7-9ac2c39619d6"*/"ebbf7b53-da3e-4247-931d-76a205592ee0", "0.1", authValues);
    }

    void Start()
    {

    }

    void Update()
    {
        // if (chatClient != null)
        // {
        Debug.Log("UpdateUI ChattingClient");
            chatClient.Service();
       // }
    }

    public void publishMessage(string roomName, string message)
    {
        chatClient.PublishMessage(roomName, message);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnChatStateChange(ChatState state)
    {
        throw new NotImplementedException();
    }

    public void OnConnected()
    {
        //subscribe to chat channel once connected to server
        chatClient.Subscribe(new string[] { PhotonNetwork.room.Name }); 
    }

    public void OnDisconnected()
    {
        throw new NotImplementedException();
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        int msgCount = messages.Length;
        for (int i = 0; i < msgCount; i++)
        {   
            //go through each received msg
            string sender = senders[i];
            string msg = messages[i].ToString();
            ui.txtChatMessages.text = sender + ": " + msg;
        }
    }

    public void Disconnect()
    {
        chatClient.Disconnect();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        Debug.Log("Subscribed to a new channel!");
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new NotImplementedException();
    }
}

