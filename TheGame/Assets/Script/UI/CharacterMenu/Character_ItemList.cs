using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TheGame.ItemSystem;


public class Character_ItemList : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Color NormalColor;
	public Color SelectedColor;
	public Color OverColor;
	public ISItem Item;
	public GameObject StatListElement;
    public GameObject SkinListElement;





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

        //TITLE DU PANNEAU
        Title.transform.GetChild (0).GetComponent<Image> ().sprite = Item.Icon;
		Title.transform.GetChild (2).GetComponent<Text> ().text = Item.Name;

		for (int i = 0; i < ItemPreview.transform.childCount; i++) 
		{
			Destroy(ItemPreview.transform.GetChild(i).gameObject);
		}


		for (int i = 0; i < ItemPreview.transform.childCount; i++) 
		{
			Destroy(ItemPreview.transform.GetChild(i).gameObject);
		}



        //MODEL 3D DU PANNEAU
		GameObject temp = Instantiate (Item.Skins[0]);
        Item.SelectedSkin = Item.Skins[0];
        temp.transform.SetParent (ItemPreview.transform);
		temp.transform.position = ItemPreview.transform.position;
		temp.transform.rotation = ItemPreview.transform.rotation;
		temp.transform.localScale = new Vector3 (1, 1, 1);



        //SkinLIst
        GameObject contentSkinssGo = GameObject.Find("SkinScrollerContent").gameObject;
        for (int i = 0; i < contentSkinssGo.transform.childCount; i++)
        {
            Destroy(contentSkinssGo.transform.GetChild(i).gameObject);
        }



        for (int i = 0; i < Item.Skins.Count; i++)
        {
            GameObject skinElement = Instantiate(SkinListElement);
            skinElement.GetComponent<Character_SkinList>().Skin = Item.Skins[i];
            skinElement.transform.GetChild(0).GetComponent<Text>().text = Item.Skins[i].name;
            skinElement.transform.SetParent(contentSkinssGo.transform);
            skinElement.transform.localScale = new Vector3(1, 1, 1);
        }



        //STAT DU PANNEAU
        GameObject contentStatsGo = GameObject.Find("SelectedItemStatPanelContent").gameObject;
		for (int i = 0; i < contentStatsGo.transform.childCount; i++) 
		{
			Destroy(contentStatsGo.transform.GetChild(i).gameObject);
		}


      
		for(int i = 0; i < Item.Stats.Count; i++)
		{
			GameObject statElement = Instantiate(StatListElement);
			statElement.transform.GetChild(0).GetComponent<Text>().text = Item.Stats[i].Name;
			statElement.transform.GetChild(1).GetComponent<Text>().text = Item.Stats[i].ValueInt.ToString();
			statElement.transform.SetParent(contentStatsGo.transform);
			statElement.transform.localScale = new Vector3(1,1,1);
		}
	


	}






}
