using UnityEngine;
using UnityEditor;
using System.Collections;


namespace TheGame.ItemSystem.Editor
{
	public partial class ISObjectEditor 
	{
		Vector2 _scrollPos = Vector2.zero;
		int _listViewWidth = 200;

		int _selectedIndex = -1;

		void ListView()
		{

		
			_scrollPos = GUILayout.BeginScrollView (_scrollPos,"Box", GUILayout.ExpandWidth (true), GUILayout.Width(_listViewWidth));

			GUILayout.Label ("ListView");
			for (int i = 0; i < database.Count; i++) 
			{
				if(GUILayout.Button(database.Get (i).Name, "Box", GUILayout.ExpandWidth(true) ))
				{
					_selectedIndex = i;
					tempWeapon = new ISWeapon(database.Get (i));
					showNewWeaponDetails = true;
					state = DisplayState.DETAILS;

				}
			}

			GUILayout.EndScrollView ();
		}

	}
}
