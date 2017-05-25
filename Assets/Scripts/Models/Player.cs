using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : Photon.MonoBehaviour {
    public PhotonView photonView;
    public int test;

	// Use this for initialization
	void Start ()
    {
        photonView = GetComponent<PhotonView>();
        //photonView = PhotonView.Get(this);
        test = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    void SyncGame(Game game)
    {
    }
}
