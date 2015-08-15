using UnityEngine;
using System.Collections;

namespace TheGame.StatSystem
{
	public interface IStat  {
		
		string Identity { get; set;}
		string Name { get; set;}
		int ValueInt { get; set;}
		float ValueFloat { get; set;}
		int Modifier  { get; set;}
	}
}

