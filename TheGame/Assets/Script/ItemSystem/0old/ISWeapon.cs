using UnityEngine;
//using UnityEditor;
using System.Collections;


namespace TheGame.ItemSystem
{

	[System.Serializable]
	public class ISWeapon : ISObject, IISWeapon
	{
		[SerializeField] int _minDamage;



		public ISWeapon()
		{

		}

		public ISWeapon(ISWeapon weapon)
		{
			Clone (weapon);
		}

		public void Clone(ISWeapon weapon)
		{
			base.Clone (weapon);

			_minDamage = weapon.MinDamage;	

		}


		#region IISWeapon implementation

		public float Attack ()
		{
			throw new System.NotImplementedException ();
		}

		public int MinDamage {
			get {
				return _minDamage;
			}
			set {
				_minDamage = value;
			}
		}

		#endregion


	

		/*//this script onathoer script

		public override void OnGUI()
		{
			base.OnGUI ();
			_minDamage = EditorGUILayout.IntField("Damage", _minDamage);
			_durability = EditorGUILayout.IntField("Durability", _durability);
			_maxDurability = EditorGUILayout.IntField("Max Durability", _maxDurability);
			DisplayEquipmentSlot ();
			DisplayPrefab ();




		}


		public void DisplayEquipmentSlot()
		{
			equipmentSlot = (EquipmentSlot)EditorGUILayout.EnumPopup ("Equipment Slot", equipmentSlot);
		}

		public void DisplayPrefab()
		{
			  _prefab = EditorGUILayout.ObjectField ("Prefab", _prefab, typeof(GameObject), false) as GameObject;

		}*/
	}
}
