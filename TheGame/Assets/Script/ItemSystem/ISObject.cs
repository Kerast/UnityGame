using UnityEngine;
using UnityEditor;
using System.Collections;


namespace TheGame.ItemSystem
{
	[System.Serializable]
	public class ISObject : IISObject  {
		[SerializeField]string _identity;
		[SerializeField]string _name;
		[SerializeField]Sprite _icon;
		[SerializeField]int _value;
		[SerializeField]ISQuality _quality;
		
		
		public ISObject(ISObject item)
		{
			Clone (item);
		}
		public ISObject()
		{

		}


		public void Clone(ISObject item)
		{
			_identity = item.Identity;
			_name = item.Name;
			_icon = item.Icon;
			_value = item.Value;
			_quality = item.Quality;

		}

		
		public string Identity
		{
			get
			{
				return _identity;
			}
			set
			{
				_identity = value;
			}
		}



		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		
		public Sprite Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				_icon = value;
			}
		}

		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		public ISQuality Quality
		{
			get
			{
				return _quality;
			}
			set
			{
				_quality = value;
			}
		}


		//this code is going to be paced in a new cclass

		/*ISQualityDatabase qdb;
		int qualitySelectedIndex = 0;
		string[] option;

		public virtual void  OnGUI()
		{

			GUILayout.BeginVertical ();

			_name = EditorGUILayout.TextField ("Name", _name);
			_value = EditorGUILayout.IntField("Value", _value);
			DisplayIcon ();
			DisplayQuality ();
			GUILayout.EndVertical ();


		}

		public void DisplayIcon()
		{

			_icon = EditorGUILayout.ObjectField ("Icon", _icon, typeof(Sprite), false) as Sprite;
		}

		public int SelectedQualityID
		{
			get 
			{ 
				return qualitySelectedIndex;
			}
		}

		public ISObject()
		{
			string DATABASE_NAME = @"tgQualityDatabase.asset";
			string DATABASE_PATH = @"Database";
			qdb = ISQualityDatabase.GetDatabase<ISQualityDatabase> (DATABASE_PATH, DATABASE_NAME);
			option = new string[qdb.Count];
			for(int i = 0; i < qdb.Count; i++)
			{
				option[i] = qdb.Get(i).Name;
			}
		}


		public void DisplayQuality()
		{
			int itemIndex = 0;
			if(_quality != null)
				itemIndex= qdb.GetIndex (_quality.Name);

			if (itemIndex == -1) 
			{
				itemIndex = 0;
			}

			qualitySelectedIndex = EditorGUILayout.Popup ("Quality", itemIndex, option );
			_quality = qdb.Get(SelectedQualityID);
		}*/




		
	}

}