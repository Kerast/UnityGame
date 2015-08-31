using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ButtonCharacter : MonoBehaviour {

	public GameObject CharacterMenu;
    public GameObject InventoryMenu;
    public GameObject PlayerDummy;



    // Use this for initialization
    void Start () {
		GetComponent<Button> ().onClick.AddListener (GoToCharacterMenu);
	}
	
	// Update is called once per frame
	void Update () {

	}


	void GoToCharacterMenu()
	{
              
        StartCoroutine(LoadInventory());       
      
    }

    IEnumerator LoadInventory()
    {

        CameraAnimator camAnim = GameObject.FindWithTag("MainCamera").GetComponent<CameraAnimator>();
        camAnim.MoveCamera(camAnim.CharacterPosition);
        yield return new WaitForSeconds(1f);
        camAnim.ChangeMenu(camAnim.Menus[0]);
        CharacterMenu.SetActive(true);
        InventoryMenu.SetActive(false);

  


        yield return StartCoroutine(GameObject.Find("WebServices").GetComponent<WebServices>().GetUserInventory());
        GameObject.Find("CharacterMenu").GetComponent<Character_Menu>().DisplayItems();

    }



}
