﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Respawn : NetworkBehaviour {

	private Player_Health healthScript;
	private Image crossHairImage;
	private GameObject respawnButton;


	void Start () {
		healthScript = GetComponent<Player_Health> ();
		healthScript.EventRespawn += EnablePlayer;
		crossHairImage = GameObject.Find ("Image").GetComponent<Image>();
		SetRespawnButton ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDisable()
	{
		healthScript.EventRespawn -= EnablePlayer;
	}

	void SetRespawnButton()
	{
		if (isLocalPlayer) {

			respawnButton = GameObject.Find ("GameManager").GetComponent<GameManager_References>().respawnButton;
			respawnButton.GetComponent<Button>().onClick.AddListener(CommenceRespawn);;
			respawnButton.SetActive(false);
		}
	}


	void EnablePlayer()
	{
		GetComponent<Collider> ().enabled = true;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<Player_Shoot> ().enabled = true;
		
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in renderers) 
		{
			ren.enabled = true;
		}
			
		
		if (isLocalPlayer) 
		{
			GetComponent<Player_Controller> ().enabled = true;
			crossHairImage.enabled = true;
			respawnButton.SetActive (false);

		}

	}



	void CommenceRespawn()
	{
		CmdRespawnOnServer ();
	}

	[Command]
	void CmdRespawnOnServer()
	{
		healthScript.ResetHealth ();
	}


}