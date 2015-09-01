using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

[System.Serializable] public class SpellEffect  {


	public GameObject EffectParticleSystem;
	public float EffectArtDuration;
	public enum EffectApplyRate{Continuous,Periodically, Once}
	public enum EffectSpellTrigger {none,EndSpell};
	public enum EffectAllowedTarget{All,Self,Enemies,Allies};
	public enum EffectType{NewSpell,MagicalDamage,PhysicalDamage,Buff,Debuff,Displacement,CrowdControl};
	public enum EffectDuration{Instant,OverTime};

	public enum EffectModifier{Absolute,Percentage};

	public string Name;
	public Sprite Icon;
	public Triggers EffectTriggers;

	public EffectSpellTrigger SpellTrigger;

	public EffectApplyRate ApplyRate;
	private bool Applyable=false;
	private bool ApplyedOnce = false;
	public float ApplyPeriod;
	private float ApplyPeriodTimer;
	
	public EffectAllowedTarget Target;
	private bool isOnAllowedTarget;

	public EffectType Type;

	public EffectDuration Duration;
	public float DurationValue;
	public float DurationTimer;
	public Stat1.StatName AffectedStat;
	
	public EffectModifier Modifier;

	public float value;
	public float PhysicalPowerScaling=0;
	public float MagicalPowerScaling=0;
	public bool isApplyedOnPlayer=false;
	private bool isOnGui=false;
	

	public bool isApplyable(Triggers SpellTriggers)
	{
		if(EffectTriggers.timeTrigger==SpellTriggers.timeTrigger || EffectTriggers.timeTrigger==Triggers.TimeTrigger.Always)
		{
			EffectTriggers.isTimeTriggered=true;
		}
		if(EffectTriggers.spatialTrigger==SpellTriggers.spatialTrigger|| EffectTriggers.spatialTrigger==Triggers.SpatialTrigger.none)
		{
			EffectTriggers.isSpatialTriggered=true;
		}
		if (EffectTriggers.isTimeTriggered && EffectTriggers.isSpatialTriggered) {
			switch (ApplyRate) {
			case EffectApplyRate.Continuous:
				Applyable = true;
				break;
			case EffectApplyRate.Once:
				if (!ApplyedOnce) {
					Applyable = true;
				} else {
					Applyable = false;
				}
				break;
			case EffectApplyRate.Periodically:
				Applyable = ApplyPeriodManager ();
				break;
			}
		}
		return Applyable;
	}

	public EffectSpellTrigger getSpellTrigger()
	{
		return SpellTrigger;
			
		
	}
	public void Apply(Player_Stats Target){
	
		ApplyedOnce=true;
		isApplyedOnPlayer=false;
		DurationTimer = DurationValue;
	/*	if (Target.GetEffectsOnPlayer ().Contains (this)) {
		
		} else {*/
		string newSpellEffect = this.Name;
		SpellEffect oldSpellEffect = Target.GetEffectsOnPlayer().Find(delegate(SpellEffect se) { return se.Name == newSpellEffect; });
		if (oldSpellEffect == null) {
			Target.addEffect (this);
		} else {
			oldSpellEffect.DurationTimer=oldSpellEffect.DurationValue;
		}
		//}
	
	}
	bool ApplyPeriodManager()
	{
		if (ApplyPeriodTimer > 0) {
			ApplyPeriodTimer -= Time.deltaTime;
			return false;
			
		} else {
			ApplyPeriodTimer = ApplyPeriod;
			return true;
		}

	}
	public bool isFinished()
	{
		if (DurationTimer > 0) {
			DurationTimer -= Time.deltaTime;
			return false;
			
		} else {
			DurationTimer = DurationValue;
			return true;
		}
		
	}

	public float ModifiedValue(float baseValue)
	{
		switch (Modifier) {
		case EffectModifier.Absolute:
			return value;
			break;

		case EffectModifier.Percentage:
			return (baseValue*value/100);

		}
		return 0;
	}
	public bool isDisplayedOnGui()
	{
		return isOnGui;
	}
	public void DisplayOnGui()
	{
		isOnGui = true;
	}


}
