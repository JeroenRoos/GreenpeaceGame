using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : Photon.MonoBehaviour {

    public PhotonView photonView;
    public Game game;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        //photonView.RPC("SyncGame", PhotonTargets.Others, GameContainer.XmlSerializeToString(game));
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            //stream.SendNext(game);
        }
        else
        {
            // Network player, receive data
            //game = (Game)stream.ReceiveNext();
        }
    }


    [PunRPC]
    void ActionStarted(string RegionName, string ActionName, bool[] pickedSectors)
    {
        Debug.Log("Action start... ismine:" + photonView.isMine);
        Debug.Log(game.regions.Count);
        if (game != null)
        {
            foreach (MapRegion r in game.regions)
            {
                Debug.Log(r.name[0]);
                if (r.name[0] == RegionName)
                {
                    foreach (RegionAction rA in r.actions)
                    {
                        Debug.Log(rA.name[0]);
                        if (rA.name[0] == ActionName)
                        {
                            r.StartOtherPlayerAction(rA, game, pickedSectors);
                            return;
                        }
                    }
                }
            }
        }
    }

    /*[PunRPC]
    void syncGame(string game)
    {
        //this.game = GameContainer.XmlDeserializeFromString(game);

        GameController gC = GetComponent<GameController>();
        game = gC.game;
    }*/

    [PunRPC]
    void syncGame()
    {
        GameController gC = GetComponent<GameController>();
        game = gC.game;
    }
}
