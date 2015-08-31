using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class FocusManager : MonoBehaviour, IPointerDownHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void OnPointerDown(PointerEventData data)
    {
        GameObject.Find("ToolPanel").SetActive(false);
        gameObject.SetActive(false);
    }
}
