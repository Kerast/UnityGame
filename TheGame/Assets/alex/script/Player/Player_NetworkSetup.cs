using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	//[SerializeField] GameObject playerUI;
	[SerializeField] AudioListener AudioListener;
	// Use this for initialization
	void Start () {

		if (isLocalPlayer) {

			GetComponent<Player_Controller> ().enabled = true;
			FPSCharacterCam.enabled = true;
			AudioListener.enabled = true;
			FPSCharacterCam.GetComponent<MouseAimCamera> ().enabled = true;
            
            



        }
        else {
            
        }
        GetComponent<Player_GUI>().enabled = true;
        GetComponent<Player_Stats>().enabled = true;
        GetComponent<Player_Info>().enabled = true;
        GetComponent<Player_Death>().enabled = true;

        GetComponent<Player_TagPlate> ().enabled = true;
        GetComponent<Player_SyncPosition>().enabled = true;
        GetComponent<Player_ClassSelector>().enabled = true;
        GetComponent<Player_FireManager>().enabled = true;

    }
	

}
