using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheGame.ItemSystem;
using TheGame.StatSystem;

namespace TheGame.ItemSystem
{
	[System.Serializable]
	public class ISItem :  IISItem
	{
		[SerializeField] string _identity ;
		[SerializeField] string _name ;
        [SerializeField] ItemType _type ;
		[SerializeField] EquipmentSlot _equipmentSlot;

		[SerializeField] List< Stat> _stats;
		[SerializeField] List<GameObject> _skins;
        [SerializeField] GameObject _selectedSkin;

        public ISItem()
        {

        }
        public ISItem(ISItem clone)
        {
            _identity = clone.Identity;
            _name = clone.Name;
            _type = clone.Type;
            _equipmentSlot = clone.EquipmentSlot;
            _stats = clone.Stats;
            _skins = clone.Skins;
            _selectedSkin = clone.SelectedSkin;
        }
	
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




		public EquipmentSlot EquipmentSlot{
			get {
				return _equipmentSlot;
			}
			set {
				_equipmentSlot = value;
			}
		}


        public ItemType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        public List<Stat> Stats{
			get {
				return _stats;
			}
			set {
				_stats = value;
			}
		}

		public List<GameObject> Skins{
			get {
				return _skins;
			}
			set {
				_skins = value;
			}
		}

        public GameObject SelectedSkin
        {
            get
            {
                return _selectedSkin;
            }
            set
            {
                _selectedSkin = value;
            }
        }




        #endregion




    }
}


public enum ItemType
{
    Equipment,
    Crate

}
