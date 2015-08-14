using UnityEngine;
using System.Collections;

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


	}
}
