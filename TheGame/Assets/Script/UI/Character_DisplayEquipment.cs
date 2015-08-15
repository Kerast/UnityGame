using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheGame.ItemSystem;
using UnityEngine.UI;

public class Character_DisplayEquipment : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] nodes = GameObject.FindGameObjectsWithTag ("ItemNode");
		GameObject[] slots = GameObject.FindGameObjectsWithTag ("ItemSlot");
		List<GameObject> Nodes = new List<GameObject> ();
		List<GameObject> Slots = new List<GameObject> ();


		foreach (var node in nodes)
		{
			Nodes.Add(node);
		}

		foreach (var slot in slots)
		{
			Slots.Add(slot);
		}


		for(int i = 0; i < Nodes.Count;  i++)
		{

			Image slotImage = Slots.Find(j => j.GetComponent<Character_ItemSlot>().equipmentSlot == Nodes[i].GetComponent<Character_ItemNode>().EquipmentSlot).transform.GetChild(0).GetComponent<Image>();
			slotImage.sprite = Nodes[i].transform.GetChild(0).GetComponent<Item>().item.Icon;
		}
	}
}
