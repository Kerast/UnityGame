using UnityEngine;
using UnityEditor;
using System.Collections;


namespace TheGame.ItemSystem
{
	
	[System.Serializable]
	public class ISArmor : ISObject, IISArmor 
	{
		[SerializeField] int _defense;
		[SerializeField] EquipmentSlot _equipmentSlot;
		
				
		public ISArmor()
		{
			
		}
		
		public ISArmor(ISArmor armor)
		{
			Clone (armor);
		}
		
		public void Clone(ISArmor armor)
		{
			base.Clone (armor);
			
			_defense = armor.Defense;	
			_equipmentSlot = armor.EquipmentSlot;
		}



		
	

		#region IISArmor implementation
		public int Defense {
			get {
				return _defense;
			}
			set {
				_defense = value;
			}
		}
		public EquipmentSlot EquipmentSlot {
			get {
				return _equipmentSlot;
			}
			set {
				_equipmentSlot = value;
			}
		}
		#endregion
	}
}
