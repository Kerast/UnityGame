using UnityEngine;
using System.Collections;
using TheGame.ItemSystem;

public class SkinInfo : MonoBehaviour {

    public string Identity;
    public string Name;
    public SkinQuality SkinQuality;
    public Sprite SkinIcon;
    public Sprite SkinPhoto;
    public float DropRate = 1;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


public enum SkinQuality
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}
