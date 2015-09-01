using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Player_Death : NetworkBehaviour {


	private Player_Stats Stats;

	void Start()
	{
		Stats = GetComponent<Player_Stats> ();
		Stats.EventDie += DisablePlayer;
	}

	void OnDisable()
	{
		if (Stats != null) {
			Stats.EventDie -= DisablePlayer;
		}
	}



	void DisablePlayer()
	{
		GetComponent<Collider> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;

		GetComponent<Player_TagPlate> ().enabled = false;
		GameObject TagPlate = GameObject.Find ("Tag" + transform.name);
		TagPlate.SetActive (false);
		Renderer[] renderers = GetComponentsInChildren<Renderer> ();
		foreach (Renderer ren in renderers) 
		{
			ren.enabled = false;
		}
		
		Stats.setIsDead (true);
		
		if (isLocalPlayer) 
		{
			GetComponent<Player_Controller> ().enabled = false;
			GetComponent<Player_FireManager> ().enabled = false;
			GameObject Respawn = GameObject.Find ("GameManager");
			Respawn.GetComponent<GameManager_References>().respawnButton.SetActive(true);
		}
		
	}
}
