using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using System.Collections;
using System.Collections.Generic;

public class CustomNetworkLobbyManager : NetworkLobbyManager {

    public GameObject SetReadyPanel;
    public GameObject ServerMaster;

    public NetworkMatch networkMatch;
    List<MatchDesc> matchList = new List<MatchDesc>();

    public MatchDesc actualMatch;


    // Use this for initialization
    void Start () {
        
        networkMatch = gameObject.AddComponent<NetworkMatch>();
        DontDestroyOnLoad(ServerMaster);

    }

    // Update is called once per frame
    void Update () {
        
       

    }


    public override void OnClientSceneChanged(NetworkConnection conn)
    {
       
        base.OnClientSceneChanged(conn);

        GameObject[] playerLobbys = GameObject.FindGameObjectsWithTag("PlayerLobby");
        foreach(var go in playerLobbys)
        {
            if(go.GetComponent<CustomPlayerLobby>().CanvasReady != null)
            {
                Destroy(go.GetComponent<CustomPlayerLobby>().CanvasReady.gameObject);
            }
        }
        


    }






    public void PlayerWantToPlay()
    {
        new WaitForSeconds(1f);
        StartCoroutine(NewPlayerWantToPlay());
        

    }





    public IEnumerator NewPlayerWantToPlay()
    {
        yield return networkMatch.ListMatches(0, 20, "", OnMatchList);

       foreach(var match in matchList)
        {
            Debug.Log(match.currentSize);
        }


        if (matchList.Count == 0)
        {
            CreateMatchRequest create = new CreateMatchRequest();
            create.name = "RoomCreated";
            create.size = 3;
            create.password = "";
            networkMatch.CreateMatch(create, OnMatchCreate);

        }
        else
        {
            foreach (var match in matchList)
            {
                if (match.currentSize < 3 && match.currentSize != 0)
                {
                    actualMatch = match;
                    networkMatch.JoinMatch(match.networkId, "", OnMatchJoined);
                }
            }
        }


    }


    public override void OnMatchCreate(CreateMatchResponse res)
    {
        base.OnMatchCreate(res);
       
        StartCoroutine(GetMatchInfo(res.networkId));

    }

    


    public override void OnMatchList(ListMatchResponse res)
    {
        base.OnMatchList(res);

        matchList.Clear();
        if (res.matches != null)
        {
            foreach (var match in res.matches)
            {
                matchList.Add(match);
            }
        }


    }


    public IEnumerator GetMatchInfo(NetworkID netId)
    {
        yield return networkMatch.ListMatches(0, 20, "", OnMatchList);
        foreach (var match in matchList)
        {
            if (match.networkId == netId)
            {
                actualMatch = match;
                break;
            }
        }

    }


    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {        
        //var player = gamePlayer.GetComponent<Player_ID>().aze = 999;
        var playerequip = gamePlayer.GetComponent<Player_Equipment>();
        playerequip.EquipItem(GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items[11], true);
        //player.myColor = cc.myColor;
        return true;
    }









}
