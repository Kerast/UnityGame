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

        List<InventoryItem> items = GameObject.Find("WebServices").GetComponent<WebServices>().Items;

		for (int i = 0; i < items.Count; i++) 
		{
			
				GameObject listItem = Instantiate(ItemListPrefab);
				listItem.transform.SetParent(ListItemsPanel.transform);
				listItem.transform.GetChild(1).GetComponent<Image>().sprite = items[i].Item.SelectedSkin.GetComponent<SkinInfo>().SkinIcon;
				listItem.transform.GetChild(2).GetComponent<Text>().text = items[i].Item.Name;
				listItem.transform.localScale = new Vector3(1,1,1);
				listItem.transform.name = "listItemElement_" + items[i].Item.Identity;
				listItem.GetComponent<Character_ItemList>().Item = items[i].Item;
           			
		}

	}
}
