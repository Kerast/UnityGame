using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Sprites;
using System.Collections.Generic;

public class Player_GUI : NetworkBehaviour {

	private Text healthText;
	private GameObject healthBar;
	private Slider healthBarFill;

	private Text staminaText;
	private GameObject staminaBar;
	private Slider staminaBarFill;

	private Text manaText;
	private GameObject manaBar;
	private Slider manaBarFill;

	private Text CastText;
	private GameObject CastBar;
	private Slider CastBarFill;


	private Text SystemMessage;
	private Text FloatingMessage;

	private Text Spell1Text;
	private Text Spell2Text;
	private Text Spell3Text;
	private Text Spell4Text;
	private List<Text> SpellTextList;

	private GameObject SkillBar;
	public GameObject SkillSlotPrefab;

	private GameObject EffectBar;
	public GameObject EffectSlotPrefab;
	
	private List<GameObject> SkillSlotList;
	private List<GameObject> EffectSlotList;

	private List<SpellEffect> Effects;
	private int EffectQte=0;

	private float currentHealth;
	private float currentStamina;
	private float currentMana;
	private GameObject UI;
	private Player_ClassSelector class_role;
	private Player_FireManager FireManager;
	private Player_Stats PlayerStats;



	// Use this for initialization











	void Start () {
		if (isLocalPlayer) {
			Effects = new List<SpellEffect> ();
			EffectSlotList = new List<GameObject> ();
			SkillSlotList = new List<GameObject> ();
			FireManager = GetComponent<Player_FireManager> ();
			SkillBar = GameObject.Find ("SkillBar");
			for (int i=0; i<FireManager.GoSpellList.Count; i++) {

				SkillSlotList.Add (Instantiate (SkillSlotPrefab));
				SkillSlotList [i].transform.SetParent (SkillBar.transform);
				SkillSlotList [i].transform.name = "SkillSlot" + i;


			}


			healthBar = GameObject.Find ("healthBar");
			healthBarFill = healthBar.GetComponent<Slider> ();
			healthText = GameObject.Find ("healthText").GetComponent<Text> ();
		
			staminaBar = GameObject.Find ("staminaBar");
			staminaBarFill = staminaBar.GetComponent<Slider> ();
			staminaText = GameObject.Find ("staminaText").GetComponent<Text> ();
		
			manaBar = GameObject.Find ("manaBar");
			manaBarFill = manaBar.GetComponent<Slider> ();
			manaText = GameObject.Find ("manaText").GetComponent<Text> ();
	
			CastBar = GameObject.Find ("CastBar");
			CastBarFill = CastBar.GetComponent<Slider> ();

			SystemMessage = GameObject.Find ("SystemMessage").GetComponent<Text> ();

			SpellTextList = new List<Text> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isLocalPlayer) {
			PlayerStats = GetComponent<Player_Stats> ();
			class_role = GetComponent<Player_ClassSelector> ();
			FireManager=GetComponent<Player_FireManager>();



			healthBarFill.maxValue=(float)class_role.selectedClass.maxHealth;
			staminaBarFill.maxValue=(float)class_role.selectedClass.maxStamina;
			manaBarFill.maxValue=(float)class_role.selectedClass.maxMana;

			UiEffectManager();
				if(FireManager.CurrentCastingSpell!=-1)
			{
				CastBar.SetActive(true);
				CastBarFill.maxValue=FireManager.SpellList[FireManager.CurrentCastingSpell].getModifiedCastTime();
				CastBarFill.value=-1*(FireManager.SpellList[FireManager.CurrentCastingSpell].CastTimer-CastBarFill.maxValue);
			}
			else{
				CastBar.SetActive(false);
			}

			currentHealth = GetComponent<Player_Stats> ().getHealth ();
			currentStamina = GetComponent<Player_Stats> ().getStamina ();
			currentMana = GetComponent<Player_Stats> ().getMana ();

			
			SetHealthUI(currentHealth);
			SetStaminaUI(currentStamina);
			SetManaUI(currentMana);
			SetCoolDowntUI();
			SetSystemMessages();
			SetFloatingMessages();


		}
	}

