using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MainMenu_ButtonHome : MonoBehaviour {

    public GameObject CharacterMenu;

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
        CharacterMenu.SetActive(false);
        Camera3dUI.orthographic = false;
        PlayerDummy.transform.position = GameObject.Find("MainMenuPosition").transform.position;
        PlayerDummy.transform.rotation = GameObject.Find("MainMenuPosition").transform.rotation;
    }
}
