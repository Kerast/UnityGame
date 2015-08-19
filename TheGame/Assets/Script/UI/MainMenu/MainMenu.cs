using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	


	void Start () {
		Camera.main.transform.position = GameObject.Find ("Waypoint Main Menu").transform.position;
		Camera.main.transform.rotation = GameObject.Find ("Waypoint Main Menu").transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
