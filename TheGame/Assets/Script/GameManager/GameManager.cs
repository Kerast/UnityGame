using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using TheGame.ItemSystem;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.Object.DontDestroyOnLoad(gameObject);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            LoadPlayerData();
        }
    }


    void LoadPlayerData()
    {
        Player_Equipment player_equip = GameObject.Find("Player_Dummy").GetComponent<Player_Equipment>();
        Player_Character player_character = GameObject.Find("Player_Dummy").GetComponent<Player_Character>();



        GameManager_Assets assets = GameObject.Find("GameManager").GetComponent<GameManager_Assets>();



        JSONObject playerData = GameObject.Find("WebServices").GetComponent<WebServices>().data;
        JSONArray characters = playerData.GetArray("character");
        JSONArray equipments = characters[0].Obj.GetArray("equipments");

        List<List<string>> Equipments = new List<List<string>>();


        player_character.UcUserId = (int) characters[0].Obj.GetNumber("uc_user_id");
        player_character.CharacterId = (int) characters[0].Obj.GetNumber("character_id");
        player_character.CharacterName= characters[0].Obj.GetString("name");

        foreach (var equipment in equipments)
        {
            JSONArray items =  equipment.Obj.GetArray("items");
            player_equip.selectedEquipment = 0;
            List<string> itemsList = new List<string>();
            Equipments.Add(itemsList);
            player_equip.equipmentsIDs.Add((int)equipment.Obj.GetNumber("character_equipment_id"));
            foreach (var item in items)
            {
                string itemName = item.Obj.GetString("equiped_item_name");
                string itemSkinName = item.Obj.GetString("equiped_item_skin");


                ISItem itemToLoad = assets.Items.Find(i => i.Identity == itemName);
                if (itemToLoad != null)
                {
                    itemToLoad.SelectedSkin = itemToLoad.Skins.Find(i => i.name == itemSkinName);
                    player_equip.EquipItem(itemToLoad, true);                   
                }
                                    
            }
        }

        GameObject.Find("Camera").transform.position = GameObject.Find("MainMenuPosition").transform.position;
        GameObject.Find("Camera").transform.rotation = GameObject.Find("MainMenuPosition").transform.rotation;


    }
}
