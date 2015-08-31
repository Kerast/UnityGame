using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ButtonShop : MonoBehaviour
{

    
    public GameObject PlayerDummy;



    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoTorMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void GoTorMenu()
    {

        StartCoroutine(LoadShop());

    }

    IEnumerator LoadShop()
    {

        CameraAnimator camAnim = GameObject.FindWithTag("MainCamera").GetComponent<CameraAnimator>();
        camAnim.MoveCamera(camAnim.MainMenuPosition);
        yield return new WaitForSeconds(0.01f);
         camAnim.ChangeMenu(camAnim.Menus[2]);

        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetShopInventory());
        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
        GameObject.Find("ShopMenu").GetComponent<Shop_Menu>().DisplayItems();

    }



}
