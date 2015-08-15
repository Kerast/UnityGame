using UnityEngine;
using System.Collections;
using TheGame.ItemSystem;

namespace TheGame.ItemSystem
{
	[System.Serializable]
	public class ISItem :  IISItem
	{
		[SerializeField] string _identity ;
		[SerializeField] string _name ;
		[SerializeField] Sprite _icon ;
		[SerializeField] ISQuality _quality ;
		[SerializeField] EquipmentSlot _equipmentSlot;
		
		
		[SerializeField] int _damage ;
		[SerializeField] int _defense ;

		#region IISItem implementation

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

		public Sprite Icon {
			get {
				return _icon;
			}
			set {
				_icon = value;
			}
		}

		public ISQuality Quality {
			get {
				return _quality;
			}
			set {
				_quality = value;
			}
		}

		public EquipmentSlot EquipmentSlot{
			get {
				return _equipmentSlot;
			}
			set {
				_equipmentSlot = value;
			}
		}

		public int Damage {
			get {
				return _damage;
			}
			set {
				_damage = value;
			}
		}

		public int Defense {
			get {
				return _defense;
			}
			set {
				_defense = value;
			}
		}

		#endregion




	}
}
