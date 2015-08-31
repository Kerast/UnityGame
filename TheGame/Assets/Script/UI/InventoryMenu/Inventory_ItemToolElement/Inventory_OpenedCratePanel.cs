using UnityEngine;
using UnityEngine.UI;
using TheGame.ItemSystem;
using System.Collections;
using System.Collections.Generic;

public class Inventory_OpenedCratePanel : MonoBehaviour {

    public Text ItemName;
    public Text SkinName;
    public Image SkinPhoto;
    public Image BackGroundQuality;

    public Button OkButton;


    // Use this for initialization
    void Start () {
        OkButton.onClick.AddListener(AcceptSkin);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DisplayInfo(GameObject skin)
    {
        List<ISItem> allItems = GameObject.Find("GameManager").GetComponent<GameManager_Assets>().Items;
        foreach(var item in allItems)
        {
            if(item.Skins.Count > 0)
            {
                if (item.Skins.Find(i => i.GetComponent<SkinInfo>().Identity == skin.GetComponent<SkinInfo>().Identity) != null)
                {
                    ItemName.text = item.Identity;
                    break;
                }
            }
            
        }

        SkinInfo skinInfo = skin.GetComponent<SkinInfo>();
        SkinName.text = skinInfo.Name;
        SkinPhoto.sprite = skinInfo.SkinPhoto;

        if (skinInfo.SkinQuality == SkinQuality.Common)
            BackGroundQuality.color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityCommonColor;
        else if (skinInfo.SkinQuality == SkinQuality.Uncommon)
            BackGroundQuality.color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityUncommonColor;
        else if (skinInfo.SkinQuality == SkinQuality.Rare)
            BackGroundQuality.color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityRareColor;
        else if (skinInfo.SkinQuality == SkinQuality.Legendary)
            BackGroundQuality.color = GameObject.Find("GameManager").GetComponent<GameManager_Colors>().QualityLegendaryColor;

    }


    public void AcceptSkin()
    {
        gameObject.SetActive(false);
    }



}
