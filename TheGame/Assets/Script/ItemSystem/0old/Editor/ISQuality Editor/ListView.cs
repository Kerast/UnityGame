using UnityEditor;
using UnityEngine;
using System.Collections;

namespace TheGame.ItemSystem.Editor
{
	public partial class ISQualityDatabaseEditor  
	{



		//list all of the stored qualities in the database
		void ListView()
		{
			_scrollPos = EditorGUILayout.BeginScrollView (_scrollPos, GUILayout.ExpandWidth (true));

			DisplayQualities ();

			EditorGUILayout.EndScrollView ();
		}


		void DisplayQualities()
		{
			for (int i = 0; i < qualityDatabase.Count; i++) 
			{
				GUILayout.BeginHorizontal("Box");

				if(qualityDatabase.Get(i).Icon)
					selectedTexture = qualityDatabase.Get(i).Icon.texture;
				else
					selectedTexture = null;

				if(GUILayout.Button (selectedTexture, GUILayout.Width (SPRITE_BUTTON_SIZE), GUILayout.Height (SPRITE_BUTTON_SIZE)))
				{
					int controlerID = EditorGUIUtility.GetControlID(FocusType.Passive);
					EditorGUIUtility.ShowObjectPicker<Sprite>(null, true, null, controlerID);
					selectedIndex = i;
				}

				string commandName = Event.current.commandName;
				if (commandName == "ObjectSelectorUpdated") 
				{
					if(selectedIndex != -1)
					{
						qualityDatabase.Get(selectedIndex).Icon = (Sprite) EditorGUIUtility.GetObjectPickerObject();
						selectedIndex = -1;
						Repaint();
					}


				
				}

				GUILayout.BeginVertical();
				qualityDatabase.Get(i).Name = GUILayout.TextField( qualityDatabase.Get(i).Name);

				if(GUILayout.Button ("x", GUILayout.Width(30), GUILayout.Height(20)))
				{
					if(EditorUtility.DisplayDialog("Delete Quality", "Are you sure that you want to delete " + qualityDatabase.Get(i).Name + "from the database ?", 
					                               "Delete", 
					                               "Cancel"))
				 	{
						qualityDatabase.Remove(i);
					}


				}
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
			}
		}
	}
}
