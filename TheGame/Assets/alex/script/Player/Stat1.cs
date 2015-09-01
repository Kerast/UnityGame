using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]public class Stat1  {

	// Use this for initialization
	public enum StatName{Health,Mana,Stamina,MaxHealth,MaxMana,MaxStamina,HealthRegen,ManaRegen,StaminaRegen,
		MovementSpeed,CooldownReduction,CastTimeReduction,
		MagicalArmor,PhysicalArmor,
		PhysicalPower,MagicalPower,
	isRooted,isSilenced,isDisarmed,isStunned};

	public StatName Name;
	public float CurrentValue;
	public float BaseValue;
	public float ModifiedValue;
	public bool hasBaseValue = true;

	public Sprite icon;


	public Stat1(StatName name, float currentValue, float baseValue,float modifiedValue)
	{
		Name = name;
		CurrentValue = currentValue;
		BaseValue = baseValue;
		ModifiedValue = modifiedValue;


	}
	public Stat1(StatName name, float currentValue, float baseValue,float modifiedValue,bool CheckifhasBaseValue)
	{
		Name = name;
		CurrentValue = currentValue;
		BaseValue = baseValue;
		ModifiedValue = modifiedValue;
		

		hasBaseValue = CheckifhasBaseValue;
	}


}