using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shop_Menu : MonoBehaviour {

    public GameObject ShopItemListElement;
    public GameObject ItemsContentPanel;

    public GameObject ConfirmPanel;


    public Button EquipmentTabButton;
    public Button CratesTabButton;



    // Use this for initialization
    void Start () {
        EquipmentTabButton.onClick.AddListener(DisplayItems);
        CratesTabButton.onClick.AddListener(DisplayCrates);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayItems()
    {
        List<InventoryItem> itemsShop = GameObject.Find("WebServices").GetComponent<WebServices>().ShopItems;
        List<InventoryItem> itemsPlayer = GameObject.Find("WebServices").GetComponent<WebServices>().Items;
        CleanContentPanel();

        foreach (var item in itemsShop)
        {

            SkinInfo skinInfo = item.Item.SelectedSkin.GetComponent<SkinInfo>();

            GameObject newItem = Instantiate(ShopItemListElement);
            newItem.transform.SetParent(ItemsContentPanel.transform);
            newItem.transform.localScale = new Vector3(1, 1, 1);


            item.Item.SelectedSkin.transform.position = new Vector3(0, 0, 0);

            GameObject itemPreview = Instantiate(item.Item.SelectedSkin);
            itemPreview.transform.SetParent(newItem.transform.GetChild(0).transform);
            itemPreview.transform.position = newItem.transform.GetChild(0).transform.GetChild(0).transform.position;
            itemPreview.transform.localScale = newItem.transform.GetChild(0).transform.GetChild(0).transform.localScale;

            newItem.GetComponent<Shop_ItemShotListElement>().ItemObject = item;


            newItem.GetComponent<Shop_ItemShotListElement>().Icon.sprite = skinInfo.SkinIcon;
            newItem.GetComponent<Shop_ItemShotListElement>().NameText.text = item.Item.Name;
            newItem.GetComponent<Shop_ItemShotListElement>().GoldText.text = item.PriceGold.ToString() ;

            if (itemsPlayer.Find(i => i.Identity == item.Identity) == null)
            {
                newItem.transform.GetChild(3).GetComponent<Text>().text = item.PriceGold.ToString();
                if (item.Promo > 0)
                {
                    newItem.transform.GetChild(4).gameObject.SetActive(true);
                    newItem.transform.GetChild(4).transform.GetChild(0).GetComponent<Text>().text = "-" + item.Promo.ToString() + "%";
                }
            }
            else
            {
                newItem.transform.GetChild(5).gameObject.SetActive(true);
            }

            
            


        }
    }


    public void DisplayCrates()
    {
        List<InventoryItem> cratesShop = GameObject.Find("WebServices").GetComponent<WebServices>().ShopCrates;
        CleanContentPanel();

        foreach (var item in cratesShop)
        {

            SkinInfo skinInfo = item.Item.SelectedSkin.GetComponent<SkinInfo>();

            GameObject newItem = Instantiate(ShopItemListElement);
            newItem.transform.SetParent(ItemsContentPanel.transform);
            newItem.transform.localScale = new Vector3(1, 1, 1);


            item.Item.SelectedSkin.transform.position = new Vector3(0, 0, 0);

            GameObject itemPreview = Instantiate(item.Item.SelectedSkin);
            itemPreview.transform.SetParent(newItem.transform.GetChild(0).transform);
            itemPreview.transform.position = newItem.transform.GetChild(0).transform.GetChild(0).transform.position;
            itemPreview.transform.localScale = newItem.transform.GetChild(0).transform.GetChild(0).transform.localScale;

            newItem.GetComponent<Shop_ItemShotListElement>().ItemObject = item;


            newItem.GetComponent<Shop_ItemShotListElement>().Icon.sprite = skinInfo.SkinIcon;
            newItem.GetComponent<Shop_ItemShotListElement>().NameText.text = item.Item.Name;
            newItem.GetComponent<Shop_ItemShotListElement>().GoldText.text = item.PriceGold.ToString() ;


        }
    }


    public void CleanContentPanel()
    {
        for (int i = 0; i < ItemsContentPanel.transform.childCount; i++)
        {
            Destroy(ItemsContentPanel.transform.GetChild(i).gameObject);
        }
    }
}
