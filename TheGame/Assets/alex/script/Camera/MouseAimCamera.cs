using UnityEngine;
using System.Collections;

public class MouseAimCamera : MonoBehaviour {
	public GameObject target;
	public GameObject virtualTarget;
	public float rotateSpeed = 5;
	Vector3 offset;
	
	void Start() {
		offset = target.transform.position - transform.position;
	
	}
	
	void LateUpdate() {
	

			//if (GetComponent<Player_Stats> ().getisStunned () == 0) {
				float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
				float vertical = Input.GetAxis ("Mouse Y") * rotateSpeed * (-1);
				target.transform.Rotate (0, horizontal, 0);
				virtualTarget.transform.Rotate (vertical, 0, 0);

				float desiredAngley = virtualTarget.transform.eulerAngles.y;
				float desiredAnglex = virtualTarget.transform.eulerAngles.x;
				Quaternion rotation = Quaternion.Euler (desiredAnglex, desiredAngley, 0);
				transform.position = virtualTarget.transform.position - (rotation * offset);


		
				transform.LookAt (virtualTarget.transform);
			
		//}

	}
}