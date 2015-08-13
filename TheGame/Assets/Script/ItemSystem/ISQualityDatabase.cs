using UnityEngine;
using UnityEditor;
using System.Linq;  // besoin de ElementAt pour les listes;
using System.Collections;
using System.Collections.Generic;


namespace TheGame.ItemSystem
{


	public class ISQualityDatabase : ScriptableObjectDatabase<ISQuality> {

		public int GetIndex(string name)
		{
			return database.FindIndex ( a => a.Name == name);
		}
	}

}