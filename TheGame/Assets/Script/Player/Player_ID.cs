using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_ID : NetworkBehaviour
{
    [SyncVar]
    public int playerCharacterId = -1;
    [SyncVar]
    public int nbPlayers = 2;
    [SyncVar]
    public bool ClientReady = false;



    public bool IsLoaded = false;


    bool isLoading = true;

    bool checker = true;
    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        SetCharacterId();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCharacterId != -1 && isLoading)
        {
            isLoading = false;
            name = "Player_" + playerCharacterId;
            StartCoroutine(LoadCharacterEquipment());
            
        }

        if (checker)
        {

            if (isLocalPlayer)
            {

                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length == nbPlayers)
                {
                    int count = 0;
                    foreach (var play in players)
                    {
                        if (play.GetComponent<Player_ID>().IsLoaded)
                        {
                            count++;
                        }
                    }

                    if (count == nbPlayers)
                    {
                        checker = false;
                        CmdTellServerClientReady();
                    }
                }

               

            }
        }
    }


    public void SetCharacterId()
    {
        playerCharacterId = GameObject.Find("Player_Dummy").GetComponent<Player_Character>().CharacterId;       
        CmdTellServerMyIdentity(playerCharacterId);
    }



    [Command]
    void CmdTellServerMyIdentity(int charcter_id)
    {
        
        playerCharacterId = charcter_id;
    }

    [Command]
    void CmdTellServerClientReady()
    {
        ClientReady = true;
    }



    public IEnumerator LoadCharacterEquipment()
    {
        //gameObject.name = "Player_" + playerCharacterId;
        WebServices webtemp = GameObject.Find("WebServices").GetComponent<WebServices>();

        yield return StartCoroutine(webtemp.GetCharacterEquipment(playerCharacterId));

        GetComponent<Player_Equipment>().LoadEquipmentFromDBData(webtemp.data);
        IsLoaded = true;
    }


 
}
