using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_TagPlate : NetworkBehaviour {
	public GameObject TagPlatePrefab;
	public float Height;
	private GameObject tagPlate;

	private Text Name;

	private Text healthText;
	private GameObject healthBar;
	private Slider healthBarFill;
	
	private Text staminaText;
	private GameObject staminaBar;
	private Slider staminaBarFill;
	
	private Text manaText;
	private GameObject manaBar;
	private Slider manaBarFill;
	
	private GameObject SpellName;
	private Text SpellText;
	private GameObject CastBar;
	private Slider CastBarFill;
	private int LastCastingSpell;

	private Player_Stats PStats;
	private Player_ClassSelector class_role;
	private Player_Info PlayerInfo;
	private Player_FireManager FireManager;
	// Use this for initialization
	void Start () {


			LastCastingSpell = -1;
			tagPlate = Instantiate (TagPlatePrefab);
			tagPlate.GetComponent<DisplayAbove> ().TargetTransform = transform;
			tagPlate.GetComponent<DisplayAbove> ().Height = Height;	
		tagPlate.transform.name = "Tag"+tagPlate.GetComponent<DisplayAbove> ().TargetTransform.name;
			tagPlate.transform.SetParent (GameObject.Find ("UI").transform);

		healthBarFill = tagPlate.transform.FindChild ("healthBar").GetComponent<Slider> ();
		manaBarFill=tagPlate.transform.FindChild("manaBar").GetComponent<Slider>();
		staminaBarFill=tagPlate.transform.FindChild("staminaBar").GetComponent<Slider>();
		CastBar = tagPlate.transform.FindChild ("CastBar").gameObject;
		CastBarFill=CastBar.GetComponent<Slider>();





		SpellName=CastBar.transform.FindChild("SpellName").gameObject;
		SpellText = SpellName.GetComponent<Text> ();

		Name=tagPlate.transform.FindChild("Name").GetComponent<Text>();

	}
	
	// Update is called once per frame
	void Update () {
	
		PStats = GetComponent<Player_Stats> ();
		PlayerInfo = GetComponent<Player_Info> ();
		class_role = GetComponent<Player_ClassSelector> ();
		FireManager = GetComponent<Player_FireManager> ();

		if (PStats != null &&
			class_role != null &&
			FireManager != null &&
			PlayerInfo != null) {


			float health = 0;
			float mana = 0;
			float stamina = 0;

			health = PStats.getHealth ();
			mana = PStats.getMana ();
			stamina = PStats.getStamina ();

			healthBarFill.maxValue = class_role.selectedClass.maxHealth;
			manaBarFill.maxValue = class_role.selectedClass.maxMana;
			staminaBarFill.maxValue = class_role.selectedClass.maxStamina;

			healthBarFill.value = health;
			manaBarFill.value = mana;
			staminaBarFill.value = stamina;

			Name.text = GetComponent<Player_Info> ().PlayerName;

				if (FireManager.CurrentCastingSpell != -1 && FireManager.SpellList.Count > 0) {

				if(LastCastingSpell!=FireManager.CurrentCastingSpell){
					CastBarFill.value=0;
					LastCastingSpell=FireManager.CurrentCastingSpell;
				}

				CastBar.SetActive (true);
				CastBarFill.maxValue = FireManager.SpellList [FireManager.CurrentCastingSpell].getModifiedCastTime();
				SpellText.text=FireManager.SpellList[FireManager.CurrentCastingSpell].Name;
				CastBarFill.value += Time.deltaTime;
			
				} else {
				CastBarFill.value=0;
					CastBar.SetActive (false);
					
				}

			}
		 else {
			int test=0;
		}
	}
}
