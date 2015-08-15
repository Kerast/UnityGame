using UnityEngine;
using System.Collections;
using TheGame.ItemSystem;

namespace TheGame.ItemSystem
{
	public interface IISItem  {
		
		string Identity { get; set; }
		string Name { get; set; }
		Sprite Icon { get; set; }
		ISQuality Quality { get; set; }
		EquipmentSlot EquipmentSlot{ get; set; }
		
		
		int Damage { get; set; }
		int Defense { get; set; }
		
	}
}

