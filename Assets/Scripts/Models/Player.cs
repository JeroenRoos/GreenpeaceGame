using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : Photon.MonoBehaviour {

    public PhotonView photonView;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update() { }

    //send current mouse position if you control the prefab, otherwise receive the mouse position of the other player
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 temp = Input.mousePosition;
            temp.z = -3;
            stream.SendNext(Camera.main.ScreenToWorldPoint(temp));
        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
        }
    }

    /*the only data sent over the network is data that is either randomly generated or manual input from a player.
    This way the game of both players stays in sync while keeping the network usage relatively low.
    Event generation for eample is only done on the masterclient.
    */
    #region OtherPlayerTriggers
    [PunRPC]
    void NextTurnClicked()
    {
        MultiplayerManager.CallNextTurnClicked();
    }

    [PunRPC]
    void MoneyChanged(double changevalue, bool isAdded)
    {
        MultiplayerManager.CallChangeOtherPlayerMoney(changevalue, isAdded);
    }

    [PunRPC]
    void TurnChanged()
    {
        MultiplayerManager.CallNextTurnStarted();
    }

    [PunRPC]
    void ActionStarted(string regionName, string actionName, bool[] pickedSectors)
    {
        MultiplayerManager.CallStartAction(regionName, actionName, pickedSectors);
    }

    [PunRPC]
    void EventGenerated(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2)
    {
        MultiplayerManager.CallStartEvent(regionName, eventName, pickedConsequences0, pickedConsequences1, pickedConsequences2,
            pickedTemporaryConsequences0, pickedTemporaryConsequences1, pickedTemporaryConsequences2);
    }

    [PunRPC]
    void EventChoiceMade(string regionName, string eventName, int pickedChoiceNumber)
    {
        MultiplayerManager.CallPickEventChoice(regionName, eventName, pickedChoiceNumber);
    }

    [PunRPC]
    void CardUsed(string regionName, double[] cardValues, bool isGlobal)
    {
        MultiplayerManager.CallPlayCard(regionName, cardValues, isGlobal);
    }

    [PunRPC]
    void InvestmentMade(string investmentType)
    {
        MultiplayerManager.CallInvest(investmentType);
    }

    [PunRPC]
    void BuildingMade(string regionName, string buildingID)
    {
        MultiplayerManager.CallMakeBuilding(regionName, buildingID);
        // MultiplayerManager.CallUpdateActivity(playerNickname + "heeft een " + buildingID + " gebouwed in " + regionName,
        //     playerNickname + "build a " + buildingID + " in " + regionName);
    }

    [PunRPC]
    void MessageReceived(string message, string senderName)
    {
        MultiplayerManager.CallUpdateChat(message, senderName);
    }

    [PunRPC]
    void PlayerLogChanged(string nl, string eng)
    {
        MultiplayerManager.CallUpdateLogMessage(nl, eng);
    }

    [PunRPC]
    void ActivityLogChanged(string nl, string eng)
    {
        MultiplayerManager.CallUpdateActivity(nl, eng);
    }
    #endregion
}
