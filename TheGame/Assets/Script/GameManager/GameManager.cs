using UnityEngine;
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
        
        GameManager_Assets assets = GameObject.Find("GameManager").GetComponent<GameManager_Assets>();


        JSONObject playerData = GameObject.Find("WebServices").GetComponent<WebServices>().data;
        JSONArray characters = playerData.GetArray("character");
        JSONArray equipments = characters[0].Obj.GetArray("equipments");

        List<List<string>> Equipments = new List<List<string>>();
        
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
       

    }
}
