using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

		List<GameObject> Items = new List<GameObject> ();
		Items = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ().Items;

		for (int i = 0; i < Items.Count; i++) 
		{
			if(Items[i].GetComponent<Item>().item.Name != "Nothing")
			{
				GameObject listItem = Instantiate(ItemListPrefab);
				listItem.transform.SetParent(ListItemsPanel.transform);
				listItem.transform.GetChild(0).GetComponent<Image>().sprite = Items[i].GetComponent<Item>().item.Icon;
				listItem.transform.GetChild(2).GetComponent<Text>().text = Items[i].GetComponent<Item>().item.Name;
				listItem.transform.localScale = new Vector3(1,1,1);
				listItem.transform.name = "listItemElement_" + Items[i].GetComponent<Item>().item.Identity;
				listItem.GetComponent<Character_ItemList>().Item = Items[i];
			}


			
		}

	}
}
