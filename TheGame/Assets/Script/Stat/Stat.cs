using UnityEngine;
using System.Collections;

namespace TheGame.StatSystem
{
	[System.Serializable]
	public class Stat : IStat {

		[SerializeField] string _identity;
		[SerializeField] string _name;
		[SerializeField] int _valueInt;
		[SerializeField] float _valueFloat;
		[SerializeField] int _modifier;


		public Stat(string id, string name, int vali, float valf, int mod)
		{
			_identity = id;
			_name = name;
			_valueInt = vali;
			_valueFloat = valf;
			_modifier = mod;
		}


		#region IStat implementation

		public string Identity {
			get {
				return _identity;
			}
			set {
				_identity = value;
			}
		}

		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}

		public int ValueInt {
			get {
				return _valueInt;
			}
			set {
				_valueInt = value;
			}
		}

		public float ValueFloat {
			get {
				return _valueFloat;
			}
			set {
				_valueFloat = value;
			}
		}

		public int Modifier {
			get {
				return _modifier;
			}
			set {
				_modifier = value;
			}
		}

		#endregion






	}
}
