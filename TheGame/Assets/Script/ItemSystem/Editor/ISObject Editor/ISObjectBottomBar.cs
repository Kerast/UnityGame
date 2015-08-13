using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem.Editor
{
	public partial class ISObjectEditor 
	{
		
		void BottomBar()
		{
			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));
			GUILayout.Label ("Status Bar");
			GUILayout.EndHorizontal ();
		}
		
		
	}
	
}
