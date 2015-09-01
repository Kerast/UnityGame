using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class Player_FireManager : NetworkBehaviour {
	


	public List<GameObject> GoSpellList;
	public List<SpellTemplate> SpellList;
	public List<string> SpellButtonList;
	//public int CurrentCastingSpell;
	[SyncVar (hook="onCurrentCastingSpellChanged")] public int CurrentCastingSpell = -1;

	private Vector3 previousPlayerPosition;
	private bool PlayerIsMoving;

	private Player_Stats PlayerStats;
	private Player_Controller PlayerController;

	private GameObject spellToUse;
	private GameObject CastEffect;
	private bool CastAnimationStarted;
	private int previousCastingSpell;

	private GameObject clone;
	[SerializeField] private Transform camTransform;
	private RaycastHit hit;


	
	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {



		}
		CurrentCastingSpell = -1;
		CastAnimationStarted = false;
		for(int i=0;i<GoSpellList.Count;i++){
			PlayerIsMoving=false;
			PlayerStats=GetComponent<Player_Stats>();
			PlayerController=GetComponent<Player_Controller>();
			SpellList.Add (GoSpellList[i].GetComponent<SpellTemplate> ());
			SpellList[i].CastState=SpellTemplate.CastStateEnum.Idle;
			SpellList[i].OnCoolDown=false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {

			BindManager ();
			LocalCastManager ();

			if (Physics.Raycast (camTransform.position, camTransform.forward, out hit,500)) {

				transform.FindChild("SpellSpawnAnchor").LookAt(hit.point);
				Debug.DrawLine(transform.FindChild("SpellSpawnAnchor").position,hit.point,Color.green);
				Debug.DrawRay(transform.FindChild("SpellSpawnAnchor").position,transform.FindChild("SpellSpawnAnchor").forward,Color.red);

			}
		}
		CastAnimation ();


		
	}
	void BindManager()
	{	int spellNumber = -1;


		int i;
		for (i=0; i<SpellList.Count; i++) {
			if(Input.GetButtonDown(SpellButtonList[i]))
			{	
				SpellList[i].CastSpell();
				for (int j=0; j<SpellList.Count; j++) {
					if(j!=i)
					{
						SpellList[j].InterruptCasting();
					}
				}


			}

		}
	}

	void LocalCastManager()
	{
		for (int i=0; i<SpellList.Count; i++) {
			SpellList [i].CastManager (PlayerStats, PlayerController.isMoving);
			switch (SpellList [i].CastState) {
			case SpellTemplate.CastStateEnum.Idle:
				break;
			case SpellTemplate.CastStateEnum.TryCast:
				//CastEffect=Instantiate(SpellList[i].CastEffect,transform.FindChild("SpellSpawnAnchor").position,transform.FindChild("SpellSpawnAnchor").rotation) as GameObject;
				//CastEffect.transform.SetParent(transform.FindChild("SpellSpawnAnchor"));
				break;
			case SpellTemplate.CastStateEnum.Casting:
				CmdUpdateCurrentCastingSpell(i);
				break;
			case SpellTemplate.CastStateEnum.Failed:
			//	Destroy(CastEffect);
				CmdUpdateCurrentCastingSpell(-1);	
				break;
			case SpellTemplate.CastStateEnum.Casted:
			//	Destroy(CastEffect);
				CmdUpdateCurrentCastingSpell(-1);	
				if (Physics.Raycast (camTransform.position, camTransform.forward, out hit,500)) {
				}
				Vector3 aim=hit.point-transform.FindChild("SpellSpawnAnchor").position;
				transform.FindChild("SpellSpawnAnchor").LookAt(hit.point);
				Debug.Log (aim);
				CmdUpdatefireState (true, transform.FindChild("SpellSpawnAnchor").position, transform.FindChild("SpellSpawnAnchor").rotation,  aim, i, gameObject.transform.name,hit.point);
				break;
			case SpellTemplate.CastStateEnum.OnCD:
				break;
			}
			if(SpellList[i].hasNewMessage())
			{
				gameObject.GetComponent<Player_Info> ().SetMessage (SpellList [i].getMsgError(),"FireManager","System");
			}
		}
	}
	void CastAnimation()
	{

		if (CurrentCastingSpell!= -1 && previousCastingSpell==CurrentCastingSpell) {
			if(!CastAnimationStarted){
				if(SpellList [CurrentCastingSpell].CastArt!=null){
				CastEffect = Instantiate (SpellList [CurrentCastingSpell].CastArt, transform.FindChild ("SpellSpawnAnchor").position, transform.FindChild ("SpellSpawnAnchor").rotation) as GameObject;
				CastEffect.transform.SetParent (transform.FindChild ("SpellSpawnAnchor"));
					CastAnimationStarted=true;

				}
			}
			else{}

		} else {
				if (CastAnimationStarted) {
					Destroy (CastEffect);
					CastAnimationStarted=false;
				    previousCastingSpell=CurrentCastingSpell;
				}

			else{
			}
			}
		previousCastingSpell=CurrentCastingSpell;
		
	}
	[Command]
	void CmdUpdatefireState(bool st,Vector3 origin, Quaternion rotation, Vector3 direction,int SpellNumber,string PlayerCaster, Vector3 pointOfAim)
	{

			spellToUse=GoSpellList[SpellNumber];
		spellToUse.GetComponent<SpellTemplate> ().Player_Owner = PlayerCaster;
		spellToUse.GetComponent<SpellTemplate> ().PointOfAim=pointOfAim;

		clone = Instantiate (spellToUse, origin + rotation * Vector3.forward * 2, rotation) as GameObject;
			
			NetworkServer.Spawn (clone);

	}
	[Command]
	void CmdUpdateCurrentCastingSpell(int spell)
	{
		CurrentCastingSpell = spell;
	}



	public void onCurrentCastingSpellChanged(int spell){
		CurrentCastingSpell=spell;

	}

}
