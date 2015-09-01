using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Player_Stats : NetworkBehaviour {

	[SyncVar (hook="onStaminaChanged")] private float stamina = 100;
	[SyncVar (hook="onHealthChanged")] private float health = 100;
	[SyncVar (hook="onManaChanged")] private float mana = 100;
	[SyncVar] private float MaxStamina = 100;
	[SyncVar] private float MaxHealth = 100;
	[SyncVar] private float MaxMana = 100;
	[SyncVar (hook="onHealthRegenChanged")] private float HealthRegen = 100;
	[SyncVar (hook="onManaRegenChanged")] private float ManaRegen= 100;
	[SyncVar (hook="onStaminaRegenChanged")] private float StaminaRegen= 100;
	[SyncVar (hook="onMovementSpeedChanged")] private float MovementSpeed = 10;
	[SyncVar (hook="onCoolDownReductionChanged")] private  float CooldownReduction = 0;



	[SyncVar (hook="onCastTimeReductionChanged")] private  float CastTimeReduction = 0;
	[SyncVar (hook="onMagicalPowerChanged")] private float MagicalPower = 100;
	[SyncVar (hook="onPhysicalPowerChanged")] private float PhysicalPower = 100;
	[SyncVar (hook="onMagicalArmorChanged")] private float MagicalArmor = 10;
	[SyncVar (hook="onPhysicalArmorChanged")] private float PhysicalArmor = 10;

	[SyncVar (hook="onisRootedChanged")] private float isRooted=0;
	[SyncVar (hook="onisSilencedChanged")] private float isSilenced=0;
	[SyncVar (hook="onisDisarmedChanged")] private float isDisarmed = 0;
	[SyncVar (hook="onisStunnedChanged")] private float isStunned=0;

	private bool kill;
	private bool isDead;
	public delegate void DieDeletage();
	public event DieDeletage EventDie;


	private List<Stat1> Stats;

	private List<SpellEffect> EffectsOnPlayer;
	private Player_ClassSelector class_role;
	
	private float staminaRegenTick;
	private float healthRegenTick;
	private float manaRegenTick;
	private float StaminaRegenOutput;
	private float HealthRegenOutput;
	private float ManaRegenOutput;

	
	void Awake(){	

	}
	// Use this for initialization
	void Start () {
		EffectsOnPlayer = new List<SpellEffect> ();
		stamina = 100;
		health = 100;
		mana = 100;


		if (class_role != null) {
			CmdSetStamina (class_role.selectedClass.maxStamina);
			CmdSetHealth (class_role.selectedClass.maxHealth);
			CmdSetMana (class_role.selectedClass.maxMana);
		}
		Stats = new List<Stat1> ();
		Stats.Add (new Stat1(Stat1.StatName.Health,100,100,0,false));
		Stats.Add (new Stat1(Stat1.StatName.Stamina,80,80,0,false));
		Stats.Add (new Stat1(Stat1.StatName.Mana, 50, 50, 0, false));

		Stats.Add (new Stat1(Stat1.StatName.MaxHealth,100,100,0));
		Stats.Add (new Stat1(Stat1.StatName.MaxStamina,80,80,0));
		Stats.Add (new Stat1(Stat1.StatName.MaxMana,50,50,0));

        Stats.Add (new Stat1(Stat1.StatName.HealthRegen,10,10,0));   //Regen is in per 2 sec ticks
        Stats.Add (new Stat1(Stat1.StatName.StaminaRegen,25,25,0));  //Regen is in per 2 sec ticks
        Stats.Add (new Stat1(Stat1.StatName.ManaRegen,10,10,0));     //Regen is in per 2 sec ticks

		Stats.Add (new Stat1(Stat1.StatName.MovementSpeed,10,10,0));		//is in unit per Sec
		Stats.Add (new Stat1(Stat1.StatName.CooldownReduction,50,50,0));  //is in %
		Stats.Add (new Stat1(Stat1.StatName.CastTimeReduction,70,70,0));  //is in %

		Stats.Add (new Stat1(Stat1.StatName.PhysicalPower,50,50,0));//100 = 100% of base Attack damage
		Stats.Add (new Stat1(Stat1.StatName.MagicalPower,50,50,0)); //100 = 100% of base spell damage

		Stats.Add (new Stat1(Stat1.StatName.PhysicalArmor,10,50,0)); //flat reduction of Attack damage
		Stats.Add (new Stat1(Stat1.StatName.MagicalArmor,10,50,0));  //flat reduction of Spell damage

		Stats.Add (new Stat1(Stat1.StatName.isRooted,0,0,0));  //flat reduction of Spell damage
		Stats.Add (new Stat1(Stat1.StatName.isSilenced,0,0,0));  //flat reduction of Spell damage
		Stats.Add (new Stat1(Stat1.StatName.isStunned,0,0,0));  //flat reduction of Spell damage
		Stats.Add (new Stat1(Stat1.StatName.isDisarmed,0,0,0));  //flat reduction of Spell damage

	}
	
	// Update is called once per frame
	void Update () {
		class_role = GetComponent<Player_ClassSelector> ();
		staminaRegen ();
		healthRegen ();
		manaRegen ();
		ApplyEffects ();
		CheckGameConditions ();


	}
	void staminaRegen(){
		staminaRegenTick += Time.deltaTime * StaminaRegen;
		if (Mathf.Abs(staminaRegenTick) >= 5.0f) {
			StaminaRegenOutput=Mathf.Round(staminaRegenTick);
			staminaRegenTick=0;
		}else {
			StaminaRegenOutput = 0.0f;
		}
	}	
	void healthRegen(){
		healthRegenTick += Time.deltaTime * HealthRegen;
		if (Mathf.Abs(healthRegenTick) >= 5.0f) {
			HealthRegenOutput=Mathf.Round(healthRegenTick);
			healthRegenTick=0;
		}else {
			HealthRegenOutput = 0.0f;
		}
	}	

	void manaRegen(){
		manaRegenTick += Time.deltaTime * ManaRegen;
		if (Mathf.Abs(staminaRegenTick) >= 5.0f) {
			ManaRegenOutput = Mathf.Round(manaRegenTick);
			manaRegenTick = 0;
		} else {
			ManaRegenOutput = 0.0f;
		}
	}	


	public void Resetstamina()
	{
		stamina = MaxStamina;
	}
	
	public void deductStamina(int sta)
	{
		if (stamina - sta <= 0) {
			stamina=0;
		}
		else if (stamina - sta >= MaxStamina) {
			stamina = MaxStamina;
		} else {
			stamina -= sta;
		}
	}
	public void deductHealth(int sta)
	{
		if (health - sta <= 0) {
			health=0;
		}
		else if (health - sta >= MaxHealth) {
			health = MaxHealth;
		} else {
			health-= sta;
		}
	}
	public void deductMana(int sta)
	{
		if (mana - sta <= 0) {
			mana=0;
		}
		else if (mana - sta >= MaxMana) {
			mana = MaxMana;
		} else {
			mana-= sta;
		}
	}
	public float getStamina()
	{
		return stamina;
	}
	public float getHealth()
	{
		return health;
	}
	public float getMana()
	{
		return mana;
	}
	public float getCooldownReduction()
	{
		return CooldownReduction;
	}
	public float getCastTimeReduction()
	{
		return CastTimeReduction;
	}
	public float getPhysicalPower()
	{
		return PhysicalPower;
	}
	public float getMagicalPower()
	{
		return MagicalPower;
	}
	public float getisRooted()
	{
		return isRooted;
	}
	public float getisSilenced()
	{
		return isSilenced;
	}
	public float getisDisarmed()
	{
		return isDisarmed;
	}
	public float getisStunned()
	{
		return isStunned;
	}

	
	public void setStamina(int sta)
	{
		stamina = sta;
	}
	public void setHealth(int hp)
	{
		health = hp;
	}

	public void setMana(int mn)
	{
		mana = mn;
	}
	[Command]
	void CmdSetStamina(int st)
	{
		stamina = st;
	}
	[Command]
	void CmdSetHealth(int hp)
	{
		health = hp;
	}
	[Command]
	void CmdSetMana(int mn)
	{
		stamina = mn;
	}
	[Command]
	void CmdSetMovementSpeed(int ms)
	{
		MovementSpeed = ms;
	}

	public float getMovementSpeed()
	{
		return MovementSpeed;
	}
	public void onHealthChanged(float hp){
		health = hp;
	}
	public void onStaminaChanged(float st){
		stamina = st;
	}
	public void onManaChanged(float mn){
		mana = mn;
	}
	public void onManaRegenChanged(float mnr){
		ManaRegen = mnr;
	}
	public void onStaminaRegenChanged(float str){
		StaminaRegen = str;
	}
	public void onHealthRegenChanged(float hpr){
		HealthRegen = hpr;
	}
	public void onMovementSpeedChanged(float ms){
		MovementSpeed = ms;
	}
	public void onCoolDownReductionChanged(float cdr){
		CooldownReduction = cdr;
	}
	public void onCastTimeReductionChanged(float ctr){
		CastTimeReduction = ctr;
	}
	public void onMagicalPowerChanged(float ap){
		MagicalPower = ap;
	}
	public void onPhysicalPowerChanged(float pd){
		PhysicalPower = pd;
	}
	public void onMagicalArmorChanged(float ma){
		MagicalArmor = ma;
	}
	public void onPhysicalArmorChanged(float pa){
		PhysicalArmor = pa;
	}
	public void onisRootedChanged(float ir){
		isRooted = ir;
	}
	public void onisSilencedChanged(float isil){
		isSilenced = isil;
	}
	public void onisDisarmedChanged(float id){
		isDisarmed= id;
	}
	public void onisStunnedChanged(float ist){
		isStunned = ist;
	}
	public void addEffect(SpellEffect effect)
	{
		EffectsOnPlayer.Add (effect);

	}
	public void dissipateEffect(SpellEffect effect)
	{
		EffectsOnPlayer.Remove (effect);
	}
	private void ApplyEffects()
	{
		ResetStatModifiers ();
		for (int i=0; i<EffectsOnPlayer.Count; i++) {
			float hpreduction=0;
			switch (EffectsOnPlayer[i].Type){
			case SpellEffect.EffectType.PhysicalDamage:

				hpreduction=(EffectsOnPlayer[i].ModifiedValue(health))*(100.0f/(100.0f+PhysicalArmor));
				if(hpreduction<0){
					hpreduction=0;
				}

				Stats.Find(delegate(Stat1 st) { return st.Name == Stat1.StatName.Health; }).ModifiedValue-=hpreduction;

				break;
			case SpellEffect.EffectType.MagicalDamage:

				hpreduction=(EffectsOnPlayer[i].ModifiedValue(health))*(100.0f/(100.0f+MagicalArmor));
				if(hpreduction<0){
					hpreduction=0;
				}
				Stats.Find(delegate(Stat1 st) { return st.Name == Stat1.StatName.Health; }).ModifiedValue-=hpreduction;
				
				break;
			case SpellEffect.EffectType.Buff:
			case SpellEffect.EffectType.Debuff:

						foreach (Stat1 st in Stats)
						{
						if(st.Name==EffectsOnPlayer[i].AffectedStat)
						{
							st.ModifiedValue+=EffectsOnPlayer[i].ModifiedValue(st.BaseValue);
						}
						}
						break;

			}
			EffectsOnPlayer[i].isApplyedOnPlayer=true;
			if(EffectsOnPlayer[i].isFinished()){
			EffectsOnPlayer.Remove(EffectsOnPlayer[i]);
			}
		}
		ApplyStatModifiers ();
	}
	void ResetStatModifiers()
	{  foreach (Stat1 st in Stats) {
			st.ModifiedValue=0;
		}
	}
	void ApplyStatModifiers()
	{  
		foreach (Stat1 st in Stats) {
			switch (st.Name)
			{
			case Stat1.StatName.MaxHealth:
				MaxHealth=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.MaxMana:
				MaxMana=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.MaxStamina:
				MaxStamina=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.HealthRegen:
				HealthRegen=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.ManaRegen:
				ManaRegen=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.StaminaRegen:
				StaminaRegen=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.Health:
				float bufferHp=health+st.ModifiedValue+HealthRegenOutput;
				if(bufferHp>MaxHealth)
				{
					health=MaxHealth;
				}
				else if(bufferHp<0){
					health=0;
				}
				else{
					health=bufferHp;
				}

				st.CurrentValue=health;
				break;
			case Stat1.StatName.Stamina:
				float bufferSta=stamina+st.ModifiedValue+StaminaRegenOutput;
				if(bufferSta>MaxStamina)
				{
					stamina=MaxStamina;
				}
				else if(bufferSta<0){
					stamina=0;
				}
				else{
					stamina=bufferSta;
				}
				
				st.CurrentValue=stamina;
				break;
			case Stat1.StatName.Mana:
				float bufferMn=mana+st.ModifiedValue+ManaRegenOutput;
			
				if(bufferMn>MaxMana)
				{
					mana=MaxMana;
				}
				else if(bufferMn<0){
					mana=0;
				}
				else{
					mana=bufferMn;
				}
				
				st.CurrentValue=mana;
				break;
			case Stat1.StatName.MovementSpeed:
				MovementSpeed=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;

			case Stat1.StatName.CooldownReduction:
				CooldownReduction=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.CastTimeReduction:
				CastTimeReduction=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;

			case Stat1.StatName.PhysicalArmor:
				PhysicalArmor=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.MagicalArmor:
				MagicalArmor=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;

			case Stat1.StatName.MagicalPower:
				MagicalPower=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.PhysicalPower:
				PhysicalPower=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.isRooted:
				isRooted=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.isSilenced:
				isSilenced=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.isDisarmed:
				isDisarmed=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;
			case Stat1.StatName.isStunned:
				isStunned=st.BaseValue+st.ModifiedValue;
				st.CurrentValue=st.BaseValue+st.ModifiedValue;
				break;




			}
		}
	}
	public List<SpellEffect> GetEffectsOnPlayer()

	{
		return EffectsOnPlayer;
	}

	public void CheckGameConditions()
	{
		if (health <= 0 && !kill && !isDead) {
			kill = true;
		}
		if (kill) {
			if (EventDie != null) {
				EventDie ();
			}
			kill = false;


		}
	}

		public void setIsDead(bool id)
		{
		isDead = id;
		}


}
