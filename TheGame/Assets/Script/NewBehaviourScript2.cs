using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using System.Collections;

public class NewBehaviourScript2 : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Analytics.Transaction("12345abcde", 0.99m, "USD", null, null);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
