using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SyncPlayerInfo : MonoBehaviour {

    public Text xp;
    public Text pp;
    public Text pg;

    public WebServices webservice;

    // Use this for initialization
    void Start () {
        webservice = GameObject.Find("WebServices").GetComponent<WebServices>();
    }
	
	// Update is called once per frame
	void Update () {
        
          xp.text = webservice.xp.ToString();
          pp.text = webservice.pp.ToString();
          pg.text = webservice.pg.ToString();
    }
}
