using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Player_ClassSelector : MonoBehaviour{

	public class Class{
	
	public string name;
	public int str; //PDmg multiply
	public int dex; //CDR + Crit + Move Speed
	public int end; //Hp / PArm multiply / Stamina regen
	public int intel; // Mdmg multiply
	public int wis; //Mp / MArm multiply / MpRegen 
	

		
		public int maxStamina=10;
		public float staminaRegen=10;

		public int maxHealth=10;
		public float healthRegen=10;

		public int maxMana=10;
		public float manaRegen=10;

			
		public int movementSpeed = 10;
		public float gcd = 10;

		public int mainAttackStaminaCost = 10;

	// Use this for initialization
	
	
	public Class(string class_name){
		if (class_name.Contains ("warrior")) 
			{
				
				this.str = 75;
				this.dex = 60;
				this.end = 95;
				this.intel = 20;
				this.wis = 25;

				maxStamina=end;
				staminaRegen=end/5;
				
				maxHealth=end;
				healthRegen=end/20;
				
				maxMana=wis;
				manaRegen=wis/40;
				
				movementSpeed = dex / 10;
				gcd =1.0f-(float)dex/200f;
			}
			
			if (class_name.Contains ("rogue")) {
				this.str = 100;
				this.dex = 100;
				this.end = 100;
				this.intel = 100;
				this.wis = 100;
				
			}
			
			if (class_name.Contains ("mage")) {
				this.str = 100;
				this.dex = 45;
				this.end = 55;
				this.intel = 100;
				this.wis = 100;
				
			}

		
	
	}
	}
	public string class_name;
	public Class selectedClass=new Class("warrior"); 

	void Start(){
		selectedClass=new Class(class_name);
	}


}

