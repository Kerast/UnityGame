using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class Inventory_ItemToolElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public Color NormalColor;
    public Color HoverColor;

    public Text ElementText;





    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData data)
    {
        gameObject.GetComponent<Image>().color = HoverColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
        gameObject.GetComponent<Image>().color = NormalColor;

    }

}
