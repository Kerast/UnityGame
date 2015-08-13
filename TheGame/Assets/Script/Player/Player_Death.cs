using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Death : NetworkBehaviour {

	private Player_Health healthScript;
	private Image crossHairImage;

	// Use this for initialization
	void Start () {
		crossHairImage = GameObject.Find ("Image").GetComponent<Image>();
		healthScript = GetComponent<Player_Health> ();
		healthScript.EventDie += DisablePlayer;
	}


	void OnDisable()
	{
		healthScript.EventDie -= DisablePlayer;
	}
	

	void DisablePlayer()
	{
		GetComponent<Collider> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Player_Shoot> ().enabled = false;

		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in renderers) 
		{
			ren.enabled = false;
		}

		healthScript.isDead = true;

		if (isLocalPlayer) 
		{
			GetComponent<Player_Controller> ().enabled = false;
			crossHairImage.enabled = false;
			GameObject aze = GameObject.Find ("GameManager");
			aze.GetComponent<GameManager_References>().respawnButton.SetActive(true);
		}
	}
}
