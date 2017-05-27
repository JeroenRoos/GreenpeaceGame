using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : Photon.MonoBehaviour {

    public PhotonView photonView;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //photonView.RPC("SyncGame", PhotonTargets.Others, GameContainer.XmlSerializeToString(game));
    }

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
    void ActionStarted(string RegionName, string ActionName, bool[] pickedSectors)
    {
        MultiplayerManager.CallStartAction(RegionName, ActionName, pickedSectors);
    }

    [PunRPC]
    void EventGenerated(string regionName, string eventName, double[] pickedConsequences0,
        double[] pickedConsequences1, double[] pickedConsequences2, double[] pickedTemporaryConsequences0,
        double[] pickedTemporaryConsequences1, double[] pickedTemporaryConsequences2)
    {
        MultiplayerManager.CallStartEvent(regionName, eventName, pickedConsequences0, pickedConsequences1, pickedConsequences2, 
            pickedTemporaryConsequences0, pickedTemporaryConsequences1, pickedTemporaryConsequences2);
    }
}
