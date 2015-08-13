using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem.Editor
{
	public partial class ISObjectEditor 
	{

		void TopTabBar()
		{
			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));
			WeaponTab ();
			ArmorTab ();
			GUILayout.Button ("Consumables");
			About ();
			GUILayout.EndHorizontal ();

		}

		void WeaponTab()
		{
			GUILayout.Button ("Weapons");
		}

		void ArmorTab()
		{
			GUILayout.Button ("Armor");
		}

		void About()
		{
			GUILayout.Button ("About");
		}


	}

}
