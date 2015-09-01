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
		[SerializeField] Sprite _icon ;
		[SerializeField] ISQuality _quality ;
		[SerializeField] EquipmentSlot _equipmentSlot;

		[SerializeField] List< Stat> _stats;
		[SerializeField] List<GameObject> _skins;
        [SerializeField] GameObject _selectedSkin;

		

	
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
