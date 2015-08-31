using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item_BuyConfirmPanel : MonoBehaviour {

    public GameObject GoldSidePanel;
    public Text ActualGoldText; 
    public Text PriceGoldText; 
    public Text ResultGoldText;

    public GameObject EuroSidePanel;

    public Button CancelButton;
    public Button BuyButton;

    public InventoryItem ItemObject;



    // Use this for initialization
    void Start () {
        CancelButton.onClick.AddListener(CancelConfirm);
        BuyButton.onClick.AddListener(BuyConfirm);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CancelConfirm()
    {
        gameObject.SetActive(false);
    }

    public void BuyConfirm()
    {
                   
        StartCoroutine(LoadUserData());

       
    }

    IEnumerator LoadUserData()
    {
        if(ItemObject.Item.Type == ItemType.Equipment)
        {
            Item_Menu itemMenu = GameObject.Find("ItemMenu").GetComponent<Item_Menu>();

            yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().BuyItemWithGold(itemMenu.ItemObject.Identity, itemMenu.ItemObject.Type));
            StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData());

            yield return new WaitForSeconds(0.01f);

            itemMenu.EuroButton.gameObject.SetActive(false);
            itemMenu.GoldButton.gameObject.SetActive(false);
            itemMenu.OwnedPanel.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }
        else if (ItemObject.Item.Type == ItemType.Crate)
        {
         
            yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().BuyItemWithGold(ItemObject.Identity, ItemObject.Type));
            StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData());

            yield return new WaitForSeconds(0.01f);
            gameObject.SetActive(false);
        }

        
    }


    public void InitialisePanel(InventoryItem itemObject, string methodPayement, int price, int actualsolde)
    {
        ItemObject = itemObject;
        gameObject.SetActive(true);
        if(methodPayement == "Gold")
        {
            GoldSidePanel.SetActive(true);
            EuroSidePanel.SetActive(false);

        }
        else
        {
            GoldSidePanel.SetActive(false);
            EuroSidePanel.SetActive(true);
        }

        ActualGoldText.text = actualsolde.ToString();
        PriceGoldText.text = "- " + price.ToString();
        ResultGoldText.text = (actualsolde - price).ToString();

    }
}
