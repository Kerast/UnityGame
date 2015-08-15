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

	public void LoadItems(EquipmentSlot equipmentSlot)
	{
		for (int i =0; i < GameObject.Find("ItemScrollerPanel").transform.childCount; i++) 
		{
			GameObject child = GameObject.Find("ItemScrollerPanel").transform.GetChild(i).gameObject; 
			Destroy(child);
		}

		GameObject.Find ("ItemScrollerPanel").transform.DetachChildren ();

		List<GameObject> items = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ().Items;
		
		for (int i = 0; i < items.Count; i++) 
		{
			if(items[i].GetComponent<Item>().item.EquipmentSlot == equipmentSlot)
			{
				GameObject button = Instantiate(Button);
				button.transform.SetParent(SrollPanel.transform);
				button.transform.GetChild(0).GetComponent<Text>().text = items[i].GetComponent<Item>().item.Identity;
				button.GetComponent<Character_ItemScrollerButon>().itemPreview = items[i];
			}
			
		}
	}

	

}
