using UnityEngine;
using System.Collections;
using TheGame.ItemSystem;
using System.Collections.Generic;
using UnityEngine.UI;
public class Character_ItemScroller : MonoBehaviour {

	public GameObject Button;
	public GameObject SrollPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadItems(EquipmentSlot equipmentslot)
	{
		for (int i =0; i < GameObject.Find("ItemScrollerPanel").transform.childCount; i++) 
		{
			GameObject child = GameObject.Find("ItemScrollerPanel").transform.GetChild(i).gameObject; 
			Destroy(child);
		}

		GameObject.Find ("ItemScrollerPanel").transform.DetachChildren ();

		if (equipmentslot == EquipmentSlot.Weapon) {
			LoadWeapons ();
		} else 
		{
			LoadArmors (equipmentslot);
		}
	}

	void LoadWeapons ()
	{
		List<GameObject> weapons = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ().Weapons;

		for (int i = 0; i < weapons.Count; i++) 
		{
			GameObject button = Instantiate(Button);
			button.transform.SetParent(SrollPanel.transform);
			button.transform.GetChild(0).GetComponent<Text>().text = weapons[i].GetComponent<Weapon>().weapon.Identity;
			button.GetComponent<Character_ItemScrollerButon>().itemPreview = weapons[i];
		}
	}


	void LoadArmors (EquipmentSlot equipmentSlot)
	{
		List<GameObject> armors = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ().Armors;
		
		for (int i = 0; i < armors.Count; i++) 
		{
			if(armors[i].GetComponent<Armor>().armor.EquipmentSlot == equipmentSlot)
			{
				GameObject button = Instantiate(Button);
				button.transform.SetParent(SrollPanel.transform);
				button.transform.GetChild(0).GetComponent<Text>().text = armors[i].GetComponent<Armor>().armor.Identity;
				button.GetComponent<Character_ItemScrollerButon>().itemPreview = armors[i];
			}

		}
	}
}
