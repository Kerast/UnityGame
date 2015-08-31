using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheGame.ItemSystem;
using Boomlagoon.JSON;


public class Player_Equipment : MonoBehaviour {


    public ISItem Weapon;
	public ISItem Helmet;
	public ISItem Torse;
	public ISItem Belt;
	public ISItem ShoulderR;
	public ISItem ShoulderL;
	public ISItem GloveR;
	public ISItem GloveL;
	public ISItem Legs;


    public GameObject weaponNode;
    public GameObject helmetNode;
    public GameObject torseNode;
    public GameObject beltNode;
    public GameObject shoulderRNode;
    public GameObject shoulderLNode;
    public GameObject gloveRNode;
    public GameObject gloveLNode;
    public GameObject legsNode;


    public List<int> equipmentsIDs = new List<int>();
    public int selectedEquipment;



    void Awake()
    {
        
        
        //Tout nu
        List<ISItem> Items = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items;
        EquipItem(Items.ElementAt(0), true);
        EquipItem(Items.ElementAt(1), true);
        EquipItem(Items.ElementAt(2), true);
        EquipItem(Items.ElementAt(3), true);
        EquipItem(Items.ElementAt(4), true);
        EquipItem(Items.ElementAt(5), true);
        EquipItem(Items.ElementAt(6), true);
        EquipItem(Items.ElementAt(7), true);
        EquipItem(Items.ElementAt(8), true);

    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void EquipItem(ISItem item, bool isLoading)
    {
        GameObject slot = null;
        ISItem oldItem =  new ISItem();
        if (item.EquipmentSlot == EquipmentSlot.Head)
        {
            slot = helmetNode;
            oldItem = Helmet;
            Helmet = item;
        }

        else if (item.EquipmentSlot == EquipmentSlot.Torse)
        {
            slot = torseNode;
            oldItem = Torse;
            Torse = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Belt)
        {
            slot = beltNode;
            oldItem = Belt;
            Belt = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.ShoulderR)
        {
            slot = shoulderRNode;
            oldItem = ShoulderR;
            ShoulderR = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.ShoulderL)
        {
            slot = shoulderLNode;
            oldItem = ShoulderL;
            ShoulderL = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.GloveL)
        {
            slot = gloveLNode;
            oldItem = GloveL;
            GloveL = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.GloveR)
        {
            slot = gloveRNode;
            oldItem = GloveR;
            GloveR = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Legs)
        {
            slot = legsNode;
            oldItem = Legs;
            Legs = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Weapon)
        {
            slot = weaponNode;
            oldItem = Weapon;
            Weapon = item;


        }

        DetachSlot(slot);


        GameObject itemTemp = Instantiate(item.SelectedSkin);   
        itemTemp.transform.SetParent(slot.transform) ;
        itemTemp.transform.position = slot.transform.position;
        itemTemp.transform.rotation = slot.transform.rotation;
        itemTemp.transform.localScale = slot.transform.localScale;

        if(!isLoading)
        {
            WebServices webservices = GameObject.Find("WebServices").GetComponent<WebServices>();
            StartCoroutine(webservices.SaveEquipmentPlayerChange(equipmentsIDs[selectedEquipment], oldItem.Identity, item.Identity, item.SelectedSkin.name));

        }
    }

    void DetachSlot(GameObject slot)
    {
        if (slot.transform.childCount > 0)
        {
            GameObject removedItem = slot.transform.GetChild(0).gameObject;
            Destroy(removedItem);
        }
    }

    public void LoadEquipmentFromDBData(JSONObject data)
    {
       
        GameManager_Assets assets = GameObject.Find("GameManager").GetComponent<GameManager_Assets>();

        JSONArray equipments = data.GetArray("equipments");

        List<List<string>> Equipments = new List<List<string>>();
        

        foreach (var equipment in equipments)
        {
            JSONArray items = equipment.Obj.GetArray("items");
            selectedEquipment = 0;
            List<string> itemsList = new List<string>();
            Equipments.Add(itemsList);
            equipmentsIDs.Add((int)equipment.Obj.GetNumber("character_equipment_id"));
            foreach (var item in items)
            {
                string itemName = item.Obj.GetString("equiped_item_name");
                string itemSkinName = item.Obj.GetString("equiped_item_skin");


                ISItem itemToLoad = assets.Items.Find(i => i.Identity == itemName);
                if (itemToLoad != null)
                {
                    itemToLoad.SelectedSkin = itemToLoad.Skins.Find(i => i.name == itemSkinName);
                    EquipItem(itemToLoad, true);
                }

            }
        }

        
    }

}
