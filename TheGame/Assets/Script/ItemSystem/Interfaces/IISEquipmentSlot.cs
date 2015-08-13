using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem
{
	public interface IISEquipmentSlot 
	{

		string Name { get; set; }
		Sprite Icon { get; set; }
	}
}
