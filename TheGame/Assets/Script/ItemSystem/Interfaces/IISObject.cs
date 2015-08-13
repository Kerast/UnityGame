using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem
{
	public interface IISObject  {

		string Identity{ get; set;}
		string Name{ get; set;}
		int Value{ get; set;}
		Sprite Icon{ get; set;}
		ISQuality Quality { get; set;}


	}

}
