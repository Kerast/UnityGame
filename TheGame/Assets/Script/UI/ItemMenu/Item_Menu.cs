using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Item_Menu : MonoBehaviour {

    public GameObject ItemPreview;

    public Image ItemIcon;
    public Text ItemTitle;
    public Text ItemDescription;
    public Button GoldButton;
    public Button EuroButton;
    public GameObject OwnedPanel;


    public GameObject ConfirmPanel;


    public InventoryItem ItemObject = new InventoryItem();


    // Use this for initialization
    void Start () {
        GoldButton.onClick.AddListener(BuyGoldMethod);

    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void BuyGoldMethod()
    {
        StartCoroutine(LoadGoldConfirmPanel());
    }

    IEnumerator LoadGoldConfirmPanel()
    {
        yield return GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData();

        ConfirmPanel.GetComponent<Item_BuyConfirmPanel>().InitialisePanel(ItemObject, "Gold", ItemObject.PriceGold, GameObject.Find("WebServices").GetComponent<WebServices>().pg);
    }



    public void DisplayItemInfo(InventoryItem itemParam)
    {
        StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData());
        ItemObject = itemParam;
        List<InventoryItem> PlayerItems = GameObject.Find("WebServices").GetComponent<WebServices>().Items;

        
       if (PlayerItems.Find(i=> i.Identity == itemParam.Identity) != null)
       {
            SetOwned(true);
       }
       else
       {
            SetOwned(false);

            if (GameObject.Find("WebServices").GetComponent<WebServices>().pg >= itemParam.PriceGold)
            {
                GoldButton.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            }
            else
            {
                GoldButton.GetComponent<Image>().color = new Color(122, 122, 122, 150);
                GoldButton.interactable = false;


            }
            GoldButton.transform.GetChild(0).GetComponent<Text>().text = itemParam.PriceGold.ToString() + " G";
            EuroButton.transform.GetChild(0).GetComponent<Text>().text = itemParam.PriceEuro.ToString() + " €";
            
        }

        CleanItemPreview();
        GameObject _item = Instantiate(ItemObject.Item.SelectedSkin);
        _item.transform.SetParent(ItemPreview.transform);
        //_item.transform.position = new Vector3(0, 0, 0);
        _item.transform.localPosition = new Vector3(0, 0, 0);
        _item.transform.rotation = new Quaternion(0, 0, 0,0);
        _item.transform.localScale = new Vector3(1, 1, 1);
        

    }


    void SetOwned(bool check)
    {       
        EuroButton.gameObject.SetActive(!check);
        GoldButton.gameObject.SetActive(!check);
        OwnedPanel.SetActive(check);
    }
    void CleanItemPreview()
    {
        for(int i = 0; i < ItemPreview.transform.childCount; i++)
        {
            Destroy(ItemPreview.transform.GetChild(i).gameObject);
        }
    }
}
