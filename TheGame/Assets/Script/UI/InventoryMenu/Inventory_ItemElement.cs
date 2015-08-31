using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Inventory_ItemElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

    public InventoryItem ItemObject;
    public GameObject ToolPanel;



    // Use this for initialization
	void Start () {
        ToolPanel = GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().ToolPanel ;
}
	
	// Update is called once per frame
	void Update () {

       

       
    }

    public void OnPointerEnter(PointerEventData data)
    {
       
       
    }


    public void OnPointerExit(PointerEventData data)
    {
       
    }

    public void OnPointerDown(PointerEventData data)
    {
      
        ToolPanel.SetActive(true);
        ToolPanel.GetComponent<Inventory_ToolPanel>().CleanToolPanel();
        ToolPanel.transform.localScale = new Vector3(1, 1, 1) ;
        ToolPanel.GetComponent<Inventory_ToolPanel>().ItemObject = ItemObject;

        ToolPanel.GetComponent<Inventory_ToolPanel>().LostFocusPanel.SetActive(true);

        if(ItemObject.Item.Type == ItemType.Equipment)
        {

        }
        else if (ItemObject.Item.Type == ItemType.Crate)
        {


            ToolPanel.transform.position = new Vector2(Input.mousePosition.x + ToolPanel.GetComponent<RectTransform>().rect.width/4, Input.mousePosition.y - ToolPanel.GetComponent<RectTransform>().rect.height / 4);


            //opencrate element to tool panel 
            GameObject toolElement = Instantiate(ToolPanel.GetComponent<Inventory_ToolPanel>().OpenCrateToolPanelElement);
            toolElement.transform.SetParent(ToolPanel.transform);
            toolElement.transform.localScale = new Vector3(1, 1, 1);


        }

    }
}
