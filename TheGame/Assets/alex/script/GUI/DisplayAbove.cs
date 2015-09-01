using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DisplayAbove : MonoBehaviour {
	public Transform TargetTransform;
	public float Height;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//GameObject Floatingtext;

	//	Instantiate(
		//Floatingtext = GameObject.Find ("GameManager").GetComponent<Messages> ().FloatingMessage;
		//Floatingtext.GetComponent<Text>().text = "TestMessage";
		
		if (Camera.main != null&&TargetTransform!=null) {
			Vector3 hitPostViewPort = Camera.main.WorldToScreenPoint (TargetTransform.position+Vector3.up*Height);
			transform.position = hitPostViewPort;
		}
	}
}
