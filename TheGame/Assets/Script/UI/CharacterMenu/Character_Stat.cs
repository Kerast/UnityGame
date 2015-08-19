using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheGame.StatSystem;
using UnityEngine.UI;

public class Character_Stat : MonoBehaviour {

	public Player_Stat Stats;
	public List<GameObject> StatListElements;
	public GameObject StatListElement;
	public GameObject Content;

	// Use this for initialization
	void Start () {

		foreach(Stat stat in Stats.Stats)
		{
			StatListElements.Add(Instantiate(StatListElement));
			StatListElements[StatListElements.Count - 1 ].transform.GetChild(0).GetComponent<Text>().text = stat.Name;
			StatListElements[StatListElements.Count - 1 ].transform.GetChild(1).GetComponent<Text>().text = stat.ValueInt.ToString();
			StatListElements[StatListElements.Count - 1 ].transform.SetParent(Content.transform);
			StatListElements[StatListElements.Count - 1 ].transform.localScale = new Vector3(1,1,1);

		}
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < StatListElements.Count; i++)
		{
			string qwe = StatListElements[StatListElements.Count - 1 ].transform.GetChild(0).GetComponent<Text>().text;
			string er = StatListElements[StatListElements.Count - 1 ].transform.GetChild(1).GetComponent<Text>().text;

			StatListElements[i].transform.GetChild(0).GetComponent<Text>().text =Stats.Stats[i].Name;
			StatListElements[i].transform.GetChild(1).GetComponent<Text>().text =Stats.Stats[i].ValueInt.ToString();
		}
	}
}
