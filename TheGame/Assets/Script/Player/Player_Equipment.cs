using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheGame.ItemSystem;


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




    void Awake()
    {
        //Tout nu
        List<ISItem> Items = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items;
        EquipItem(Items.ElementAt(0));
        EquipItem(Items.ElementAt(1));
        EquipItem(Items.ElementAt(2));
        EquipItem(Items.ElementAt(3));
        EquipItem(Items.ElementAt(4));
        EquipItem(Items.ElementAt(5));
        EquipItem(Items.ElementAt(6));
        EquipItem(Items.ElementAt(7));
        EquipItem(Items.ElementAt(8));

    }



    public void EquipItem(ISItem item)
    {
        GameObject slot = null;
        if (item.EquipmentSlot == EquipmentSlot.Head)
        {
            slot = helmetNode;
            Helmet = item;
        }

        else if (item.EquipmentSlot == EquipmentSlot.Torse)
        {
            slot = torseNode;
            Torse = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Belt)
        {
            slot = beltNode;
            Belt = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.ShoulderR)
        {
            slot = shoulderRNode;
            ShoulderR = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.ShoulderL)
        {
            slot = shoulderLNode;
            ShoulderL = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.GloveL)
        {
            slot = gloveLNode;
            GloveL = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.GloveR)
        {
            slot = gloveRNode;
            GloveR = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Legs)
        {
            slot = legsNode;
            Legs = item;


        }
        else if (item.EquipmentSlot == EquipmentSlot.Weapon)
        {
            slot = weaponNode;
            Weapon = item;


        }

        DetachSlot(slot);


        GameObject itemTemp = Instantiate(item.SelectedSkin);
        itemTemp.transform.parent = slot.transform;
        itemTemp.transform.position = slot.transform.position;
        itemTemp.transform.localScale = slot.transform.localScale;
    }

    void DetachSlot(GameObject slot)
    {
        if (slot.transform.childCount > 0)
        {
            GameObject removedItem = slot.transform.GetChild(0).gameObject;
            Destroy(removedItem);
        }
    }



}
