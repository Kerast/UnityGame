using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
public class SpellTemplate : NetworkBehaviour {

	public enum CastStateEnum{Idle,TryCast,Failed,Casting,Interrupted,Casted, OnCD};
	public enum VectorType{Projectile,Telecast,OnCaster,FromCaster};
	public enum SpellType{Magical,Physical};
	public string Name;
	public string Description;
	public SpellType Category;
	public VectorType DeliveryMode;
	public float PropellingForce;
	public string Player_Owner;
	public bool PhysicallyCollidable;
	public float LifeSpan;

	public GameObject CastArt;
	public GameObject StartArt;
	public float StartArtDuration;
	public GameObject OnGoingArt;
	public float OnGoingArtDuration;
	public GameObject EndArt;
	public float EndArtDuration;
	

	public int BaseStaminaCost;
	public float BaseManaCost;
	public float BaseHealthCost;
	
	public float BaseCoolDownTime;
	private float ModifiedCoolDownTime;
	public float CoolDownTimer;
	[SyncVar(hook="OnCooldownChanged")]public bool OnCoolDown;
	
	public float BaseCastTime;
	private float ModifiedCastTime;
	public float CastTimer;
	public bool isStaticCast;	
	[SyncVar(hook="OnCastStateChanged")] public CastStateEnum CastState;

	public  Vector3 PointOfAim;
	
	private bool newMessageAvailable;
	private string msgError;
	private bool hasEnoughRessources;
	private bool isInstantCast;

	private bool isTryingToCastWhileMoving;
	private bool isInterruptedByMovement;

	private bool isSpellCasting;

	public List <SpellEffect> SpellEffects;

	public void CastSpell()
	{
		if (CastState == CastStateEnum.Idle) {
			isSpellCasting=true;
	} else {
			isSpellCasting=false;
		}
		if (OnCoolDown) {
			setMsgError("Cannot Cast while on CD");
		}

	}

	public void InterruptCasting()
	{
		if (CastState == CastStateEnum.Casting) {
			CmdCastStateChange(CastStateEnum.Failed);
		}
	}

	public void CoolDownManager(){

			if (CoolDownTimer > 0) {
				CoolDownTimer -= Time.deltaTime;
			} else {
			CoolDownTimer = 0;
			CmdCoolDownToggle (false);
			}			
	
	}
	void OnCooldownChanged(bool cd)
	{
		OnCoolDown = cd;
	}

	public void CmdCoolDownToggle(bool cd)
	{
		OnCoolDown=cd;
		if (OnCoolDown == true) {
			CoolDownTimer=ModifiedCoolDownTime;
		}
	}
	public void CastManager(Player_Stats Caster,bool CasterIsMoving)
	{	ModifiedCastTime = BaseCastTime * (1 - Caster.getCastTimeReduction () / 100);
		ModifiedCoolDownTime = BaseCoolDownTime * (1 - Caster.getCooldownReduction () / 100);
	
		if (CastState == CastStateEnum.Idle) {
			CastTimer = ModifiedCastTime;
			if(isSpellCasting)
			{
				CmdCastStateChange(CastStateEnum.TryCast);
				isSpellCasting=false;
			}

		} 
		else if (CastState == CastStateEnum.TryCast) {

			bool castfail=false;
			if (BaseHealthCost < Caster.getHealth() && BaseStaminaCost < Caster.getStamina() && BaseManaCost < Caster.getMana()) {
				hasEnoughRessources = true;
			} else {
				hasEnoughRessources = false;
			}

			if (BaseCastTime == 0) {
				isInstantCast = true;
			} else {
				isInstantCast = false;
			}
			if(Caster.getisSilenced()!=0&&Category==SpellType.Magical)
			{
				castfail=true;
			}
			if(Caster.getisDisarmed()!=0&&Category==SpellType.Physical)
			{
				castfail=true;
			}
			if(Caster.getisStunned()!=0)
			{
				castfail=true;
			}
		

			if (hasEnoughRessources&&castfail==false) {
				if (isInstantCast) {
					CastState = CastStateEnum.Casted;
				} else {
					if(!CasterIsMoving)
					{
					CastState = CastStateEnum.Casting;
					}
					else{
						if(isStaticCast){
						setMsgError("Cannot Cast while moving");
							CastState = CastStateEnum.Failed;}
						else{
							CastState = CastStateEnum.Casting;
						}
					}
				}
			} else {
				if(castfail==false){
					setMsgError("Unsufficient Ressources");}
				else if(castfail==true)
				{
					setMsgError("Unable to use that ability");
				}
				CastState = CastStateEnum.Failed;

			}
		} 
		else if (CastState == CastStateEnum.Failed) {
			CastTimer = ModifiedCastTime;
			CastState = CastStateEnum.Idle;
		}
		else if (CastState == CastStateEnum.Casting) {


			if (CastTimer > 0) {
				CastTimer -= Time.deltaTime;
			} else {
				CastTimer = 0;
				CmdCastStateChange(CastStateEnum.Casted);
			}
			if(CasterIsMoving&&isStaticCast){
			
				setMsgError("Cast Interrupted");
				CmdCastStateChange(CastStateEnum.Failed);
			}
		}
	
		else if (CastState == CastStateEnum.Casted) {
			CmdCoolDownToggle(true);

			CmdCastStateChange(CastStateEnum.OnCD);
			//Next State is accessed by fire manager
		}
		else if (CastState == CastStateEnum.OnCD) {

			if (OnCoolDown == false) {
				CmdCastStateChange(CastState=CastStateEnum.Idle);
			} else{
				CoolDownManager();
			}
		}

	}
	void OnCastStateChanged(CastStateEnum cse)
	{
		CastState=cse;
	}
	
	public void CmdCastStateChange(CastStateEnum cse)
	{
		CastState=cse;

	}

	public void setMsgError(string s)
	{
		msgError = s;
		newMessageAvailable = true;


	}
	public string getMsgError()
	{
		newMessageAvailable = false;
		return msgError;
	}
	public bool hasNewMessage()
	{
		return newMessageAvailable;
	}
	public float getModifiedCastTime()
	{
		return ModifiedCastTime;
	}
	public float getModifiedCooldownTime()
	{
		return ModifiedCoolDownTime;
	}
}
