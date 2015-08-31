using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenu_ButtonInventory : MonoBehaviour
{
    public GameObject CharacterMenu;
    public GameObject InventoryMenu;
    public GameObject PlayerDummy;


    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToInventoryMenu);
    }

    // Update is called once per frame
    void Update()
    {


    }


    void GoToInventoryMenu()
    {      

        StartCoroutine(LoadInventory());
        
        
        

    }


    IEnumerator LoadInventory()
    {

        CameraAnimator camAnim = GameObject.FindWithTag("MainCamera").GetComponent<CameraAnimator>();
        camAnim.MoveCamera(camAnim.MainMenuPosition);
        yield return new WaitForSeconds(0.01f);
        camAnim.ChangeMenu(camAnim.Menus[1]);


        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
        GameObject.Find("InventoryMenu").GetComponent<Inventory_Menu>().DisplayItems();
        
    }


}
