using UnityEngine;
using System.Collections;

public class RotateSphereSweg : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Find("Rotate").transform.Rotate(Vector3.up*75 * Time.deltaTime, Space.World);
		GameObject.Find("Rotate").transform.Rotate(Vector3.left*60 * Time.deltaTime, Space.World);

		GameObject.Find("BigPlanet").transform.Rotate(Vector3.right*60 * Time.deltaTime, Space.World);


	}
}
