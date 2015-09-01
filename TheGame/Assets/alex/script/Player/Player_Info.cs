using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player_Info : MonoBehaviour {


	private List<Message> Messages;
	private List<Message> FloatingMessages;
	
	private enum MessageState{Idle,Set, OnCd, Canceled};
	private MessageState MsgState;
	public string PlayerName;
	public int TeamNumber;

	public class Message
	{
		public string msg;
		public float timeStamp;
		public string sender;
		public string receiver;
		public float lifeDuration;

		public Message(string s, float time,string sn,string rc)
		{
			msg=s;
			timeStamp=time;
			sender=sn;
			receiver=rc;
			if(receiver.Contains("system"))
			{
				lifeDuration=2.0f;
			}
			else{
				lifeDuration=1.5f;
			}

		}
	}


	// Use this for initialization
	void Start () {

		Messages = new List<Message> ();
	

	}

	
	// Update is called once per frame
	void Update () {

		for (int i=0; i<Messages.Count; i++) {
			if((Messages[i].timeStamp+Messages[i].lifeDuration)<Time.time)
			{
				Messages.RemoveAt(i);
			}
		}

		
	
	}

	public void SetMessage(string s,string sn,string rc)
	{

	
		Message msg = new Message (s, Time.time,sn,rc);
		Messages.Add (msg);


	}
	public List<string> GetMessagesString(string rc)
	{
		List<string> list = new List<string> ();
		for (int i=0; i<Messages.Count; i++) {
			if(Messages[i].receiver.Contains(rc)){

			list.Add (Messages[i].msg);
			}
		}
		return list;
	}




}
