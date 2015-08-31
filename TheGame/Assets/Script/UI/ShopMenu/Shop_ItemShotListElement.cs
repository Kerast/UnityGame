using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Shop_ItemShotListElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public Transform MyTransform;
    public GameObject ItemPreviewContent;
    public Color HoverBorder;
    public Color HoverBackground;
    public Color NormalBorder;
    public Color NormalBackground;


    public Image Icon;
    public Text NameText;
    public Text GoldText;



    public InventoryItem ItemObject;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SetItemPreview(GameObject item)
    {
        item.transform.SetParent(ItemPreviewContent.transform);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        MyTransform.GetComponent<Image>().color = HoverBorder;
        MyTransform.GetChild(0).GetComponent<Image>().color = HoverBackground;
    }

    public void OnPointerExit(PointerEventData data)
    {
        MyTransform.GetComponent<Image>().color = NormalBorder;
        MyTransform.GetChild(0).GetComponent<Image>().color = NormalBackground;
    }

    public void OnPointerClick(PointerEventData data)
    {
        if(ItemObject.Item.Type == ItemType.Equipment)
            StartCoroutine(LoadItemMenu());
        else if (ItemObject.Item.Type == ItemType.Crate)
            StartCoroutine(LoadCratePanelMenu());

    }

    IEnumerator LoadItemMenu()
    {

        StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData());
        StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetShopInventory());
        StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());

        CameraAnimator camAnim = GameObject.FindWithTag("MainCamera").GetComponent<CameraAnimator>();
        camAnim.MoveCamera(camAnim.ItemMenuPosition);
        yield return new WaitForSeconds(0.01f);
        camAnim.ChangeMenu(camAnim.Menus[3]);

        

        camAnim.Menus[3].GetComponent<Item_Menu>().DisplayItemInfo(ItemObject);



    }

    IEnumerator LoadCratePanelMenu()
    {

        yield return GameObject.Find("WebServices").GetComponent<WebServices>().GetUserData();

        GameObject.Find("ShopMenu").GetComponent<Shop_Menu>().ConfirmPanel.GetComponent<Item_BuyConfirmPanel>().InitialisePanel(ItemObject, "Gold", ItemObject.PriceGold, GameObject.Find("WebServices").GetComponent<WebServices>().pg);


    }


}
