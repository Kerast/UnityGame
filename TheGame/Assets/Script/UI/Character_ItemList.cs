using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Character_ItemList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Color NormalColor;
	public Color SelectedColor;
	public Color OverColor;
	public GameObject Item;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void OnPointerEnter(PointerEventData data)
	{


		if (tag != "SelectedItem_ItemsListContent") 
		{
			Image temp = GetComponent<Image> ();
			temp.color = OverColor;
		} 
	}

	public void OnPointerExit(PointerEventData data)
	{
		if (tag != "SelectedItem_ItemsListContent") 
		{
			Image temp = GetComponent<Image> ();
			temp.color = NormalColor;
		} 

	
	}

	public void OnPointerClick(PointerEventData data)
	{

		GameObject go = GameObject.FindGameObjectWithTag ("SelectedItem_ItemsListContent");
		if (go != null) 
		{
			Image lastSelectedItemColor = go.GetComponent<Image> ();
			lastSelectedItemColor.color = NormalColor;

			go.tag = "Untagged";
		}
		go = gameObject;
		Image temp = GetComponent<Image> ();
		temp.color = SelectedColor;
		tag = "SelectedItem_ItemsListContent";

		DisplayItemPreview ();


	}

	public void DisplayItemPreview()
	{
		GameObject Title = GameObject.Find ("SelectedItemTitlePanel");
		GameObject ItemPreview = GameObject.Find ("ItemPreview");

		Title.transform.GetChild (0).GetComponent<Image> ().sprite = Item.GetComponent<Item> ().item.Icon;
		Title.transform.GetChild (2).GetComponent<Text> ().text = Item.GetComponent<Item> ().item.Name;

		for (int i = 0; i < ItemPreview.transform.childCount; i++) 
		{
			Destroy(ItemPreview.transform.GetChild(i).gameObject);
		}

		GameObject temp = Instantiate (Item);
		temp.transform.SetParent (ItemPreview.transform);
		temp.transform.position = ItemPreview.transform.position;
		temp.transform.rotation = ItemPreview.transform.rotation;
		temp.transform.localScale = new Vector3 (1, 1, 1);


	}




}
