using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Inventory_ItemToolElementOpenCrate : MonoBehaviour, IPointerDownHandler {

    

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData data)
    {
        InventoryItem itemObject = GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().ToolPanel.GetComponent<Inventory_ToolPanel>().ItemObject;


        GameObject winnedSkin = itemObject.Item.SelectedSkin.GetComponent<Crate_Content>().WinnedSkin();
        StartCoroutine(AddSKin(itemObject, winnedSkin));


        GameObject openedCratePanel = GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().OpenedCratePanel;
        openedCratePanel.SetActive(true);
        openedCratePanel.GetComponent<Inventory_OpenedCratePanel>().DisplayInfo(winnedSkin);

        
        
    }

    IEnumerator AddSKin(InventoryItem itemObject, GameObject skin)
    {
        
        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().RemoveUserItem(itemObject.ItemId));
        
        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().AddUserItem(skin.GetComponent<SkinInfo>().Identity, "Skin"));
      
        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
      
        GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().DisplayItems();

        GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().ToolPanel.SetActive(false);
        GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().ToolPanel.GetComponent<Inventory_ToolPanel>().LostFocusPanel.SetActive(false);

    }


}
