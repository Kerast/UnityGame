using UnityEngine;
using System.Collections;
using UnityEngine.Networking;



public class Player_SyncRotation : NetworkBehaviour {


	[SyncVar] private Quaternion SyncPlayerRotation;
	[SyncVar] private Quaternion SyncCamRotation;

	[SerializeField] private Transform playerTransform;
	[SerializeField] private Transform camTransform;
	[SerializeField] private float lerpRate = 15;

	private Quaternion lastPlayerRot;
	private Quaternion lastCamRot;
	private float threshold = 5;




	void Update(){
		LerpRotation();
	}

	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotation ();

	}


	void LerpRotation()
	{
		if (!isLocalPlayer) {
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, SyncPlayerRotation, Time.deltaTime * lerpRate);
			camTransform.rotation = Quaternion.Lerp (camTransform.rotation, SyncCamRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationToServer(Quaternion playerRot, Quaternion camRot)
	{
		SyncPlayerRotation = playerRot;
		SyncCamRotation = camRot;
		//Debug.Log ("Command angle");
	}

	[Client]
	void TransmitRotation()
	{
		if (isLocalPlayer)
		{
			if( Quaternion.Angle(playerTransform.rotation, lastPlayerRot) > threshold || Quaternion.Angle(camTransform.rotation, lastCamRot) > threshold)
			{
				CmdProvideRotationToServer(playerTransform.rotation, camTransform.rotation);			
				lastPlayerRot = playerTransform.rotation;
				lastCamRot = camTransform.rotation;
			}
		}
	}
}
