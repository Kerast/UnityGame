using UnityEngine;
using System.Collections;

public class Inventory_ToolPanel : MonoBehaviour {
    //prefabs
    public GameObject OpenCrateToolPanelElement;

    public GameObject LostFocusPanel;


    //item to pass
    public InventoryItem ItemObject;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CleanToolPanel()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
