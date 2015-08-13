using UnityEngine;
using System.Collections;

public class HingeRotationSync : MonoBehaviour {
	public GameObject Camera;


	void Start() {

	}
	
	void Update() {

		
		transform.rotation = Camera.transform.rotation;

	}
}