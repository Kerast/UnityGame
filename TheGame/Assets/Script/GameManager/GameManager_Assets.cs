using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheGame.ItemSystem;

public class GameManager_Assets : MonoBehaviour {

	public List<GameObject> Items;
	public GameObject PlayerDummy;

	void Awake()
	{
		//Tout nu
		EquipItem (Items.ElementAt (0).GetComponent<Item> ());
		EquipItem (Items.ElementAt (1).GetComponent<Item> ());
		EquipItem (Items.ElementAt (2).GetComponent<Item> ());
		EquipItem (Items.ElementAt (3).GetComponent<Item> ());
		EquipItem (Items.ElementAt (4).GetComponent<Item> ());
		EquipItem (Items.ElementAt (5).GetComponent<Item> ());
		EquipItem (Items.ElementAt (6).GetComponent<Item> ());
		EquipItem (Items.ElementAt (7).GetComponent<Item> ());
		EquipItem (Items.ElementAt (8).GetComponent<Item> ());

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void EquipItem(Item item)
	{
		Player_Equipment playerequip = PlayerDummy.GetComponent<Player_Equipment>();
		GameObject slot = null;
		if (item.item.EquipmentSlot == EquipmentSlot.Head) 
			slot = playerequip.helmetNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.Torse) 
			slot = playerequip.torseNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.Belt) 
			slot = playerequip.beltNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.ShoulderR) 
			slot = playerequip.shoulderRNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.ShoulderL) 
			slot = playerequip.shoulderLNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.GloveL) 
			slot = playerequip.gloveLNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.GloveR) 
			slot = playerequip.gloveRNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.Legs) 
			slot = playerequip.legsNode;
		else if (item.item.EquipmentSlot == EquipmentSlot.Weapon) 
			slot = playerequip.weaponNode;
	
		DetachSlot (slot);
		

		GameObject itemTemp = Instantiate(Items.Find(i => i.GetComponent<Item>().item.Identity == item.item.Identity ));
		itemTemp.transform.parent = slot.transform;
		itemTemp.transform.position = slot.transform.position;
		itemTemp.transform.localScale = slot.transform.localScale;
	}
	
	void DetachSlot(GameObject slot)
	{
		if (slot.transform.childCount > 0) 
		{
			GameObject removedItem = slot.transform.GetChild (0).gameObject;
			Destroy(removedItem);
		} 
	}

}
