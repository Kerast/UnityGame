using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections.Generic;

[NetworkSettings(channel = 0, sendInterval=0.033f)]
public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar (hook = "SyncPositionValues")]
	private Vector3 syncPos;

	[SerializeField] Transform myTransform;
	private float lerpRate;
	private float normalLerpRate = 17;
	private float fasterLerpRate = 27;

	private Vector3 lastPos;
	private float threshhold = 0.5f;

	private NetworkClient nClient;
	private int latency;
	private Text latencyText;

	private List<Vector3> syncPosList = new List<Vector3>();
	[SerializeField] private bool useHistoricalLerping = false;
	private float closeEnough = 0.1f;

	void Start()
	{
		nClient = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().client;
		latencyText = GameObject.Find ("Latency Text").GetComponent<Text> ();
	}

	void Update(){
		LerpPosition ();
		ShowLatency ();
		lerpRate = normalLerpRate;
	}


	
	// Update is called once per frame
	void FixedUpdate () {

			TransmitPosition ();
			

	}

	void LerpPosition()
	{
		if (!isLocalPlayer) {

			if(useHistoricalLerping)
			{
				HistoricalLerping();
			}
			else
			{
				OrdinaryLerping();
			}

		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		//Debug.Log ("Command called");
		syncPos = pos;
	}


	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshhold) {
			CmdProvidePositionToServer (myTransform.position);
			lastPos = myTransform.position;
		}
	}
	
	[Client]
	void SyncPositionValues(Vector3 latestPos)
	{
		syncPos = latestPos;
		syncPosList.Add (syncPos);
	}


	void ShowLatency()
	{
		if (isLocalPlayer) {
			latency = nClient.GetRTT();
			latencyText.text = latency.ToString();
		}
	}

	void OrdinaryLerping()
	{
			myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime*lerpRate);
		
	}

	void HistoricalLerping()
	{
		if(syncPosList.Count > 0 )
		{
			myTransform.position = Vector3.Lerp (myTransform.position, syncPosList[0], Time.deltaTime*lerpRate);
			if(Vector3.Distance(myTransform.position, syncPosList[0]) < closeEnough)
			{
				syncPosList.RemoveAt(0);
			}

			if(syncPosList.Count > 10)
			{
				lerpRate = fasterLerpRate;
			}
			else
			{
				lerpRate = normalLerpRate;
			}

			Debug.Log (syncPosList.Count.ToString());
		}
	}
}
