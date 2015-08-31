using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Crate_Content : MonoBehaviour {

    public List<GameObject> Skins;

    // Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
	
	}



    public GameObject WinnedSkin()
    {

        int sumRate = 0;
        int sumRateCumu = 0;


        foreach (var skin in Skins)
        {
            sumRate += (int)skin.GetComponent<SkinInfo>().DropRate;
        }

        
        int randomNumber = (int) Random.Range(0, sumRate);
        Debug.Log("Random de 0 - "+ sumRate + " = " + randomNumber);

        foreach(var skin in Skins)
        {
            sumRateCumu += (int)skin.GetComponent<SkinInfo>().DropRate;
            if(sumRateCumu>= randomNumber)
            {
                return skin;
                
            }
        }

        return null;
    }

}
