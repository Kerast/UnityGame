using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LoadingProgress : NetworkBehaviour {

    [SyncVar] public int CountPlayerLoaded = 0;
    public int nbPlayers = 2;

    public List<GameObject> Positions;

    bool checkerClient = true;
    bool checkerServer = true;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        

        if (checkerServer)
        {
            if (isServer)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length == nbPlayers)
                {
                    int count = 0;
                    foreach (var play in players)
                    {
                        if (play.GetComponent<Player_ID>().ClientReady)
                        {
                            count++;
                        }
                    }

                    if (count == nbPlayers)
                    {
                        RpcMovePlayers();
                        StartCoroutine(ChangeLevel());
                    }
                }
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




    IEnumerator ChangeLevel()
    {
        yield return new WaitForSeconds(2f);
        RpcLoadScene(3);
    }


    [ClientRpc]
    public void RpcLoadScene(int levelNumber)
    {
        Application.LoadLevel(levelNumber);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Player_NetworkSetup>().enabled = true;
        }
    }
}