	void SetHealthUI(float hp)
	{
		healthText.text = "Health" + hp.ToString();
		healthBarFill.value = hp;

	}
	void SetStaminaUI(float st)
	{
		staminaBarFill.value = st;
			staminaText.text = "Stamina" + st.ToString();
			

	}
	void SetManaUI(float mn)
	{

			manaText.text = "Mana" + mn.ToString();
			manaBarFill.value = mn;

	}
	void SetCastUI(int mn)
	{
		
		manaText.text = "Mana" + mn.ToString();
		manaBarFill.value = mn;
		

	}
	void SetCoolDowntUI()
	{
		int i = 0;
		for (i=0; i<FireManager.SpellList.Count; i++) {

			if(FireManager.SpellList[i].OnCoolDown==true){
				SkillSlotList[i].transform.GetChild(1).GetComponent<Text>().text=FireManager.SpellList[i].name+" Ready in :"+FireManager.SpellList[i].CoolDownTimer.ToString ("#.##")+"s";
				SkillSlotList[i].transform.GetChild(1).GetComponent<Text>().color=Color.red;
			}
			else{
				SkillSlotList[i].transform.GetChild(1).GetComponent<Text>().text=FireManager.SpellList[i].name;
				SkillSlotList[i].transform.GetChild(1).GetComponent<Text>().color=Color.green;
			}

		}
	
	}
	void SetSystemMessages()
	{
		SystemMessage.text = "";
		List<string> Messages = gameObject.GetComponent<Player_Info> ().GetMessagesString ("System");
		for (int i=0; i<Messages.Count; i++) {

			SystemMessage.text += Messages[Messages.Count-i-1]+"\n";
		}

			
	}
	void SetFloatingMessages()
	{
		//FloatingMessage.text = "";
		List<string> Messages = gameObject.GetComponent<Player_Info> ().GetMessagesString ("Floating");

		for (int i=0; i<Messages.Count; i++) {
			
			//FloatingMessage.text += Messages[Messages.Count-i-1]+"\n";
		
		}
		
		
	}

	void UiEffectManager()
	{

	
		EffectBar = GameObject.Find ("EffectBar");

		Effects = PlayerStats.GetEffectsOnPlayer ();


		if (EffectQte != Effects.Count) {

			Effects = PlayerStats.GetEffectsOnPlayer ();
			foreach (GameObject effect in EffectSlotList) {
				Destroy (effect);

			}
			EffectSlotList.Clear ();
			for (int i=0; i<Effects.Count; i++) {
				if(Effects[i].DurationValue>0){
				EffectSlotPrefab.transform.GetChild (0).GetComponent<Image> ().sprite=Effects[i].Icon;
				
				EffectSlotList.Add (Instantiate (EffectSlotPrefab));		
					EffectSlotList[EffectSlotList.Count-1].transform.SetParent (EffectBar.transform);
			//	}
			
				
				EffectQte = Effects.Count;
			}
				else{

				}
			}
		} else {
			for (int i=0; i<EffectSlotList.Count; i++) {
					if(Effects[i].DurationValue>0){
				EffectSlotList [i].transform.GetChild (1).GetComponent<Text> ().text = Effects [i].DurationTimer.ToString ("#.#");
				EffectSlotList [i].transform.GetChild (1).GetComponent<Text> ().color = Color.green;
				//Stat StatIcon=new Stat(Effects [i].AffectedStat,0,0,0);
				}
			
			}
		}

	}
	/*
		for (int i=0; i<Effects.Count; i++) {
			if (!Effects [i].isDisplayedOnGui ()) {
				EffectSlotList.Add(Instantiate(EffectSlotPrefab));
				EffectSlotList[i].transform.SetParent (EffectBar.transform);
				EffectSlotList[i].transform.name = "EffectSlot" + i;


			
				Effects [i].DisplayOnGui ();
			}
			
			*/
		

}
