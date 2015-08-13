using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem
{
	public interface IISWeapon  
	{
		int MinDamage { get; set; }
		float Attack();
	}
	
}
