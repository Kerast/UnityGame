using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TheGame.ItemSystem;
using TheGame.StatSystem;

public class Player_Stat : MonoBehaviour {

	public List<Stat> Stats;

	public Player_Equipment Equipment; 

	// Use this for initialization
	void Awake()
	{
		//Stat de base avec valeur de base dans équipment
		Stats.Add(new Stat("health", "Name", 100, 0,0));
		Stats.Add(new Stat("mana", "Mana", 100, 0,0));
		Stats.Add(new Stat("stamina", "Stamina", 100, 0,0));
		Stats.Add(new Stat("defense", "Defense", 100, 0,0));
		Stats.Add(new Stat("damage", "Damage", 100, 0,0));
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		ResetStat();
		ActualiseItem (Equipment.Weapon);
		ActualiseItem (Equipment.Helmet);
		ActualiseItem (Equipment.Torse);
		ActualiseItem (Equipment.Belt);
		ActualiseItem (Equipment.ShoulderL);
		ActualiseItem (Equipment.ShoulderR);
		ActualiseItem (Equipment.GloveL);
		ActualiseItem (Equipment.GloveR);
		ActualiseItem (Equipment.Legs);
	}


	void ResetStat()
	{
		foreach (var stat in Stats) 
		{
			stat.ValueInt = 100;
			stat.ValueFloat = 0;
			stat.Modifier = 0;
		}
	}
	

	void ActualiseItem(ISItem item)
	{
		foreach(var stat in item.Stats)
		{
			Stat st = Stats.Find(i => i.Identity == stat.Identity);
			if(st != null)
			{
				ModifyStat(st, stat);
			}
			else
			{
				Debug.Log ("ItemStat not present in playetr Stats ACTUALISE PLAYER STAT or CHECK ITEM STAT");
			}
		}
	}

	void ModifyStat(Stat player_stat, Stat itemStat)
	{
		player_stat.ValueInt += itemStat.ValueInt;
		player_stat.ValueFloat += itemStat.ValueFloat;
		player_stat.Modifier += itemStat.Modifier;
	}
}
