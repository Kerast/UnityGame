using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TheGame.ItemSystem;
public class Character_ItemSlot : MonoBehaviour, IPointerClickHandler {

	public EquipmentSlot equipmentSlot;

	// Use this for initialization
	void Start () {
		
	}
	


	public void OnPointerClick(PointerEventData data)
	{
		GameObject.Find ("ItemScrollerPanel").GetComponent<Character_ItemScroller> ().LoadItems (equipmentSlot);
	}
}
