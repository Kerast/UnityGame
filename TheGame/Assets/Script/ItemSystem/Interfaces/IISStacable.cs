using UnityEngine;
using System.Collections;


namespace TheGame.ItemSystem
{
	public interface IISStacable 
	{
		int MaxStack { get; }
		int Stack (int amount);  //default value of 0


	}
}
