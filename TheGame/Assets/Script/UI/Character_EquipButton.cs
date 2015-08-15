using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_EquipButton : MonoBehaviour {

	GameObject itemPreview;
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (EquipItem);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("ItemPreview").transform.childCount > 0) 
		{
			itemPreview = GameObject.Find ("ItemPreview").transform.GetChild(0).gameObject;
		}

		if (itemPreview != null) 
		{
			GetComponent<Image> ().color = new Color (255, 255, 255, 255);
		} 
		else 
		{
			GetComponent<Image> ().color = new Color (0, 0, 0, 255);
		}
	}

	void EquipItem()
	{
		GameManager_Assets assets = GameObject.Find ("GameManager").GetComponent<GameManager_Assets> ();
		assets.EquipItem(itemPreview.GetComponent<Item> ());
		
	}
}
