using UnityEditor;
using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem.Editor
{
	public partial class ISQualityDatabaseEditor : EditorWindow 
	{

		ISQualityDatabase qualityDatabase;
		ISQuality selectedItem;
		Texture2D selectedTexture;
		int selectedIndex = -1;
		Vector2 _scrollPos;			//scroll position for the listview      


		const int SPRITE_BUTTON_SIZE = 46;
		const string DATABASE_NAME = @"tgQualityDatabase.asset";
		const string DATABASE_PATH = @"Database";
		const string DATABASE_FULL_PATH = @"Assets/" + DATABASE_PATH + "/"+ DATABASE_NAME;

		[MenuItem("TG/Database/Quality Editor %#w")]
		public static void Init()
		{
			ISQualityDatabaseEditor window = EditorWindow.GetWindow<ISQualityDatabaseEditor> ();
			window.minSize = new Vector2 (400, 300);
			window.titleContent.text= "Quality Database";
			window.Show ();
		}


		void OnEnable()
		{
//			if(qualityDatabase == null)
//				qualityDatabase = ISQualityDatabase.GetDatabase<ISQualityDatabase> (DATABASE_PATH, DATABASE_NAME);
		}

		void OnGUI()
		{
			if (qualityDatabase == null)
			{
				Debug.LogWarning("quality database not loaded");
				return;
			}

			ListView ();

			GUILayout.BeginHorizontal ("Box", GUILayout.ExpandWidth (true));
			BottomBar ();
			GUILayout.EndHorizontal ();

		}

		void BottomBar()
		{
			//count
			GUILayout.Label ("Qualities : " + qualityDatabase.Count) ;

			//addbutton
			if (GUILayout.Button ("Add")) 
			{
				qualityDatabase.Add(new ISQuality());
			}
		}




	}
}
