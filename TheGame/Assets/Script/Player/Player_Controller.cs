using UnityEngine;
using System.Collections;


public class Player_Controller : MonoBehaviour {
	public float movementSpeed = 10;
	public float turningSpeed = 60;

	private Vector2 m_Input;

	void Update() {
		float horizontal = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
		transform.Translate(horizontal, 0, 0);
		
		float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
		transform.Translate(0, 0, vertical);

		if(Input.GetKeyDown(KeyCode.Space))
			GetComponent<Rigidbody>().AddForce(Vector3.up * 350);
	}







}




