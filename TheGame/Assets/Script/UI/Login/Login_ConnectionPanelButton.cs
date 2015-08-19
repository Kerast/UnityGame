using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login_ConnectionPanelButton : MonoBehaviour {

	
    
    // Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(RemoveConnectionPanel);
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void RemoveConnectionPanel()
    {
        GameObject.Find("ConnectionPanel").SetActive(false);
    }
}
