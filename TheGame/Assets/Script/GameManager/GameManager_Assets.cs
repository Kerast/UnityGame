using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheGame.ItemSystem;

public class GameManager_Assets : MonoBehaviour {

	public List<GameObject> Weapons;
	public List<GameObject> Armors;


	void Awake()
	{
		EquipWeapon (Weapons.ElementAt(0).GetComponent<Weapon>());
		EquipArmor (Armors.ElementAt (0).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (1).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (2).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (3).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (4).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (5).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (6).GetComponent<Armor> ());
		EquipArmor (Armors.ElementAt (7).GetComponent<Armor> ());

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EquipWeapon(Weapon weapon)
	{

		Player_Equipment playerequip = GameObject.Find ("Player_Dummy").GetComponent<Player_Equipment>();
		DetachSlot (playerequip.weaponNode);
	
		GameObject wep = Instantiate(Weapons.Find(i => i.GetComponent<Weapon>().weapon.Name == weapon.weapon.Name ));
		wep.transform.parent = playerequip.weaponNode.transform;
		wep.transform.position = playerequip.weaponNode.transform.position;
		wep.transform.rotation = playerequip.weaponNode.transform.rotation;

	}

	public void EquipArmor(Armor armor)
	{
		Player_Equipment playerequip = GameObject.Find ("Player_Dummy").GetComponent<Player_Equipment>();
		GameObject slot = null;
		if (armor.armor.EquipmentSlot == EquipmentSlot.Head) 
			slot = playerequip.helmetNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.Torse) 
			slot = playerequip.torseNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.Belt) 
			slot = playerequip.beltNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.ShoulderR) 
			slot = playerequip.shoulderRNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.ShoulderL) 
			slot = playerequip.shoulderLNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.GloveL) 
			slot = playerequip.gloveLNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.GloveR) 
			slot = playerequip.gloveRNode;
		else if (armor.armor.EquipmentSlot == EquipmentSlot.Legs) 
			slot = playerequip.legsNode;
	



		DetachSlot (slot);
		

		GameObject arm = Instantiate(Armors.Find(i => i.GetComponent<Armor>().armor.Identity == armor.armor.Identity ));
		arm.transform.parent = slot.transform;
		arm.transform.position = slot.transform.position;
		//arm.transform.rotation = new Quaternion ();
		arm.transform.localScale = slot.transform.localScale;
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
