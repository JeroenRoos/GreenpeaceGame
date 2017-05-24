using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplayer : Photon.PunBehaviour {

    public string playerID;


	// Use this for initialization
	void Start ()
    {
        playerID = PhotonNetwork.player.UserId;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
