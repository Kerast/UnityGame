using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

[NetworkSettings(channel = 0, sendInterval=0.01f)]
public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar (hook = "SyncPositionValues")]
	private Vector3 syncPos;
	[SyncVar]
	private Quaternion syncRotation;
	[SyncVar]
	private Quaternion syncCameraRotation;

	[SerializeField] Transform myTransform;
	[SerializeField] Transform myCamera;
	[SerializeField]private float lerpRate;
	private float normalLerpRate = 12;

	private Vector3 lastPos;
	private float threshhold = 0.5f;
	private Quaternion lastRotation;
	private Quaternion lastCameraRotation;
	private float RotationThreshhold = 0.1f;
	private Quaternion lastCamRot;

	private NetworkClient nClient;
	private int latency;
	private Text latencyText;


	void Start()
	{
		nClient = GameObject.Find("CustomNetworkLobbyManager").GetComponent<NetworkManager>().client;
		latencyText = GameObject.Find ("LatencyText").GetComponent<Text> ();
		lerpRate = normalLerpRate;
	}
	void Update(){

		if (!isLocalPlayer) {
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime*lerpRate);
			myTransform.rotation=Quaternion.Lerp(myTransform.rotation,syncRotation,Time.deltaTime*lerpRate);
			myCamera.rotation=Quaternion.Lerp(myCamera.rotation,syncCameraRotation,Time.deltaTime*lerpRate);
		}
		//ShowLatency ();

	}
	// Update is called once per frame
	void FixedUpdate () {
			TransmitPosition ();
			TransmitRotation ();
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;

	}
	[Command]
	void CmdProvideRotationToServer(Quaternion rot,Quaternion rotCamera)
	{
		syncRotation = rot;
		syncCameraRotation = rotCamera;
		
	}
	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshhold) {
			CmdProvidePositionToServer (myTransform.position);
			lastPos = myTransform.position;
		}
	}
	[ClientCallback]
	void TransmitRotation()
	{
		if (isLocalPlayer && Quaternion.Angle(myTransform.rotation, lastRotation) > RotationThreshhold) {
			CmdProvideRotationToServer (myTransform.rotation,myCamera.rotation);
			lastRotation = myTransform.rotation;
			lastCameraRotation = myCamera.rotation;
		}
	}
	[Client]
	void SyncPositionValues(Vector3 latestPos)
	{
		syncPos = latestPos;
	}
	[Client]
	void SyncRotationValues(Quaternion latestRot)
	{
		syncRotation = latestRot;
	}
	void ShowLatency()
	{
		if (isLocalPlayer) {
			latency = nClient.GetRTT();
			latencyText.text = latency.ToString();
		}
	}
}
