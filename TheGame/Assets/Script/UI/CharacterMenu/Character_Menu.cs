using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TheGame.ItemSystem;

public class Character_Menu : MonoBehaviour {

	public GameObject ListItemsPanel;
	public GameObject ItemListPrefab; 
	// Use this for initialization
	void Start () {
		DisplayItems();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisplayItems()
	{
		for (int i = 0; i < ListItemsPanel.transform.childCount; i++) 
		{
			Destroy(ListItemsPanel.transform.GetChild(i).gameObject);

		}

		List<ISItem> Items = new List<ISItem> ();
		Items = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ().Items;

		for (int i = 0; i < Items.Count; i++) 
		{
			if(Items[i].Name != "Nothing")
			{
				GameObject listItem = Instantiate(ItemListPrefab);
				listItem.transform.SetParent(ListItemsPanel.transform);
				listItem.transform.GetChild(0).GetComponent<Image>().sprite = Items[i].Icon;
				listItem.transform.GetChild(2).GetComponent<Text>().text = Items[i].Name;
				listItem.transform.localScale = new Vector3(1,1,1);
				listItem.transform.name = "listItemElement_" + Items[i].Identity;
				listItem.GetComponent<Character_ItemList>().Item = Items[i];
			}


			
		}

	}
}
