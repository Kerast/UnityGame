using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheGame.ItemSystem;

public class GameManager_Assets : MonoBehaviour {

	//public List<GameObject> Items2;
	public List<ISItem> Items;
	public GameObject PlayerDummy;

	void Awake()
	{
		//Tout nu
		EquipItem (Items.ElementAt(0));
		EquipItem (Items.ElementAt (1));
		EquipItem (Items.ElementAt (2));
		EquipItem (Items.ElementAt (3));
		EquipItem (Items.ElementAt (4));
		EquipItem (Items.ElementAt (5));
		EquipItem (Items.ElementAt (6));
		EquipItem (Items.ElementAt (7));
		EquipItem (Items.ElementAt (8));

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	public void EquipItem(ISItem item)
	{
		Player_Equipment playerequip = PlayerDummy.GetComponent<Player_Equipment>();
		GameObject slot = null;
		if (item.EquipmentSlot == EquipmentSlot.Head) 
			slot = playerequip.helmetNode;
		else if (item.EquipmentSlot == EquipmentSlot.Torse) 
			slot = playerequip.torseNode;
		else if (item.EquipmentSlot == EquipmentSlot.Belt) 
			slot = playerequip.beltNode;
		else if (item.EquipmentSlot == EquipmentSlot.ShoulderR) 
			slot = playerequip.shoulderRNode;
		else if (item.EquipmentSlot == EquipmentSlot.ShoulderL) 
			slot = playerequip.shoulderLNode;
		else if (item.EquipmentSlot == EquipmentSlot.GloveL) 
			slot = playerequip.gloveLNode;
		else if (item.EquipmentSlot == EquipmentSlot.GloveR) 
			slot = playerequip.gloveRNode;
		else if (item.EquipmentSlot == EquipmentSlot.Legs) 
			slot = playerequip.legsNode;
		else if (item.EquipmentSlot == EquipmentSlot.Weapon) 
			slot = playerequip.weaponNode;
	
		DetachSlot (slot);
		

		GameObject itemTemp = Instantiate(Items.Find(i => i.Identity == item.Identity ).Skins[0]);
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
