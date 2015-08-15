using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheGame.ItemSystem;
using TheGame.StatSystem;

namespace TheGame.ItemSystem
{
	public interface IISItem  {
		
		string Identity { get; set; }
		string Name { get; set; }
		Sprite Icon { get; set; }
		ISQuality Quality { get; set; }
		EquipmentSlot EquipmentSlot{ get; set; }
		
		
		List<Stat> Stats { get; set; }
		List<GameObject> Skins { get; set; }


		
	}
}

