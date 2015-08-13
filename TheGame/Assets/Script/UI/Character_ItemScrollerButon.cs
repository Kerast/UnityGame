using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character_ItemScrollerButon : MonoBehaviour {
	public GameObject itemPreview;
	// Use this for initialization
	void Start () {
		GetComponent<Button> ().onClick.AddListener (PreviewItem);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PreviewItem()
	{
		if (GameObject.Find ("itemPreview") != null) 
		{
			Destroy(GameObject.Find ("itemPreview").gameObject);
		}

		GameObject item = Instantiate (itemPreview);
		item.transform.position = GameObject.Find ("ItemPreview").transform.position;
		item.name = "itemPreview";
	}
}
