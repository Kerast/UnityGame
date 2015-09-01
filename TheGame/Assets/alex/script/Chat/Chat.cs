using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Chat : MonoBehaviour {

	public class ChatMsg{
		public string UserName;
		public Time timeStamp;
		public string msg;
		public Color color;

	}

	private List<ChatMsg> listMsg;

	// Use this for initialization
	void Start () {
		listMsg=new List<ChatMsg>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
