using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpellGenerator: NetworkBehaviour  {
	
	private float TriggerDelay;	
	private Triggers triggers;
	private bool OnTargetEntered;
	private bool OnTargetInside;
	private bool OnTargetExited;
	
	//public float isInstant;  //MainProcess
	//private float LifeSpan;

	private float SpellTimer;
	
	private RaycastHit hit;
	private SpellTemplate spellTemplate;
	private GameObject SpellOwner;
	private List<GameObject> TargetsInside;
	
	// Use this for initialization
	void Start () {

		spellTemplate = GetComponent<SpellTemplate> ();
		triggers = new Triggers ();
		triggers.timeTrigger = Triggers.TimeTrigger.OnStart;
		SpellTimer = spellTemplate.LifeSpan;
		TargetsInside = new List<GameObject>();

		
		SpellOwner=GameObject.Find (spellTemplate.Player_Owner);
		SpellRessourceVerification (SpellOwner, spellTemplate);

		switch (spellTemplate.DeliveryMode){
			case SpellTemplate.VectorType.Telecast:
			transform.position = spellTemplate.PointOfAim;
			break;
		case SpellTemplate.VectorType.Projectile:
			GetComponent<Rigidbody>().AddForce(transform.rotation*Vector3.forward * spellTemplate.PropellingForce);

			break;
		case SpellTemplate.VectorType.OnCaster:
		transform.position=SpellOwner.transform.position;
			TargetsInside.Add (SpellOwner);
		transform.SetParent(SpellOwner.transform);
			break;
		case SpellTemplate.VectorType.FromCaster:
			transform.position=SpellOwner.transform.position;
		
			transform.SetParent(SpellOwner.transform);
			
			break;
		}

		if (spellTemplate.StartArt != null) {
			GameObject StartArt = Instantiate(spellTemplate.StartArt, transform.position, transform.rotation) as GameObject;
			StartArt.transform.SetParent (transform);
			Destroy (StartArt, spellTemplate.StartArtDuration);
		}
		if (spellTemplate.OnGoingArt != null) {
			GameObject StartArt = Instantiate(spellTemplate.OnGoingArt, transform.position, transform.rotation) as GameObject;
			StartArt.transform.SetParent (transform);
			Destroy (StartArt, spellTemplate.OnGoingArtDuration);
		}
		for (int i=0; i<spellTemplate.SpellEffects.Count; i++) {
			spellTemplate.SpellEffects [i].value = spellTemplate.SpellEffects [i].value *
				(1.0f + SpellOwner.GetComponent<Player_Stats> ().getPhysicalPower () * spellTemplate.SpellEffects [i].PhysicalPowerScaling +
				SpellOwner.GetComponent<Player_Stats> ().getMagicalPower () * spellTemplate.SpellEffects [i].MagicalPowerScaling);
		}
		
		
	}
	
	void Update () {
		
		
		if (SpellIsOver ()) {
			triggers.timeTrigger = Triggers.TimeTrigger.OnFinish;
			if (spellTemplate.EndArt != null) {
				GameObject EndArt = Instantiate (spellTemplate.EndArt, transform.position, transform.rotation) as GameObject;
				Destroy (EndArt, spellTemplate.EndArtDuration);
			}
			
			Destroy (gameObject);
		} else {
		
			for (int i=0; i<spellTemplate.SpellEffects.Count; i++) {
			
				if (spellTemplate.SpellEffects [i].isApplyable (triggers)) {
					foreach (GameObject pl in TargetsInside) {


						

						spellTemplate.SpellEffects [i].Apply (pl.GetComponent<Player_Stats> ());
						if (spellTemplate.SpellEffects [i].getSpellTrigger () == SpellEffect.EffectSpellTrigger.EndSpell) {
							SpellTimer = 0;
						}
						if (spellTemplate.SpellEffects [i].EffectParticleSystem != null) {
							if (spellTemplate.SpellEffects [i].EffectParticleSystem != null) {
								GameObject EffectAnimation = Instantiate (spellTemplate.SpellEffects [i].EffectParticleSystem, pl.transform.position, pl.transform.rotation) as GameObject;
								EffectAnimation.transform.SetParent (pl.transform);
								Destroy (EffectAnimation, spellTemplate.SpellEffects [i].EffectArtDuration);
							}
						}
					}
				}
			}
		
			triggers.timeTrigger = Triggers.TimeTrigger.OnUpdate;
		}

	}
	
	void OnTriggerEnter(Collider other){
		
		
		if(other.gameObject.tag == "Player") //Changer avec la detection enemis, amis, autres
		{triggers.spatialTrigger = Triggers.SpatialTrigger.OnTargetEntered;
			TargetsInside.Add (other.gameObject);
			
		}
		
		
	}
	
	void OnTriggerExit(Collider other){
		
		if(other.gameObject.tag == "Player") //Changer avec la detection enemis, amis, autres
		{triggers.spatialTrigger = Triggers.SpatialTrigger.OnTargetExited;
			TargetsInside.Remove (other.gameObject);
			
		}
		
	}
	
	void OnTriggerStay(Collider other){
		if(other.gameObject.tag == "Player"){
			triggers.spatialTrigger = Triggers.SpatialTrigger.OnTargetEntered;
		}
	}
	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player") {
			triggers.spatialTrigger = Triggers.SpatialTrigger.OnPhysicalCollide;
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			TargetsInside.Add (col.gameObject);
			col.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
	bool SpellIsOver()
	{
		if (SpellTimer >0) {
			SpellTimer -= Time.deltaTime;
			return false;
			
		} else {
			SpellTimer=0;
			return true;
			
		}
		
	}
	
	public void SpellRessourceVerification(GameObject so,SpellTemplate st)
	{
		if (so.GetComponent<Player_Stats> ().getHealth () > st.BaseHealthCost&&
		    so.GetComponent<Player_Stats> ().getMana () > st.BaseManaCost&&
		    so.GetComponent<Player_Stats> ().getStamina () > st.BaseStaminaCost) {
			
			so.GetComponent<Player_Stats> ().deductHealth ((int)st.BaseHealthCost);
			so.GetComponent<Player_Stats> ().deductMana ((int)st.BaseManaCost);
			so.GetComponent<Player_Stats> ().deductStamina ((int)st.BaseStaminaCost);
			
			//GetComponent<MeshRenderer> ().enabled = true;
			
			
		} else {
			Destroy (gameObject);
		}
	}
	
}

