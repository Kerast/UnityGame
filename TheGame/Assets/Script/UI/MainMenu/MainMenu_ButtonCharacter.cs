using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ButtonCharacter : MonoBehaviour {

	public GameObject CharacterMenu;

    public Camera Camera3dUI;
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
        CharacterMenu.SetActive (true);

        Camera3dUI.orthographic = true;
        PlayerDummy.transform.position = GameObject.Find("CharacterMenuPosition").transform.position;
        PlayerDummy.transform.rotation = GameObject.Find("CharacterMenuPosition").transform.rotation;
    }
}
