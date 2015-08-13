using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu_ButtonCharacter : MonoBehaviour {

	public GameObject MenuCharacter;

	bool MoveTo = false;
	Vector3 refer = Vector3.zero;

	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (LoadCharacterMenu);
	}
	
	// Update is called once per frame
	void Update () {

		if (MoveTo) 
		{
			Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, GameObject.Find ("Waypoint Character").transform.position, ref refer, 15f);
			Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, GameObject.Find ("Waypoint Character").transform.rotation, 0.012f);
		}
	}


	void LoadCharacterMenu()
	{
		GameObject.Find ("Camera").transform.position = GameObject.Find ("Waypoint Character").transform.position;
		GameObject.Find ("Camera").transform.rotation = GameObject.Find ("Waypoint Character").transform.rotation;
		MenuCharacter.SetActive (true);
		MoveTo = true;
	}
}
