using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class PlayersLoading : NetworkBehaviour {

    public bool checker = true;

    public List<GameObject> Positions;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


        if (isServer && checker)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 2  )
            {

                for (int i = 0; i < players.Length; i++)
                {


                    RpcMovePlayers();


                }

                checker = false;
            }
        }
    }


    [ClientRpc]
    public void RpcMovePlayers()
    {

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = Positions[i].transform.position;
            players[i].transform.rotation = Positions[i].transform.rotation;
        }
    }


 
}
