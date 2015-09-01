using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
[NetworkSettings(channel = 0, sendInterval=0.01f)]
public class Player_Controller : NetworkBehaviour {
	public float movementSpeed = 10;
	public float turningSpeed = 60;
	public bool isMoving;
	private Vector3 previousPosition;

	private Vector2 m_Input;

	[Header("Options")]
	public float smoothSpeed = 10f;
	
	[SyncVar]
	private Vector3 mostRecentPos;
	private Vector3 prevPos;

	void Start()
	{
		isMoving = false;

	}
	/*
	void FixedUpdate()
	{
		//float horizontal = Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;
		if (Input.GetAxisRaw("Horizontal")!=0||Input.GetAxisRaw("Vertical")!=0) {
			CmdSendMovementRequest (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		}
	}*/
	

	void FixedUpdate() {
		if (GetComponent<Player_Stats> () != null) {
			movementSpeed = GetComponent<Player_Stats> ().getMovementSpeed ();
		}
		if (GetComponent<Player_Stats> ().getisRooted () == 0&&GetComponent<Player_Stats> ().getisStunned () == 0) {
			float horizontal = Input.GetAxisRaw ("Horizontal") * movementSpeed * Time.deltaTime;
			transform.Translate (horizontal, 0, 0);
		
			float vertical = Input.GetAxisRaw ("Vertical") * movementSpeed * Time.deltaTime;
			transform.Translate (0, 0, vertical);

			if (Input.GetKeyDown (KeyCode.Space))
				GetComponent<Rigidbody> ().AddForce (Vector3.up * 350);
			MovementDetection ();
		}
	}

	public void MovementDetection(){
		if (gameObject.transform.position != previousPosition) {
			isMoving=true;	
		}
	
		
	 else {
		isMoving=false;
		
	}
		previousPosition = gameObject.transform.position;

}

}



