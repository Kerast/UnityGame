using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	[SerializeField] AudioListener AudioListener;
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {

			GetComponent<Player_Controller>().enabled = true;
			FPSCharacterCam.enabled = true;
			AudioListener.enabled=true;
			FPSCharacterCam.GetComponent<MouseAimCamera>().enabled = true;

		}
	}
	
	
}
