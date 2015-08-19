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
		

        List<ISItem> PlayerItems = new List<ISItem>();
        Player_Equipment playerequipment = GameObject.Find("Player_Dummy").GetComponent<Player_Equipment>();
        PlayerItems.Add(playerequipment.Weapon);
        PlayerItems.Add(playerequipment.Helmet);
        PlayerItems.Add(playerequipment.Torse);
        PlayerItems.Add(playerequipment.Belt);
        PlayerItems.Add(playerequipment.ShoulderR);
        PlayerItems.Add(playerequipment.ShoulderL);
        PlayerItems.Add(playerequipment.GloveR);
        PlayerItems.Add(playerequipment.GloveL);
        PlayerItems.Add(playerequipment.Legs);


        GameObject[] slots = GameObject.FindGameObjectsWithTag("ItemSlot");
        List<GameObject> Slots = new List<GameObject> ();

		foreach (var slot in slots)
		{
			Slots.Add(slot);
		}


		for(int i = 0; i < PlayerItems.Count;  i++)
		{

			Image slotImage = Slots.Find(j => j.GetComponent<Character_ItemSlot>().equipmentSlot == PlayerItems[i].EquipmentSlot).transform.GetChild(0).GetComponent<Image>();
            if(PlayerItems[i].Icon != null)
                slotImage.sprite = PlayerItems[i].Icon;
           
        }




	}
}
