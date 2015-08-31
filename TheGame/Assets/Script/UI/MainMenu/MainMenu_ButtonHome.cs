using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenu_ButtonHome : MonoBehaviour {

    public GameObject CharacterMenu;
    public GameObject InventoryMenu;

    public Camera Camera3dUI;
    public GameObject PlayerDummy;

    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(GoToHome);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GoToHome()
    {

        StartCoroutine(LoadHome());
       
    }


    IEnumerator LoadHome()
    {
        CameraAnimator camAnim = GameObject.FindWithTag("MainCamera").GetComponent<CameraAnimator>();
        camAnim.MoveCamera(camAnim.MainMenuPosition);
        yield return new WaitForSeconds(1f);
        camAnim.CloseAllMenus();

        
    }
}
