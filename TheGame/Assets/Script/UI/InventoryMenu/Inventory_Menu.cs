using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Boomlagoon.JSON;
using System.Collections.Generic;
using TheGame.ItemSystem;


public class Inventory_Menu : MonoBehaviour {

    public GameObject SkinPanel;
    public GameObject ItemPanel;
    public GameObject CratePanel;
    public GameObject ItemElementPrefab;


    public GameObject ToolPanel;
    public GameObject OpenedCratePanel;

    public List<InventoryItem> SkinsLoaded = new List<InventoryItem>();
    public List<ISObject> ItemsLoaded = new List<ISObject>();

    public Button AddRandomItemButton;

    // Use this for initialization
    void Start () {
        AddRandomItemButton.onClick.AddListener(AddRandomItem);
    }
	
	// Update is called once per frame
	void Update () {

	    
	}


    public void DisplayItems()
    {
      
        LoadUserSkins();
        LoadUserItems();
        LoadUserCrates();

    }

    public void LoadUserSkins()
    {

        RemoveUserSkins();
        List<InventoryItem> skins = GameObject.Find("WebServices").GetComponent<WebServices>().Skins;
       
        
        foreach (var skin in skins)
        {

            SkinInfo skinInfo = skin.Item.SelectedSkin.GetComponent<SkinInfo>();

            GameObject newSkinItem = Instantiate(ItemElementPrefab);
            newSkinItem.GetComponent<Inventory_ItemElement>().ItemObject = skin;
            newSkinItem.transform.SetParent(SkinPanel.transform);
            newSkinItem.transform.localScale = new Vector3(1, 1, 1);        
            newSkinItem.transform.GetChild(0).GetComponent<Image>().sprite = skinInfo.SkinPhoto;
            newSkinItem.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = skin.Item.Name;
            newSkinItem.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = skinInfo.Name;

            if (skinInfo.SkinQuality == SkinQuality.Common)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityCommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Uncommon)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityUncommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Rare)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityRareColor;
            else if (skinInfo.SkinQuality == SkinQuality.Legendary)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityLegendaryColor;
          
        }


    }


    public void LoadUserItems()
    {

        RemoveUserItems();
        List<InventoryItem> items = GameObject.Find("WebServices").GetComponent<WebServices>().Items;

        foreach (var item in items)
        {

            SkinInfo skinInfo = item.Item.SelectedSkin.GetComponent<SkinInfo>();

            GameObject newSkinItem = Instantiate(ItemElementPrefab);
            newSkinItem.transform.SetParent(ItemPanel.transform);
            newSkinItem.GetComponent<Inventory_ItemElement>().ItemObject = item;
            newSkinItem.transform.localScale = new Vector3(1, 1, 1);
            newSkinItem.transform.GetChild(0).GetComponent<Image>().sprite = skinInfo.SkinPhoto;
            newSkinItem.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = item.Item.Name;
            newSkinItem.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = skinInfo.Name;

            if (skinInfo.SkinQuality == SkinQuality.Common)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityCommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Uncommon)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityUncommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Rare)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityRareColor;
            else if (skinInfo.SkinQuality == SkinQuality.Legendary)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityLegendaryColor;

        }


    }

    public void LoadUserCrates()
    {

        RemoveUserCrates();
        List<InventoryItem> Crates = GameObject.Find("WebServices").GetComponent<WebServices>().Crates;

        foreach (var item in Crates)
        {

            SkinInfo skinInfo = item.Item.SelectedSkin.GetComponent<SkinInfo>();

            GameObject newSkinItem = Instantiate(ItemElementPrefab);
            newSkinItem.GetComponent<Inventory_ItemElement>().ItemObject = item;
            newSkinItem.transform.SetParent(CratePanel.transform);           
            newSkinItem.transform.localScale = new Vector3(1, 1, 1);
            newSkinItem.transform.GetChild(0).GetComponent<Image>().sprite = skinInfo.SkinPhoto;
            newSkinItem.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = item.Item.Name;
            newSkinItem.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = skinInfo.Name;

            if (skinInfo.SkinQuality == SkinQuality.Common)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityCommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Uncommon)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityUncommonColor;
            else if (skinInfo.SkinQuality == SkinQuality.Rare)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityRareColor;
            else if (skinInfo.SkinQuality == SkinQuality.Legendary)
                newSkinItem.GetComponent<Image>().color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityLegendaryColor;

        }


    }


    public void RemoveUserSkins()
    {
        for(int i = 0; i < SkinPanel.transform.childCount; i++)
        {
            Destroy(SkinPanel.transform.GetChild(i).gameObject);
        }
    }


    public void RemoveUserItems()
    {
        for (int i = 0; i < ItemPanel.transform.childCount; i++)
        {
            Destroy(ItemPanel.transform.GetChild(i).gameObject);
        }
    }

    public void RemoveUserCrates()
    {
        for (int i = 0; i < CratePanel.transform.childCount; i++)
        {
            Destroy(CratePanel.transform.GetChild(i).gameObject);
        }
    }



    public IEnumerator LoadInventory()
    {

        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
        GameObject.Find("CharacterMenu").GetComponent<Character_Menu>().DisplayItems();

    }



    public void AddRandomItem()
    {
        List<ISItem> allItems = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items;
        List<ISItem> onlySlotItem = new List<ISItem>();
        foreach(var item in allItems)
        {
            if (item.Type == ItemType.Equipment)
                onlySlotItem.Add(item);
        }

        int randomItemIndex = Random.Range(0, onlySlotItem.Count);
        int randomSkinIndex = Random.Range(1, onlySlotItem[randomItemIndex].Skins.Count);

        if(onlySlotItem[randomItemIndex].Skins.Count > 1)
         StartCoroutine(AddItem(onlySlotItem[randomItemIndex].Skins[randomSkinIndex].GetComponent<SkinInfo>().Identity, "Skin"));
            

    }


    public IEnumerator AddItem(string item_name, string item_type)
    {

        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().AddUserItem(item_name, item_type));
        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
        
        DisplayItems();

    }
}



public class InventoryItem
{
    public int ItemId;
    public string Identity;
    public string Type;
    public int Promo;
    public int PriceGold;
    public float PriceEuro;
    public bool IsNew;
    public ISItem Item;
}