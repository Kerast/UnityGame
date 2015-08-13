using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem
{
	public interface IISArmor 
	{
		int Defense { get; set; }
		EquipmentSlot EquipmentSlot { get; set; }
	
	}
	
}
